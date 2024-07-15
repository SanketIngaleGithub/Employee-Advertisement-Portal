using AutoMapper;
using EmployeeAdvertisementPortal.BAL.IAgent.IAdminAgent;
using EmployeeAdvertisementPortal.Core.APILinks;
using EmployeeAdvertisementPortal.DAL.Data;
using EmployeeAdvertisementPortal.DAL.IService.IAdminService;
using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal.BAL.Agent.AdminAgent
{
    public class AdminAgent : IAdminAgent
    {
        public Employee_Advertisement_PortalEntity db = new Employee_Advertisement_PortalEntity();

        Uri baseAddress = new Uri("https://localhost:44364/api/");
        HttpClient client;
        private readonly IMapper _mapper;
        private readonly IAdminService _adminService;
        public AdminAgent(IMapper mapper, IAdminService adminService)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _mapper = mapper;
            _adminService = adminService;
        }

        public List<AdvertisementViewModel> GetAdvertisementRequests()
        {
            HttpResponseMessage response = client.GetAsync(APIUrl.GetAdvertisementRequests).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<AdvertisementViewModel> result = JsonConvert.DeserializeObject<List<AdvertisementViewModel>>(data);
            return result;
        }

        public bool ApproveAdvertisement(int id)
        {
            HttpResponseMessage response = client.PostAsync($"{APIUrl.ApproveAdvertisement}{id}", null).Result;
            return response.IsSuccessStatusCode;
        }

        public bool RejectAdvertisement(int id)
        {
            HttpResponseMessage response = client.PostAsync($"{APIUrl.RejectAdvertisement}{id}", null).Result;
            return response.IsSuccessStatusCode;
        }

        public bool DeleteAdvertisement(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"{APIUrl.DeleteAdvertisement}{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public IEnumerable<SelectListItem> GetCategoryName()
        {
            List<AdvertisementCategory_tbl> category = _adminService.GetCategoryName();
            IEnumerable<SelectListItem> categoryItems = category.Select(r => new SelectListItem
            {
                Value = r.AdvCategoryId.ToString(),
                Text = r.Category
            }).ToList();
            return categoryItems;
        }

        public int? GetEmpIdByAdvId(int advId)
        {
            return _adminService.GetEmpIdByAdvId(advId);
        }

        public string GetAdvertiserEmailByEmpId(int empId)
        {
            return _adminService.GetEmailByEmpId(empId);
        }

        public string GetEmailIdByEmpId(int EmpId)
        {
            return _adminService.GetEmailByEmpId(EmpId);
        }
    }
}