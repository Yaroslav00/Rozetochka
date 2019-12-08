using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess;
using DataAccess.Dto;
using DataAccess.Repository;

namespace Business.Services
{
    public class OrderService: IOrderService
    {
        public async Task<decimal> GetOrderTotalPrice(int orderId)
        {
          return await OrderRepository.GetOrderTotalPrice(orderId);
        }

        public List<OrderedGoodDto> GetAllOrderedGoodsByBuyerId(int buyerId)
        {
            var orderId = OrderRepository.FindOrderIdIfExists(buyerId);
            if (orderId.HasValue)
            {
                return OrderedGoodRepository.GetAllOrderedGoods(orderId.Value);
            }

            return new List<OrderedGoodDto>();
        }

        public async Task<OrderedGoodDto> AddGoodsToOrdered(int goodId, int amount, int buyerId)
        {
            int orderId;
            decimal totalPrice = await ItemRepository.GetItemPrice(goodId);

            int? orderIdUnresolved = OrderRepository.FindOrderIdIfExists(buyerId);

            if (orderIdUnresolved.Value <= 0)
            {
                orderId = OrderRepository.CreateNewOrderAsync(buyerId);
            }
            else
            {
                orderId = orderIdUnresolved.Value;
            }

            var orderedGood = await OrderedGoodRepository.AddToOrderedGood(goodId, amount, buyerId, orderId, totalPrice);

            await OrderRepository.UpdateOrderPrice(orderId,orderedGood.CurrentPrice * orderedGood.Amount);

            return orderedGood;
        }

        public CartDto GetCart(int buyerId)
        {
            int orderId;
            var orderIdUnresolved = OrderRepository.FindOrderIdIfExists(buyerId);

            if (orderIdUnresolved.Value <= 0)
            {
                orderId = OrderRepository.CreateNewOrderAsync(buyerId);
            }
            else
            {
                orderId = orderIdUnresolved.Value;
            }

            return OrderRepository.GetCart(orderId);
        }

        public async void Checkout(int orderId)
        {
            await OrderRepository.Checkout(orderId);
        }
    }
}
