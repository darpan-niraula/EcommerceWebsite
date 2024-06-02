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

    public class OrderListViewModel
    {
        public OrderListViewModel(OrderDetail order)
        {
            OrderNumber = order.OrderNumber;
            Name = order.Customerdetail.Name;
            Number = Convert.ToString(order.Customerdetail.Number);
            Address = order.Customerdetail.Address;
            Status = order.StatusCode.Description;
            Image = Convert.ToBase64String(order.Image);
        }

        public OrderListViewModel(OrderListResult order)
        {
            OrderNumber = order.OrderNumber;
            Name = order.Name;
            Number = order.Number;
            Address = order.Address;
            Status = order.Status;
            Image = Convert.ToBase64String(order.Image);
        }

        public string OrderNumber { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
    }

    public class OrderListResult
    {
        public string? OrderNumber { get; set; }
        public string? Name { get; set; }
        public string Number { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public byte[]? Image { get; set; }
    }

}
