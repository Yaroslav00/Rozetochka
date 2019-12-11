using Business.Interfaces;
using DataAccess.Dto;
using DataAccess.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class GoodsService: IGoodsService
    {
        public List<GoodDto> GetGoods(int? categoryId, string orderBy)
        {
            return ItemRepository.GetItems(categoryId, orderBy);
        }

        /*
        public async Task AddGood( int categoryId, string name, string description, decimal price, string imageRef)
        {
            await ItemRepository.AddItem(categoryId, name, description, price, imageRef);
        }
        */
        public async Task<int> AddGood(int categoryId, string name, string description, decimal price, string imageRef)
        {
            int goodId = await ItemRepository.AddItem(categoryId, name, description, price, imageRef);
            return  goodId;
        }

        public async Task DeleteGood(int goodId)
        {
            await ItemRepository.DeleteGood(goodId);
        }
        public async Task UpdateGood(int goodId,int categoryId, string name, string description, decimal price, string imageRef)
        {
            await ItemRepository.UpdateGood(goodId, categoryId, name, description, price, imageRef);
        }
    }
}
