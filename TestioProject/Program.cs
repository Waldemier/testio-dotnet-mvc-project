using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestioProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //For SeedData
            //var host = CreateHostBuilder(args).Build();
            //using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            //SeedData.EnsureSeedData(scope.ServiceProvider);
            //host.Run();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
