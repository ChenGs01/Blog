using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLOG.Models;
using BLOG.BLL;
using BLOG.Common;

namespace BLOG.Controllers
{
    public class HomeController : Controller
    {

        private readonly IArticlesService articleService;
        private readonly ICategorysService categorysService;
        private readonly IUsersService userService;
        public HomeController(IArticlesService _articleService, ICategorysService _categorysService, IUsersService _userService)
        {
            articleService = _articleService;
            categorysService = _categorysService;
            userService = _userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Categories()
        {
            return View();
        }
        public IActionResult Friends()
        {
            return View();
        }
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCategories()
        {
            var list = categorysService.LoadEntities(p => p.Id != 0);
            return Json(list);
        }
        /// <summary>
        /// 获取浏览量前六条
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTopBrowBlog()
        {
            var list = articleService.LoadEntities(p => p.Id != 0&&p.State!=false).OrderByDescending(p => p.Browamount).Take(6);
            var list1 = userService.LoadEntities(p => p.Id != 0);
            var list2 = categorysService.LoadEntities(p => p.Id != 0);
            var list3 = from c in list
                        join p in list1
                        on c.Userid equals p.Id
                        join t in list2
                        on c.Categoryid equals t.Id
                        select new
                        {
                            Id = c.Id,
                            userName = p.Username,
                            title = c.Blogtitle,
                            content = c.Blogcontent,
                            cateName = t.Name,
                            Date = c.Date,
                            Browamount = c.Browamount
                        };
            return Json(list3);
        }
        /// <summary>
        /// 最新六条
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTopNewBlog()
        {
            var list = articleService.LoadEntities(p => p.Id != 0&&p.State!=false).OrderByDescending(p => p.Date).Take(6);
            var list1 = userService.LoadEntities(p => p.Id != 0);
            var list2 = categorysService.LoadEntities(p => p.Id != 0);
            var list3 = from c in list
                        join p in list1
                        on c.Userid equals p.Id
                        join t in list2
                        on c.Categoryid equals t.Id
                        select new
                        {
                            Id = c.Id,
                            userName = p.Username,
                            title = c.Blogtitle,
                            content = c.Blogcontent,
                            cateName = t.Name,
                            Date = c.Date,
                            Browamount = c.Browamount
                        };
            return Json(list3);
        }
        /// <summary>
        /// 推荐首页六条
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTopIsFirstBlog()
        {
            var list = articleService.LoadEntities(p => p.Isfrist == true&&p.State!=false).OrderByDescending(p => p.Date).Take(6);
            var list1 = userService.LoadEntities(p => p.Id != 0);
            var list2 = categorysService.LoadEntities(p => p.Id != 0);

            var list3 = from c in list
                        join p in list1
                        on c.Userid equals p.Id
                        join t in list2
                        on c.Categoryid equals t.Id
                        select new
                        {
                            Id = c.Id,
                            userName = p.Username,
                            title = c.Blogtitle,
                            content = c.Blogcontent,
                            cateName = t.Name,
                            Date = c.Date,
                            Browamount = c.Browamount
                        };
            List<ArticleViewModel> lists = new List<ArticleViewModel>();
            foreach (var item in list3)
            {
                ArticleViewModel artcle = new ArticleViewModel();
                artcle.Blogcontent = item.content;
                artcle.Blogtitle = item.title;
                artcle.Browamount = item.Browamount.ToString();
                artcle.Id = item.Id.ToString();
                artcle.CategoryName = item.cateName;
                artcle.Date = item.Date;
                artcle.Userid = item.userName;
                lists.Add(artcle);
            }
            return Json(lists);
        }
    }
}
