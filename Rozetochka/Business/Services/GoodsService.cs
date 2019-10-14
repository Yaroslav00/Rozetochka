﻿using Business.Interfaces;
using DataAccess.Dto;
using DataAccess.Repository;
using System.Collections.Generic;

namespace Business.Services
{
    public class GoodsService: IGoodsService
    {
        public List<GoodDto> GetGoods(int? categoryId, string orderBy)
        {
            return ItemRepository.GetItems(categoryId, orderBy);
        }
    }
}
