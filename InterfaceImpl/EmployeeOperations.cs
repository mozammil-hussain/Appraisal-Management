using AppraisalManagentSystem.Data;
using AppraisalManagentSystem.Interfaces;
using AppraisalManagentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppraisalManagentSystem.InterfaceImpl
{
    public enum Designation
    {
        HR,
        Manager,
        QA,
        Designer,
        CEO,
        Devlopment

    }
    public class EmployeeOperations : IEmployeeOperations
    {
        private readonly Db _context;
        public EmployeeOperations(Db context)
        {
            _context = context;
        }

        public Employees AddEmployee(Employees employee)
        {
            //var _saveEmployee = new Employees
            //{
            //    _empId = employee._empId,
            //    _emp_name = employee._emp_name,
            //    _emp_email = employee._emp_email,
            //    _emp_phone = employee._emp_phone,
            //    _designation = employee._designation,
            //    _managerId = employee._managerId,

            //};


           
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee;

            

            
        }


        


        public List<Employees> GetAllEmployees()
        {
            var emp = _context.Employees.ToList();
            return emp;
        }


        //this methid will add new employee
        //public async Task<bool> AddEmployee(Employee emp)
        //{
        //    Employee employee = new Employee()
        //    {

        //        Name = emp.Name,
        //        email = emp.email,
        //        Phone = emp.Phone,
        //        MID = emp.MID,
        //        password = emp.password,
        //        Designation = Enum.GetName(typeof(Designation), int.Parse(emp.Designation))


        //    };

        //    await _employeeContext.Employees.AddAsync(employee);
        //    await _employeeContext.SaveChangesAsync();

        //    return true;
        //}

        //checking the email exist or not

        public async Task<bool> UserAlreadyExist(string email)
        {


            return await _context.Employees.AnyAsync(m => m._emp_email == email);
           
        }


        public Dictionary<int, string> ManagerList()
        {


            dynamic empdata = _context.Employees.Select(m => new { m.Id, m._emp_name, m._designation }).ToDictionary(m => m.Id, m => m._emp_name + $" - ({m._designation + " " + m.Id})");
            return empdata;


        }
        public List<Employees> EmpList()
        {
            return _context.Employees.ToList();
        }

        public Employees GetEmployeeById(int empID)
        {
            return _context.Employees.Where(emp => emp.Id == empID).FirstOrDefault();
        }

        public bool EditEmployeeEmailExist(string email, int id)
        {
            bool data = _context.Employees.Any(m => m._emp_email == email);

            if (data)
            {
                var isameUser = _context.Employees.Where(m => m._emp_email == email && m.Id == id).FirstOrDefault();
               
                if (isameUser != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            else
            {
                return true;
            }
        }

        public int EditEmployee(Employees e)
        {

            if (_context.Employees.Find(e.Id) != null)
            {
                _context.Entry(e).State = EntityState.Deleted;
            }
            e._designation = Enum.GetName(typeof(Designation), int.Parse(e._designation));

            _context.Entry(e).State = EntityState.Modified;
            int a = _context.SaveChanges();
            return a;

        }

        public IEnumerable<Employees> EmployeesUnderManager(int managerID)
        {

            var employees = _context.Employees
            .Where(e => e._managerId == managerID)
            .ToList();
            return employees;


        }
        public int CurrentUserID(string email)
        {
            var user = _context.Employees.Where(mail => mail._emp_email == email).FirstOrDefault();

            if (user != null)
            {
                return user.Id;
            }
            else
            {
                return -1;
            }
        }

        public async Task<bool> EmailDoesNotExist(string email)
        {
            return await _context.Employees.AnyAsync(m => m._emp_email == email);
        }
    }
}
