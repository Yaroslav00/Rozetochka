﻿using Business.Interfaces;
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

        public async Task AddGood(int categoryId, string name, string description, decimal price)
        {
            await ItemRepository.AddItem(categoryId, name, description, price);
        }

        public async Task DeleteGood(int goodId)
        {
            await ItemRepository.DeleteGood(goodId);
        }
    }
}
