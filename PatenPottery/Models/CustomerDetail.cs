namespace PatenPottery.Models
{
    public class CustomerDetail
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public long Number { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
