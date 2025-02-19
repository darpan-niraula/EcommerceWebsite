﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatenPottery.Common;
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
                try
                {

                    await _orderDetailService.AddOrderDetailAsync(model);
                    return Json(new { success = true, message = "Successfully Saved" });
                }
                catch (Exception ex)
                {
                    // Log the exception and return an appropriate error message
                    return Json(new { success = false, message = "An error occurred while saving the order." });
                }
            }
            var message = Utility.GetModelStateErrors(ModelState);

            return Json(new { success = false, message = message });
        }

        public async Task<IActionResult> ViewOrder(string orderNum)
        {
            if (string.IsNullOrEmpty(orderNum))
            {
                return NotFound();
            }
            ViewBag.OrderStatus = await _orderDetailService.GetcodeByParent("ORDER_STATUS");
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