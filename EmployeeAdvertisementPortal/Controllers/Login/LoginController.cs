using EmployeeAdvertisementPortal.Core.Utility;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeAdvertisementPortal.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginAgent _loginAgent;

        #region Public Constructor
        public LoginController(ILoginAgent loginAgent)
        {
            _loginAgent = loginAgent;  
        }
        #endregion

        #region Public Method Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Public Method Login
        public ActionResult UserLogin()
        {
            Session["Email"] = null;
            Session["FirstName"] = null;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UserLogin(LoginViewModel login)
        {
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                if (await _loginAgent.ValidateUser(login))
                {
                    int userId = _loginAgent.GetUserIdByEmail(login.Email);

                    if (userId > 0)
                    {
                        Session["UserId"] = userId;
                        Session["Email"] = login.Email;
                        Session["FirstName"]=login.Email;    
                        FormsAuthentication.SetAuthCookie(login.Email, true);
                        Session["UserName"] = login.Email;
                        return RedirectToAction("ApprovedAdvertisementList", "Advertisement");
                    }
                }
                ModelState.AddModelError("", "Invalid user credentials .");
            }
            return View(login);
        }

        #endregion

        #region Public Method Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
        #endregion

        #region Public Method ForgotPassword
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            bool emailExists = _loginAgent.IsEmailUnique(email);

            if (!emailExists)
            {
                string otp = GenerateOTP();
                SendOTPByEmail(email, otp);

                Session["OTP"] = otp;
                Session["ResetEmail"] = email;    
                ViewBag.ShowOtpMessage = true;
                if (ViewBag.ShowOtpMessage)
                {
                    return RedirectToAction("VerifyOTP");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewBag.ShowOtpMessage = false;
                ModelState.AddModelError("Email", "Email address not found.");
                return View();
            }
        }

        #endregion

        #region Public Method VerifyOtp 
        [HttpGet]
        public ActionResult VerifyOTP()
        {
            ViewBag.ShowOtpMessage = true;

            if (Session["OTP"] != null && Session["ResetEmail"] != null)
            {
                return View();
            }
            else
            {
                ViewBag.ShowOtpMessage = false;
                return RedirectToAction("ForgotPassword");
            }
        }

        [HttpPost]
        public ActionResult VerifyOTP(string otp)
        {
            string storedOTP = Session["OTP"].ToString();
            if (otp == storedOTP)
            {
                return RedirectToAction("ResetPassword");
            }
            else
            {
                ModelState.AddModelError("OTP", "Invalid OTP. Please try again.");
                return View();
            }
        }
        #endregion

        #region Public Method GenerateOtp 
        private string GenerateOTP()
        {
            Random rnd = new Random();
            int otp = rnd.Next(100000, 999999);
            return otp.ToString();
        }
        #endregion

        #region Public Method SendOTPByEmail 
        private void SendOTPByEmail(string email, string otp)
        {
            string subject = "OTP for Password Reset";
            string body = $"Your OTP for password reset is: {otp}";
            MailUtility.SendEmail(email, subject, body);
        }
        #endregion

        #region Public Method ResetPassword
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(LoginViewModel model)
        {
            ModelState.Remove("Email");
            if (ModelState.IsValid)
            {
                string email = Session["ResetEmail"].ToString();
                bool passwordReset = _loginAgent.ResetPassword(email, model.Password, model.ConfirmPassword);

                if (passwordReset)
                {
                    return RedirectToAction("UserLogin");
                }

                ModelState.AddModelError("", "Password reset failed. Please try again.");
            }
            return View(model);
        }
        #endregion
    }
}
