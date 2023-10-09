#nullable disable
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Concentrix_Coding_Task.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            properties.Headers = new Dictionary<string, object>
            {
                { "PublishDate", DateTime.Now.AddSeconds(20).ToString() }
            };

            channel.QueueDeclare("orders", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "orders", basicProperties: properties, body: body);

        }
    }
}
