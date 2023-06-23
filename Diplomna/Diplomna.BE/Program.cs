using System.Threading.Tasks;
using Diplomna.Models;
using Diplomna.Services;
using Diplomna.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Diplomna.BE
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

                    services.AddDbContext<DiplomnaContext>(options =>
                        options.UseSqlServer(configuration.GetValue<string>("DiplomnaContext")));

                    services.AddScoped<IGroupsService, GroupsService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
