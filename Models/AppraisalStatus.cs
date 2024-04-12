

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppraisalManagentSystem.Models
{
    public class AppraisalStatus
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
        public string Objective { get; set; } = "None";

       




        [DisplayName("Appraisal Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }


        [DisplayName("Last Appraisal Date")]
        [Required(ErrorMessage = "End Date is required..")]
        [DataType(DataType.Date)]
        public string EndDate { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Please select the cometency")]

        [DisplayName("Select Competencies")]

        public int[] tempCompetency { get; set; }

    }

}

