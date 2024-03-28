using Microsoft.Extensions.DependencyInjection;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;

namespace Route.C41.G02.PL.Extensions
{
    public static class ApllicationServiceExtensions
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
