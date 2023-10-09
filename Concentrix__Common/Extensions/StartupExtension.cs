using Concentrix__Common.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Concentrix__Common.Data;
using System.Configuration;


namespace Concentrix_Common.Extensions
{
    public static class StartupExtension
    {
        public static void AddCommonService(this IServiceCollection services)
        {

            //services.AddDbContext<OrderDbContext>(options =>
            //    options.UseInMemoryDatabase("ASPNETCoreRabbitMQ"), ServiceLifetime.Singleton);
            services.AddDbContext<OrderDbContext>();
            services.AddSingleton<IOrderDbContext, OrderDbContext>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

        }
    }
}
