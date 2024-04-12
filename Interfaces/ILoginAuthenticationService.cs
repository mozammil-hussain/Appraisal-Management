using AppraisalManagentSystem.Models;

namespace AppraisalManagentSystem.Interfaces
{
    public interface ILoginAuthenticationService
    {

        public void LogUser(string userName, string password);
        public Employees AuthencicationAsync(LoginDetails loginDetails);
        public void AuthProcess(Employees emp, LoginDetails logDetails);
        public int CheckNoOfData();



    }
}
