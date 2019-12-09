using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Dto;

namespace Business.Interfaces
{
    public interface IOrderService
    {
        Task<OrderedGoodDto> AddGoodsToOrdered(int goodId, int amount, int buyerId);

        List<CartDto> GetCart(int orderId);

        List<OrderedGoodDto> GetAllOrderedGoodsByBuyerId(int buyerId);

        Task DeleteGoodFromOrder(int goodId, int orderId);

        Task Checkout(int userId);

        decimal SumCart (List<OrderedGoodDto> goods);
    }
}
