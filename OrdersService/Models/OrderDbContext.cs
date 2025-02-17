using Microsoft.EntityFrameworkCore;

namespace OrdersService.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Customers { get; set; }
    }
}
