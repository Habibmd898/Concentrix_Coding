using RabbitMQ.Client.Events;
using System;
using RabbitMQ.Client;
using System.Threading.Tasks;
using Concentrix_Consumer.MessageQueue;
using System.Text;

namespace Concentrix_Consumer.ConsumerServices
{
    public interface IConsumerService
    {
        void ReadMessgaes();
    }

    public class ConsumerService : IConsumerService
    {
        private readonly IMessageConsumer _consumer;

        public ConsumerService(IMessageConsumer rabbitMqService)
        {
            _consumer = rabbitMqService;
        }
        const string _queueName = "User";
        public void ReadMessgaes()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("orders", exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message received: {message}");
            };

             channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
        }
    }
}
