using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using System;
using MediatR;
using System.Reflection;
using ServiceLayer.Database;
using Microsoft.EntityFrameworkCore;

namespace Playground.Web
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
            AddHangfire(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            RegisterMediatrServices(services);

            //If you want to place the database files elsewhere in your filesystem, add an absolute filepath to the connection string.
            services.AddDbContext<EventContext>(options => options.UseSqlServer("Data Source=(local);Initial Catalog=DapperExample;Integrated Security=SSPI"));
        }

        private static void RegisterMediatrServices(IServiceCollection services)
        {
            //ref: https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection

            //Scans assemblies and adds handlers, preprocessors, and postprocessors implementations to the container.
            //services.AddMediatR(typeof(MyHandler)); OR below line 
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly); //or services.AddMediatR(Assembly.GetExecutingAssembly());

            //To customize 
            //services.AddMediatR(cfg => cfg.Using<MyCustomMediator>().AsSingleton(), typeof(Startup));


            //Refer: https://github.com/jbogard/MediatR/wiki
        }

        private void AddHangfire(IServiceCollection services)
        {
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBackgroundJobClient backgroundJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            //below line to use hangfire dashboard
            app.UseHangfireDashboard("/hangfire");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
