using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppraisalManagentSystem.Models
{
    public class AppraisalWithCopm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("AppraisalStatus")]
        public int AppraisalId { get; set; }

        [Required]
        public int Compitency {  get; set; }

    }
}
