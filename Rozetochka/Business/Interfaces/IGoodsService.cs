using System;
using DataAccess.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IGoodsService
    {
        List<GoodDto> GetGoods(int? categoryId, string orderBy);
    }
}
