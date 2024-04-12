using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppraisalManagentSystem.Models
{
    public class Appraisal
    {

        [Key]
     
        public int _detailsId { get; set; }

        [Required]
        public int _appraisalId { get; set; }

        [Required]
        public int _competency { get; set; }

        [Required]
        public int _emp_rating { get; set; }

        [StringLength(100)]
        public string? _emp_comments{get; set;}
        [Required]
        public int _manager_rating { get; set; }

     
        public string _manager_comments { get; set; }



        [NotMapped]
        [Required(ErrorMessage = "Employee comment is required")]

        public string[]? EComments { get; set; }

        [NotMapped]
        public string[]? MComments { get; set; } = { "dummy" };

        [NotMapped]

        public int[] ERating { get; set; }


        [NotMapped]

        public int[] MRating { get; set; } = { 0 };


    }
}
