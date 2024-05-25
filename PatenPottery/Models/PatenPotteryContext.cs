using Microsoft.EntityFrameworkCore;

namespace PatenPottery.Models
{
    public class PatenPotteryContext : DbContext
    {
        public DbSet<CustomerDetail> CustomerDetails { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProcessFlow> ProcessFlows { get; set; }
        public DbSet<Code> Codes { get; set; }

        public PatenPotteryContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configuring relationships for OrderDetail

        //    // Example: One-to-Many relationship between CustomerDetail and OrderDetail
        //    modelBuilder.Entity<OrderDetail>()
        //        .HasOne(o => o.CustomerId)
        //        .WithMany(c => c.OrderDetails)
        //        .HasForeignKey(o => o.CustomerId);

        //    // Example: One-to-Many relationship between ProcessFlow and OrderDetail
        //    modelBuilder.Entity<OrderDetail>()
        //        .HasOne(o => o.ProcessFlow)
        //        .WithMany(p => p.OrderDetails)
        //        .HasForeignKey(o => o.ProcessFlowId);

        //    // Example: One-to-One relationship between OrderDetail and Code
        //    modelBuilder.Entity<OrderDetail>()
        //        .HasOne(o => o.Code)
        //        .WithOne(c => c.OrderDetail)
        //        .HasForeignKey<OrderDetail>(o => o.CodeId);

        //    // Configure other entities...
        //}
    }
}
