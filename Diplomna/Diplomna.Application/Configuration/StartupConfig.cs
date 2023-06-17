using Diplomna.Common.Constants;
using Diplomna.Common.Validators;
using Diplomna.Models;
using Diplomna.Services;
using Diplomna.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Diplomna.Application.Configuration
{
    public static class StartupConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoomsService, RoomsService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
        }

        public static void AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DiplomnaContext>(options =>
                options.UseSqlServer(configuration.GetValue<string>("DiplomnaContext")));
        }

        public static void AddCommonClasses(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            var authConstants = new AuthConstants();
            configuration.GetSection("AuthConstants").Bind(authConstants);
            services.AddSingleton(authConstants);
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                option.IgnoreObsoleteActions();
                option.IgnoreObsoleteProperties();
                option.CustomSchemaIds(type => type.FullName);
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Diplomna", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }
    }
}
