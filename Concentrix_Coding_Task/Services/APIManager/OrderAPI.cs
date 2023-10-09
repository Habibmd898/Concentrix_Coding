using Concentrix__Common.Data;
using Concentrix__Common.Modal;
using Concentrix__Common.Repository;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Concentrix_Producer.Services.APIManager
{
    public interface IOrdersAPI
    {
        Task<IEnumerable<Order>> GetOrders(PagingParams pagingParams);
    }


    public class OrderAPI : IOrdersAPI
    {
        private readonly IMemoryCache _orderCache;
        private readonly IOrderRepository _orderRepository;

        public OrderAPI(IMemoryCache OrderCache, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _orderCache = OrderCache;
        }

        public async Task<IEnumerable<Order>> GetOrders(PagingParams pagingParams)
        {
            if (_orderCache.TryGetValue(pagingParams.PageNumber, out IEnumerable<Order> orderList))
            {
                return await (Task<IEnumerable<Order>>)orderList;
            }
            var orderData = await _orderRepository.GetAllOrders(pagingParams);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);
            _orderCache.Set(pagingParams.PageNumber, orderData, cacheEntryOptions);
            return orderData;
        }
    }


}
