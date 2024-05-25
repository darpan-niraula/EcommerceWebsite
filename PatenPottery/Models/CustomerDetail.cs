using System.ComponentModel.DataAnnotations;

namespace PatenPottery.Models
{
    public class CustomerDetail
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public int Number { get; set; } 

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
