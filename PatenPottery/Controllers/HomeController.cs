using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatenPottery.Interface;
using PatenPottery.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PatenPottery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderDetailService _orderDetailService;
        private readonly PatenPotteryContext _patentContext;

        public HomeController(ILogger<HomeController> logger, IOrderDetailService orderDetailService, PatenPotteryContext patentContext)
        {
            _logger = logger;
            _orderDetailService = orderDetailService;
            _patentContext = patentContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            var services = _patentContext.Services.ToList();
            return View(services);
        }

        public IActionResult TrackOrder()
        {
            return View();
        }

        public IActionResult Products()
        {
            var products = _patentContext.Products.ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TrackOrder(string orderId)
        {
            _logger.LogInformation($"Tracking order with ID: {orderId}");

            var orderStatus = await _orderDetailService.GetOrderStatusAsync(orderId);
            if (orderStatus == null)
            {
                _logger.LogWarning($"Order with ID: {orderId} not found");
                return Json(new { success = false, message = "Order not found" });
            }

            return Json(new { success = true, orderNumber = orderStatus.OrderNumber, statusDescription = orderStatus.StatusDescription, image = orderStatus.Image });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
