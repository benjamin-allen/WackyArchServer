using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WackyArchServer.Model
{
    public class RunLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ChallengeId { get; set; }
        public DateTime SubmittedTime { get; set; }
        public DateTime CompletedTime { get; set; }
        public Account SubmitterAccount { get; set; }
        public string Code { get; set; }
        public string Result { get; set; }
    }
}
