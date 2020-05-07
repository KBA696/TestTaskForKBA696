using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

using System.Linq;
using ТестовоеЗадание1.Model;

namespace ТестовоеЗадание1
{
    public class Startup
    {
#region К базе подключаться лень
        /// <summary>
        /// Перечень товаров
        /// </summary>
        public static List<Product> products = new List<Product>() { 
            new Product() { Name="Хонор10", Category=0, Description="Супер пупер телефон", Price=583, Rating=2 },
            new Product() { Name="Хонор11", Category=0, Description="Супер пупер телефон", Price=6343, Rating=1 },
            new Product() { Name="Хонор12", Category=0, Description="Супер пупер телефон", Price=434, Rating=5 },
            new Product() { Name="Хонор13", Category=1, Description="Супер пупер телефон", Price=6863, Rating=1 },
            new Product() { Name="Хонор14", Category=1, Description="Супер пупер телефон", Price=690, Rating=1 },
            new Product() { Name="Хонор15", Category=1, Description="Супер пупер телефон", Price=682, Rating=1 },
            new Product() { Name="Хонор16", Category=1, Description="Супер пупер телефон", Price=6189, Rating=1 },
            new Product() { Name="Хонор16", Category=2, Description="Супер пупер телефон", Price=6189, Rating=1 },
            new Product() { Name="Хонор16", Category=2, Description="Супер пупер телефон", Price=6189, Rating=1 },
            new Product() { Name="Хонор16", Category=2, Description="Супер телефон", Price=6189, Rating=1 },
            new Product() { Name="Хонор10", Category=0, Description="Супер пупер телефон", Price=583, Rating=2 },
            new Product() { Name="Хонор11", Category=0, Description="Gупер телефон", Price=6343, Rating=1 },
            new Product() { Name="Хонор12", Category=0, Description="Так себе телефон", Price=434, Rating=5 },
            new Product() { Name="Хонор10", Category=0, Description="Супер пупер телефон", Price=583, Rating=2 },
            new Product() { Name="Хонор11", Category=0, Description="Супер пупер телефон", Price=6343, Rating=1 },
            new Product() { Name="Хонор12", Category=0, Description="Супер пупер телефон", Price=434, Rating=5 },
        };

        /// <summary>
        /// Список категорий
        /// </summary>
        public static List<string> Category = new List<string>() { "смартфоны", "компьютеры", "бытовая техника" };
        
        /// <summary>
        /// максимальная цена из всех продуктов
        /// </summary>
        public static int max_price = 0;

        /// <summary>
        /// максимальный рейтинг из всех продуктов
        /// </summary>
        public static int max_rating = 0;
#endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            max_price = (int)products.Max(x => x.Price);
            max_rating = (int)products.Max(x => x.Rating);
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
                x.MemoryBufferThreshold = Int32.MaxValue;
                x.MultipartBodyLengthLimit = 60000000;
                x.MultipartBoundaryLengthLimit = int.MaxValue;
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "admin",
                    pattern: "admin",
                    defaults: new { controller = "Home", action = "Admin" });
                endpoints.MapControllerRoute(name: "admi",
                    pattern: "admi",
                    defaults: new { controller = "Home", action = "Admi" });
                endpoints.MapControllerRoute(name: "addproduct",
                    pattern: "addproduct/{Name?}/{Category?}/{Description?}/{Price?}",
                    defaults: new { controller = "Home", action = "Addproduct" });
                endpoints.MapControllerRoute(name: "time",
                    pattern: "time/{category?}/{min_price?}/{max_price?}/{min_rating?}/{max_rating?}/{sort?}",
                    defaults: new { controller = "Home", action = "Time" });
            });
        }
    }
}
