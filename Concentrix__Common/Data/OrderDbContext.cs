#nullable disable
using Microsoft.EntityFrameworkCore;
using Concentrix__Common.Data;
using Microsoft.Extensions.Configuration;


public class OrderDbContext : DbContext, IOrderDbContext
{
    protected readonly IConfiguration Configuration;

    public OrderDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
        this.Database.EnsureCreated();
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string connectString = Configuration.GetConnectionString("WebApiDatabase");
        // in localDB database used for simplicity, change to a real db for production applications
        options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Order>()
            .HasKey(x => x.Id);
    }
 
    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }
}
