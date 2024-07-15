using AutoMapper;
using EmployeeAdvertisementPortal;
using EmployeeAdvertisementPortal.DAL.Data;
using EmployeeAdvertisementPortal.DAL.IService.IAdvertisementService;
using EmployeeAdvertisementPortal.DAL.Service.AdvertisementService;
using System.Net.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebApiModel;
using WebApiModel.EmployeeDetail;

namespace ApiEmployeeAdvertisementPortal
{
    public static class UnityConfig
    {

        public static void Initialize()
        {
            var container = new UnityContainer();

            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IAdvertisementService, AdvertisementService>();   

            container.RegisterInstance(new HttpClient());

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoginAPIModel, EmployeeDetails_tbl>().ReverseMap();
                cfg.CreateMap<EmployeeDetailAPIModel, EmployeeDetails_tbl>().ReverseMap();
                cfg.CreateMap<APIAdvertisementDetails, AdvertisementDetails_tbl>().ReverseMap();
            });

            // IMapper instance
            container.RegisterInstance(mapperConfiguration.CreateMapper());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));



        }
    }
}