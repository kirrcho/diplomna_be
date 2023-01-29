using Diplomna.App.Messaging;
using Diplomna.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Diplomna.App.Startup))]
namespace Diplomna.App
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(configuration.GetConnectionString("ServiceBus"));
            });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<IMessageClient, MessageClient>();
        }
    }
}
