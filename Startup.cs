using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace AngularApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BoonSiewContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddCors( options => {
                    options.AddPolicy(
                   "MAYUR", builder => 
                   builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCors("MAYUR");
            app.UseMvc(routes =>
            {
               routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
               routes.MapSpaFallbackRoute(
                   name: "spa-fallback",
                   defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
































// using AngularApp.Data;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.SpaServices.AngularCli;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;

// namespace AngularApp
// {
//     public class Startup
//     {
//         public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//         }

//         public IConfiguration Configuration { get; }

//         // This method gets called by the runtime. Use this method to add services to the container.
//         public void ConfigureServices(IServiceCollection services)
//         {
//             services.AddControllersWithViews();
//             // In production, the Angular files will be served from this directory
//             services.AddSpaStaticFiles(configuration =>
//             {
//                 configuration.RootPath = "ClientApp/dist";
//             });
//             services.AddDbContext<BoonSiewContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));
//             services.AddMvc();
//         }

//         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//             }
//             else
//             {
//                 app.UseExceptionHandler("/Error");
//                 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                 app.UseHsts();
//             }

//             app.UseHttpsRedirection();
//             app.UseStaticFiles();
//             if (!env.IsDevelopment())
//             {
//                 app.UseSpaStaticFiles();
//             }

//             app.UseRouting();

//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllerRoute(
//                     name: "default",
//                     pattern: "{controller}/{action=Index}/{id?}");
//             });

//             app.UseSpa(spa =>
//             {
//                 // To learn more about options for serving an Angular SPA from ASP.NET Core,
//                 // see https://go.microsoft.com/fwlink/?linkid=864501

//                 spa.Options.SourcePath = "ClientApp";

//                 if (env.IsDevelopment())
//                 {
//                     spa.UseAngularCliServer(npmScript: "start");
//                 }
//             });
//         }
//     }
// }
