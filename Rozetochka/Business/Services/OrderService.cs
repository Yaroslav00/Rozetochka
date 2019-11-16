using System;
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

        public async Task<OrderedGoodDto> AddGoodsToOrdered(int goodId, int amount, int buyerId)
        {
            int orderId;
            decimal totalPrice = await ItemRepository.GetItemPrice(goodId) * amount;
            int? orderIdUnresolved = await OrderedGoodRepository.FindOrderIdIfExists(buyerId);

            if (!orderIdUnresolved.HasValue)
            {
                orderId = await OrderRepository.CreateNewOrder();
            }
            else
            {
                orderId = orderIdUnresolved.Value;
            }

            var orderedGood = await OrderedGoodRepository.AddToOrderedGood(goodId, amount, buyerId, orderId, totalPrice);

            var currentTotal = await GetOrderTotalPrice(orderId);

            OrderRepository.UpdateOrderPrice(orderId,currentTotal);

            return orderedGood;
        }

        public async Task<CartDto> GetCart(int orderId)
        {
            return await OrderRepository.GetCart(orderId);
        }

        public async void Checkout(int orderId)
        {
            await OrderRepository.Checkout(orderId);
        }
    }
}
