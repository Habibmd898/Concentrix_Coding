namespace Concentrix_Coding_Task.RabbitMQ
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
