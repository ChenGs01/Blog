using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOG.Common
{
    public class BuildMessage
    {
        public static object GetResult(string rel, string msg, object data)
        {
            return new Dictionary<string, object> { { "rel", rel }, { "msg", msg }, { "obj", data } };
        }

    }
}
