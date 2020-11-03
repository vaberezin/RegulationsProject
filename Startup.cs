using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Regulations.Models;
using Regulations.Models.DatabaseContexts;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Regulations
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("Default");
            services.AddDbContext<RegulationContext>(options => 
                    options.UseMySql(Configuration.GetConnectionString("Default")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => //cookieAuthenticationOptions)
                    {
                        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");                        
                        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/AccessDenied");                      
                    });
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddMvcLocalization();
                    //.AddDataAnnotationsLocalization()                    
                    //.AddViewLocalization(); //add views localization
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
            }

            var supportedCultures = new[]
            {
                new CultureInfo("ru"),
                new CultureInfo("en"),
                new CultureInfo("de")
        };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            }) ;
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
