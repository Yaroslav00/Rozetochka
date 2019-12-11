using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business.Services;
using DataAccess;
namespace RozetochkaUnitTest
{
    [TestClass]
    public class GoodUnitTest
    {
        [TestMethod]
        public async void TestAddMethod()
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
            await  _goodService.AddGood(testGategoryId, testGoodName,testGoodDescription, testGoodPrice,testFGoodImageRef);
            var post_goods = _goodService.GetGoods(1, "За алфавітом");
            var post_count = post_goods.Count;
            Assert.IsTrue(pre_count == post_count - 1);
        }
    }
}
