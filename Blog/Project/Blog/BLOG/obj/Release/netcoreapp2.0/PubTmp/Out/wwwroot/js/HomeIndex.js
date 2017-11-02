$(function () {
    blogList();
});
function blogList() {
    $("#contents").empty();
    $.ajax({
        url: "../Home/GetTopIsFirstBlog",
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            $.each(data, function (idx, obj) {
                var itemHtml = "";
                itemHtml = "<div class=\"blogs\" id=\"item+" + obj.id + "\"  ><figure>";
                var s = obj.blogcontent.indexOf("<img");
                var t = 0;
                if (s >= 0) {
                    var e = obj.blogcontent.indexOf(">", s);
                    var str = obj.blogcontent.substring(s, e);
                    str += "   style=\"width: 167px; height: 136px\">";
                    itemHtml += str;
                    t = 1;
                }
                if (t == 0) {
                    itemHtml += "  <img src=\"/images/logo4.png\" style=\"width: 167px; height: 136px\">";
                }
                itemHtml += "</figure>";
                itemHtml += " <ul><h3><a href=\"../Blog/Detail?id=" + obj.id + "&cate=" + obj.categoryName + "&owner=" + obj.userid + "\"  >" + obj.blogtitle + "</a></h3>";
                var imgs = obj.blogcontent.indexOf("<"), imge = obj.blogcontent.indexOf(">", imgs), info = "";
                var reg = /[\u3002|\uff1f|\uff01|\uff0c|\u3001|\uff1b|\uff1a|\u201c|\u201d|\u2018|\u2019|\uff08|\uff09|\u300a|\u300b|\u3008|\u3009|\u3010|\u3011|\u300e|\u300f|\u300c|\u300d|\ufe43|\ufe44|\u3014|\u3015|\u2026|\u2014|\uff5e|\ufe4f|\uffe5]/;
                var f = 0;
                //截取显示的文章内容
                for (var i = 0; i < obj.blogcontent.length; i++) {
                    //若在图片编码内则跳出到 尾部继续循环
                    if (i >= imgs && i <= imge + 1) {
                        i = imge + 1;
                        continue;
                    }
                    if (f > 100)
                        break;
                    if (i > imge + 1 && imge != 0) {
                        if (obj.blogcontent.indexOf("<", imge + 1) >= 0) {
                            imgs = obj.blogcontent.indexOf("<", imge);
                            imge = obj.blogcontent.indexOf(">", imgs);
                        }
                        else {
                            imgs = 0;
                            imge = 0;
                        }
                    }
                    if (imge == imgs || i < imgs) {
                        if (obj.blogcontent.charCodeAt(i) > 255 || reg.test(obj.blogcontent[i])) {
                            info += obj.blogcontent[i];
                            f++;
                        }
                    }
                }
                // info = " indexOf 方法返回一个整数值，指出 String 对象内子字符串的开始位置。如果没有找到子字符串，则返回 -1。 ";
                itemHtml += "<p>" + info + "...<p/>";
                itemHtml += "<p class=\"autor\"><span class=\"lm f_l\"><a href=\"/ \">" + obj.categoryName + "</a></span><span class=\"dtime f_l\">" + obj.date + "</span><span class=\"viewnum f_r\">浏览量（<a href=\"/ \">" + obj.browamount + "</a>）</span></p>";
                itemHtml += " </ul></div >";
                $("#contents").append(itemHtml);
            });
        }
    });
}