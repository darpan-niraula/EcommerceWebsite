using PatenPottery.Common;
using PatenPottery.Models;

namespace PatenPottery.ViewModels
{
    public class OrderDetailViewModel
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public long Number { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
        public bool DoFire { get; set; }

        public bool DoGlaze { get; set; }

        public OrderCreate ToorderDetail()
        {
            var utility = new Utility();
            var orderDetail = new OrderDetail
            {
                Image = Image == null ? null : utility.ConvertToByteArray(Image)
            };
            var customerDetail = new CustomerDetail
            {
                Name = Name,
                Email = Email ?? "",
                Address = Address ?? "",
                Number = Number,
            };
            var processFlow = new ProcessFlow
            {
                DoFire = DoFire,
                DoGlaze = DoGlaze,
            };

            var orderCreate = new OrderCreate
            {
                customerDetail = customerDetail,
                processFlow = processFlow,
                orderDetail = orderDetail
            };
            return orderCreate;
        }


    }
    public class OrderCreate
    {
        public OrderDetail orderDetail { get; set; }
        public CustomerDetail customerDetail { get; set; }
        public ProcessFlow processFlow { get; set; }
    }

    public class OrderListView
    {
        public OrderListView(OrderDetail order)
        {
            OrderNumber = order.OrderNumber;
            Name = order.Customerdetail.Name;
            Number = order.Customerdetail.Number;
            Address = order.Customerdetail.Address;
            Status = order.StatusCode.Description;
        }

        public string OrderNumber { get; set; }
        public string Name { get; set; }
        public long Number { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
