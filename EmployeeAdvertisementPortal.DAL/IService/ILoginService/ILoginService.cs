using EmployeeAdvertisementPortal.DAL.Data;
using System.Collections.Generic;

namespace EmployeeAdvertisementPortal
{
    public interface ILoginService
    {
        /// <summary>
        /// Validates a user's credentials against the stored User login details.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>true if credentials are valid, otherwise false.</returns>
        bool ValidateUserLoginDetails(EmployeeDetails_tbl employeeDetails_Tbl);

        /// <summary>
        /// Method to map employee emails and passwords
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        List<EmployeeDetails_tbl> MapEmployeeEmailAndPassword(List<EmployeeDetails_tbl> employees);
        
        /// <summary>
        /// Method to get user by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        int GetUserIdByEmail(string email);

        /// <summary>
        /// Method to check if email entered is already registered or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsEmailUnique(string email);

        /// <summary>
        /// Method to reset password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool  ResetPassword(string email, string newPassword);
    }
}
