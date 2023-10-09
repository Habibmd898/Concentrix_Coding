using Concentrix_Consumer.Data;

namespace Concentrix__Consumer.Repository
{
    public interface IOrderRepository
    {
        Task<Order> PostOrder(Order order);
        void GetOrders();
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderDbContext _context;

        public OrderRepository(IOrderDbContext context) 
        {
            _context = context;
        }
        void IOrderRepository.GetOrders()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
