using EmployeeAdvertisementPortal.DAL.Data;
using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using System.Collections.Generic;

namespace EmployeeAdvertisementPortal.DAL.IService.IAdvertisementService
{
    public interface IAdvertisementService
    {
        List<AdvertisementDetails_tbl> GetApprovedList(int pageNumber, int pageSize);

        /// <summary>
        /// Method to get advertisement by it's Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AdvertisementDetails_tbl GetAdvertisementById(int id);

        /// <summary>
        /// Method to create new advertisement.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool CreateAdvertisement(AdvertisementDetails_tbl model);


        /// <summary>
        /// Method to update existing advertisement.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateAdvertisement(AdvertisementDetails_tbl model);

        /// <summary>
        /// Method to delete advertisement.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteAdvertisement(int id);

        /// <summary>
        /// Method to get advertisement category
        /// </summary>
        /// <returns></returns>
        List<AdvertisementCategory_tbl> GetAllCategory();

        /// <summary>
        /// Method to get Advertisement list
        /// </summary>
        /// <returns></returns>
        List<AdvertisementDetails_tbl> GetAllAdvertisement();

        /// <summary>
        /// Method to get Advertisement by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<AdvertisementViewModel> GetAdvertisementsByEmpId(int id);

        string GetEmailByEmpId(int EmpId);

        /// <summary>
        /// Method to check if AdvId exists in db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckAdvIdExists(int id);

        /// <summary>
        /// Method to save Interests of posts
        /// </summary>
        /// <param name="AdvId"></param>
        /// <param name="userId"></param>
        /// <param name="isInterested"></param>
        /// <returns></returns>
        bool SaveInterest(int AdvId, int userId, bool isInterested);

        /// <summary>
        /// Method to get GetInterestMappingsByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<AdvertInterestMapper_tbl> GetInterestMappingsByUserId(int userId);

        /// <summary>
        /// Methid to get Avdertisement details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AdvertisementDetails_tbl GetAdvertisementDetailsById(int id);

        /// <summary>
        /// Method to get emails by advId
        /// </summary>
        /// <param name="advId"></param>
        /// <returns></returns>
        List<string> GetEmailByAdvId(int advId);
    }
}
