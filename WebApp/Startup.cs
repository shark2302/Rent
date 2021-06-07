using App.Domain.WEB;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, UnitOfWork>(serviceProvider =>
            {
                return new UnitOfWork("Server = (localdb)\\mssqllocaldb; Database = RentDb; Trusted_Connection = True;");
            });
    
            services.AddSingleton<IAddressService, AddressService>();
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IRentStoreService, RentStoreService>();
            services.AddSingleton<IProductPriceService, ProductPriceService>();
            services.AddSingleton<IManagerService, ManagerService>();
            services.AddSingleton<IRentService, RentService>();
            services.AddMvc();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
         
           app.UseEndpoints(endpoints =>
           {
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
