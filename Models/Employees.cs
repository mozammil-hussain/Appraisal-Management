using System.ComponentModel.DataAnnotations;

namespace AppraisalManagentSystem.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }


      
        [Required]
        public string _emp_name { get; set; }


        //[Unique(ErrorMessage = "This email is already taken.")]
        //[Index(IsUnique = true)]
        //[StringLength(50)]
        [Required]
        public string _emp_email { get; set; }
        //[StringLength(12)]
        [Required]

        public string _emp_password { get; set; }
        public string _emp_phone { get; set; }


        [Required]
        public string _designation { get; set; }
        [Required]
        public int _managerId { get; set; }

        

    }
}

