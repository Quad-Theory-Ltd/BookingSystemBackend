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
using BookingSundorbon.Features.Repositories.ParcelBookingInformationRepository;
using BookingSundorbon.Features.Repositories.PickupRepository;
using BookingSundorbon.Features.Repositories.MeasurementUnitRepository;
using BookingSundorbon.Features.Repositories.WeigthRepository;
using BookingSundorbon.Features.Repositories.VATConfigurationRepository;
using BookingSundorbon.Features.Repositories.ShipmentProcessRepository;
using BookingSundorbon.Features.Repositories.ShippingServiceRepository;
using BookingSundorbon.Features.Repositories.RoleRepository;
using BookingSundorbon.Features.Repositories.ScreenRepository;
using BookingSundorbon.Features.Repositories.WeightRepository;
using BookingSundorbon.Features.Repositories.ScreenFunctionRepository;
using BookingSundorbon.Features.Repositories.AdditionalCostRepository;
using BookingSundorbon.Features.Repositories.AgentRepository;
using BookingSundorbon.Features.Repositories.DepartmentalOperationRepository;
using BookingSundorbon.Features.Repositories.DepartmentRepository;
using BookingSundorbon.Features.Repositories.DeviceRepository;
using BookingSundorbon.Features.Repositories.DimensionRepository;
using BookingSundorbon.Features.Repositories.DiscountedOfferRepository;
using BookingSundorbon.Features.Repositories.DiscountedOfferDetailRepository;
using BookingSundorbon.Features.Repositories.ExtraPackagingRepository;
using BookingSundorbon.Features.Repositories.FunctionRepository;
using BookingSundorbon.Features.Repositories.ItemCategoryRepository;
using BookingSundorbon.Features.Repositories.ScanningPointRepository;
using BookingSundorbon.Features.Repositories.BarcodeStatusRepository;
using BookingSundorbon.Features.Repositories.BarcodeStatusDetailRepository;
using BookingSundorbon.Features.Repositories.AgentRequisitionRepository;
using BookingSundorbon.Features.Repositories.IssueRepository;
using BookingSundorbon.Features.Repositories.ReceiveRepository;
using BookingSundorbon.Features.Repositories.PaymentTypeMethodRepository;
using BookingSundorbon.Features.Repositories.AgentBoxAssignRepository;
using BookingSundorbon.Features.Repositories.ParcelStatusRepository;
using BookingSundorbon.Features.Repositories.TransportAgentRepository;
using BookingSundorbon.Features.Repositories.TransportAgentCostRepository;
using BookingSundorbon.Features.Repositories.ParcelNumbersWithBarcodeRepository;
using BookingSundorbon.Features.Repositories.AgentBoxAssignmentRepository;

namespace BookingSundorbon.Features
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            #region "A"
            services.AddScoped<IAgentBookingRepository, AgentBookingRepository>();
            services.AddScoped<IAdditionalCostRepository, AdditionalCostRepository>();
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<IAgentRequisitionRepository, AgentRequisitionRepository>();
            services.AddScoped<IAgentBoxAssignRepository, AgentBoxAssignRepository>();
            services.AddScoped<IAgentBoxAssignmentRepository, AgentBoxAssignmentRepository>();
            #endregion

            #region "B"
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IBarcodeStatusRepository, BarcodeStatusRepository>();
            services.AddScoped<IBarcodeStatusDetailRepository, BarcodeStatusDetailRepository>();
            #endregion

            #region "C"
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICargoTypeRepository, CargoTypeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            #endregion

            #region "D"
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IDepartmentalOperationRepository, DepartmentalOperationRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IDimensionRepository, DimensionRepository>();
            services.AddScoped<IDiscountedOfferRepository, DiscountedOfferRepository>();
            services.AddScoped<IDiscountedOfferDetailRepository, DiscountedOfferDetailRepository>();
            #endregion

            #region "E"
            services.AddScoped<IExtraPackagingRepository, ExtraPackagingRepository>();
            #endregion

            #region "F"
            services.AddScoped<IFunctionRepository, FunctionRepository>();
            #endregion


            #region "I"
            services.AddScoped<IItemTypeRepository, ItemTypeRepository>();
            services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            #endregion

            #region "L"
            services.AddScoped<ILoginRepository, LoginRepository>();
            #endregion
            #region "M"
            services.AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>();
            #endregion

            #region "P"
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IParcelContentRepository, ParcelContentRepository>();
            services.AddScoped<IProhibitedItemRepository, ProhibitedItemRepository>();
            services.AddScoped<IParcelBookingInformationRepository, ParcelBookingInformationRepository>();
            services.AddScoped<IPickupRepository, PickupRepository>();
            services.AddScoped<IPaymentTypeMethodRepository, PaymentTypeMethodRepository>();
            services.AddScoped<IParcelStatusRepository, ParcelStatusRepository>();
            services.AddScoped<IParcelNumbersWithBarcodeRepository, ParcelNumbersWithBarcodeRepository>();
            #endregion

            #region "R"
            services.AddScoped<IReceiverRepository, ReceiverRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IReceiveRepository, ReceiveRepository>();
            #endregion

            #region "S"
            services.AddScoped<ISenderRepository, SenderRepository>();
            services.AddScoped<IShippingServiceRepository, ShippingServiceRepository>();
            services.AddScoped<IScreenRepository, ScreenRepository>();
            services.AddScoped<IScreenFunctionRepository, ScreenFunctionRepository>();
            services.AddScoped<IScanningPointRepository, ScanningPointRepository>();
            
            #endregion
            #region "T"
            services.AddScoped<ITransitionCostRepository, TransitionCostRepository>();
            services.AddScoped<ITransportAgentRepository, TransportAgentRepository>();
            services.AddScoped<ITransportAgentCostRepository, TransportAgentCostRepository>();
            #endregion
            #region "U"
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region "V"
            services.AddScoped<IVATConfigurationRepository,VATConfigurationRepository>();
            #endregion

            #region "W"
            services.AddScoped<IWeightRepository, WeightRepository>();
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
