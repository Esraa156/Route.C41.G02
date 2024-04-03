using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;

using Route.C41.G02.PL.Helpers;

namespace Route.C41.G02.PL.Extensions
{
    public static class ApllicationServiceExtensions
    {
     
            public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            {
                services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            return services;
            }
        }
    }

