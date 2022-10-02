namespace WackyArchServer.Model
{
    public class CompletedBetaChallenge
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public BetaChallenge BetaChallenge { get; set; }
    }
}
