namespace WackyArchServer.Model
{
    public class CompletedChallenge
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid ChallengeId { get; set; }
    }
}
