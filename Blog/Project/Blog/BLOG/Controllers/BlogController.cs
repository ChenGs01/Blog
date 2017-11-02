using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLOG.BLL;
using BLOG.Models;
using BLOG.Common;

namespace BLOG.Controllers
{
    public class BlogController : Controller
    {
        private readonly IArticlesService articleService;
        private readonly ICategorysService categorysService;
        private readonly IUsersService userService;
        public BlogController(IArticlesService _articleService, ICategorysService _categorysService, IUsersService _userService)
        {
            articleService = _articleService;
            categorysService = _categorysService;
            userService = _userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail()
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
        /// 获取页面信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetRecordsCountInfo(string keyValue)
        {
            string[] key = keyValue.Split(',');
            long cateid = long.Parse(key[0]);
            IQueryable<Articles> list;
            if (key[1] != "all")
                list = articleService.LoadEntities(p => p.User.Username.Equals(key[1]) && p.Categoryid == cateid && p.State != false);
            else
                list = articleService.LoadEntities(p => p.Categoryid == cateid && p.State != false);
            return Content(list.Count().ToString());
        }
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SingleData(string keyValue)
        {
            long id = long.Parse(keyValue);
            Articles article = articleService.GetSingleData(p => p.Id == id);
            ArticleViewModel artViewModel = new ArticleViewModel();
            if (article != null)
            {
                artViewModel.Blogtitle = article.Blogtitle;
                artViewModel.Blogcontent = article.Blogcontent;
                artViewModel.Id = keyValue;
                artViewModel.Browamount = article.Browamount.ToString();
                artViewModel.Date = article.Date;
            }
            return Json(artViewModel);
        }
        /// <summary>
        /// 获取 文章列表
        /// </summary>
        /// <param name="pag"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult BlogList(Pagination pag, string keyValue)
        {
            string[] key = keyValue.Split(',');
            var expression = ExtLinq.True<Articles>();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                long cateID = long.Parse(key[0]);
                expression = expression.And(p => p.Categoryid.Equals(cateID) && p.State != false);
                if (key[1] != "all")
                    expression = expression.And(p => p.User.Username.Equals(key[1]));
            }
            var list = articleService.LoadNormalEntities(expression, pag);
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

            List<ArticleViewModel> articleList = new List<ArticleViewModel>();
            foreach (var item in list3)
            {
                ArticleViewModel articViewModel = new ArticleViewModel();
                articViewModel.Id = item.Id.ToString();
                articViewModel.Blogtitle = item.title;
                articViewModel.Blogcontent = item.content;
                articViewModel.Userid = item.userName;
                articViewModel.CategoryName = item.cateName;
                articViewModel.Date = item.Date;
                articViewModel.Browamount = item.Browamount.ToString();
                articleList.Add(articViewModel);
            }
            return Json(articleList);
        }
        /// <summary>
        /// 统计浏览量
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult AritcleBrow(string keyValue)
        {
            long id = long.Parse(keyValue);
            Articles article = articleService.GetSingleData(p=>p.Id.Equals(id));
            article.Browamount = article.Browamount+1;
            articleService.EditEntity(article);
            return new EmptyResult();
        }
    }
 
}