using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal.BAL.IAgent.IAdvertisementAgent
{
    public interface IAdvertisementAgent
    {
        /// <summary>
        /// It returns the list of all Advertisement.
        /// </summary>
        /// <returns></returns>
        List<AdvertisementViewModel> GetAllAdvertisement();

        /// <summary>
        /// Method to get advertisement by it's Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AdvertisementViewModel GetAdvertisementById(int id);

        /// <summary>
        /// Method to create new Advertisement.
        /// </summary>
        /// <param name="model"></param>
        Task<string> CreateAdvertisement(AdvertisementViewModel model);

        /// <summary>
        /// Method to Update existing Advertisement.
        /// </summary>
        /// <param name="model"></param>
        void UpdateAdvertisement(AdvertisementViewModel model);

        /// <summary>
        /// Method to delete Advertisement.
        /// </summary>
        /// <param name="id"></param>
        string DeleteAdvertise(int id);

        /// <summary>
        /// Method to Get CategoryList 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetCategoryList();
        /// <summary>
        /// Method to get Advertisement By Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<AdvertisementViewModel> GetAdvertisementsByEmpId(int id);

        /// <summary>
        /// Method to get Email By Employee Id 
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        string GetEmailIdByEmpId(int EmpId);

        /// <summary>
        /// Method to check AdvId Exists in db
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
        /// Method to get lists of Advertisements approved by admin
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<AdvertisementViewModel> GetApprovedList(int userId, int pageNumber, int pageSize);

        /// <summary>
        /// Method to get emails of employees who are interested in that advertisement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<string> GetInterestedEmails(int id);

        /// <summary>
        /// Method to get count of interested employee emails 
        /// </summary>
        /// <param name="advId"></param>
        /// <returns></returns>
        int GetInterestedEmailCount(int advId);
    }
}