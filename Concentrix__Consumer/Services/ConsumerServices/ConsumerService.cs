using RabbitMQ.Client.Events;
using System;
using RabbitMQ.Client;
using System.Threading.Tasks;
using Concentrix_Consumer.MessageQueue;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Concentrix__Common.Repository;
using Concentrix__Common.Data;

namespace Concentrix_Consumer.ConsumerServices
{
    public interface IConsumerService
    {
        Task ReadMessgaes();
    }

    public class ConsumerService : IConsumerService
    {
        private readonly IMessageConsumer _consumer;
        private readonly IOrderRepository _orderRepository;

        public ConsumerService(IMessageConsumer rabbitMqService, IOrderRepository orderRepository)
        {
            _consumer = rabbitMqService;
            _orderRepository = orderRepository;
        }
        const string _queueName = "User";
        public async Task ReadMessgaes()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("orders", exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                var order = JsonConvert.DeserializeObject<Order>(message);
                if(order != null)
                    await _orderRepository.PostOrder(order);
                await Task.CompletedTask;
            };

             channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
            await Task.CompletedTask;

              Console.Read();
        }
    }
}
