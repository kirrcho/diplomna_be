using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Diplomna.App
{
    public class UserFunction
    {
        private readonly ILogger<UserFunction> _logger;

        public UserFunction(ILogger<UserFunction> log)
        {
            _logger = log;
        }

        [FunctionName("Login")]
        public void Run([ServiceBusTrigger("", "", Connection = "")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
