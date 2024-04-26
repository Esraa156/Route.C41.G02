using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.Extensions;
using Route.C41.G02.PL.Helpers;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;



namespace Route.C41.G02.PL
{
    public class Startup
    {
        public IConfiguration Configuration { get; } = null;
        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddControllersWithViews();
            services.AddMvc();
            // services.AddTransient<ApplicationDbContext>();
            // services.AddScoped<ApplicationDbContext>();
            // services.AddSingleton<ApplicationDbContext>();
            //services.AddScoped<DbContextOptions<ApplicationDbContext>>();

            services.AddDbContext<ApplicationDbContext>
                (
                options =>
                {
                    options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultCollection"));
                }
                );
            services.AddApplicationServices();
			services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
			//services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			//services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			//services.AddIdentity<ApplicationUser,IdentityRole>(options =>
			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/SignIn";
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.AccessDeniedPath = "/Home/Error";
			});

			//services.AddAuthentication("Hamda");
			services.AddAuthentication(options =>
			{
				//options.DefaultAuthenticateScheme = "Identity.Application";
			}).AddCookie("Hamda", options =>
			{
				options.LoginPath = "/Account/SignIn";
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.AccessDeniedPath = "/Home/Error";
			});


			//)
			services.AddApplicationServices();
            services.AddAuthentication();
			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequiredUniqueChars = 2;
				options.Password.RequireDigit = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequiredLength = 5;
				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.User.RequireUniqueEmail = true;

			}).AddEntityFrameworkStores<ApplicationDbContext>();
			//services.AddAuthentication();
		

	}
	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
			


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
			app.UseAuthentication();


			app.UseAuthorization();
		}
    }
}