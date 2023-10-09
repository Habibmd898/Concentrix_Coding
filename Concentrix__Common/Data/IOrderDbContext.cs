using Microsoft.EntityFrameworkCore;

namespace Concentrix__Common.Data
{
    public interface IOrderDbContext
    {
        DbSet<Order> Orders { get; set; }

        Task<int> SaveChangesAsync();
    }
}
