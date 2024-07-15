using EmployeeAdvertisementPortal.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeAdvertisementPortal
{
    public class EmployeeService : IEmployeeService
    {
        Employee_Advertisement_PortalEntity db = new Employee_Advertisement_PortalEntity();
        public List<UserRole_tbl> GetAllRoles()
        {
            var roles = db.UserRole_tbl.ToList(); 
            return roles;
        }

        public List<EmployeeDetails_tbl> GetAllEmployee()
        {
            List<EmployeeDetails_tbl> listOfEmployees = db.EmployeeDetails_tbl.Include("UserRole_tbl").ToList();
            return MapEmployeeDetails(listOfEmployees);
        }

        public EmployeeDetails_tbl GetEmployeeById(int id)
        {
            return db.EmployeeDetails_tbl.FirstOrDefault(x => x.EmpId == id);
        }

        public bool CreateEmployee(EmployeeDetails_tbl model)
        {
            try
            {
                model.EmpId = 0;
                model.UserRole_tbl = db.UserRole_tbl.FirstOrDefault(x => x.RoleId == model.RoleId);
                if(!db.EmployeeDetails_tbl.Any(m=> m.Email== model.Email))
                {
                    db.EmployeeDetails_tbl.Add(model);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateEmployee(EmployeeDetails_tbl model)
        {
            try
            {
                db.SP_EmployeeDetail(model.EmpId, model.FirstName, model.LastName, model.Email, model.Gender, model.DOB, model.Address, model.RoleId, model.CreatedBy, model.CreatedDate, model.ModifiedBy, model.ModifiedDate, model.Password);
                db.SaveChanges(); 
                return true;
            }
            catch (Exception)
            {
               throw;
            }
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                EmployeeDetails_tbl employee = db.EmployeeDetails_tbl.FirstOrDefault(x => x.EmpId == id);
                if (employee != null)
                {
                    db.EmployeeDetails_tbl.Remove(employee);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<EmployeeDetails_tbl> MapEmployeeDetails(List<EmployeeDetails_tbl> employees)
        {
            List<EmployeeDetails_tbl> modifiedListOfEmployees = new List<EmployeeDetails_tbl>();
            foreach (EmployeeDetails_tbl item in employees)
            {
                EmployeeDetails_tbl modifiedEmployee = new EmployeeDetails_tbl();
                modifiedEmployee.EmpId = item.EmpId;
                modifiedEmployee.FirstName = item.FirstName;
                modifiedEmployee.LastName = item.LastName;
                modifiedEmployee.Email = item.Email;
                modifiedEmployee.Gender = item.Gender;
                modifiedEmployee.DOB = item.DOB;
                modifiedEmployee.Address = item.Address;
                modifiedEmployee.UserRole_tbl = new UserRole_tbl();
                modifiedEmployee.UserRole_tbl.Role = item.UserRole_tbl.Role;
                modifiedEmployee.RoleId = item.RoleId;
                modifiedEmployee.CreatedBy = item.CreatedBy;
                modifiedEmployee.CreatedDate = item.CreatedDate;
                modifiedEmployee.ModifiedBy = item.ModifiedBy;
                modifiedEmployee.ModifiedDate = item.ModifiedDate;
                modifiedEmployee.Password = item.Password;
                modifiedListOfEmployees.Add(modifiedEmployee);
             }
            return modifiedListOfEmployees;
        }

        public bool IsEmailUnique(string email)
        {
            return !db.EmployeeDetails_tbl.Any(e => e.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public List<EmployeeDetails_tbl> GetEmployeeDetail(int id)
        {
            return db.EmployeeDetails_tbl.Where(a => a.EmpId == id).ToList();
        }

        public bool CheckEmpIdExists(int id)
        {
            return db.EmployeeDetails_tbl.Any(x => x.EmpId == id);
        }
    }
}