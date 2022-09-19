namespace WackyArchServer.Model
{
    /// <summary>
    /// Object sent to the client to display the test environment for that challenge
    /// </summary>
    public class AlphaChallenge
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Flag { get; set; }
        public AlphaChallenge Predecessor { get; set; }
        public string Description { get; set; }

        // These will be JSON Strings, to be decoded.
        // Schema: An array of objects, {"name": "xyz", "data": [1,2,3,4,5]}
        public string InputTextJson { get; set; }
        public string OutputTextJson { get; set; }
    }
}
