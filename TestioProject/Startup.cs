using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using TestioProject.BLL.Implementations;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;
using TestioProject.DAL.Models;

namespace TestioProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using TestioProject.BLL;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            
            services.AddTransient<IAnswersRepository, EFAnswersRepository>();
            services.AddTransient<IQuestionsRepository, EFQuestionsRepository>();
            services.AddTransient<ITestsRepository, EFTestsRepository>();
            services.AddTransient<IUsersRepository, EFUsersRepository>();
            services.AddTransient<IStatisticRepository, EFStatisticRepository>();
            services.AddTransient<IWrittenLettersRepository, EFWrittenLettersRepository>();

            services.AddScoped<DataManager>();
            //services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
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
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //Not works
            /*app.Use(async (context, next) =>
            {
                try
                {
                    var db = context.RequestServices.GetRequiredService<IUsersRepository>();
                    var email = context.User.Identity.Name;
                    string userId = db.GetIdByEmail(email);
                    bool isBaned = db.GetUserById(userId).Baned;

                    if (isBaned)
                    {
                        await context.SignOutAsync();
                    }
                    await next();
                }
                catch (Exception e)
                {
                    await next();
                }
            });*/
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
