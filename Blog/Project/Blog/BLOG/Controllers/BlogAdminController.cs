using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BLOG.Models;
using BLOG.BLL;
using BLOG.Common;

namespace BLOG.Controllers
{
    public class BlogAdminController : Controller
    {
        private readonly IArticlesService articleService;
        private readonly ICategorysService categorysService;
        private readonly IUsersService userService;
        public BlogAdminController(IArticlesService _articleService, ICategorysService _categorysService, IUsersService _userService)
        {
            articleService = _articleService;
            categorysService = _categorysService;
            userService = _userService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult BlogManage()
        {

            return View();
        }
        public IActionResult Form()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registor()
        {
            return View();
        }
        /// <summary>
        /// 获取 文章内容
        /// </summary>
        /// <param name="pag"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetArticleData(Pagination pag, string keyValue)
        {
            string[] key = keyValue.Split(',');
            var expression = ExtLinq.True<Articles>();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                int cateID = int.Parse(key[0]);
                expression = expression.And(p => p.Categoryid.Equals(cateID) && p.User.Username.Equals(key[1]));
            }
            var list = articleService.LoadNormalEntities(expression, pag);
            List<ArticleViewModel> articleList = new List<ArticleViewModel>();
            foreach (var item in list)
            {
                ArticleViewModel articViewModel = new ArticleViewModel();
                articViewModel.Id = item.Id.ToString();
                articViewModel.Blogtitle = item.Blogtitle;
                articViewModel.State = item.State ? "启用" : "禁用";
                articViewModel.Lasteditdate = item.Lasteditdate;
                articViewModel.Date = item.Date;
                articViewModel.Browamount = item.Browamount.ToString();
                articleList.Add(articViewModel);
            }
            return Json(articleList);
        }
        /// <summary>
        /// 修改或者新增
        /// </summary>
        /// <param name="articleModel"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult EditSubmit(ArticleViewModel articleModel, string keyValue)
        {
            bool b = false;
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                Articles article = new Articles();
                article.Blogcontent = articleModel.Blogcontent;
                article.Blogtitle = articleModel.Blogtitle;
                article.Categoryid = int.Parse(articleModel.Categoryid);
                article.Lasteditdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                article.Browamount = 0;
                article.Isfrist = false;
                article.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                article.State = true;
                string uname = articleModel.Userid;
                Users user = userService.GetSingleData(p=>p.Username.Equals(uname));
                article.Userid = user.Id; 
                b = articleService.AddEntity(article);
            }
            else
            {
                long ids = long.Parse(keyValue);
                Articles article = articleService.GetSingleData(p => p.Id == ids);
                article.Blogcontent = articleModel.Blogcontent;
                article.Categoryid = long.Parse(articleModel.Categoryid);
                article.Blogtitle = articleModel.Blogtitle;
                article.Lasteditdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                b = articleService.EditEntity(article);
            }
            return b ? Content("执行成功!") : Content("执行失败!");
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult Disable(string keyValue)
        {
            string[] key = keyValue.Split(',');
            long id = long.Parse(key[0]);
            Articles article = articleService.GetSingleData(p => p.Id == id);
            article.Lasteditdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            article.State = (key[1] == "启用") ? false : true;
            return articleService.EditEntity(article) ? Content("执行成功!") : Content("执行失败!");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult Delete(string keyValue)
        {
            Articles article = new Articles();
            article.Id = long.Parse(keyValue);
            return articleService.DeleteEntity(article) ? Content("执行成功!") : Content("执行失败!");
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
            artViewModel.Blogtitle = article.Blogtitle;
            artViewModel.Blogcontent = article.Blogcontent;
            artViewModel.Id = keyValue;
            artViewModel.Categoryid = article.Categoryid.ToString();
            return Json(artViewModel);
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
            int cateid = int.Parse(key[0]);
            var list = articleService.LoadEntities(p => p.User.Username.Equals(key[1]) && p.Categoryid == cateid);
            return Content(list.Count().ToString());
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult SubmitLogin(UsersViewModel user)
        {

            Users userInfo = userService.GetSingleData(p => p.Username.Equals(user.Username) && p.Password.Equals(user.Password)&&p.State!=false);
            if (userInfo != null)
            {
                TempData["UserName"] = user.Username;
                return RedirectToAction("Index", "Blog");
            }
            TempData["submitState"] = "用户名和密码不匹配!";
            return RedirectToAction("Login", "BlogAdmin");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult SubmitRegistor(UsersViewModel user)
        {
            Users userInfo = new Users();
            userInfo.Name = user.Name;
            userInfo.Username = user.Username;
            userInfo.Password = user.Password;
            userInfo.Email = user.Email;
            userInfo.State = true;
            TempData["submitState"] = userService.AddEntity(userInfo) ? "提交成功!" : "提交失败!";
            return RedirectToAction("Registor", "BlogAdmin");
        }
    }
}