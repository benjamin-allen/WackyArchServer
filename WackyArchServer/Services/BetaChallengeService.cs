using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WackyArch.Components;
using WackyArch.CPUs;
using WackyArch.Utilities;
using WackyArchServer.Model;
using WackyArchServer.Utilities;

namespace WackyArchServer.Services
{
    public class BetaChallengeService
    {
        public int AllowedCycles = 1_000_000;
        private readonly IDbContextFactory<WAContext> contextFactory;
        private readonly AuthenticationStateProvider authProvider;

        public BetaChallengeService(IDbContextFactory<WAContext> contextFactory, AuthenticationStateProvider authProvider)
        {
            this.contextFactory = contextFactory;
            this.authProvider = authProvider;
        }

        public async Task<BetaChallenge> GetBetaChallengeAsync(Guid id)
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                return await context.BetaChallenges.Where(c => c.Id.ToString() == id.ToString()).SingleOrDefaultAsync();
            }
        }

        public async Task<List<BetaChallenge>> GetBetaChallengesCompletedByUserAsync()
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var userId = (await authProvider.GetAuthenticationStateAsync()).User.GetUserId();

                return await context.CompletedBetaChallenges.Where(x => x.AccountId == Guid.Parse(userId)).Select(x => x.BetaChallenge).ToListAsync();
            }
        }

        public async Task<List<BetaChallenge>> GetUncompletedAvailableBetaChallengesForUserAsync()
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var completedchallengeIds = (await GetBetaChallengesCompletedByUserAsync()).Select(x => x.Id).ToHashSet();
                var availableChallenges = await context.BetaChallenges.Where(x => (x.Predecessor.Id == null) || completedchallengeIds.Contains(x.Predecessor.Id)).ToListAsync();

                return availableChallenges.Where(x => completedchallengeIds.Contains(x.Id) == false).ToList();
            }
        }

        public async Task CompleteBetaChallenge(Guid betaChallengeId)
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var userId = (await authProvider.GetAuthenticationStateAsync()).User.GetUserId();
                var betaChallenge = await context.BetaChallenges.SingleAsync(x => x.Id == betaChallengeId);
                var existingCompletionEntry = await context.CompletedBetaChallenges.SingleOrDefaultAsync(x => x.AccountId == Guid.Parse(userId) && x.BetaChallenge.Id == betaChallengeId);

                if (existingCompletionEntry == null)
                {
                    await context.CompletedBetaChallenges.AddAsync(new CompletedBetaChallenge { AccountId = Guid.Parse(userId), BetaChallenge = betaChallenge });
                    await context.SaveChangesAsync();
                }
                return;
            }
        }

        public List<Word> GetInputProgramBinary(string programJson)
        {
            return JsonConvert.DeserializeObject<List<int>>(programJson).Select(x => new Word { Value = x }).ToList();
        }

        public async Task<string> RunBetaChallenge(Guid challengeId, List<Word> inputWords)
        {
            using (var context = await contextFactory.CreateDbContextAsync())
            {
                var userId = (await authProvider.GetAuthenticationStateAsync()).User.GetUserId();
                var runLog = new RunLog { ChallengeId = challengeId, Code = inputWords.Select(x => x.Value).ToString(), SubmitterAccountId = Guid.Parse(userId), SubmittedTime = DateTime.Now };

                var challenge = await GetBetaChallengeAsync(challengeId);
                if (challenge == null)
                {
                    runLog.CompletedTime = DateTime.Now;
                    runLog.Result = $"Beta Challenge {challengeId} not found";
                    context.RunLogs.Add(runLog);
                    await context.SaveChangesAsync();
                    return "Challenge not found";
                }

                // Run the beta challenge executable with the provided input
                var memory = new Memory(256);
                var dataPort = new Port(memory.Data, "DATA");
                var addrPort = new Port(memory.Address, "ADDR");
                var userInPort = new FilledPort(inputWords, new Pipe(), "KP");
                var cpu = new StackCPU(new Port[] { dataPort, addrPort, userInPort });
                var cyclables = new List<ICyclable> { cpu, memory, userInPort };

                var programBinary = GetInputProgramBinary(challenge.InputProgramJson);
                cpu.Load(programBinary);

                string outputMessage = "";

                for(int i = 0; i < AllowedCycles; i++)
                {
                    try
                    {
                        cyclables.ForEach(c => c.Cycle());
                    }
                    catch (ComponentException cex)
                    {
                        outputMessage = $"A test failed: {cex.Message}";
                        break;
                    }
                    catch (Interrupt it)
                    {
                        switch (it.InterruptType)
                        {
                            case InterruptType.UNLOCK:
                                outputMessage = challenge.Flag; break;
                            case InterruptType.HALT:
                                outputMessage = "INT HALT"; break;
                            case InterruptType.END:
                                outputMessage = "INT END"; break;
                        }
                        break;
                    }
                }

                runLog.Result = outputMessage;
                runLog.CompletedTime = DateTime.Now;
                context.RunLogs.Add(runLog);
                await context.SaveChangesAsync();
                return outputMessage;
            }
        }
    }
}
