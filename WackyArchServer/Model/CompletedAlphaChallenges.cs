namespace WackyArchServer.Model
{
    public class CompletedAlphaChallenge
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public AlphaChallenge AlphaChallenge { get; set; }
    }
}
