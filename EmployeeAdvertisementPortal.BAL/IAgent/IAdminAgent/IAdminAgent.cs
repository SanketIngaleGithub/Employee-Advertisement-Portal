using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal.BAL.IAgent.IAdminAgent
{
    public interface IAdminAgent
    {
        /// <summary>
        /// Method to get the list of new Advertisements to 
        /// be approved by the Admin
        /// </summary>
        /// <returns></returns>
        List<AdvertisementViewModel> GetAdvertisementRequests();

        /// <summary>
        /// Method to Approve advertisement by Admin and 
        /// modifying bool value of IsApproved in DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ApproveAdvertisement(int id);

        /// <summary>
        /// Method to Reject advertisement by Admin and 
        /// modifying bool value of IsRejected in DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool RejectAdvertisement(int id);

        /// <summary>
        /// Method to delete advertisement by admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteAdvertisement(int id);

        /// <summary>
        /// Method to get Category name from CategoryId
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetCategoryName();
        /// <summary>
        /// Method to get Employee Id by Advertisement Id
        /// </summary>
        /// <param name="advId"></param>
        /// <returns></returns>
        int? GetEmpIdByAdvId(int advId);
        /// <summary>
        /// Method to get Agvertisement By Employee Id 
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        string GetAdvertiserEmailByEmpId(int empId);
        /// <summary>
        /// Method to ger Email Id by Employee Id
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        string GetEmailIdByEmpId(int EmpId);
    }
}
