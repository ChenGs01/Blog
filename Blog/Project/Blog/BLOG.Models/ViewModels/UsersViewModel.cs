using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLOG.Models
{
    public class UsersViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        [Required(ErrorMessage = "用户名不能为空。")]
        public string Username { get; set; }
        [Required(ErrorMessage = "密码不能为空。")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
    }
}
