using System;
using System.Collections.Generic;
using System.Text;

namespace BLOG.Models
{
    public class ArticleViewModel
    {
        public string Id { get; set; }
        public string Blogtitle { get; set; }
        public string Blogcontent { get; set; }
        public string Categoryid { get; set; }
        public string CategoryName { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
        public string Browamount { get; set; }
        public string Lasteditdate { get; set; }
        public string Isfrist { get; set; }
        public string Userid { get; set; }

    }
}
