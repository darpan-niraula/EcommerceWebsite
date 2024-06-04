namespace PatenPottery.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public byte[]? Image { get; set; }
        public float ? Price { get; set; }
        public string? Description { get; set; }

    }

}
