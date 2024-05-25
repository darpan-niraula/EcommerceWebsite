using PatenPottery.Models;
using PatenPottery.ViewModels;

namespace PatenPottery.Interface
{
    public interface IOrderDetailService
    {
        Task AddOrderDetailAsync(OrderDetailViewModel model);
        Task<Code> Getcode(string codeValue);
        Task<Code> Getcode(int codeid);
        Task<List<Code>> GetcodeByParent(string codeParent);
        Task<List<OrderListView>> GetOrders();
    }
}
