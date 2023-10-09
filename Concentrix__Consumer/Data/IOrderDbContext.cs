using Microsoft.EntityFrameworkCore;

namespace Concentrix_Consumer.Data
{
    public interface IOrderDbContext
    {
        DbSet<Order> Order { get; set; }

        Task<int> SaveChangesAsync();
    }
}
