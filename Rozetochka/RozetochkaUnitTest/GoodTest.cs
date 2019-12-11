using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business.Services;
using DataAccess;
using EntityFramework;
using DataAccess;
using Business.Interfaces;
using Business.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DataAccess.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace RozetochkaUnitTest
{
    [TestClass]
    public class GoodUnitTest
    {
        [TestMethod]
        public async Task TestAddMethod()
        {
            var _goodService = new GoodsService();
            int testGoodIndex = 101;
            int testGategoryId = 1;
            decimal testGoodPrice = 0;
            string testGoodDescription = "TestDescription";
            string testGoodName = "TestName";
            string testFGoodImageRef = "TestImageRef";
            var pre_goods = _goodService.GetGoods(1, "За алфавітом");
            var pre_count = pre_goods.Count;
            int goodId = await _goodService.AddGood(testGategoryId, testGoodName, testGoodDescription, testGoodPrice, testFGoodImageRef);
           
            var post_goods = _goodService.GetGoods(1, "За алфавітом");
           
            var post_count = post_goods.Count;
            
            Assert.IsTrue(pre_count == post_count-1);
            await _goodService.DeleteGood(goodId);
        }
        [TestMethod]
        public async Task TestUpdateMethod()
        {
            var _goodService = new GoodsService();
            int testGoodIndex = 101;
            int testGategoryId = 1;
            decimal testGoodPrice = 0;
            string testGoodDescription = "TestDescription";
            string testGoodName = "TestName";
            string testFGoodImageRef = "TestImageRef";
         
            
            int goodId = await _goodService.AddGood(testGategoryId, testGoodName, testGoodDescription, testGoodPrice, testFGoodImageRef);

            var preGood = _goodService.GetGoods(1, "За алфавітом").Find(g => g.ID == goodId);
            decimal newPrice = 100;
            await _goodService.UpdateGood(preGood.ID, testGategoryId, testGoodName, testGoodDescription, newPrice, testFGoodImageRef);
            var postGood = _goodService.GetGoods(1, "За алфавітом").Find(g => g.ID == goodId);
            Assert.IsFalse(preGood.Price == postGood.Price);
            await _goodService.DeleteGood(goodId);
        }
    }
}
