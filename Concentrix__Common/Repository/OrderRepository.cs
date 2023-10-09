using Concentrix__Common.Data;
using Concentrix__Common.Modal;
using System.Collections.Concurrent;

namespace Concentrix__Common.Repository
{
    public interface IOrderRepository
    {
        Task<Order> PostOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrders(PagingParams pagingParams);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderDbContext _context;
        //In future we can utilize this
        private ConcurrentBag<Order> orderList = new();

        public OrderRepository(IOrderDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllOrders(PagingParams pagingParams)
        {
            //int totalItems = _context.Orders.Count();
            int pageNumber = pagingParams.PageNumber;
            int pageSize = pagingParams.PageSize;

            return _context.Orders.Skip(pageSize * (pageNumber - 1))
                         .Take(pageSize)
                         .ToList();
        }

        public async Task<Order> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
