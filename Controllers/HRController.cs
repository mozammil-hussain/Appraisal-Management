using AppraisalManagentSystem.CommonInfo;
using AppraisalManagentSystem.Data;
using AppraisalManagentSystem.InterfaceImpl;
using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AppraisalManagentSystem.Controllers
{

    public class HRController : Controller
    {
        private Db _context;
        private readonly IEmployeeOperations _employeeOperations;

       // private readonly IEmailService _emailService;

        private readonly ICompetencies _competencies;

        private readonly ILoginUser _loginUser;

        private readonly IAppraisalServices _appraisalServices;

        public HRController(IEmployeeOperations employeeOperations, ICompetencies competencies, ILoginUser loginUser, IAppraisalServices appraisalServices,Db db/*, IEmailService emailService*/)
        {
            _context = db;
           // _emailService = emailService;
            _appraisalServices = appraisalServices;
            _competencies = competencies;
            _employeeOperations = employeeOperations;
            _loginUser = loginUser;
        }


        public IActionResult Index(string pattern)
        {
            

            try
            {
                int UserId = _employeeOperations.CurrentUserID(User.Claims.ToList()[1].Value);
                Employees emp1 = _employeeOperations.GetEmployeeById(UserId);
                var HrDetails = emp1;
                ViewBag.HrDetails = HrDetails;
                List<Employees> emp = _employeeOperations.GetAllEmployees();
                List<Competencies> com = _competencies.GetCompetencies();

                List<Employees> emps = _employeeOperations.GetAllEmployees();
                ViewBag.emps = emps;


                List<AppraisalStatus> CompletedApprsailForm = _appraisalServices.AllApraisalHavingStatus("Completed");
                ViewBag.CompletedApprsailForm = CompletedApprsailForm;

                ViewBag.comm = com;
                return View(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Content("Please Provide Username and Password Before Access Any resource..");

        }

        public IActionResult AddEmployee(string pattern) 
        {
            ViewBag.Pattern = pattern;
            
            return View();
        }

        [HttpPost]
        public IActionResult SaveEmployee(Employees employee)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            //_emailService.SendEmail(employee._emp_email,"ZerozeroOne", "Trying");

            //Task<bool> b = _employeeOperations.UserAlreadyExist(employee._emp_email);

            //if (b!=null)
            //{
            //    TempData["UnableToAdd"] = "User Already Exist User Other email";
            //    return RedirectToAction("AddEmployee");
            //}


            //ViewData["emp"] = employee;

            if (Regex.IsMatch(employee._emp_password,pattern))
            {
                Employees emp = _employeeOperations.AddEmployee(employee);
           
             if(emp!= null)
            {
                _loginUser.LoggedIn(emp._emp_email, emp._emp_password);


            }
            }
            else
            {
                return RedirectToAction("AddEmployee", new { pattern = "Pls provide strong password" });
            }

            return RedirectToAction("Index");
        }

         
        public IActionResult AddCompetencies()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCompetencies(Competencies competencies)
        {

            _competencies.SaveCompetencies(competencies);
            return RedirectToAction("Index");
        }



        public IActionResult SeeDetailsAppraisal(int AId)
        {
            var forms = _appraisalServices.GetCurrentAppraisalFormHR(AId, "Completed");

            ViewBag.fm = forms;

            var Comps = _appraisalServices.GetAppraisalFormDetails(AId);

            ViewBag.comps = Comps;
            return View();
        }

        public IActionResult EditEmployee(int EmpId)
        {
            DropDown();

            var employee = _employeeOperations.GetEmployeeById(EmpId);
            if (employee != null)
            {

                return View(employee);
            }
            else
            {
                return Content("Employee Does not Exist");
            }


        }

        [HttpPost]
        public IActionResult EditEmployee(Employees emp)
        {
            DropDown();



            if (ModelState.IsValid)
            {
                //cheking email already exist or not
                bool isExist = _employeeOperations.EditEmployeeEmailExist(emp._emp_email, emp.Id);

                if (isExist == false)
                {
                    ViewBag.Error = "Email Already Exist..";

                    return View();
                }
                else
                {

                    int isEdited = _employeeOperations.EditEmployee(emp);
              
                    if (isEdited > 0)
                    {
                        TempData["Edited"] = $"{emp._emp_name} is updated successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Content("Something went wrong..");

                    }

                }


            }
            else
            {
                return View();
            }




        }

        private void DropDown()
        {
            Dictionary<int, string> data = _employeeOperations.ManagerList();
            data.Add(0, "None");
            var selectList = new SelectList(data.Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }), "Value", "Text");


            ViewBag.SelectList = selectList;
        }

        public IActionResult DeleteCompetency(int CompId)
        {
            var Comps = _appraisalServices.GetAppraisalFormDetails(CompId);

            return RedirectToAction("Index");

        }
        
        public IActionResult DeleteEmployeeById(int EmployeeID)
        {
            var employeeToDelete = _employeeOperations.GetEmployeeById(EmployeeID);

            if (employeeToDelete == null)
            {
                throw new ArgumentException("Employee not found");
            }

            _context.Employees.Remove(employeeToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        


        }
}
