using eShop.Core.Contracts;
using eShop.Core.Models;
using eShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> OrderContext;
        public OrderService(IRepository<Order> OrderContext)
        {
            this.OrderContext = OrderContext;

        }
        public void CreateOrder(Order basketOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
                basketOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            OrderContext.Insert(basketOrder);
            OrderContext.Commit();
        }
    }
}
