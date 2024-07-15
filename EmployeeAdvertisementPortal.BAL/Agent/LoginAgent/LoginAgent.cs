using AutoMapper;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using WebApiModel;
using EmployeeAdvertisementPortal.Core.APILinks;

namespace EmployeeAdvertisementPortal
{
    public class LoginAgent : ILoginAgent
    {
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        Uri baseAddress = new Uri("http://localhost:44364");
        HttpClient client;
        public LoginAgent(IMapper mapper, ILoginService loginService)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _mapper = mapper;
            _loginService = loginService;
        }

        public async Task<bool> ValidateUser(LoginViewModel loginDetails)
        {
            try
            {
                
                LoginAPIModel LoginAPI = _mapper.Map<LoginAPIModel>(loginDetails);

               
                string jsonData = JsonConvert.SerializeObject(LoginAPI);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(APIUrl.Verification, content);
               
                if (response.IsSuccessStatusCode)
                {                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"Failed to verify user: {ex.Message}");
                return false;
            }
        }

        public int GetUserIdByEmail(string email)
        {
            return _loginService.GetUserIdByEmail(email);
        }

        public bool IsEmailUnique(string email)
        {
            return _loginService.IsEmailUnique(email);
        }

        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            if (newPassword == confirmPassword)
            {
                return _loginService.ResetPassword(email, newPassword);
            }
            return false;
        }
    }
}