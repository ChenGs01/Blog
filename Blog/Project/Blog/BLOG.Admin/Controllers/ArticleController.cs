using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLOG.BLL;
using BLOG.Models;

namespace BLOG.Admins.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticlesService articleService;
        private readonly ICategorysService categorysService;
        private readonly IUsersService userService;
        public ArticleController(IArticlesService _articleService, ICategorysService _categorysService, IUsersService _userService)
        {
            articleService = _articleService;
            categorysService = _categorysService;
            userService = _userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll(string keyValue)
        {
            int cateid = int.Parse(keyValue);
            var list = articleService.LoadEntities(p => p.Categoryid.Equals(cateid));
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
                            isfirst=c.Isfrist,
                            state=c.State,
                            Browamount = c.Browamount,
                        };
            return Json(list3);
        }
        public IActionResult Disable(string keyValue)
        {
            string[] key = keyValue.Split(',');
            long id = long.Parse(key[0]);
            Articles article = articleService.GetSingleData(p => p.Id.Equals(id));
            article.State = (key[1] == "启用") ? false: true;
            return articleService.EditEntity(article) ? Content("执行成功!") : Content("执行失败!");
        }
        public IActionResult IsFirst(string keyValue)
        {
            string[] key = keyValue.Split(',');
            long id = long.Parse(key[0]);
            Articles article = articleService.GetSingleData(p => p.Id.Equals(id));
            article.Isfrist = (key[1] == "是") ? false : true;
            return articleService.EditEntity(article) ? Content("执行成功!") : Content("执行失败!");
        }
      
    }
}