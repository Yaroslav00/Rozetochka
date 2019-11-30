using System.Threading.Tasks;
using DataAccess.Dto;

namespace Business.Interfaces
{
    public interface IOrderService
    {
        Task<OrderedGoodDto> AddGoodsToOrdered(int goodId, int amount, int buyerId);

        Task<decimal> GetOrderTotalPrice(int orderId);

        Task<CartDto> GetCart(int orderId);
    }
}
