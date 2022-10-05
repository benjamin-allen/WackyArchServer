using System.ComponentModel.DataAnnotations.Schema;

namespace WackyArchServer.Model
{
    public class BetaChallenge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Flag { get; set; }
        public Guid? PredecessorId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

        public string InputProgramJson { get; set; }
    }
}
