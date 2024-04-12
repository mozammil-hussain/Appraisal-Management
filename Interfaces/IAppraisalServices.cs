using AppraisalManagentSystem.Models;


namespace AppraisalManagentSystem.Interfaces
{
    public interface IAppraisalServices
    {


        public void AddAppraisal(AppraisalStatus appraisal); 
        public dynamic GetNewEmployeeAppraisal(int id, string status);  

        public AppraisalStatus GetCurrentAppraisalForm(int id);                                


        public List<AppraisalWithCopm> GetComp(int id); 

        public Competencies GetCompName(int id);   

        public List<AppraisalStatus> GetAppraisalFormHavingStatus(string status, int empid); 

        public void ChanageAppraisalStatus(int aid, string status);               

        public void SaveFirstAppraisalFormDetails(Appraisal ad, string status, int appid);                                     

        public List<Appraisal> GetAppraisalFormDetails(int aid);   

        public void SaveInformationFromManagerSide(int aid, Appraisal ad, string status);                                       

        public AppraisalStatus GetCurrentAppraisalFormForCurrentEmployee(int ID, int Empid, string status);
        public AppraisalStatus GetCurrentAppraisalFormForCurrentManager(int ID, int Mid, string status);  

        public List<AppraisalStatus> AllApraisalHavingStatus(string status); 

        public AppraisalStatus GetCurrentAppraisalFormHR(int aid, string status);  

        public dynamic CheckNewAppraisalForEmployee(int id, string status);



    }
}
