namespace RabbitMq.RabbitQueue
{
    public interface IRabbitBus
    {
        Task ReceiveAsync<T>(string queue, Action<T> onMessage);
        Task SendAsync<T>(string queue, T message);
    }
}