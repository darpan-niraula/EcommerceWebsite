using Microsoft.EntityFrameworkCore;
using PatenPottery.Interface;
using PatenPottery.Models;
using PatenPottery.ViewModels;

namespace PatenPottery.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly PatenPotteryContext _context;

        public OrderDetailService(PatenPotteryContext context)
        {
            _context = context;
        }

        public async Task<Code> Getcode(string codeValue)
        {
            var code = await _context.Codes.Where(a => a.Value == codeValue).FirstAsync();
            return code;
        }

        public async Task<Code> Getcode(int codeid)
        {
            var code = await _context.Codes.Where(a => a.CodeId == codeid).FirstAsync();
            return code;
        }


        public async Task<List<Code>> GetcodeByParent(string codeParent)
        {
            var parentCode = await _context.Codes.Where(a => a.Value == codeParent).FirstAsync();
            var codes = await _context.Codes.Where(a => a.ParentCodeId == parentCode.CodeId).ToListAsync();
            return codes;
        }

        public async Task<List<OrderListView>> GetOrders()
        {
            var orders = await _context.OrderDetails
                .Include(a => a.Customerdetail)
                .Include(a => a.ProcessFlow)
                .Include(a => a.StatusCode).Select(a => new OrderListView(a))
                .ToListAsync();
            return orders;
        }

        public async Task AddOrderDetailAsync(OrderDetailViewModel model)
        {
            var orderCreate = model.ToorderDetail();
            var okayFlag = false;
            using var transaction = _context.Database.BeginTransaction();
            {
                try
                {
                    _context.Add(orderCreate.customerDetail);
                    _context.Add(orderCreate.processFlow);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    okayFlag = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    okayFlag = false;
                }
            }
            if (okayFlag)
            {
                using var transaction1 = _context.Database.BeginTransaction();
                {
                    try
                    {
                        orderCreate.orderDetail.ProcessFlowID = orderCreate.processFlow.ProcessFlowID;
                        orderCreate.orderDetail.CustomerId = orderCreate.customerDetail.CustomerId;
                        orderCreate.orderDetail.StatusCD = (await Getcode("ORDER_RECEIVED")).CodeId;
                        orderCreate.orderDetail.OrderNumber = Guid.NewGuid().ToString();
                        _context.Add(orderCreate.orderDetail);
                        await _context.SaveChangesAsync();
                        transaction1.Commit();
                    }
                    catch (Exception ex)
                    {
                        _context.CustomerDetails.Remove(orderCreate.customerDetail);
                        _context.ProcessFlows.Remove(orderCreate.processFlow);
                        await _context.SaveChangesAsync();
                        transaction1.Rollback();
                    }
                }
            }

        }
    }
}
