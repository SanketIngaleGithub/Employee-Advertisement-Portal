using EmployeeAdvertisementPortal.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeAdvertisementPortal
{
    public class LoginService : ILoginService
    {
        private readonly Employee_Advertisement_PortalEntity _db;
        public LoginService(Employee_Advertisement_PortalEntity db)
        {
            _db = db;
        }

        public List<EmployeeDetails_tbl> MapEmployeeEmailAndPassword(List<EmployeeDetails_tbl> employees)
        {
            List<EmployeeDetails_tbl> modifiedListOfEmployees = new List<EmployeeDetails_tbl>();
            foreach (EmployeeDetails_tbl item in employees)
            {
                EmployeeDetails_tbl modifiedEmployee = new EmployeeDetails_tbl();
               
                modifiedEmployee.Email = item.Email;
                modifiedEmployee.Password = item.Password;
                modifiedListOfEmployees.Add(modifiedEmployee);
            }
            return modifiedListOfEmployees;
        }

        public bool ValidateUserLoginDetails(EmployeeDetails_tbl employee)
        {
            try
            {
                List<EmployeeDetails_tbl> modifiedEmployees = MapEmployeeEmailAndPassword(_db.EmployeeDetails_tbl.ToList());

                return modifiedEmployees.Any(x => x.Email == employee.Email && x.Password == employee.Password);
            }
            catch (Exception ex)
            {               
                throw ex;
            }
        }

        public int GetUserIdByEmail(string email)
        {
            try
            {
                using (Employee_Advertisement_PortalEntity context = new Employee_Advertisement_PortalEntity())
                {
                    EmployeeDetails_tbl user = context.EmployeeDetails_tbl.FirstOrDefault(x => x.Email == email);
                    if (user != null)
                    {
                        return user.EmpId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        public bool IsEmailUnique(string email)
        {
            return !_db.EmployeeDetails_tbl.Any(e => e.Email == email);
        }

        public bool ResetPassword(string email, string Password)
        {
            EmployeeDetails_tbl user = _db.EmployeeDetails_tbl.FirstOrDefault(e => e.Email == email);
            if (user != null)
            {
                user.Password = Password;
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}