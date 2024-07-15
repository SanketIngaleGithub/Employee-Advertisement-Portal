using EmployeeAdvertisementPortal.DAL.Data;
using System.Collections.Generic;

namespace EmployeeAdvertisementPortal
{
    public interface IEmployeeService
    {
        /// <summary>
        /// It returns the list of all Employees.
        /// </summary>
        /// <returns></returns>
        List<EmployeeDetails_tbl> GetAllEmployee();

        /// <summary>
        /// Method to get employee by it's Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EmployeeDetails_tbl GetEmployeeById(int id);

        /// <summary>
        /// Method to create new employee.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool CreateEmployee(EmployeeDetails_tbl model);

        /// <summary>
        /// Method to update existing employee.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateEmployee(EmployeeDetails_tbl model);

        /// <summary>
        /// Method to delete employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteEmployee(int id);

        /// <summary>
        /// Method to get role by Id.
        /// </summary>
        /// <returns></returns>
        bool IsEmailUnique(string email);

        /// <summary>
        /// Method to map Employee Details
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        List<EmployeeDetails_tbl> MapEmployeeDetails(List<EmployeeDetails_tbl> employees);

        /// <summary>
        /// Method to get role of employee
        /// </summary>
        /// <returns></returns>
        List<UserRole_tbl> GetAllRoles();

        /// <summary>
        /// Method to check if EmpId exists in db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckEmpIdExists(int id);
    }
}