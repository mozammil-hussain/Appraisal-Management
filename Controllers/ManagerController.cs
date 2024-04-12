using AppraisalManagentSystem.Data;
using AppraisalManagentSystem.InterfaceImpl;
using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppraisalManagentSystem.Controllers
{

   // [Authorize(Policy = "Manager")]

    public class ManagerController : Controller
    {

        public IEmployeeOperations employeeOperations;
        private readonly ICompetencies _compitencies;

        private readonly IAppraisalServices _appraisalServices;
        private readonly Db _context;

        public ManagerController(IEmployeeOperations emp, ICompetencies compitencies, IAppraisalServices appraisalService,Db db)
        {
            _context = db;

            employeeOperations = emp;
            _compitencies = compitencies;
            _appraisalServices = appraisalService;
        }
        public IActionResult Index()
        {

            //Current user id//
            try
            {


                int CurrentUserId = employeeOperations.CurrentUserID(User.Claims.ToList()[1].Value);

                Employees emp1 = employeeOperations.GetEmployeeById(CurrentUserId);
                var ManagerDetails = emp1;
                ViewBag.HrDetails = ManagerDetails;

                List<AppraisalStatus> NewForm = _appraisalServices.GetAppraisalFormHavingStatus("New", CurrentUserId);

                //self Rated//
                List<AppraisalStatus> RatedByEmployee = _appraisalServices.GetAppraisalFormHavingStatus("Self Rated", CurrentUserId);

                List<AppraisalStatus> CreatedStage = _appraisalServices.GetAppraisalFormHavingStatus("Created", CurrentUserId);

                ViewBag.CreatedFlag = CreatedStage;


                List<string> Ename = new List<string>();

                foreach (var item in RatedByEmployee)
                {
                    Ename.Add(employeeOperations.GetEmployeeById(item.EmployeeId)._emp_name);
                }

                //Appraisal Completed..
                List<AppraisalStatus> CompletedStage = _appraisalServices.GetAppraisalFormHavingStatus("Completed", CurrentUserId);

                ViewBag.com = CompletedStage;

                ViewBag.Ename = Ename;//employee names
                ViewBag.selfrated = RatedByEmployee;//status self rated
                ViewBag.NewStatus = NewForm;


                return View();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Content("Please Provide Username and Password Before Access Any resource..\"");
        }

        public IActionResult CreateAppraisal()
        {
            return View();
        }


        //for adding new appraisel
        public IActionResult AddAppraisal()
        {


            AppraisalStatus apps = new AppraisalStatus();
          
            int id = employeeOperations.CurrentUserID(User.Claims.ToList()[1].Value);
           DropDownEmployeeList(id);
            var comp = _compitencies.GetCompetencies();


            IEnumerable<SelectListItem> selectList = comp.Select(x => new SelectListItem { Text = x.CompetencyName, Value = x.ID.ToString() });

            ViewBag.compitency = selectList;

            return View(apps);
        }

        [HttpPost]
        public IActionResult AddAppraisal(AppraisalStatus fm,int data)
        {

            //AppraisalStatus fm = new AppraisalStatus()
            //{
            //    AppraisalId = all.AppraisalId,
            //    EmployeeId = all.EmployeeId,
            //    ManagerId = all.ManagerId,
            //    Status = all.Status,
            //    Objective = all.Objective,
            //    StartDate = all.StartDate,
            //    EndDate = all.EndDate,
            //    //tempCompetency] = 


            //};
            //AppraisalWithCopm appraisalWithCopm = new AppraisalWithCopm()
            //{
            //    AppraisalId = all.AppraisalId,
            //    Compitency = _appraisalServices.GetCompName(all.tempCompetency).ID


            //};
            //_context.Add(appraisalWithCopm);
            //_context.SaveChanges();

            
           
            int id = employeeOperations.CurrentUserID(User.Claims.ToList()[1].Value);
            DropDownEmployeeList(id);

            var comp = _compitencies.GetCompetencies();


            IEnumerable<SelectListItem> selectList = comp.Select(x => new SelectListItem { Text = x.CompetencyName, Value = x.ID.ToString() });

            ViewBag.compitency = selectList;

            fm.ManagerId = id;



            if (true)
            {
                _appraisalServices.AddAppraisal(fm);


                ViewBag.Success = "New Appraisal Created";
                ModelState.Clear();
                return View();
            }
            else
            {

                return View(fm);
            }


        }

        public IActionResult ChangeAppraisalStatus(int appid, string status)
        {
            _appraisalServices.ChanageAppraisalStatus(appid, "Created");

            TempData["success"] = "Status Changed successfully..";
            return RedirectToAction("Index");
        }

        public void DropDownEmployeeList(int id)
        {


            IEnumerable<Employees> data = employeeOperations.EmployeesUnderManager(id);

            var selectList = new SelectList(data.Select(x => new SelectListItem { Text = x._emp_name, Value = x.Id.ToString() }), "Value", "Text");

            ViewBag.SelectList = selectList;



        }


        public IActionResult ActionByManager(int AppID)

        {

                AppraisalStatus fm = _appraisalServices.GetCurrentAppraisalForm(AppID);


            ViewBag.app = fm;//contain current appraisal form

            var Comps = _appraisalServices.GetAppraisalFormDetails(AppID);

            //list of competencies
            List<Competencies> mycomps = new List<Competencies>();

            foreach (var comp in Comps)
            {
                mycomps.Add(_appraisalServices.GetCompName(comp._competency));
            }

            ViewBag.comps = Comps;


            //passing the realted appraisal details

            Appraisal ad = new Appraisal();

            return View(ad);
        }

        [HttpPost]

        public IActionResult ActionByManager(Appraisal details, int appid)
        {

            _appraisalServices.SaveInformationFromManagerSide(appid, details, "Rated");

            return RedirectToAction("Index");
            
        }

        public IActionResult ShowDetails(int appid)
        {
            int UserId = employeeOperations.CurrentUserID(User.Claims.ToList()[1].Value);

            var ad = _appraisalServices.GetCurrentAppraisalFormForCurrentManager(appid, UserId, "Completed");

            ViewBag.fm = ad;

            var Comps = _appraisalServices.GetAppraisalFormDetails(appid);

            ViewBag.comps = Comps;

            return View();
        }
    }
}