using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLOG.BLL;
using BLOG.Models;


namespace BLOG.Admins.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService _adminService)
        {
            adminService = _adminService;
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AdminLogin(Admin info)
        {
            Admin adminInfo = adminService.GetSingleData(p => p.Adminname.Equals(info.Adminname) && p.Password.Equals(info.Password));
            if (adminInfo != null)
            {
                TempData["adminName"] = adminInfo.Adminname;
                return RedirectToAction("Index", "Home");
            }
            TempData["submitState"] = "用户名和密码不匹配!";
            return RedirectToAction("Login", "Admin");
        }
    }
}