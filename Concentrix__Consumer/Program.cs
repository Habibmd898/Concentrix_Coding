// See https://aka.ms/new-console-template for more information
using Concentrix__Common.Data;
using Concentrix__Common.Repository;
using Concentrix_Consumer.ConsumerServices;
using Concentrix_Consumer.HostedServices;
using Concentrix_Consumer.MessageQueue;
using Concentrix_Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

    CreateHostBuilder(args).Build().Run();


static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            //services.AddDbContext<OrderDbContext>(options =>
            //    options.UseInMemoryDatabase("ASPNETCoreRabbitMQ"));
            services.AddHostedService<ConsumerHostedService>();
            //services.AddSingleton<IOrderDbContext, OrderDbContext>();
            services.AddCommonService();
            services.AddSingleton<IMessageConsumer, RabbitMQConsumer>();
            services.AddSingleton<IConsumerService, ConsumerService>();
            //services.AddSingleton<IOrderRepository, OrderRepository>();
        });
