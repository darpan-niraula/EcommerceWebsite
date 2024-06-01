using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatenPottery.Interface;
using PatenPottery.Models;
using PatenPottery.ViewModels;
using System.Diagnostics;

namespace PatenPottery.Controllers
{
    
    public class OrderDetailController : Controller
    {
        private readonly ILogger<OrderDetailController> _logger;
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(ILogger<OrderDetailController> logger, IOrderDetailService orderDetailService)
        {
            _logger = logger;
            _orderDetailService = orderDetailService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var orderList = await _orderDetailService.GetOrders();
            return View(orderList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _orderDetailService.AddOrderDetailAsync(model);
                return RedirectToAction(nameof(Create));
            }
            return View(model);
        }

        public async Task<IActionResult> ViewOrder(string orderNum)
        {
            if (string.IsNullOrEmpty(orderNum))
            {
                return NotFound();
            }

            var order = await _orderDetailService.GetOrderDetails(orderNum);

            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromBody] string newStatus)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderDetailService.UpdateStatusAsync(newStatus);
                if (result)
                {
                    return Ok(newStatus);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}