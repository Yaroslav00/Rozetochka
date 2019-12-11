using System;
using DataAccess.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IGoodsService
    {
        List<GoodDto> GetGoods(int? categoryId, string orderBy);

        Task AddGood(int categoryId, string name, string description, decimal price, string imageRef);
        Task<int> AddGoodWithIdReturn(int categoryId, string name, string description, decimal price, string imageRef);
        Task DeleteGood(int goodId);
        Task UpdateGood(int goodId, int categoryId, string name, string description, decimal price, string imageRef);
    }
}
