using System;
using System.Collections.Generic;

namespace BLOG.Models
{
    public partial class Categorys
    {
        public Categorys()
        {
            Articles = new HashSet<Articles>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Articles> Articles { get; set; }
    }
}
