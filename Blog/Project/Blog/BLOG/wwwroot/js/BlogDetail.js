var owner = null;
var id = "", ty = "";
$(function () {
    id = GetUrlParam("id");
    ty = decodeURI(GetUrlParam("cate"));
    owner = GetUrlParam("owner");
    sessionStorage["owner"] = owner;
    if (sessionStorage["UserName"] != 'null' && sessionStorage["UserName"] != undefined && sessionStorage["UserName"] != 'undefined') {
        $("#user").html("<a style=\"cursor: pointer\" onclick='myBlog()' >[" + sessionStorage["UserName"] + "</a>&nbsp;&nbsp;<a style=\"cursor: pointer\" onclick='logOut()'>退出]</a>");
    }
    $("#names").html(owner + "的博客");
    brow();
    load();
});
function brow() {
    $.ajax({
        url: "../Blog/AritcleBrow",
        data: { "keyValue": id },
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {

        }
    });
}
function myBlog() {
    sessionStorage["owner"] = null;
    if (sessionStorage["UserName"] == 'null' || sessionStorage["UserName"] == undefined || sessionStorage["UserName"] == 'undefined')
        location.href = "../BlogAdmin/Login";
    else
        location.href = "../Blog/Index";
}
function load() {
    $.ajax({
        url: "../Blog/SingleData",
        data: { "keyValue": id },
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            $("#titles").html(data.blogtitle);
            $("#infos").html("类别：" + ty + "&nbsp;&nbsp;日期：" + data.date + " &nbsp;&nbsp;浏览量:" + data.browamount + "");
            $("#contents").html(data.blogcontent);
        }
    });
}