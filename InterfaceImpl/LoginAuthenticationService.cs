using AppraisalManagentSystem.Data;
using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace AppraisalManagentSystem.InterfaceImpl
{
   
    public class LoginAuthenticationService : ILoginAuthenticationService
    {
        private readonly Db _context;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public LoginAuthenticationService(Db context, IHttpContextAccessor httpContextAccessor)
        { 
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public Employees AuthencicationAsync(LoginDetails loginDetails)
        {
            var user = _context.Employees.Where(u => u._emp_email == loginDetails.user_name && u._emp_password==loginDetails.password).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public void LogUser(string userName, string password)
        {
            var loggedUserData = new LoginDetails
            {
                user_name = userName,
                password = password
            };
            _context.LoginDetails.Add(loggedUserData);
            _context.SaveChanges();

        }

        public int CheckNoOfData()
        {
            int data = _context.Employees.Count();

            return data;
        }

        public void AuthProcess(Employees emp, LoginDetails logDetails)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,emp._emp_name),
                new Claim(ClaimTypes.Email,emp._emp_email),
                new Claim("Designation",emp._designation),

            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                          CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            AuthenticationProperties properties = new AuthenticationProperties()
            {

                AllowRefresh = true,

            };


            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimsIdentity), properties);
        }
    }
}
