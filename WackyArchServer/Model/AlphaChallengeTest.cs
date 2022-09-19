namespace WackyArchServer.Model
{
    public class AlphaChallengeTest
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public AlphaChallenge AlphaChallenge { get; set; }

        // These will be JSON Strings, to be decoded.
        // Schema: An array of objects, {"name": "xyz", "data": [1,2,3,4,5]}
        public string InputTextJson { get; set; }
        public string OutputTextJson { get; set; }
    }
}
