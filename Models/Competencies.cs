using System.ComponentModel.DataAnnotations;

namespace AppraisalManagentSystem.Models
{
    public class Competencies
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string CompetencyName { get; set; }
        public string TypeC { get; set; }
    }
}
