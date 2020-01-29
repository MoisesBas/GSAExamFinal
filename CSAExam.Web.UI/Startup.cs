using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using GSAExam.Core.Common;
using GSAExam.Infrastructure;
using FluentValidation.AspNetCore;
using GSAExam.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using CSAExam.Web.UI.Common;
using Microsoft.OpenApi.Models;
using Hangfire;
using CSAExam.Web.UI.Security;
using GSAExam.Infrastructure.EWS;
using GSAExam.Core.CQRS;
using GSAExam.Core.DomainModel.Student;
using AutoMapper;
using GSAExam.Core.Mapping;
using GSAExam.Infrastructure.DropBox;
using GSAExam.Core.Domain.Entities;
using System.Net.Http.Headers;

namespace CSAExam.Web.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDomainAutoMapper();
            services.AddApplication();        
            services.Configure<DropBoxSettings>(Configuration.GetSection("DropBoxSettings"));
            services.AddTransient(typeof(IEmailService), typeof(EmailService));
          


            services.AddInfrastructure(Configuration);
            services.AddEntityQueries<IGSADbContext, int, StudentReadModel>();
            services.AddEntitCommand<IGSADbContext, int, Students, StudentUpdateCreateModel, StudentReadModel>();
            services.AddHttpClient<IDropBoxService, DropBoxService>((provider, client) =>
            {
                client.BaseAddress = new Uri("https://www.dropbox.com");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/zip"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/zip");
            });
          
            services.AddSingleton<IHostedService, DropBoxScheduleTask>();
            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IGSADbContext>())
                .AddNewtonsoftJson();
          
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CSA Exam API", Version = "v1" });
            });

            services.AddRazorPages();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHangfireDashboard(options: new DashboardOptions
            //{
            //    Authorization = new[] { new DashboardAuthorization() }
            //});

            app.UseHttpsRedirection();
            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CSA Exam API");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {


                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(200);
                }
            });
        }
    }
}
