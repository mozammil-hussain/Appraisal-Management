
using AppraisalManagentSystem.InterfaceImpl;
using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AppraisalManagentSystem.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace AppraisalManagentSystem.Controllers
{

    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginUser _loginUser;
        private readonly ILoginAuthenticationService _loginAuthenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;



        private  Db _context;
        public HomeController(ILogger<HomeController> logger,Db context , ILoginUser loginUser, ILoginAuthenticationService loginAuthenticationService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _loginAuthenticationService= loginAuthenticationService;
            _loginUser = loginUser;
            _logger = logger;
            _context = context;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> LoginAsync(LoginDetails loginDetails)
        //{

        //    //Console.WriteLine(useremail + " "+password);
        //    ////_loginUser = new Login();
        //    ////_loginUser.LoggedIn(email, password);
        //    //_loginAuthenticationService = new LoginAuthenticationService(_context);
        //    //ViewData["email"] = useremail;
        //    //ViewData["password"] = password;
        //    //_loginAuthenticationService.LogUser(useremail, password);

        //    //var login = new LoginDetails
        //    //{
        //    //    user_name = useremail,
        //    //    password = password
        //    //};

        //    //Console.WriteLine(_loginAuthenticationService.AuthencicationAsync(loginDetails.user_name, loginDetails.password));

        //    //_context.LoginDetails.Add(loginDetails);
        //    //_context.SaveChanges();


        //    var user = await _context.Employees.FirstOrDefaultAsync(u => u._emp_email == loginDetails.user_name && u._emp_password == loginDetails.password);
        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    ViewBag.username = loginDetails.user_name;

        //    await Console.Out.WriteLineAsync("Login Success");

        //    return RedirectToAction("Index");
        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Login()
        {
           ClaimsPrincipal claimUser = HttpContext.User;
           
            if (claimUser.Identity.IsAuthenticated)
            {
                if (User.Claims.ToList()[2].Value == "HR" && User.Claims.ToList()[0].Value == "Kabeer")
                    return RedirectToAction("Index", "HR");
                if (User.Claims.ToList()[2].Value == "Manager")
                    return RedirectToAction("Index", "Manager");
                return RedirectToAction("Index", "Employee");
              
            }

            //LoginForm data
            LoginDetails loginForm = new LoginDetails();

            return View(loginForm);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDetails logDeatil)
        {

            if (ModelState.IsValid)
            {
                int data = _loginAuthenticationService.CheckNoOfData();

                if (data > 0)
                {
                    var isCorrect = _loginAuthenticationService.AuthencicationAsync(logDeatil);

                    if (isCorrect != null)
                    {
                        _loginAuthenticationService.AuthProcess(isCorrect, logDeatil);

                        if (isCorrect._designation == "HR")
                            return RedirectToAction("Index", "HR");
                        else if (isCorrect._designation == "Manager")
                            return RedirectToAction("login");
                        
                        else return RedirectToAction("login");

                    }
                    else
                    {
                        ViewBag.ErrorName = "Incorrect Username or Password";
                        return View();
                    }

                }
                else
                {
                    ViewBag.ErrorName = "Internal Database Error...";
                    return View();

                }

            }
            else
            {

                return View();
            }

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");

        }


    }

    
}
