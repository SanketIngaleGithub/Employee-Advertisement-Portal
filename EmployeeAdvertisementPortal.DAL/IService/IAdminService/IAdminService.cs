using EmployeeAdvertisementPortal.DAL.Data;
using System.Collections.Generic;

namespace EmployeeAdvertisementPortal.DAL.IService.IAdminService
{
    public interface IAdminService
    {
        /// <summary>
        /// Method to get the list of new Advertisements to 
        /// be approved by the Admin
        /// </summary>
        /// <returns></returns>
        List<AdvertisementDetails_tbl> GetAdvertisementRequests();

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
        List<AdvertisementCategory_tbl> GetCategoryName();

        /// <summary>
        /// Method to get EmpId by advId
        /// </summary>
        /// <param name="advId"></param>
        /// <returns></returns>
        int? GetEmpIdByAdvId(int advId);

        /// <summary>
        /// Method to get Email using empId
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        string GetEmailByEmpId(int empId);

        /// <summary>
        /// Method to get Title by advId
        /// </summary>
        /// <param name="advId"></param>
        /// <returns></returns>
        string GetTitleByAdvId(int advId);

        /// <summary>
        /// Method to get price by AdvId
        /// </summary>
        /// <param name="advId"></param>
        /// <returns></returns>
        int GetPriceByAdvId(int advId);
    }
}
