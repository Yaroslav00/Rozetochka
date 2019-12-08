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
        public List<OrderedGoodDto> GetAllOrderedGoodsByBuyerId(int buyerId)
        {
            var orderId = GetOrCreateOrderId(buyerId);

            return OrderedGoodRepository.GetAllOrderedGoods(orderId);
        }

        public async Task<OrderedGoodDto> AddGoodsToOrdered(int goodId, int amount, int buyerId)
        {
            int orderId = GetOrCreateOrderId(buyerId);
            decimal totalPrice = await ItemRepository.GetItemPrice(goodId);

            var orderedGood = await OrderedGoodRepository.AddToOrderedGood(goodId, amount, buyerId, orderId, totalPrice);

            await OrderRepository.UpdateOrderPrice(orderId);

            return orderedGood;
        }

        public CartDto GetCart(int buyerId)
        {
            var orderId = GetOrCreateOrderId(buyerId);

            return OrderRepository.GetCart(orderId);
        }

        private int GetOrCreateOrderId(int buyerId)
        {
            int orderId;
            var orderIdUnresolved = OrderRepository.FindOrderIdIfExists(buyerId);

            if (orderIdUnresolved.Value <= 0)
            {
                orderId = OrderRepository.CreateNewOrder(buyerId);
            }
            else
            {
                orderId = orderIdUnresolved.Value;
            }

            return orderId;
        }

        public async void Checkout(int orderId)
        {
            await OrderRepository.Checkout(orderId);
        }
    }
}
