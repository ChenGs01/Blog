using System;
using System.Collections.Generic;

namespace BLOG.Models
{
    public partial class Message
    {
        public long Id { get; set; }
        public long? ArticleId { get; set; }
        public string SendUser { get; set; }
        public string ReceivedUser { get; set; }
        public string Contents { get; set; }
        public string Time { get; set; }
        public bool? IsCheck { get; set; }
    }
}
