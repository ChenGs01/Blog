using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLOG.BLL;
using BLOG.Models;

namespace BLOG.Admins.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategorysService categoryService;
        public CategoryController(ICategorysService _categoryService)
        {
            categoryService = _categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Form()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var list = categoryService.LoadEntities(p => p.Id != 0);
            return Json(list);
        }
        public IActionResult Edit(Categorys category, string keyValue)
        {
            bool b = false;
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                b = categoryService.AddEntity(category);
            }
            else
            {
                int id = int.Parse(keyValue);
                Categorys cate = categoryService.GetSingleData(p => p.Id.Equals(id));
                cate.Name = category.Name;
                b = categoryService.EditEntity(cate);
            }
            return b ? Content("执行成功!") : Content("执行失败!");
        }
    }
}