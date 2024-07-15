using AutoMapper;
using EmployeeAdvertisementPortal.BAL.IAgent.IAdvertisementAgent;
using EmployeeAdvertisementPortal.Core.APILinks;
using EmployeeAdvertisementPortal.DAL.Data;
using EmployeeAdvertisementPortal.DAL.IService.IAdvertisementService;
using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal.BAL.Agent.AdvertisementAgent
{
    public class AdvertisementAgent : IAdvertisementAgent
    {
        private readonly IMapper _mapper;
        private readonly IAdvertisementService _advertisementService;
        Uri baseAddress = new Uri("http://localhost:44364");
        HttpClient client;
        public AdvertisementAgent(IMapper mapper, IAdvertisementService advertisementService)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _mapper = mapper;
            _advertisementService = advertisementService;
        }

        public List<AdvertisementViewModel> GetApprovedList(int userId, int pageNumber, int pageSize)
        {
            HttpResponseMessage response = client.GetAsync($"{APIUrl.GetApprovedList}?userId={userId}&pageNumber={pageNumber}&pageSize={pageSize}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<AdvertisementViewModel> advertisementListView = JsonConvert.DeserializeObject<List<AdvertisementViewModel>>(data);

                List<AdvertisementViewModel> transformedList = new List<AdvertisementViewModel>();
                foreach (AdvertisementViewModel item in advertisementListView)
                {
                    AdvertisementViewModel newDataMapped = new AdvertisementViewModel
                    {
                        AdvId = item.AdvId,
                        EmpId = item.EmpId,
                        Title = item.Title,
                        Description = item.Description,
                        Price = item.Price,
                        PostedDate = item.PostedDate,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        ModifiedBy = item.ModifiedBy,
                        ModifiedDate = item.ModifiedDate,
                        Location = item.Location,
                        AdvCategoryId = item.AdvCategoryId,
                        IsApproved = item.IsApproved,
                        IsRejected = item.IsRejected,
                        MediaPath = item.MediaPath,
                        Interested = item.Interested
                    };

                    transformedList.Add(newDataMapped);
                }

                return transformedList;
            }
            return new List<AdvertisementViewModel>();
        }

        public List<AdvertisementViewModel> GetAllAdvertisement()
        {
            
                HttpResponseMessage response = client.GetAsync(APIUrl.GetAllAdvertisement).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    List<AdvertisementViewModel> advertisements = JsonConvert.DeserializeObject<List<AdvertisementViewModel>>(data);
                    return advertisements;
                }
                return new List<AdvertisementViewModel>();
            
        }

        public AdvertisementViewModel GetAdvertisementById(int id)
        {
            HttpResponseMessage response = client.GetAsync($"{APIUrl.GetAdvertisementById}{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                AdvertisementViewModel advertisements = JsonConvert.DeserializeObject<AdvertisementViewModel>(data);
                return advertisements;
            }
            return new AdvertisementViewModel();
        }

        public async Task<string> CreateAdvertisement(AdvertisementViewModel model)
        {
            try
            {
                APIAdvertisementDetails advAPI = _mapper.Map<APIAdvertisementDetails>(model);
                StringContent content = new StringContent(JsonConvert.SerializeObject(advAPI), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(APIUrl.CreateAdvertisement, content);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.SerializeObject(new { Message = "Advertisement created successfully." });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Error = $"Failed to create advertisement. Status code: {response.StatusCode}" });
                }
            }
            catch (HttpRequestException e)
            {
                return JsonConvert.SerializeObject(new { Error = $"HTTP Request Exception: {e.Message}" });
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { Error = $"An error occurred: {e.Message}" });
            }
        }

        public void UpdateAdvertisement(AdvertisementViewModel model)
        {
            APIAdvertisementDetails advAPI = _mapper.Map<APIAdvertisementDetails>(model);
            StringContent content = new StringContent(JsonConvert.SerializeObject(advAPI), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(APIUrl.UpdateAdvertisement, content).Result;
        }

        public string DeleteAdvertise(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"{APIUrl.DeleteAdvertise}{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.SerializeObject(new { Message = "Advertisement deleted successfully." });
            }
            else
            {
                return JsonConvert.SerializeObject(new { Error = $"Failed to delete advertisement. Status code: {response.StatusCode}" });
            }
        }

        public IEnumerable<SelectListItem> GetCategoryList()
        {
            List<AdvertisementCategory_tbl> category = _advertisementService.GetAllCategory();
            IEnumerable<SelectListItem> categoryItems = category.Select(r => new SelectListItem
            {
                Value = r.AdvCategoryId.ToString(),
                Text = r.Category
            }).ToList();
            return categoryItems;
        }

        public List<AdvertisementViewModel> GetAdvertisementsByEmpId(int id)
        {
            return _advertisementService.GetAdvertisementsByEmpId(id);
        }

        public string GetEmailIdByEmpId(int EmpId)
        {
            return _advertisementService.GetEmailByEmpId(EmpId);
        }

        public bool CheckAdvIdExists(int id)
        {
            HttpResponseMessage response = client.GetAsync($"{APIUrl.CheckAdvIdExists}{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool SaveInterest(int AdvId, int userId, bool isInterested)
        {
            try
            {
                return _advertisementService.SaveInterest(AdvId, userId, isInterested);
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public AdvertisementViewModel GetAdvertisementDetailsById(int id)
        {
            HttpResponseMessage response = client.GetAsync($"{APIUrl.GetAdvertisementById}{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                AdvertisementViewModel advertisement = JsonConvert.DeserializeObject<AdvertisementViewModel>(data);
                return advertisement;
            }
            return null;
        }

        public List<string> GetInterestedEmails(int id)
        {
            return _advertisementService.GetEmailByAdvId(id);
        }

        public int GetInterestedEmailCount(int advId)
        {
            return _advertisementService.GetEmailByAdvId(advId).Count;
        }
    }
}