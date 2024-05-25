namespace PatenPottery.Models
{
    public class Code
    {
        public int CodeId { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public int? ParentCodeId { get; set; } 
    }
}
