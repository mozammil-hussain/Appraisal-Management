using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AppraisalManagentSystem.Models
{
    public class AllDetailsAppraisalsCompetensies
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppraisalId { get; set; }
        public int EmployeeId { get; set; }
        public int ManagerId { get; set; }

        [Required]
        public string Status { get; set; } = "New";

        //[Required(ErrorMessage = "select the compitency")]

        //public int Competency { get; set; }
        [DisplayName("Objective")]
        public string Objective { get; set; } = "No objectives";






        [DisplayName("Appraisal Start Date")]
        [Required(ErrorMessage = "Start Date is required..")]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }


        [DisplayName("Last Evaluation Date")]
        [Required(ErrorMessage = "End Date is required..")]
        [DataType(DataType.Date)]
        public string EndDate { get; set; }


        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Required(ErrorMessage = "Please select the compitency..")]

        [DisplayName("Select Competencies")]
        public int tempCompetency { get; set; }

        public string CompetencyName { get; set; }
        public string TypeC { get; set; }
    }
}
