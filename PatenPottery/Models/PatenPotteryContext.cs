using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PatenPottery.Models
{
    public class PatenPotteryContext : IdentityDbContext <IdentityUser>
    {
        public DbSet<CustomerDetail> CustomerDetails { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProcessFlow> ProcessFlows { get; set; }
        public DbSet<Code> Codes { get; set; }

        public PatenPotteryContext(DbContextOptions<PatenPotteryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDetail>()
            .HasKey(c => c.CustomerId);

            modelBuilder.Entity<OrderDetail>()
            .HasKey(c => c.OrderId);

            modelBuilder.Entity<ProcessFlow>()
            .HasKey(c => c.ProcessFlowID);

            modelBuilder.Entity<Code>()
            .HasKey(c => c.CodeId);

            modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.Customerdetail)
            .WithOne(c => c.OrderDetail)
            .HasForeignKey<OrderDetail>(o => o.CustomerId);


            modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.ProcessFlow)
            .WithOne(c => c.OrderDetail)
            .HasForeignKey<OrderDetail>(o => o.ProcessFlowID);


            modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.StatusCode)
            .WithOne(c => c.OrderDetail)
            .HasForeignKey<OrderDetail>(o => o.StatusCD);
            base.OnModelCreating(modelBuilder);

        }
    }
}
