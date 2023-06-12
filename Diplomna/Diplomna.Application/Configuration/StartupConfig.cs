using Diplomna.Common.Constants;
using Diplomna.Common.Validators;
using Diplomna.Models;
using Diplomna.Services;
using Diplomna.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Diplomna.Application.Configuration
{
    public static class StartupConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DiplomnaContext>(options =>
                options.UseSqlServer(configuration.GetValue<string>("DiplomnaContext")));
        }

        public static void AddCommonClasses(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            services.AddTransient<AuthConstants>();
        }
    }
}
