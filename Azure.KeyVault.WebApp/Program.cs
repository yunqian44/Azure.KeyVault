using Azure.Identity;
using Azure.KeyVault.WebApp.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // 创建可用于解析作用域服务的新 Microsoft.Extensions.DependencyInjection.IServiceScope。

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var env = services.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    try
                    {
                        // 从 system.IServicec提供程序获取 T 类型的服务。
                        var configuration = services.GetRequiredService<Appsettings>();
                        
                    }
                    catch (Exception e)
                    {
                        var logger = loggerFactory.CreateLogger<Program>();
                        logger.LogError(e, "Error occured seeding the Database.");
                    }
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, service) =>
            {
                //service.AddSingleton(new Appsettings(context.HostingEnvironment.ContentRootPath));
            })
            .ConfigureAppConfiguration((context, config) =>
                {

                    var settings = config.Build();
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        var keyVaultEndpoint = Appsettings.app("AzureKeyVault", "Endpoint");
                    }
                    else if (context.HostingEnvironment.IsStaging())
                    {
                        var keyVaultEndpoint = Appsettings.app("AzureKeyVault", "Endpoint");
                    }
                    else if (context.HostingEnvironment.IsProduction())
                    {
                        var keyVaultEndpoint = Appsettings.app("AzureKeyVault", "Endpoint");
                    }

                    /*
                     * Method one configuration["StorageConnectionString"]
                     * 
                     */
                    var credential = new DefaultAzureCredential();
                    config.AddAzureKeyVault(new Uri("https://mykv.vault.azure.net/"), credential);

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseUrls("https://*:9004")
                    .UseStartup<Startup>();
                });
    }
}
