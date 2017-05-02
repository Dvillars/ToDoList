using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ToDoList.Models;
using Microsoft.Extensions.Configuration;

namespace ToDoList
{
    
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
                .AddDbContext<ToDoListContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var context = app.ApplicationServices.GetService<ToDoListContext>();
            AddTestData(context);

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context1) =>
            {
                await context1.Response.WriteAsync("Hello World!");
            });
        }

        private static void AddTestData(ToDoListContext context)
        {
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE Categories");

            var seedMarketingContent = new Models.Category
            {
                Name = "household"
            };
            context.Categories.Add(seedMarketingContent);

            var seedMarketingContent2 = new Models.Category
            {
                Name = "outdoors"
            };
            context.Categories.Add(seedMarketingContent2);

            var seedMarketingContent3 = new Models.Category
            {
                Name = "school"
            };
            context.Categories.Add(seedMarketingContent3);

            context.SaveChanges();
        }
    }
}
