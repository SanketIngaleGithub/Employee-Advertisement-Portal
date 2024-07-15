using System.Threading.Tasks;

namespace EmployeeAdvertisementPortal
{
    public interface ILoginAgent
    {
        /// <summary>
        /// Validates a user's credentials against the stored User login details.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>true if credentials are valid, otherwise false.</returns>
        Task<bool> ValidateUser(LoginViewModel loginDetails);

        /// <summary>
        /// Method to Get user Id by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        int GetUserIdByEmail(string email);

        /// <summary>
        /// Method to Check Email Unique
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsEmailUnique(string email);

        /// <summary>
        /// Method to Reset Password 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
