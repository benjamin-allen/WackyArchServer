namespace WackyArchServer.Model
{
    public class RunLog
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public Guid ChallengeID { get; set; }
        public DateTime SubmittedTime { get; set; }
        public DateTime CompletedTime { get; set; }
        public Account SubmitterAccount { get; set; }
        public string Code { get; set; }
        public string Result { get; set; }
    }
}
