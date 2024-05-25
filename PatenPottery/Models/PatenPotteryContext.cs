using Microsoft.EntityFrameworkCore;

namespace PatenPottery.Models
{
    public class PatenPotteryContext : DbContext
    {
        public DbSet<CustomerDetail> CustomerDetails { get; set; }

        public PatenPotteryContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
