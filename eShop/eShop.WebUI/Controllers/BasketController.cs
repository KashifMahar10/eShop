using eShop.Core.Contracts;
using eShop.Core.Models;
using eShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;
        IOrderService OrderService;

        public BasketController(IBasketService BasketService,IOrderService orderService)
        {
            this.basketService = BasketService;
            this.OrderService = orderService;
                
        }

        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);

            return View(model);
        }

        public ActionResult AddtoBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }
        //Means No user can move towards Check out prcoess without login or get registred first that why we use Authorize.
        [Authorize]
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";

            //Process payment

            order.OrderStatus = "Payment Processed";
            OrderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);
            

            return RedirectToAction("ThankYou", new { OrderId = order.Id });
        }
    }
}