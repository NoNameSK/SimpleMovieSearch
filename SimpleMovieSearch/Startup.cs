using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SimpleMovieSearch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMovieSearch.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SimpleMovieSearch
{
    public class Startup
    {
        private IConfigurationRoot _confsting;

        public Startup(IWebHostEnvironment hostEnv)
        {
            _confsting = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Data.AppDBContext>(options => options.UseSqlServer(_confsting.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSession();
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = new PathString("/Account/Login");
                });
            services.AddControllersWithViews();

            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();

            app.UseAuthentication();    
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "categoryFilter", template: "Video/{action}/{category?}", defaults: new { Controller = "Car", action = "List" });
            });


        }
    }
}
