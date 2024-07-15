using AutoMapper;
using EmployeeAdvertisementPortal.BAL.Agent.AdminAgent;
using EmployeeAdvertisementPortal.BAL.Agent.AdvertisementAgent;
using EmployeeAdvertisementPortal.BAL.IAgent.IAdminAgent;
using EmployeeAdvertisementPortal.BAL.IAgent.IAdvertisementAgent;
using EmployeeAdvertisementPortal.DAL.Data;
using EmployeeAdvertisementPortal.DAL.IService.IAdminService;
using EmployeeAdvertisementPortal.DAL.IService.IAdvertisementService;
using EmployeeAdvertisementPortal.DAL.Service.AdminService;
using EmployeeAdvertisementPortal.DAL.Service.AdvertisementService;
using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using EmployeeAdvertisementPortal.Model.InterestedViewModel;
using System.Net.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebApiModel;
using WebApiModel.EmployeeDetail;

namespace EmployeeAdvertisementPortal
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<ILoginAgent, LoginAgent>();
            container .RegisterType<ILoginService, LoginService>();
            container .RegisterType<IEmployeeAgent, EmployeeAgent>();
            container .RegisterType<IAdvertisementAgent, AdvertisementAgent>();
            container .RegisterType<IAdvertisementService, AdvertisementService>();
            container.RegisterType<IAdminAgent, AdminAgent>();
            container.RegisterType<IAdminService, AdminService>();
            container.RegisterType<HttpClient, HttpClient>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoginViewModel, LoginAPIModel>().ReverseMap();
                cfg.CreateMap<EmployeeViewModel, EmployeeDetailAPIModel>().ReverseMap();
                cfg.CreateMap<AdvertisementViewModel, APIAdvertisementDetails>().ReverseMap();
                cfg.CreateMap<InterestedViewModel, AdvertInterestMapper_tbl>().ReverseMap();

            });

            // Mapper mapper = new Mapper(config);

            // Register IMapper instance
            container.RegisterInstance(mapperConfiguration.CreateMapper());
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}