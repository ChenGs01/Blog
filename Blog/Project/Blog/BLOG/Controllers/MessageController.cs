using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLOG.BLL;
using BLOG.Models;
using Microsoft.AspNetCore.Mvc;

namespace BLOG.Controllers
{
    public class MessageController : Controller
    {
        private readonly IUsersService userService;
        private readonly IMessageService messageService;

        public MessageController(IUsersService _userService, IMessageService _messageService)
        {
            userService = _userService;
            messageService = _messageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetMessageByBlogId(string keyValue)
        {
            long Id = long.Parse(keyValue);
            var list = messageService.LoadEntities(p => p.ArticleId.Equals(Id)).OrderBy(p => p.Time);
            //var list1 = from p in list
            //            select new
            //            {
            //                ArticleID = p.ArticleId,
            //                SendUser = userService.GetSingleData(t => t.Id.Equals(p.SendUser)).Name,
            //                ReceivedUser = userService.GetSingleData(t => t.Id.Equals(p.ReceivedUser)).Name,
            //                p.Contents,
            //                p.Time
            //            };
            return Json(list);
        }
        public IActionResult Insert(Message model)
        {
            if (ModelState.IsValid)
            {
                model.Contents = model.Contents.Replace("\n", "<br/>");
                model.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bool b = messageService.AddEntity(model);
                return b ? Content("成功") : Content("失败");
            }
            return Content("验证失败");
        }
        public IActionResult Delete(Message model)
        {
            bool b = messageService.DeleteEntity(model);
            return b ? Content("成功") : Content("失败");
        }
    }
}