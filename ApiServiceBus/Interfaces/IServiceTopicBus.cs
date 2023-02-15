namespace ApiServiceBus.Interfaces
{
    public interface IServiceTopicBus
    {
        Task SendMessageAsync(string messageBody);
    }
}
