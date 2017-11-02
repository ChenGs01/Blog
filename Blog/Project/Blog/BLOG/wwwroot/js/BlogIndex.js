var owner = null;
var count = 0, pageCount = 0, page = 1, rows = 6;
$(function () {
    if ($("#tabs").attr("title") != undefined && $("#tabs").attr("title") != 'undefined') {
        sessionStorage["UserName"] = $("#tabs").attr("title");
    }
    if (sessionStorage["owner"] != 'null' && sessionStorage["owner"] != undefined && sessionStorage["owner"] != 'undefined') {
        owner = sessionStorage["owner"];
        if (sessionStorage["UserName"] != 'null' && sessionStorage["UserName"] != undefined && sessionStorage["UserName"] != 'undefined') {
            $("#user").html("<a style=\"cursor: pointer\" onclick='myBlog()' >[" + sessionStorage["UserName"] + "</a>&nbsp;&nbsp;<a style=\"cursor: pointer\" onclick='logOut()'>退出]</a>");
        }
    }
    else if (sessionStorage["UserName"] != 'null' && sessionStorage["UserName"] != undefined && sessionStorage["UserName"] != 'undefined') {
        owner = sessionStorage["UserName"];
        $("#user").html("<a style=\"cursor: pointer\" onclick='myBlog()' >[" + sessionStorage["UserName"] + "</a>&nbsp;&nbsp;<a style=\"cursor: pointer\" onclick='logOut()'>退出]</a>");
    }
    loadCategories();
    $("#tabs").find("a").get(0).click();
});
function loadCategories() {
    $.ajax({
        url: "../Blog/GetCategories",
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            var html = "<span>";
            $.each(data, function (idx, obj) {
                html += "<a style='cursor:pointer' onclick=\"Fresh(" + obj.id + ")\" >" + obj.name + "</a>";
            });
            html += "</span><b>" + owner + "</b>的博客</h2>";
            $("#tabs").html(html);
        }
    });
}
var cateId = "";
function Fresh(id) {
    cateid = id;
    GetPageInfo(id);
    blogList(cateid, owner);
}
function myBlog() {
    sessionStorage["owner"] = null;
    if (sessionStorage["UserName"] == 'null' || sessionStorage["UserName"] == undefined || sessionStorage["UserName"] == 'undefined')
        location.href = "../BlogAdmin/Login";
    else
        location.href = "../Blog/Index";
}
function TopPage() {
    page--;
    if (page <= 0)
        page = 1;
    Fresh(cateid);
}
function NextPage() {
    page++;
    if (page >= pageCount)
        page = pageCount;
    Fresh(cateid);
}
