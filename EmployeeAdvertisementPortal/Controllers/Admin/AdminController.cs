using EmployeeAdvertisementPortal.BAL.IAgent.IAdminAgent;
using EmployeeAdvertisementPortal.BAL.IAgent.IAdvertisementAgent;
using EmployeeAdvertisementPortal.Core.Constant;
using EmployeeAdvertisementPortal.Core.Utility;
using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly IAdminAgent _adminAgent;
        private readonly IAdvertisementAgent _advertisementAgent;

        #region Constructor
        public AdminController(IAdminAgent adminAgent, IAdvertisementAgent advertisementAgent)
        {
            _adminAgent = adminAgent;
            _advertisementAgent = advertisementAgent;
        }
        #endregion

        #region Public Method ApprovalList
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult ApprovalList()
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ViewBag.AdvCategoryId = _adminAgent.GetCategoryName();
            List<AdvertisementViewModel> listOfAdvertisements = _adminAgent.GetAdvertisementRequests();
            foreach (AdvertisementViewModel adv in listOfAdvertisements)
            {
                adv.CreatedByEmail = _adminAgent.GetEmailIdByEmpId(adv.EmpId);
            }
            return View("ApprovalList", listOfAdvertisements);
        }
        #endregion

        #region Public Method ApprovalList
        [HttpPost]
        public ActionResult Approve(int id)
        {
            if (_adminAgent.ApproveAdvertisement(id))
            {              
                AdvertisementViewModel advertisement =_advertisementAgent.GetAdvertisementById(id);

                if (advertisement.EmpId > 0)
                {
                    string advertiserEmail = _adminAgent.GetAdvertiserEmailByEmpId(advertisement.EmpId);
                    if (!string.IsNullOrEmpty(advertiserEmail))
                    {
                        string approveSubject = ConstantFile.ApproveSubject;
                        string approveBody = $"{ConstantFile.WelcomeMessageApproval} <br />• Your Advertisement Id : {id}<br />• Your Advertisement Title : {advertisement.Title} <br />• Your Advertisement Description : {advertisement.Description} <br />• Your Advertisement Price : {advertisement.Price}<br />• Your Advertisement PostedDate : {advertisement.PostedDate}";
                        MailUtility.SendEmail(advertiserEmail, approveSubject, approveBody);
                    }
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Approval failed." });
            }
        }
        #endregion

        #region Public Method Reject
        [HttpPost]
        public ActionResult Reject(int id)
        {
            if (_adminAgent.RejectAdvertisement(id))
            {
                AdvertisementViewModel advertisement = _advertisementAgent.GetAdvertisementById(id);

                if (advertisement.EmpId > 0)
                {
                    string advertiserEmail = _adminAgent.GetAdvertiserEmailByEmpId(advertisement.EmpId);
                    if (!string.IsNullOrEmpty(advertiserEmail))
                    {
                        string rejectSubject = ConstantFile.RejectSubject;
                        string rejectBody = $"{ConstantFile.WelcomeMessageReject} <br />• Your Advertisement Id : {id}<br />• Your Advertisement Title : {advertisement.Title} <br />• Your Advertisement Description : {advertisement.Description} <br />• Your Advertisement Price : {advertisement.Price}<br />• Your Advertisement PostedDate : {advertisement.PostedDate}";
                        MailUtility.SendEmail(advertiserEmail, rejectSubject, rejectBody);
                    }
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Rejection failed." });
            }
        }
        #endregion

        #region Public Method DeleteAdvertise
        [HttpGet]
        public ActionResult DeleteAdvertise(int id)
        {
            AdvertisementViewModel advertisement = _advertisementAgent.GetAdvertisementById(id);
            if (advertisement.EmpId > 0)
            {
                string advertiserEmail = _adminAgent.GetAdvertiserEmailByEmpId(advertisement.EmpId);
                if (!string.IsNullOrEmpty(advertiserEmail))
                {
                    string deleteSubject = ConstantFile.DeleteSubject;
                    string deleteBody = $"{ConstantFile.WelcomeMessageDelete} <br />• Your Advertisement Id : {id}<br />• Your Advertisement Title : {advertisement.Title} <br />• Your Advertisement Description : {advertisement.Description} <br />• Your Advertisement Price : {advertisement.Price}<br />• Your Advertisement PostedDate : {advertisement.PostedDate}";
                    MailUtility.SendEmail(advertiserEmail, deleteSubject, deleteBody);
                }
            }
            _adminAgent.DeleteAdvertisement(id);
            return RedirectToAction("ApprovalList", "Admin", id);
        }
        #endregion 
    }
}
