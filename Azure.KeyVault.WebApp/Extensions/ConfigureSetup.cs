using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp.Extensions
{
    public static class ConfigureSetup
    {
        public static IHostBuilder AddConfigureSetup(this IHostBuilder host)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));

            return host.ConfigureAppConfiguration((context, config) =>
            {
                string Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

                
               //这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
                var settings =config.SetBasePath(context.HostingEnvironment.ContentRootPath)
               .Add(new JsonConfigurationSource { Path = Path, Optional = false, ReloadOnChange = true }).Build();
                
                /*
                 * Method one configuration["StorageConnectionString"]
                 * 
                 */
                var credential = new DefaultAzureCredential();
                config.AddAzureKeyVault(new Uri(settings["AzureKeyVault:Endpoint"]), credential);
            });
        }
    }
}
