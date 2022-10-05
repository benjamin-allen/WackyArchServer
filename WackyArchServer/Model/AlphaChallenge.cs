using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WackyArchServer.Model
{
    /// <summary>
    /// Object sent to the client to display the test environment for that challenge
    /// </summary>
    public class AlphaChallenge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Flag { get; set; }
        public Guid? PredecessorId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

        // These will be JSON Strings, to be decoded.
        // Schema: An array of objects, {"name": "xyz", "data": [1,2,3,4,5]}
        public string InputTextJson { get; set; }
        public string OutputTextJson { get; set; }
    }
}
