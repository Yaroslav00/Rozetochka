using DataAccess.Dto;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IGoodsService
    {
        List<GoodDto> GetGoods(int? categoryId);
    }
}
