using Microsoft.AspNetCore.Mvc;
using PatenPottery.Interface;
using PatenPottery.Models;
using System.Diagnostics;

namespace PatenPottery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderDetailService _orderDetailService;


        public HomeController(ILogger<HomeController> logger, IOrderDetailService orderDetailService)
        {
            _logger = logger;
            _orderDetailService = orderDetailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult TrackOrder()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TrackOrder(int orderId)
        {
            _logger.LogInformation($"Tracking order with ID: {orderId}");

            var orderStatus = await _orderDetailService.GetOrderStatusAsync(orderId);
            if (orderStatus == null)
            {
                _logger.LogWarning($"Order with ID: {orderId} not found");
                var Error = "Order not found";
                return View();
            }

            var OrderNumber = orderStatus.OrderNumber;
            var StatusDescription = orderStatus.StatusDescription;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}