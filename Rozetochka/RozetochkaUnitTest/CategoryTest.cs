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
    public class CategoryTest
    {
        [TestMethod]
        public async Task TestAddMethod()
        {
            var _categoryService = new CategoryService();
            string testCategoryName = "TestName";
            var pre_categories = _categoryService.GetCategories();
            var pre_count = pre_categories.Count;
            int categoryId = await _categoryService.AddCategory(testCategoryName);
            var post_categories = _categoryService.GetCategories();
            var post_count = post_categories.Count;
            Assert.IsTrue(pre_count == post_count - 1);
            await _categoryService.DeleteCategory(categoryId);
        }
    }
}
