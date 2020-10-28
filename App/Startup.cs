using AutoMapper;
using FitKidCateringApp.Extensions;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using FitKidCateringApp.Services.Core;
using FitKidCateringApp.Helpers;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FitKidCateringApp
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region AutoMapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region Database
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(Configuration["Database:DefaultConnection"])
            );
            #endregion

            #region Services
            services.RegisterDataServices();
            #endregion

            #region Identity
            services.AddScoped<IPasswordHasher<CoreUser>, PasswordHasher<CoreUser>>();
            services.AddIdentity<CoreUser, CoreRole>();
            #endregion

            #region Authentication
            var key = Encoding.ASCII.GetBytes(Configuration["TokenKey"]);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var provider = context.HttpContext.RequestServices;
                            var users = provider.GetService<CoreUserService>();
                            var user = users.GetCoreUser(context.Principal.Id());

                            if (user.IsAdmin)
                            {
                                context.Principal.AddPermission(StandardPermissions.AdminAccess);
                            }
                            else
                            {
                                context.Principal.AddPermission(StandardPermissions.UserAccess);
                                users.GetGlobalPermissions(user).ForEach(p => p.Value
                                    .Where(q => q.Value == PermissionState.Allow.ToString())
                                    .ForEach((q, i) =>
                                    {
                                        context.Principal.AddPermission(p.Key, q.Key);
                                    })
                                );
                            }
                        }
                    };
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext?.User ?? new ClaimsPrincipal());
            #endregion

            #region Services
            services.RegisterDataServices();
            services.RegisterSecurity();
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fit Kid Catering", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny",
                    builder => {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Database.Migrate();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAny");

            app.UseHttpsRedirection();
            app.UseMvc();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fit Kid Catering API");
            });
            #endregion
        }
    }
}
