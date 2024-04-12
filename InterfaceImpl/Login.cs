using AppraisalManagentSystem.Data;
using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace AppraisalManagentSystem.InterfaceImpl
{
    public class Login : ILoginUser
    {

        private readonly Db _context;
        private readonly string _username;
        private readonly string _password;
        public Login(Db db)
        {
            _context = db;
        }
        public int LoggedIn(string username, string password)
        {
            var login = new LoginDetails
            {
                user_name = username,
                password = password
            };
            _context.LoginDetails.Add(login);
            _context.SaveChanges();
            return 1;
        }
    }
}
