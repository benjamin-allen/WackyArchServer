﻿using Microsoft.EntityFrameworkCore;
using WackyArchServer.Model;
using Newtonsoft.Json;
using WackyArch.Components;
using WackyArch.CPUs;
using WackyArch.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace WackyArchServer.Services
{
    public class AlphaChallengeService
    {
        public int AllowedCycles = 1_000_000;
        private readonly IDbContextFactory<WAContext> contextFactory;
        private readonly AuthenticationStateProvider authProvider;

        public AlphaChallengeService(IDbContextFactory<WAContext> contextFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            this.contextFactory = contextFactory;
            this.authProvider = authenticationStateProvider;
        }

        public async Task<AlphaChallenge> GetAlphaChallengeAsync(Guid id)
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                return await context.AlphaChallenges.Where(c => c.Id.ToString() == id.ToString()).SingleAsync();
            }
        }

        public List<FilledPort> GetInputPorts(string inputTextJson)
        {
            var inputPorts = JsonConvert.DeserializeObject<List<AlphaChallengePort>>(inputTextJson);
            return inputPorts.Select(ip => new FilledPort(ip.Data.Select(x => new Word { Value = x }).ToList(), new Pipe(), ip.Name)).ToList();
        }

        public List<ExpectationPort> GetOutputPorts(string outputTextJson)
        {
            var outputPorts = JsonConvert.DeserializeObject<List<AlphaChallengePort>>(outputTextJson);
            return outputPorts.Select(op => new ExpectationPort(op.Data.Select(x => new Word { Value = x }).ToList(), op.Name)).ToList();
        }

        public async Task<string> RunAlphaChallengeTests(Guid challengeId, string programText)
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var user = (await authProvider.GetAuthenticationStateAsync()).User;
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var runLog = new RunLog { ChallengeId = challengeId, Code = programText, SubmitterAccountId = Guid.Parse(userId), SubmittedTime = DateTime.Now };

                var challenge = await GetAlphaChallengeAsync(challengeId);
                if (challenge == null)
                {
                    runLog.CompletedTime = DateTime.Now;
                    runLog.Result = $"Challenge {challengeId} not found";
                    context.RunLogs.Add(runLog);
                    await context.SaveChangesAsync();
                    return "Challenge not found.";
                }

                var tests = context.AlphaChallengeTests.Where(t => t.AlphaChallenge.Id.ToString() == challengeId.ToString()).ToList();

                foreach (var test in tests)
                {
                    var inputPorts = GetInputPorts(test.InputTextJson);
                    var outputPorts = GetOutputPorts(test.OutputTextJson);
                    var ports = new List<Port>(inputPorts);
                    ports.AddRange(outputPorts);

                    var cpu = new InterpreterCPU(ports.ToArray());

                    var cyclables = new List<ICyclable> { cpu };
                    cyclables.AddRange(ports);

                    cpu.Load(programText);

                    for (int i = 0; i < AllowedCycles; i++)
                    {
                        try
                        {
                            cyclables.ForEach(c => c.Cycle());
                        }
                        catch (ComponentException cex)
                        {
                            runLog.Result = $"Test failure: {cex.Message} | Test ID: {test.Id}";
                            runLog.CompletedTime = DateTime.Now;
                            context.RunLogs.Add(runLog);
                            await context.SaveChangesAsync();
                            return $"A test failed. {cex.Message}";
                        }
                        if (cpu.IsErrored || cpu.IsHalted)
                        {
                            break;
                        }
                    }


                    foreach (var outputPort in outputPorts)
                    {
                        if (outputPort.ExpectedData.Count != 0)
                        {
                            runLog.Result = $"Test failure: Not all expected output was written to {outputPort.Name} or the program failed to finish in {AllowedCycles} cycles. Test ID {test.Id}.";
                            runLog.CompletedTime = DateTime.Now;
                            context.RunLogs.Add(runLog);
                            await context.SaveChangesAsync();
                            return $"Test failure: Not all expected output was written to {outputPort.Name} or the program failed to finish in {AllowedCycles} cycles. Test ID {test.Id}.";
                        }
                    }
                }

                runLog.Result = "Success";
                runLog.CompletedTime = DateTime.Now;
                context.RunLogs.Add(runLog);
                await context.SaveChangesAsync();
                return challenge.Flag;
            }
        }
    }

    public class AlphaChallengePort
    {
        public string Name;
        public List<int> Data;
    }
}
