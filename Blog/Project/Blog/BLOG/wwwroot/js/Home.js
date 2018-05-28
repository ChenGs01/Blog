$(function () {
    if (sessionStorage["UserName"] != 'null' && sessionStorage["UserName"] != undefined && sessionStorage["UserName"] != 'undefined') {
        $("#user").html("<a style=\"cursor: pointer\" onclick='myBlog()' >[" + sessionStorage["UserName"] + "</a>&nbsp;&nbsp;<a style=\"cursor: pointer\" onclick='logOut()'>退出]</a>");
    }
    LoadCate();
    LoadTopInfo();
});
function LoadCate() {
    $.ajax({
        url: "../Home/GetCategories",
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            var html = "<a href=\"../Home/Index\">首页</a><a href=\"../Home/AboutUs\"> 关于我们</a>";
            $.each(data, function (idx, obj) {
                html += "<a href=\"../Home/Categories?id=" + obj.id + "&cate=" + obj.name + "\">" + obj.name + "</a>";
            });
            html += " <a style='cursor:pointer' onclick='myBlog()'>我的博客</a>";
            $("#topnav").html(html);
        }
    });
}
function logOut() {
    sessionStorage["UserName"] = null;
    location.reload();
}
function myBlog() {
    //sessionStorage["owner"] = null;
    if (sessionStorage["UserName"] == 'null' )
        location.href = "../BlogAdmin/Login";
    else 
        location.href = "../Blog/Index";
}