using AutoMapper;
using EmployeeAdvertisementPortal;
using EmployeeAdvertisementPortal.DAL.Data;
using EmployeeAdvertisementPortal.DAL.IService.IAdvertisementService;
using EmployeeAdvertisementPortal.DAL.Service.AdminService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using WebApiModel;
using WebApiModel.EmployeeDetail;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace ApiEmployeeAdvertisementPortal.Controllers
{
    public class APIEmployeeController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IAdvertisementService _advertisementService;
        private readonly ILoginService _loginService;
        EmployeeService employeeService = new EmployeeService();
        AdminService _adminService = new AdminService();
        public APIEmployeeController()
        {
            _mapper = DependencyResolver.Current.GetService<IMapper>();
            _advertisementService = DependencyResolver.Current.GetService<IAdvertisementService>();
            _loginService = DependencyResolver.Current.GetService<ILoginService>();
        }

        #region Login API 
        [HttpPost]
        [System.Web.Mvc.Route("APIEmployee/Verification")]
        public IHttpActionResult Verification([FromBody] LoginAPIModel loginAPI)
        {
            try
            {
                EmployeeDetails_tbl employeeData = _mapper.Map<EmployeeDetails_tbl>(loginAPI);

                bool employeeEntered = _loginService.ValidateUserLoginDetails(employeeData);

                if (employeeEntered)
                {
                    return Ok(employeeEntered);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
        [HttpGet]
        public IHttpActionResult Index()
        {
            List<EmployeeDetails_tbl> employees = employeeService.GetAllEmployee();
            if (employees != null)
            {
                return Ok(employees);
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult CreateEmployee(EmployeeDetailAPIModel employeeAPI)
        {
            try
            {
                EmployeeDetails_tbl employeeData = _mapper.Map<EmployeeDetails_tbl>(employeeAPI);

                bool employeeEntered = employeeService.CreateEmployee(employeeData);

                if (employeeEntered)
                {
                    return Ok(employeeEntered);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateEmployee(EmployeeDetailAPIModel employeeAPI)
        {
            try
            {
                EmployeeDetails_tbl employeeData = _mapper.Map<EmployeeDetails_tbl>(employeeAPI);

                bool employeeEntered = employeeService.UpdateEmployee(employeeData);

                if (employeeEntered)
                {
                    return Ok(employeeEntered);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            try
            {
                employeeService.DeleteEmployee(id);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetEmployeeById(int id)
        {
            try
            {
                EmployeeDetails_tbl employee = employeeService.GetEmployeeById(id);
                EmployeeDetails_tbl employeeDTO = new EmployeeDetails_tbl();
                employeeDTO.EmpId = employee.EmpId;
                employeeDTO.FirstName = employee.FirstName;
                employeeDTO.LastName = employee.LastName;
                employeeDTO.Email = employee.Email;
                employeeDTO.Gender = employee.Gender;
                employeeDTO.DOB = employee.DOB;
                employeeDTO.Address = employee.Address;
                employeeDTO.RoleId = employee.RoleId;
                employeeDTO.UserRole_tbl = new UserRole_tbl();
                employeeDTO.UserRole_tbl.Role = employee.UserRole_tbl.Role;
                employeeDTO.Password = employee.Password;

                if (employeeDTO != null)
                {
                    return Ok(employeeDTO);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [System.Web.Mvc.Route("APIEmployee/AdvApprovalList")]
        public IHttpActionResult AdvApprovalList()
        {
            List<AdvertisementDetails_tbl> advertisementDetails = _adminService.GetAdvertisementRequests();
            if (advertisementDetails != null)
            {
                return Ok(advertisementDetails);
            }
            return NotFound();
        }

        [HttpPost]
        [System.Web.Mvc.Route("APIEmployee/Approve/{id}")]
        public IHttpActionResult Approve(int id)
        {
            if (_adminService.ApproveAdvertisement(id))
                return Ok();
            else
                return NotFound();
        }

        [HttpPost]
        [System.Web.Mvc.Route("APIEmployee/Reject/{id}")]
        public IHttpActionResult Reject(int id)
        {
            if (_adminService.RejectAdvertisement(id))
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete]
        [System.Web.Mvc.Route("APIEmployee/DeleteAdvertise/{id}")]
        public IHttpActionResult DeleteAdvertise(int id)
        {
            try
            {
                _adminService.DeleteAdvertisement(id);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [System.Web.Http.Route("APIEmployee/GetAdvertisementList")]
        public List<APIAdvertisementDetails> GetAdvertisementList()
        {
            List<AdvertisementDetails_tbl> advertisement = _advertisementService.GetAllAdvertisement();
            if (advertisement != null)
            {
                return _mapper.Map<List<APIAdvertisementDetails>>(advertisement);
            }
            return null;
        }

        [HttpPost]
        [System.Web.Http.Route("APIEmployee/CreateAdvertisement")]
        public IHttpActionResult CreateAdvertisement(APIAdvertisementDetails advertisementAPI)
        {
            try
            {
                AdvertisementDetails_tbl advData = _mapper.Map<AdvertisementDetails_tbl>(advertisementAPI);

                bool advEntered = _advertisementService.CreateAdvertisement(advData);

                if (advEntered)
                {
                    return Ok(advEntered);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [System.Web.Http.Route("APIEmployee/UpdateAdvertisement")]
        public IHttpActionResult UpdateAdvertisement(APIAdvertisementDetails advertisement)
        {
            try
            {
                AdvertisementDetails_tbl advData = _mapper.Map<AdvertisementDetails_tbl>(advertisement);

                bool advEntered = _advertisementService.UpdateAdvertisement(advData);

                if (advEntered)
                {
                    return Ok(advEntered);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [System.Web.Http.Route("APIEmployee/CheckAdvIdExists/")]
        public IHttpActionResult CheckAdvIdExists(int id)
        {
            bool checkId = _advertisementService.CheckAdvIdExists(id);
            if (checkId)
            {
                return Ok(checkId);
            }
            else
            {
                return BadRequest("Invalid Id ");
            }
        }

        [HttpDelete]
        [System.Web.Http.Route("APIEmployee/DeleteAdvertisement/{id}")]
        public IHttpActionResult DeleteAdvertisement(int id)
        {
            try
            {
                _advertisementService.DeleteAdvertisement(id);
                return Ok("Advertisement deleted successfully.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [System.Web.Http.Route("APIEmployee/GetApprovedList")]
        public List<APIAdvertisementDetails> GetApprovedList(int userId, int pageNumber, int pageSize)
        {
            List<AdvertisementDetails_tbl> advertisement = _advertisementService.GetApprovedList(pageNumber, pageSize);
            if (advertisement != null)
            {
                var adDetails = _mapper.Map<List<APIAdvertisementDetails>>(advertisement);

                var userInterests = _advertisementService.GetInterestMappingsByUserId(userId);
                if (userInterests != null)
                {
                    foreach (var ad in adDetails)
                    {
                        ad.Interested = userInterests.Any(i => i.AdvId == ad.AdvId);
                    }
                }

                return adDetails;
            }
            return null;
        }

        [HttpGet]
        [System.Web.Http.Route("APIEmployee/GetAdvertisementById/{id}")]
        public IHttpActionResult GetAdvertisementById(int id)
        {
           try
           {
                AdvertisementDetails_tbl advertisement = _advertisementService.GetAdvertisementById(id);
                AdvertisementDetails_tbl advertisementDTO = new AdvertisementDetails_tbl();
                advertisementDTO.AdvId = advertisement.AdvId;
                advertisementDTO.EmpId = advertisement.EmpId;
                advertisementDTO.Title = advertisement.Title;
                advertisementDTO.Description = advertisement.Description;
                advertisementDTO.Price = advertisement.Price;
                advertisementDTO.PostedDate = advertisement.PostedDate;
                advertisementDTO.CreatedBy = advertisement.CreatedBy;
                advertisementDTO.CreatedDate = advertisement.CreatedDate;
                advertisementDTO.ModifiedBy = advertisement.ModifiedBy;
                advertisementDTO.ModifiedDate = advertisement.ModifiedDate;
                advertisementDTO.Location = advertisement.Location;
                advertisementDTO.AdvCategoryId = advertisement.AdvCategoryId;
                advertisementDTO.AdvertisementCategory_tbl = new AdvertisementCategory_tbl();
                advertisementDTO.AdvertisementCategory_tbl.Category = advertisement.AdvertisementCategory_tbl.Category;
                advertisementDTO.IsApproved = advertisement.IsApproved;
                advertisementDTO.IsRejected = advertisement.IsRejected;
                advertisementDTO.MediaPath = advertisement.MediaPath;


                if (advertisementDTO != null)
                {
                    return Ok(advertisementDTO);
                }
                else
                {
                    return NotFound();
                }
           }
           catch (Exception ex)
           {
                throw;
           }
        }

        [HttpGet]
        public IHttpActionResult CheckEmpIdExists(int id)
        {
            bool empId = employeeService.CheckEmpIdExists(id);
            if (empId)
            {
                return Ok(empId);
            }
            else
            {
                return BadRequest();
            }
        }

        public IHttpActionResult GetAdvertisementDEtailsById(int id)
        {
            AdvertisementDetails_tbl advertisement = _advertisementService.GetAdvertisementById(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return Ok(advertisement);
        }
    }
}