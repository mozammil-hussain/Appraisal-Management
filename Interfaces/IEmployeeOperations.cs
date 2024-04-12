using AppraisalManagentSystem.Models;

namespace AppraisalManagentSystem.Interfaces
{
    public interface IEmployeeOperations
    {
        public Employees AddEmployee(Employees employee);
       
        public int EditEmployee(Employees e);

        public int CurrentUserID(string email);

        public List<Employees> GetAllEmployees();


        public Task<bool> EmailDoesNotExist(string email);

        public Dictionary<int, string> ManagerList();

        public IEnumerable<Employees> EmployeesUnderManager(int managerId);

        public List<Employees> EmpList();

        public Employees GetEmployeeById(int EmpId);

        public bool EditEmployeeEmailExist(string email, int id);

        public  Task<bool> UserAlreadyExist(string email);


    }
}
