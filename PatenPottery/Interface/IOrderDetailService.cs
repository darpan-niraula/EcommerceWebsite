using PatenPottery.Models;
using PatenPottery.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatenPottery.Interface
{
    public interface IOrderDetailService
    {
        Task AddOrderDetailAsync(OrderDetailViewModel model);
        Task<Code> Getcode(string codeValue);
        Task<Code> Getcode(int codeid);
        Task<List<Code>> GetcodeByParent(string codeParent);
        Task<List<OrderListViewModel>> GetOrders();

        Task<OrderStatusViewModel> GetOrderStatusAsync(string orderId);

        Task<OrderListViewModel> GetOrderDetails(string orderNum);

    }
}
