using System;
using System.Text.RegularExpressions;
using Aloha.Models.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Aloha
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(swagger =>
            {
                var info = new Info()
                {
                    Title = SwaggerConfig.DocInfoTitle,
                    Version = SwaggerConfig.DocInfoVersion,
                    Description = SwaggerConfig.DocInfoDescription,
                    Contact = new Contact() { Name = SwaggerConfig.ContactName, Url = SwaggerConfig.ContactUrl }
                };

                swagger.SwaggerDoc(SwaggerConfig.DocNameV1, info);
            });

            services.AddDbContext<AlohaContext>(options => {
                string connectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb");
                string portNumber = Regex.Match(connectionString, @"(?<=Data Source.+:)\d+")?.Value;

                connectionString += ";Port=" + portNumber;
                connectionString = connectionString.Replace(":" + portNumber, "");

                options.UseMySql(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(SwaggerConfig.EndpointUrl, SwaggerConfig.EndpointDescription));

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
