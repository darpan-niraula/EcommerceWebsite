using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatenPottery.Interface;
using PatenPottery.Models;
using PatenPottery.ViewModels;
using System.Diagnostics;

namespace PatenPottery.Controllers
{
    [Authorize]
    public class OrderDetailController : Controller
    {
        private readonly ILogger<OrderDetailController> _logger;
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(ILogger<OrderDetailController> logger, IOrderDetailService orderDetailService)
        {
            _logger = logger;
            _orderDetailService = orderDetailService;
        }

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
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}