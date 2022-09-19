using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WackyArchServer.Model
{
    public class AlphaChallengeTest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public AlphaChallenge AlphaChallenge { get; set; }

        // These will be JSON Strings, to be decoded.
        // Schema: An array of objects, {"name": "xyz", "data": [1,2,3,4,5]}
        public string InputTextJson { get; set; }
        public string OutputTextJson { get; set; }
    }
}
