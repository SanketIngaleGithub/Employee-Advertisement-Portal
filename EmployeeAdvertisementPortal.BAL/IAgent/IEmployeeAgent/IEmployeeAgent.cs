using System.Collections.Generic;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal
{
    public interface IEmployeeAgent
    {
        /// <summary>
        /// It returns the list of all Employees.
        /// </summary>
        /// <returns></returns>
        List<EmployeeViewModel> GetAllEmployee();
      
        /// <summary>
        /// Method to get employee by it's Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EmployeeViewModel GetEmployeeById(int id);

        /// <summary>
        /// Method to create new Employee.
        /// </summary>
        /// <param name="model"></param>
        void CreateEmployee(EmployeeViewModel model);

        /// <summary>
        /// Method to Update existing Employee.
        /// </summary>
        /// <param name="model"></param>
        void UpdateEmployee(EmployeeViewModel model);

        /// <summary>
        /// Method to delete Employee.
        /// </summary>
        /// <param name="id"></param>
        bool DeleteEmployee(int id);
       /// <summary>
       /// Method to check IsEmailUnique
       /// </summary>
       /// <param name="email"></param>
       /// <returns></returns>
        bool IsEmailUnique(string email);
        /// <summary>
        /// Method to Get Roles List
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetRolesList();
        /// <summary>
        /// Method to Get Employee Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<EmployeeViewModel> GetEmployeeDetail(int id);

        /// <summary>
        /// Method to check if EmpId exists in db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckEmpIdExists(int id);
    }
}