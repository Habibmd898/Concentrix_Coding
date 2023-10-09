using Concentrix_Consumer.ConsumerServices;

namespace Concentrix_Consumer.HostedServices
{
    public class ConsumerHostedService : BackgroundService
    {
        private readonly IConsumerService _consumerService;

        public ConsumerHostedService(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             _consumerService.ReadMessgaes();
        }
    }
}
