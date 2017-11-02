using System;
using System.Collections.Generic;

namespace BLOG.Models
{
    public partial class Articles
    {
        public long Id { get; set; }
        public string Blogtitle { get; set; }
        public string Blogcontent { get; set; }
        public long Categoryid { get; set; }
        public bool State { get; set; }
        public string Date { get; set; }
        public long Browamount { get; set; }
        public string Lasteditdate { get; set; }
        public bool Isfrist { get; set; }
        public long? Userid { get; set; }

        public Categorys Category { get; set; }
        public Users User { get; set; }
    }
}
