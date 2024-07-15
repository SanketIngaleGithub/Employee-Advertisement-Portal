using EmployeeAdvertisementPortal.BAL.IAgent.IAdvertisementAgent;
using EmployeeAdvertisementPortal.Helpers;
using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal.Controllers.Advertisement
{
    [Authorize(Roles = "Admin,Employee")]
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementAgent _advertisementAgent; 

        #region Public Constructor
        public AdvertisementController(IAdvertisementAgent advertisementAgent)
        {
            _advertisementAgent = advertisementAgent;
        }
        #endregion

        #region Public Method ApprovedAdvertisement
        [HttpGet]
        public ActionResult ApprovedAdvertisementList( AdvertisementViewModel model, int pageNumber = 0, int pageSize = 5)
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            List<AdvertisementViewModel> advertisements = _advertisementAgent.GetApprovedList(userId.Value,  pageNumber ,  pageSize );
            foreach (AdvertisementViewModel adv in advertisements)
            {
                adv.CreatedByEmail = _advertisementAgent.GetEmailIdByEmpId(adv.EmpId);
            }
            ViewBag.AdvCategoryId = _advertisementAgent.GetCategoryList();
            return View("ApprovedAdvertisementList", advertisements);
        }
        #endregion Public Method ApprovedAdvertisement

        #region Public Method AdvertisementList
        [HttpGet]
        public ActionResult AdvertisementList()
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ViewBag.AdvCategoryId = _advertisementAgent.GetCategoryList();
            List<AdvertisementViewModel> listofData = _advertisementAgent.GetAdvertisementsByEmpId(userId.Value);
            foreach (var advertisement in listofData)
            {
                advertisement.EmailCount = _advertisementAgent.GetInterestedEmailCount(advertisement.AdvId);
            }
            return View("AdvertisementList", listofData);
        }
        #endregion Public Method AdvertisementList

        #region Public Method CreateAdvertisement
        [HttpGet]
        public ActionResult CreateAdvertisement()
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            ViewBag.AdvCategoryId = _advertisementAgent.GetCategoryList();
            AdvertisementViewModel model = new AdvertisementViewModel
            {
                CreatedBy = userId.Value,
                EmpId = userId.Value,
                Category = _advertisementAgent.GetCategoryList()
            };

            return View("CreateUpdateAdvertisement", model);
        }

        [HttpPost]
        public ActionResult CreateAdvertisement(AdvertisementViewModel model)
        {
            int? userId = Session["UserId"] as int?;
            ViewBag.AdvCategoryId = _advertisementAgent.GetCategoryList();
            model.CreatedDate = DateTime.Now;
            model.PostedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.CreatedBy = userId.Value;
            model.EmpId = userId.Value;
            FileHelper.SaveAdvertisementFile(model, Server.MapPath("~/Content/Advertisement/"));
            _advertisementAgent.CreateAdvertisement(model);

            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Data Insert Successfully";
                return RedirectToAction("AdvertisementList");
            }

            return View("CreateUpdateAdvertisement", model);
        }

        #endregion

        #region Public Method UpdateAdvertisement
        [HttpGet]
        public ActionResult UpdateAdvertisement(int id)
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null || !_advertisementAgent.CheckAdvIdExists(id))
            {
                return RedirectToAction("UserLogin", "Login");
            }

            AdvertisementViewModel model = _advertisementAgent.GetAdvertisementById(id);

            if (model == null || model.EmpId != userId.Value)
            {
                return RedirectToAction("NotFound", "Error");
            }
            model.ModifiedBy = userId.Value;
            model.Category = _advertisementAgent.GetCategoryList();
            
            return View("CreateUpdateAdvertisement", model);
        }

        [HttpPost]
        public ActionResult UpdateAdvertisement(AdvertisementViewModel model, HttpPostedFileBase AdvertisementFile)
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ViewBag.AdvCategoryId = _advertisementAgent.GetCategoryList();
            if (ModelState.IsValid)
            {
                if (AdvertisementFile != null)
                {
                    string fileName = Path.GetFileName(AdvertisementFile.FileName);
                    string filePath = "~/Content/Advertisement/" + fileName;
                    AdvertisementFile.SaveAs(Server.MapPath(filePath));
                    model.MediaPath = filePath;
                }

                model.EmpId = userId.Value;
                model.CreatedDate = DateTime.Now;
                model.PostedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.CreatedBy = userId.Value;
                model.ModifiedBy = userId.Value;

                AdvertisementViewModel existingAd = _advertisementAgent.GetAdvertisementById(model.AdvId);
                if (existingAd == null || existingAd.EmpId != userId.Value)
                {
                    return HttpNotFound();
                }

                _advertisementAgent.UpdateAdvertisement(model);

                return RedirectToAction("AdvertisementList");
            }
            return View("CreateUpdateAdvertisement", model);
        }

        #endregion

        #region Public Method DeleteAdvertisement
        public ActionResult DeleteAdvertisement(int id)
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            AdvertisementViewModel data = _advertisementAgent.GetAdvertisementById(id);
            if (data == null || data.EmpId != userId.Value)
            {
                return RedirectToAction("NotFound", "Error");
            }

            _advertisementAgent.DeleteAdvertise(id);
            return RedirectToAction("AdvertisementList");
        }
        #endregion

        #region Public Method SaveInterests
        [HttpPost]
        public ActionResult SaveInterest(int AdvId, bool isInterested)
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            bool success = _advertisementAgent.SaveInterest(AdvId, userId.Value, isInterested);
            if (success)
            {
                ViewBag.Message = isInterested ? "Interest saved successfully." : "Not Interested saved successfully.";
            }
            else
            {
                ViewBag.ErrorMessage = isInterested ? "Failed to save interest." : "Failed to save Not Interested.";
            }
            return RedirectToAction("ApprovedAdvertisementList");
        }
        #endregion

        #region Public Method AdvertisementDetails
        [HttpGet]

        public ActionResult AdvertisementDetails(int id)
        {
            int? userId = Session["UserId"] as int?;
            if (userId == null) 
            {
                return RedirectToAction("UserLogin", "Login");
            }
            AdvertisementViewModel advertisement = _advertisementAgent.GetAdvertisementById(id);
            if (advertisement == null || advertisement.EmpId != userId.Value)
            {
                return RedirectToAction("NotFound", "Error");
            }
            return View(advertisement);
        }
        #endregion

        #region Public Method GetInterestedEmails
        [HttpGet]
        public ActionResult GetInterestedEmails(int id)
        {
            List<string> model = _advertisementAgent.GetInterestedEmails(id);
            return PartialView("GetInterestedEmails", model);
        }
        #endregion
    }
}