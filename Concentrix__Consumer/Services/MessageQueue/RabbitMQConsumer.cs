#nullable disable
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Concentrix_Consumer.MessageQueue
{
    public class RabbitMQConsumer : IMessageConsumer
    {
        public void ReceiveMessage()
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
                byte[] publishDateHeader = (byte[])eventArgs.BasicProperties.Headers["PublishDate"];
                DateTime publishDate = Convert.ToDateTime(Encoding.UTF8.GetString(publishDateHeader));
                //Assuming we are geeting the date difference and replacing the 1000 with datediff
                Thread.Sleep(10000);
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message received: {message}");
            };

            channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
            Console.Read();
        }
    }
}

