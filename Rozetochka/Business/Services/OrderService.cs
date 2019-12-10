using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
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
            var orderId = GetOrCreateOrderId(buyerId, false);

            return OrderedGoodRepository.GetAllOrderedGoods(orderId);
        }

        public async Task<OrderedGoodDto> AddGoodsToOrdered(int goodId, int amount, int buyerId)
        {
            int orderId = GetOrCreateOrderId(buyerId, false);
            decimal totalPrice = await ItemRepository.GetItemPrice(goodId);

            var orderedGood = await OrderedGoodRepository.AddToOrderedGood(goodId, amount, buyerId, orderId, totalPrice);

            await OrderRepository.UpdateOrderPrice(orderId);

            return orderedGood;
        }

        public async Task DeleteGoodFromOrder(int goodId, int orderId)
        {
            await OrderedGoodRepository.DeleteGoodFromOrder(orderId, goodId);
            await OrderRepository.UpdateOrderPrice(orderId);
        }

        public List<CartDto> GetCart(int buyerId)
        {
            var orderId = GetOrCreateOrderIds(buyerId);

            return OrderRepository.GetCartsHistory(orderId);
        }

        private List<int> GetOrCreateOrderIds(int buyerId)
        {
            return OrderRepository.FindAllOrderIds(buyerId);
        }

        private int GetOrCreateOrderId(int buyerId, bool takeCheckouted)
        {
            int orderId;
            var orderIdUnresolved = OrderRepository.FindOrderIdIfExists(buyerId, takeCheckouted);

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

        public decimal SumCart(List<OrderedGoodDto> goods)
        {
            decimal sum = 0;
            goods.ForEach(i => { sum += i.Amount * i.CurrentPrice; });

            return sum;
        }

        public async Task Checkout(int userId)
        {
            int? orderId = OrderRepository.FindOrderIdIfExists(userId, false);

            if(orderId.HasValue)
                await OrderRepository.Checkout(orderId.Value);
        }
    }
}
