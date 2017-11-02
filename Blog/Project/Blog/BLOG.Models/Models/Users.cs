using System;
using System.Collections.Generic;

namespace BLOG.Models
{
    public partial class Users
    {
        public Users()
        {
            Articles = new HashSet<Articles>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool State { get; set; }

        public ICollection<Articles> Articles { get; set; }
    }
}
