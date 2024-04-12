using System.ComponentModel.DataAnnotations;

namespace AppraisalManagentSystem.Models
{
    public class LoginDetails
    {
        [Key]
        public int Id { get; set; }
        public string user_name { get; set; }
        
        public string password { get; set; } 
    }
}
