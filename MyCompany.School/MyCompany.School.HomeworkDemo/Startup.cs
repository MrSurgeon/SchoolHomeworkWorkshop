using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.School.HomeworkDemo.Data;
using MyCompany.School.HomeworkDemo.Security;
using System;

namespace MyCompany.School.HomeworkDemo
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDbContext<SchoolDataDbContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["SchoolDb"]));
            services.AddDbContext<SchoolIdentityDbContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["SchoolDb"]));
            services.AddIdentity<SchoolUser, SchoolRole>()
                .AddEntityFrameworkStores<SchoolIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapAreaRoute("AdminAreaRoute", "AdminAuthorities",
                                        "AdminAuthorities/{controller=Home}/{action=Index}/{id?}");
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            routeBuilder.MapRoute("Route1", "{controller=Security}/{action=Register}/{id?}");

        }
    }
}
