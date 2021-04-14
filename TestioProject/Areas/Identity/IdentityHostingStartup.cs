using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestioProject.BLL;
using TestioProject.BLL.Implementations;
using TestioProject.BLL.Interfaces;
using TestioProject.DAL.Data;
using TestioProject.DAL.Models;

[assembly: HostingStartup(typeof(TestioProject.Areas.Identity.IdentityHostingStartup))]
namespace TestioProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TestioDbContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("TestioDbContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireUppercase = true;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = false;
                })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<TestioDbContext>();

                // services.AddTransient<Interface, Implementation>();
                //To add datamanager 
                //services.AddScoped<DataManager>();

                services.AddTransient<IAnswersRepository, EFAnswersRepository>();
                services.AddTransient<IQuestionsRepository, EFQuestionsRepository>();
                services.AddTransient<ITestsRepository, EFTestsRepository>();
                services.AddTransient<IUsersRepository, EFUsersRepository>();
                services.AddTransient<IStatisticRepository, EFStatisticRepository>();
                services.AddTransient<IRestrictedRepository, EFRestrictedRepository>();
                services.AddTransient<IUserAvatarsRepository, EFUserAvatarRepository>();
                services.AddTransient<IWrittenLettersRepository, EFWrittenLettersRepository>();

                services.AddScoped<DataManager>();

            });
        }
    }
}