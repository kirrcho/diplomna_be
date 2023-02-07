using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace Diplomna.App.Messaging
{
    public class MessageClient : IMessageClient
    {
        private ServiceBusClient _client;

        public MessageClient(AzureBusSettings settings)
        {
            _client = new ServiceBusClient(settings.ConnectionString);


        }

        public async Task SendMesssageAsync(string message, string queue)
        {
            var sender = _client.CreateSender(queue);

            try
            {
                await sender.SendMessageAsync(new ServiceBusMessage(message));
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                await sender.DisposeAsync();
                await _client.DisposeAsync();
            }
        }
    }
}
