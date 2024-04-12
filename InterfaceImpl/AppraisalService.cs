using AppraisalManagentSystem.Data;
using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;

namespace AppraisalManagentSystem.InterfaceImpl
{
    public class AppraisalService : 
        IAppraisalServices
    {

        private readonly Db _context;

        public AppraisalService(Db db)
        {
            _context = db;
        }

        public void AddAppraisal(AppraisalStatus appraisal)
        {
            AppraisalStatus appraiselform = new AppraisalStatus()
            {
                EmployeeId = appraisal.EmployeeId,
                EndDate = appraisal.EndDate,
                StartDate = appraisal.StartDate,
                ManagerId = appraisal.ManagerId,
                Objective = appraisal.Objective,
                Status = appraisal.Status,

            };

            //saving in the appraisalStatus
            _context.AppraisalStatus.Add(appraiselform);
            _context.SaveChanges();


            var compi = appraisal.tempCompetency;
            foreach (var comp in compi)
            {
                AppraisalWithCopm ac = new AppraisalWithCopm()
                {

                    AppraisalId = appraiselform.AppraisalId,
                    Compitency = comp

                };
                _context.AppraisalWithCopms.Add(ac);
                _context.SaveChanges();
            }
        }

        public List<AppraisalStatus> AllApraisalHavingStatus(string status)
        {
            return _context.AppraisalStatus.Where(x => x.Status == status).ToList();
        }

        public void ChanageAppraisalStatus(int aid, string status)
        {
            var k = _context.AppraisalStatus.Where(m => m.AppraisalId == aid).FirstOrDefault();

            k.Status = status;

            _context.SaveChanges();
        }

        public List<Appraisal> GetAppraisalFormDetails(int aid)
        {
            return _context.Appraisals.Where(m => m._appraisalId == aid).ToList();
        }

        public List<AppraisalStatus> GetAppraisalFormHavingStatus(string status, int empid)
        {
            return _context.AppraisalStatus.Where(m => m.ManagerId == empid && m.Status == status).ToList();
        }

        public List<AppraisalWithCopm> GetComp(int id)
        {
            return _context.AppraisalWithCopms.Where(m => m.AppraisalId == id).ToList();
        }

       
       
        public AppraisalStatus GetCurrentAppraisalFormForCurrentEmployee(int ID, int Empid, string status)
        {
            var appraisalForm = _context.AppraisalStatus.Where(emp => emp.AppraisalId == ID && emp.EmployeeId == Empid && emp.Status == status).FirstOrDefault();

            if (appraisalForm == null)
                return null;
            else
                return appraisalForm;
        }

   
        public AppraisalStatus GetCurrentAppraisalFormHR(int aid, string status)
        {
            return _context.AppraisalStatus.Where(m => m.AppraisalId == aid && m.Status == status).FirstOrDefault();
        }

        public dynamic GetNewEmployeeAppraisal(int id, string status)
        {
            var appraisalForm = _context.AppraisalStatus.Where(emp => emp.EmployeeId == id && emp.Status == status);

            if (appraisalForm.Any()) return appraisalForm;
            else
            {
                return null;
            }

        }

       

        public AppraisalStatus GetCurrentAppraisalForm(int ID)
        {
            var appraisalForm = _context.AppraisalStatus.Where(emp => emp.AppraisalId == ID).FirstOrDefault();

            if (appraisalForm == null)
                return null;
            else
                return appraisalForm;
        }


       //return compitency Name take compitency id and length of total compitencies exists
        public Competencies GetCompName(int id)
        {
            //compitencies id will store in the k
            var k = _context.AppraisalWithCopms.Where(m => m.AppraisalId == id).ToList();

            //return comptencies object
            var c = _context.Competencies.Where(m => m.ID == id).FirstOrDefault();

            return c; 
        }
       

        //this will change the status from new TO given status

        public void ChnageAppraisalStatus(int aid, string status)
        {
            var k = _context.AppraisalStatus.Where(m => m.AppraisalId == aid).FirstOrDefault();

            k.Status = status;

            _context.SaveChanges();



        }


        //save the first appraisal from submitted by Emplyee and change the status i.e SelfRated..

       


        //saving information from manager side and status will be rated...

       

        //give the appraisal form only for current login user or the user belongs to this form
     

        public void SaveFirstAppraisalFormDetails(Appraisal ad, string status, int appid)
        {
            //list of the compitencies (ID)
            var compitenciesList = GetComp(appid);

            //for each comment we will save in appraisal database
            for (int i = 0; i < ad.EComments.Length; i++)
            {

                Appraisal appraisal = new Appraisal()
                {
                    _emp_rating = ad.ERating[i],
                    _appraisalId = appid,
                    _emp_comments = ad.EComments[i],
                    _manager_comments ="Demo"/* ad._manager_comments*/,
                    _manager_rating = ad._manager_rating,
                    _competency = compitenciesList[i].Compitency


                };

                _context.Appraisals.Add(appraisal);
                _context.SaveChanges();
            }

            //now changing the appraisal status

            AppraisalStatus? af = _context.AppraisalStatus.Where(m => m.AppraisalId == appid).FirstOrDefault();

            af.Status = status;

            _context.SaveChanges();
        }

        public void SaveInformationFromManagerSide(int aid, Appraisal ad, string status)
        {
            //get the appraisal form
            var k = _context.AppraisalStatus.Where(m => m.AppraisalId == aid).FirstOrDefault();

            if (k != null)
            {

                //   k.Status = status;
                //get the related compitencies
                var compi = _context.AppraisalWithCopms.Where(m => m.AppraisalId == aid).ToList();

                int len = compi.Count;

                for (int i = 0; i < len; i++)
                {
                     
                    var apd = _context.Appraisals.Where(m => m._appraisalId == aid && m._competency == compi[i].Compitency).FirstOrDefault();

                    if (apd != null)
                    {
                        apd._manager_comments = ad.MComments[i];
                        apd._manager_rating = ad.MRating[i];
                        _context.SaveChanges();
                    }

                }


                k.Status = status;


                _context.SaveChanges();

            }
        }

        public AppraisalStatus GetCurrentAppraisalFormForCurrentManager(int ID, int Mid, string status)
        {
            {
                var appraisalForm = _context.AppraisalStatus.Where(emp => emp.AppraisalId == ID && emp.ManagerId == Mid && emp.Status == status).FirstOrDefault();

                if (appraisalForm == null)
                    return null;
                else
                    return appraisalForm;
            }
        }
        public dynamic CheckNewAppraisalForEmployee(int id, string status)
        {
            //first check by id...
            var appraisalForm = _context.AppraisalStatus.Where(emp => emp.EmployeeId == id && emp.Status == status);

            if (appraisalForm!=null) return appraisalForm;
            else
            {//if no appraisal form exist for the current employee
                return null;
            }


        }
    }
}
