namespace PatenPottery.Models
{
    public class ProcessFlow
    {
        public int ProcessFlowID { get; set; }

        public bool DoFire { get; set; }

        public bool DoGlaze { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
