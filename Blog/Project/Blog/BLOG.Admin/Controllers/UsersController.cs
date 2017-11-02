using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLOG.BLL;
using BLOG.Models;

namespace BLOG.Admins.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService userService;
        public UsersController(IUsersService _userService)
        {
            userService = _userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var list = userService.LoadEntities(p=>p.Id!=0);
            return Json(list);
        }
        public IActionResult Disable(string keyValue)
        {
            string[] key = keyValue.Split(',');
            int id = int.Parse(key[0]);
            Users user = userService.GetSingleData(p=>p.Id.Equals(id));
            user.State = (key[1] == "启用") ? false: true;
            return userService.EditEntity(user) ? Content("执行成功!") : Content("执行失败!");
        }
    }
}