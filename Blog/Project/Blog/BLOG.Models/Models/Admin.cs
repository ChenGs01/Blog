using System;
using System.Collections.Generic;

namespace BLOG.Models
{
    public partial class Admin
    {
        public long Id { get; set; }
        public string Adminname { get; set; }
        public string Password { get; set; }
    }
}
