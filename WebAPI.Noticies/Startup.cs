using Application.Applications;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfacesServices;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Configs;
using Infrastructure.Repository;
using Infrastructure.Repository.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Noticies.Token;

namespace WebAPI.Noticies
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

            services.AddDbContext<Context>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<Context>();

            //Add  Interfaces and Rpository in Singleton
            services.AddSingleton(typeof(IGenerics<>), typeof (GenericRepository<>));
            services.AddSingleton<INoticies, NoticiesRepository>();
            services.AddSingleton<IUser, UserRepository>();

            // Services Domain
            services.AddSingleton<IServicesNoticies, ServiceNoticies>();

            // Application interfaces
            services.AddSingleton<IApplicationNoticies, ApplicationNoticies>();
            services.AddSingleton<IApplicationUser, ApplicationUsers>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(option =>
     {
         option.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,

             ValidIssuer = "Teste.Securiry.Bearer",
             ValidAudience = "Teste.Securiry.Bearer",
             IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
         };

         option.Events = new JwtBearerEvents
         {
             OnAuthenticationFailed = context =>
             {
                 Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                 return Task.CompletedTask;
             },
             OnTokenValidated = context =>
             {
                 Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                 return Task.CompletedTask;
             }
         };
     });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI.Noticies", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI.Noticies v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
