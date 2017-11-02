//加载tab推荐信息
function LoadTopInfo() {
    $.ajax({
        url: "../Home/GetTopBrowBlog",
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            // alert(JSON.stringify(data));
            var html = "<ul>";
            $.each(data, function (idx, obj) {
                html += "<li><a href=\"../Blog/Detail?id=" + obj.id + "&cate=" + obj.cateName + "&owner=" + obj.userName + "\"  > " + obj.title + "</a ></li >";
            });
            html += "</ul>";
            $("#TopBrow").html(html);
        }
    });
    $.ajax({
        url: "../Home/GetTopNewBlog",
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            var html = "<ul>";
            $.each(data, function (idx, obj) {
                html += "<li><a href=\"../Blog/Detail?id=" + obj.id + "&cate=" + obj.cateName + "&owner=" + obj.userName + "\"  > " + obj.title + "</a ></li >";
            });
            html += "</ul>";
            $("#TopNew").html(html);
        }
    });
    $.ajax({
        url: "../Home/GetTopIsFirstBlog",
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            var html = "<ul>";
            $.each(data, function (idx, obj) {
                html += "<li><a href=\"../Blog/Detail?id=" + obj.id + "&cate=" + obj.categoryName + "&owner=" + obj.userid + "\"  > " + obj.blogtitle + "</a ></li >";
            });
            html += "</ul>";
            $("#TopFirst").html(html);
        }
    });
}
