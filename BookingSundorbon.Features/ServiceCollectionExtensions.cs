using BookingSundorbon.Features.Repositories.LoginRepository;
using BookingSundorbon.Features.Repositories.UnitOfWorkReporsitory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Features.Services.EmailService;
using BookingSundorbon.Features.Repositories.CompanyRepository;
using BookingSundorbon.Features.Repositories.BranchRepository;
using BookingSundorbon.Features.Repositories.CityRepository;
using BookingSundorbon.Features.Repositories.DistrictRepository;
using BookingSundorbon.Features.Repositories.ReceiverRepository;
using BookingSundorbon.Features.Repositories.SenderRepository;
using BookingSundorbon.Features.Repositories.ProductTypeRepository;
using BookingSundorbon.Features.Repositories.ParcelContentRepository;
using BookingSundorbon.Features.Repositories.ProhibitedItemRepository;
using BookingSundorbon.Features.Repositories.GetTransitionCostRepository;
using BookingSundorbon.Features.Repositories.RouteRepository;
using BookingSundorbon.Features.Repositories.ItemTypeRepository;
using BookingSundorbon.Features.Repositories.CargoTypeRepository;
using BookingSundorbon.Features.Repositories.CountryRepository;
using BookingSundorbon.Features.Repositories.AgentBookingRepository;

namespace BookingSundorbon.Features
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            #region "A"
            services.AddScoped<IAgentBookingRepository, AgentBookingRepository>();
            #endregion

            #region "B"
            services.AddScoped<IBranchRepository, BranchRepository>();
            #endregion

            #region "C"
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICargoTypeRepository, CargoTypeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            #endregion

            #region "D"
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            #endregion

            #region "I"
            services.AddScoped<IItemTypeRepository, ItemTypeRepository>();
            #endregion

            #region "L"
            services.AddScoped<ILoginRepository, LoginRepository>();
            #endregion

            #region "P"
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IParcelContentRepository, ParcelContentRepository>();
            services.AddScoped<IProhibitedItemRepository, ProhibitedItemRepository>();
            #endregion

            #region "R"
            services.AddScoped<IReceiverRepository, ReceiverRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            #endregion

            #region "S"
            services.AddScoped<ISenderRepository, SenderRepository>();
            #endregion
            #region "T"
            services.AddScoped<ITransitionCostRepository, TransitionCostRepository>();
            #endregion
            #region "U"
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion



            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
