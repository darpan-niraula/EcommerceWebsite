namespace PatenPottery.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public int ProcessFlowID { get; set; }

        public string OrderNumber { get; set; }

        public byte[] Image { get; set; }

        public int StatusCD { get; set; }
        public CustomerDetail Customerdetail { get; set; }
        public ProcessFlow ProcessFlow { get; set; }
        public Code StatusCode { get; set; }
    }
}
