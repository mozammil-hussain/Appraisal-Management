using AppraisalManagentSystem.Data;
using AppraisalManagentSystem.InterfaceImpl;
using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace AppraisalManagentSystem.Controllers
{


    public class EmployeeController : Controller
    {
        

        public IEmployeeOperations employeeOperations;
        private readonly ICompetencies _compitencies;

        private readonly IAppraisalServices _appraisalServices;

        public EmployeeController(IEmployeeOperations emp, ICompetencies compitencies, IAppraisalServices appraisalService)
        {

            employeeOperations = emp;
            _compitencies = compitencies;
            _appraisalServices = appraisalService;
        }
        public IActionResult Index()
        {
            try
            {

                int UserId = employeeOperations.CurrentUserID(User.Claims.ToList()[1].Value);
                Employees emp1 = employeeOperations.GetEmployeeById(UserId);


                ViewBag.EmpDetails = emp1;


                var MyAppraisalForm = _appraisalServices.CheckNewAppraisalForEmployee(UserId, "Created");


                var FinalApp = _appraisalServices.CheckNewAppraisalForEmployee(UserId, "Rated");
                var completedAppraisal = _appraisalServices.CheckNewAppraisalForEmployee(UserId, "Completed");

                if (MyAppraisalForm != null)
                {


                    ViewBag.status = MyAppraisalForm;
                    ViewBag.final = FinalApp;
                    ViewBag.ca = completedAppraisal;
                }



                return View();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return Content("Please Provide Username and Password Before Access Any resource..");

        }

        public IActionResult FirstAppraisalDetail(int appID)
        {
            int UserId = employeeOperations.CurrentUserID(User.Claims.ToList()[1].Value);

            var MyAppraisalForm = _appraisalServices.GetCurrentAppraisalForm(appID);
            var Comps = _appraisalServices.GetComp(appID);
            //appraisal form
            ViewBag.status = MyAppraisalForm;
            //list of competencies
            List<Competencies> mycomps = new List<Competencies>();

            foreach (var comp in Comps)
            {
                mycomps.Add(_appraisalServices.GetCompName(comp.Compitency));
            }

            ViewBag.comps = mycomps;
            ViewBag.Manager = employeeOperations.GetEmployeeById(MyAppraisalForm.ManagerId)._emp_name;

            Appraisal ad = new Appraisal();


            return View(ad);
        }

        [HttpPost]
        public IActionResult FirstAppraisalDetail(Appraisal ap, int appID)
        {
            if (true)
            {
                _appraisalServices.SaveFirstAppraisalFormDetails(ap, "Self Rated", appID);

                return RedirectToAction("Index", "Employee");
            }
            else
            {
                TempData["ErrorMsg"] = "Something Went Wrong Plese fill the comment Section..";

                return RedirectToAction("FirstAppraisalDetail", new { appID = appID });
            }
        }

        public IActionResult EmployeeApproval(int appID)
        {
            var MyAppraisalForm = _appraisalServices.GetCurrentAppraisalForm(appID);


            ViewBag.fm = MyAppraisalForm;

            //Compitencies ..comment
            var Comps = _appraisalServices.GetAppraisalFormDetails(appID);



            ViewBag.comps = Comps;

            return View();

        }
        [HttpPost]
        public IActionResult AcceptAppraisal(int appID)
        {
            TempData["success"] = "Appraisal Process Done, I have Accepted the Appraisal";
            _appraisalServices.ChanageAppraisalStatus(appID, "Completed");
            return RedirectToAction("Index");
        }

        public IActionResult CheckDetailsOfAppraisal (int AppID)
        {
            int UserId = employeeOperations.CurrentUserID(User.Claims.ToList()[1].Value);

            var MyAppraisalForm = _appraisalServices.GetCurrentAppraisalFormForCurrentEmployee(AppID, UserId, "Completed");


            ViewBag.fm = MyAppraisalForm;

            var Comps = _appraisalServices.GetAppraisalFormDetails(AppID);



            ViewBag.comps = Comps;

            return View();
        }


       

    }
}
