using System.Threading.Tasks;
using Diplomna.Common.Constants;
using Diplomna.Common.Messaging;
using Diplomna.Common.Validators;
using Diplomna.Models;
using Diplomna.Services;
using Diplomna.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Diplomna.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

                    var authConstants = new AuthConstants();
                    configuration.Bind(nameof(AuthConstants), authConstants);

                    var azureBusSettings = new AzureBusSettings();
                    configuration.Bind(nameof(AzureBusSettings), azureBusSettings);

                    services.AddSingleton(azureBusSettings);
                    services.AddSingleton(authConstants);

                    services.AddDbContext<DiplomnaContext>(options =>
                        options.UseSqlServer(configuration.GetValue<string>("DiplomnaContext")));

                    // todo: add log conf

                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<IBarcodeService, BarcodeService>();
                    services.AddSingleton<IMessageClient, MessageClient>();
                    services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
