using System.Threading.Tasks;

namespace Diplomna.App.Messaging
{
    public interface IMessageClient
    {
        public Task SendMesssageAsync(string message, string queue);
    }
}