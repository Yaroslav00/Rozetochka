using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Dto;

namespace Business.Interfaces
{
    public interface IOrderService
    {
        Task<OrderedGoodDto> AddGoodsToOrdered(int goodId, int amount, int buyerId);

        CartDto GetCart(int orderId);

        List<OrderedGoodDto> GetAllOrderedGoodsByBuyerId(int buyerId);
    }
}
