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

namespace BookingSundorbon.Features
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILoginRepository,LoginRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
