using AutoMapper;
using EmployeeAdvertisementPortal.Core.APILinks;
using EmployeeAdvertisementPortal.Core.Constant;
using EmployeeAdvertisementPortal.DAL.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApiModel.EmployeeDetail;

namespace EmployeeAdvertisementPortal
{
    public class EmployeeAgent : IEmployeeAgent
    {
        Uri baseAddress = new Uri("http://localhost:44364/api/");
        HttpClient client;
        private readonly IMapper _mapper;
        private readonly EmployeeService _employeeService;
        public EmployeeAgent(IMapper mapper, EmployeeService employeeService)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _mapper = mapper;
            _employeeService = employeeService;
        }

        public List<EmployeeViewModel> GetAllEmployee()
        {
            HttpResponseMessage response = client.GetAsync(APIUrl.GetAllEmployee).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<EmployeeViewModel> listOfEmployee= JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);
            return listOfEmployee;
        }

        public EmployeeViewModel GetEmployeeById(int id)
        {
            EmployeeDetailAPIModel employeeAPI = new EmployeeDetailAPIModel();
            string apiUrl = $"{APIUrl.GetEmployeeById}{id}";
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                employeeAPI = JsonConvert.DeserializeObject<EmployeeDetailAPIModel>(json);
            }
            else
            {
                string errorMessage = $"{ConstantFile.errorMessage} + Status code: {response.StatusCode}";
                throw new Exception(errorMessage);
            }
            EmployeeViewModel employeeDetails = _mapper.Map<EmployeeViewModel>(employeeAPI);
            return employeeDetails;
        }
        public void CreateEmployee(EmployeeViewModel model)
        {
            EmployeeDetailAPIModel employeeAPI = _mapper.Map<EmployeeDetailAPIModel>(model);
            string jsonData = JsonConvert.SerializeObject(employeeAPI);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Task <HttpResponseMessage> response = client.PostAsync(APIUrl.CreateEmployee, content);
        }

        public void UpdateEmployee(EmployeeViewModel model)
        {
            EmployeeDetailAPIModel employeeAPI = _mapper.Map<EmployeeDetailAPIModel>(model);
            string jsonData = JsonConvert.SerializeObject(employeeAPI);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PutAsync(APIUrl.UpdateEmployee, content);
        }

        public bool DeleteEmployee(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"{APIUrl.DeleteEmployee}{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public bool IsEmailUnique(string email)
        {
            try
            {
                return _employeeService.IsEmailUnique(email);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

         public IEnumerable<SelectListItem> GetRolesList()
         {
            List<UserRole_tbl> roles = _employeeService.GetAllRoles();
            IEnumerable<SelectListItem> roleItems = roles.Select(r => new SelectListItem
            {
                Value = r.RoleId.ToString(),
                Text = r.Role
            }).ToList();
            return roleItems;
         }

        public List<EmployeeViewModel> GetEmployeeDetail(int id)
        {
            List<EmployeeDetails_tbl> employeelist = _employeeService.GetEmployeeDetail(id);
            List<EmployeeViewModel> employeeByEmpId = MapEmployeeDetailsToViewModel(employeelist);
            return employeeByEmpId;
        }

        private List<EmployeeViewModel> MapEmployeeDetailsToViewModel(List<EmployeeDetails_tbl> employeeDetails)
        {
            return employeeDetails.Select(x => new EmployeeViewModel
            {
                EmpId = x.EmpId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Gender = x.Gender,
                DOB = x.DOB,
                Address = x.Address,
                RoleId = x.RoleId,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                Password = x.Password
            }).ToList();
        }

        public bool CheckEmpIdExists(int id)
        {
            HttpResponseMessage response = client.GetAsync($"{APIUrl.CheckEmpIdExists}{id}").Result;
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}