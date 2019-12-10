﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Dto;
using DataAccess.Models;
using Unity.Injection;

namespace DataAccess.Repository
{
    public static class ItemRepository
    {
        public static async Task<decimal> GetItemPrice(int itemId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return await dbContext.Merchandise.Where(p => p.ID == itemId).Select(p => p.Price).FirstOrDefaultAsync();
            }
        }

        public static List<GoodDto> GetItems(int? categoryId, string orderBy)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            using (dbContext)
            {
                System.Reflection.PropertyInfo orderbyObj;
                switch (orderBy)
                {
                    case "За алфавітом":
                        orderbyObj = typeof(GoodDto).GetProperty("Name");
                        break;
                    case "Ціна за зростанням":
                        orderbyObj = typeof(GoodDto).GetProperty("Price");
                        break;
                    default:
                        orderbyObj = typeof(GoodDto).GetProperty("Name");
                        break;
                }

                var allItems = dbContext.Merchandise.Select(good =>
                new GoodDto
                {
                    CategoryID = good.CategoryID,
                    Description = good.Description,
                    ID = good.ID,
                    Name = good.Name,
                    Price = good.Price,
                    ImageRef = good.ImageRef
                });

                if (orderBy == "Ціна за спаданням")
                {
                    return categoryId != null ? allItems.Where(item => item.CategoryID == categoryId).OrderByDescending(el => el.Price)
                        .ToList() : allItems.OrderByDescending(el => el.Price).ToList();
                }

                return categoryId != null ? allItems.Where(item => item.CategoryID == categoryId).AsEnumerable()
                    .OrderBy(o => orderbyObj.GetValue(o,null)).ToList() :
                    allItems.AsEnumerable().OrderBy(o => orderbyObj.GetValue(o, null)).ToList();
            }
        }

        public static async Task AddItem(int categoryId, string name, string description, decimal price, string imageref)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var item = new Goods(name, price, description, categoryId, imageref);
                dbContext.Merchandise.Add(item);
                await dbContext.SaveChangesAsync();
            };
        }

        public static async Task DeleteGood(int goodId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var good = new Goods {ID = goodId};

                dbContext.Merchandise.Attach(good);
                dbContext.Merchandise.Remove(good);

                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task UpdateGood(int goodId, int categoryId, string name, string description, decimal price, string imageref)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                

                Goods good = dbContext.Merchandise.First(p => p.ID == goodId);
                good.ID = goodId;
                good.Name = name;
                good.Price = price;
                good.ImageRef = imageref;
                good.Description = description;
                good.CategoryID = categoryId;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
