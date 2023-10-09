using Concentrix_Consumer.Data;
using Microsoft.EntityFrameworkCore;
using Concentrix_Consumer.MessageQueue;
using Concentrix_Consumer.HostedServices;


Console.WriteLine();
//CreateHostBuilder(args).Build().Run();

//Host.CreateDefaultBuilder(args)
//        .ConfigureServices((hostContext, services) =>
//        {
//            services.AddDbContext<OrderDbContext>(options =>
//                options.UseInMemoryDatabase("ASPNETCoreRabbitMQ"));
//            services.AddHostedService<ConsumerHostedService>();
//            services.AddScoped<IOrderDbContext, OrderDbContext>();
//            services.AddScoped<IMessageConsumer, RabbitMQConsumer>();
//        });





//static void ConsumeData()
//{
//    var factory = new ConnectionFactory
//    {
//        HostName = "localhost"
//    };
//    var connection = factory.CreateConnection();
//    using var channel = connection.CreateModel();
//    channel.QueueDeclare("orders", exclusive: false);

//    var consumer = new EventingBasicConsumer(channel);
//    consumer.Received += (model, eventArgs) =>
//    {
//        var body = eventArgs.Body.ToArray();
//        var message = Encoding.UTF8.GetString(body);

//        Console.WriteLine($"Message received: {message}");
//    };

//    channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);

//    Console.ReadLine();

}