using Microsoft.EntityFrameworkCore;
using WackyArchServer.Model;
using Newtonsoft.Json;
using WackyArch.Components;
using WackyArch.CPUs;
using WackyArch.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WackyArchServer.Utilities;

namespace WackyArchServer.Services
{
    public class AlphaChallengeService
    {
        public int AllowedCycles = 100_000;
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

        public async Task<List<Guid>> GetChallengesCompletedByUserAsync()
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var userId = (await authProvider.GetAuthenticationStateAsync()).User.GetUserId();

                return await context.CompletedChallenges.Where(x => x.AccountId == Guid.Parse(userId)).Select(x => x.ChallengeId).ToListAsync();
            }
        }

        public async Task<List<AlphaChallenge>> GetAlphaChallengesCompletedByUserAsync()
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var completedChallengeIds = (await GetChallengesCompletedByUserAsync()).ToHashSet();
                return await context.AlphaChallenges.Where(x => completedChallengeIds.Contains(x.Id)).ToListAsync();
            }
        }

        public async Task<List<AlphaChallenge>> GetUncompletedAvailableChallengesForUserAsync()
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var completedChallengeIds = (await GetChallengesCompletedByUserAsync()).ToHashSet();
                var availableChallenges = await context.AlphaChallenges.Where(x => (x.PredecessorId == null) || completedChallengeIds.Contains(x.PredecessorId.Value)).ToListAsync();

                return availableChallenges.Where(x => completedChallengeIds.Contains(x.Id) == false).ToList();
            }
        }

        public async Task CompleteChallenge(Guid alphaChallengeId)
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var userId = (await authProvider.GetAuthenticationStateAsync()).User.GetUserId();
                var alphaChallenge = await context.AlphaChallenges.SingleAsync(x => x.Id == alphaChallengeId);
                var existingCompletionEntry = await context.CompletedChallenges.SingleOrDefaultAsync(x => x.AccountId == Guid.Parse(userId) && x.ChallengeId == alphaChallengeId);

                if (existingCompletionEntry == null)
                {
                    await context.CompletedChallenges.AddAsync(new CompletedChallenge { AccountId = Guid.Parse(userId), ChallengeId = alphaChallenge.Id });
                    await context.SaveChangesAsync();
                }
                return;
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
                var userId = (await authProvider.GetAuthenticationStateAsync()).User.GetUserId();

                var runLog = new RunLog { ChallengeId = challengeId, Code = programText, SubmitterAccountId = Guid.Parse(userId), SubmittedTime = DateTime.Now };

                var challenge = await GetAlphaChallengeAsync(challengeId);
                if (challenge == null)
                {
                    runLog.CompletedTime = DateTime.Now;
                    runLog.Result = $"Alpha Challenge {challengeId} not found";
                    context.RunLogs.Add(runLog);
                    await context.SaveChangesAsync();
                    return "Challenge not found.";
                }

                var tests = context.AlphaChallengeTests.Where(t => t.AlphaChallenge.Id.ToString() == challengeId.ToString()).ToList();

                foreach (var test in tests.OrderBy(t => t.InputTextJson.Length))
                {
                    var inputPorts = GetInputPorts(test.InputTextJson);
                    var outputPorts = GetOutputPorts(test.OutputTextJson);
                    var ports = new List<Port>(inputPorts);
                    ports.AddRange(outputPorts);

                    var cpu = new InterpreterCPU(ports.ToArray());

                    var cyclables = new List<ICyclable> { cpu };
                    cyclables.AddRange(ports);

                    cpu.Load(programText);

                    int i = 0;
                    for (; i < AllowedCycles; i++)
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
                        catch (Interrupt it)
                        {
                            switch (it.InterruptType)
                            {
                                case InterruptType.UNLOCK:
                                    runLog.Result = challenge.Flag; break;
                                case InterruptType.HALT:
                                    runLog.Result = "INT HALT"; break;
                                case InterruptType.END:
                                    runLog.Result = "INT END"; break;
                            }
                            runLog.CompletedTime = DateTime.Now;
                            context.RunLogs.Add(runLog);
                            await context.SaveChangesAsync();
                            return $"Program interrupted unexpectedly: {runLog.Result}";
                        }
                        if (cpu.IsInterrupted || cpu.IsHalted || outputPorts.All(x => x.ExpectedData.Count == 0))
                        {
                            break;
                        }
                    }


                    foreach (var outputPort in outputPorts)
                    {
                        if (outputPort.ExpectedData.Count != 0 && i != AllowedCycles)
                        {
                            runLog.Result = $"Test failure: Not all expected output was written to {outputPort.Name}. Test ID {test.Id}.";
                            runLog.CompletedTime = DateTime.Now;
                            context.RunLogs.Add(runLog);
                            await context.SaveChangesAsync();
                            return $"Test failure: Not all expected output was written to {outputPort.Name}. Test ID {test.Id}.";
                        }
                        else if (outputPort.ExpectedData.Count != 0 && i == AllowedCycles)
                        {
                            runLog.Result = $"Test failure: The program failed to finish in {AllowedCycles} cycles. Test ID {test.Id}.";
                            runLog.CompletedTime = DateTime.Now;
                            context.RunLogs.Add(runLog);
                            await context.SaveChangesAsync();
                            return $"Test failure: The program failed to finish in {AllowedCycles} cycles. Test ID {test.Id}.";

                        }
                    }
                }

                runLog.Result = "Success";
                runLog.CompletedTime = DateTime.Now;
                context.RunLogs.Add(runLog);
                await context.SaveChangesAsync();

                await CompleteChallenge(challengeId);
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
