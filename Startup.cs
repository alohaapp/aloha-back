using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Aloha.Controllers;
using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Contexts;
using Aloha.Model.Entities;
using Aloha.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Aloha
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddCors();

            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var key = Encoding.ASCII.GetBytes(Configuration["JwtKey"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

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

                var apiKeyScheme = new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                };

                swagger.AddSecurityDefinition("Bearer", apiKeyScheme);

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } },
                };

                swagger.AddSecurityRequirement(security);
            });

            services.AddDbContext<AlohaContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

                options.UseNpgsql(connectionString);
            });

            // Services
            services.AddScoped<ISecurityService, SecurityService>();

            // Controllers
            services.AddScoped<SecurityController, SecurityController>();
            services.AddScoped<WorkersController, WorkersController>();
            services.AddScoped<WorkstationsController, WorkstationsController>();
            services.AddScoped<FloorsController, FloorsController>();
            services.AddScoped<OfficesController, OfficesController>();

            // Mappings
            services.AddScoped<IClassMapping<User, UserDto>, UserToUserDtoMapping>();
            services.AddScoped<IClassMapping<UserDto, User>, UserDtoToUserMapping>();
            services.AddScoped<IClassMapping<Worker, WorkerDto>, WorkerToWorkerDtoMapping>();
            services.AddScoped<IClassMapping<WorkerDto, Worker>, WorkerDtoToWorkerMapping>();
            services.AddScoped<IClassMapping<Workstation, WorkstationDto>, WorkstationToWorkstationDtoMapping>();
            services.AddScoped<IClassMapping<WorkstationDto, Workstation>, WorkstationDtoToWorkstationMapping>();
            services.AddScoped<IClassMapping<Floor, FloorDto>, FloorToFloorDtoMapping>();
            services.AddScoped<IClassMapping<FloorDto, Floor>, FloorDtoToFloorMapping>();
            services.AddScoped<IClassMapping<Office, OfficeDto>, OfficeToOfficeDtoMapping>();
            services.AddScoped<IClassMapping<OfficeDto, Office>, OfficeDtoToOfficeMapping>();

            // Updaters
            services.AddScoped<IEntityUpdater<Worker>, WorkerUpdater>();
            services.AddScoped<IEntityUpdater<Workstation>, WorkstationUpdater>();
            services.AddScoped<IEntityUpdater<Floor>, FloorUpdater>();
            services.AddScoped<IEntityUpdater<Office>, OfficeUpdater>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(SwaggerConfig.EndpointUrl, SwaggerConfig.EndpointDescription));

            app.UseAuthentication();

            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AlohaContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
