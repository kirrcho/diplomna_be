namespace Diplomna.Common.Messaging
{
    public interface IMessageClient
    {
        public Task SendMesssageAsync(string message, string queue);
    }
}