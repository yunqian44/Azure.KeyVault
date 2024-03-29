using Azure.Identity;
using Azure.KeyVault.WebApp.Common;
using Azure.KeyVault.WebApp.Service;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Method 1
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddSingleton(x => new SecretClient(vaultUri: new Uri(Appsettings.app("AzureKeyVault", "Endpoint")), credential: new DefaultAzureCredential()));

            //services.AddSingleton(x => new SecretClient(new Uri(Appsettings.app("AzureKeyVault", "Endpoint")),new DefaultAzureCredential()));

            // Method 2
            //services.AddSingleton(x => new SecretClient(Configuration.GetValue<string>("AzureBlobStorageConnectionString"),));
            //services.AddSingleton(x => new SecretClient(vaultUri: new Uri(Configuration.GetValue<string>("AzureKeyVault:Endpoint")), credential: new DefaultAzureCredential()));
            services.AddScoped<ISecretsService, SecretsService>();
            services.AddScoped<IKeyVaultService, KeyVaultService>();
            services.AddControllersWithViews();
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
            }
            app.UseStaticFiles();

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
