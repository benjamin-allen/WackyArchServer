using Microsoft.EntityFrameworkCore;
using WackyArchServer.Model;
using Newtonsoft.Json;
using WackyArch.Components;
using WackyArch.CPUs;
using WackyArch.Utilities;

namespace WackyArchServer.Services
{
    public class AlphaChallengeService
    {
        public int AllowedCycles = 1_000_000;
        private readonly IDbContextFactory<WAContext> contextFactory;

        public AlphaChallengeService(IDbContextFactory<WAContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public AlphaChallenge GetAlphaChallenge(Guid id)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                // FOR SOME REASON context.AlphaChallenges.Where(c => c.Id == id).ToList() returns nothing???
                // even though context.AlphaChallenges.ToList().Where(c => c.Id == id).ToList() returns data???
                // Probably a fuckin Sqlite thing.
                return context.AlphaChallenges.Where(c => c.Id.ToString() == id.ToString()).Single();
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

        public async Task<string> RunAlphaChallengeTests(Guid challengeId, string programText, Account account)
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var runLog = new RunLog { ChallengeId = challengeId, Code = programText, SubmitterAccount = account, SubmittedTime = DateTime.Now };

                var challenge = GetAlphaChallenge(challengeId);
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

                    var cyclables = new List<ICyclable>(inputPorts);
                    cyclables.AddRange(outputPorts);
                    cyclables.Add(cpu);

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
                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return "";
                }
                return challenge.Flag; // Select the flag out of the database, not the fake one.
            }
        }
    }

    public class AlphaChallengePort
    {
        public string Name;
        public List<int> Data;
    }
}
