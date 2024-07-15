using EmployeeAdvertisementPortal.Core.Constant;
using EmployeeAdvertisementPortal.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeAgent _empAgent;

        #region Public Controller
        public EmployeeController(IEmployeeAgent empAgent)
        { 
           _empAgent = empAgent;
        }
        #endregion

        #region Public Method Employee List 

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            int? userId = Session["UserId"] as int?; 
            if (userId == null)
            {                                   
                return RedirectToAction("UserLogin", "Login"); 
            }
            ViewBag.RoleID = _empAgent.GetRolesList();
            List<EmployeeViewModel> listofData = _empAgent.GetAllEmployee();
            return View(listofData);
        }
        #endregion

        #region Public Method CreateEmployee
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateEmployee()
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ViewBag.RoleID = _empAgent.GetRolesList();
            EmployeeViewModel model = new EmployeeViewModel
            {
                CreatedBy = userId.Value, 
                Gender = "Male"
            };

            return View("CreateUpdateEmployee", model); 
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateEmployee(EmployeeViewModel model)
        {
            int? userId = Session["UserId"] as int?;
            ViewBag.RoleID = _empAgent.GetRolesList();
            model.CreatedDate = DateTime.Today;
            model.ModifiedDate = DateTime.Today;
            model.CreatedBy = userId.Value;
            _empAgent.CreateEmployee(model);
           
            if (userId == null)
            { 
                return RedirectToAction("UserLogin", "Login");
            }
            if (ModelState.IsValid)
            {
                if (!_empAgent.IsEmailUnique(model.Email))
                {
                    ModelState.AddModelError("Email", "Email address already exists.");

                    return View("CreateUpdateEmployee", model);
                }
                ViewBag.Message = "Data Insert Successfully";
                string subject = ConstantFile.Subject;
                string body = $"{ConstantFile.WelcomeMessageForNewEmployee}<br />• Your Username is : {model.Email} <br />• Please Reset Your Password On Mentioned Below Link <br/> <a href='https://localhost:44321/Login/ForgotPassword'> Reset Password</a>";
                MailUtility.SendEmail(model.Email, subject, body);
                return RedirectToAction("Index");    
            }
            return View("CreateUpdateEmployee", model); 
        }
        #endregion

        #region Public Method UpdateEmployee
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateEmployee(int id)
        {
           if (_empAgent.CheckEmpIdExists(id) == false)
            {
                return RedirectToAction("NotFound", "Error");
            }
            else
            {
                int? userId = Session["UserId"] as int?;
                if (userId == null)
                {
                    return RedirectToAction("Login", "UserLogin");
                }
                EmployeeViewModel model = _empAgent.GetEmployeeById(id);
                model.Role = _empAgent.GetRolesList();
                model.ModifiedBy = userId.Value;
                return View("CreateUpdateEmployee", model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateEmployee(EmployeeViewModel model)
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            model.Role = _empAgent.GetRolesList();
            EmployeeViewModel originalData = _empAgent.GetEmployeeById(model.EmpId);
            if (originalData == null)
            {
                return HttpNotFound();
            }
            model.Password = originalData.Password;
            model.CreatedDate = DateTime.Today;
            model.ModifiedDate = DateTime.Today;
            model.CreatedBy = userId.Value;
            model.ModifiedBy = userId.Value;
            _empAgent.UpdateEmployee(model);
            return RedirectToAction("Index");
        }
        #endregion

        #region Public Method DeleteEmployee
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteEmployee(int id)
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            EmployeeViewModel currentUser = _empAgent.GetEmployeeById(userId.Value);
            if (currentUser == null)
            {
                return HttpNotFound();
            }
            IEnumerable<SelectListItem> roles = _empAgent.GetRolesList();
            SelectListItem currentUserRole = roles.FirstOrDefault(r => r.Value == currentUser.RoleId.ToString());
            if (currentUserRole != null && currentUserRole.Text == "Admin" && currentUser.EmpId == id)
            {

                return RedirectToAction("Index");
            }
            EmployeeViewModel userToDelete = _empAgent.GetEmployeeById(id);
            if (userToDelete == null)
            {
                return HttpNotFound();
            }
            _empAgent.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Public Method EmployeeDetails
        public ActionResult EmployeeDetails()
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ViewBag.RoleID = _empAgent.GetRolesList();
            List<EmployeeViewModel> listofData = _empAgent.GetEmployeeDetail(userId.Value);
            EmployeeViewModel employee = listofData.FirstOrDefault();
            return View(employee);
        }
        #endregion
    }
}