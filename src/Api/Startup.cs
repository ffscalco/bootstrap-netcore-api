using System;
using Api.Helpers;
using AutoMapper;
using Core;
using Core.Models;
using Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api
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
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            });
            services.AddDbContext<ImperiusDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Default")));

            ConfigureCors(services);
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Imperius", Version = "v1" }));

            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ImperiusDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(1));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imperius V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseImperiusExceptionMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(Configuration["AllowedAddresses:Default"])
                                            .AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                            .WithExposedHeaders("X-Pagination");
                    });
            });
        }
    }
}
