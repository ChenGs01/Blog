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
    message();
    $("#submit").click(function () {
        submit();
    });
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
function message() {
    $("#message").html("");
    $.ajax({
        url: "../Message/GetMessageByBlogId",
        data: { "keyValue": id },
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var b = i + 1;
                var str = data[i].contents.split('<br/>').join('&hc');
                $("#message").append(" <h1>#" + b + "楼 " + data[i].time + "| " + data[i].sendUser + " <label class=\"message_a\"  onclick=\"delMessage('" + data[i].id + "','" + data[i].sendUser + "')\">|删除</label> <label class=\"message_a\"  onclick=\"answer('" + str + "','" + data[i].sendUser + "'," + b + ")\">回复</label></h1>");
                $("#message").append(" <h1>" + data[i].contents + "</h1> <br />");
            }
        }
    });
}
function delMessage(id, user) {
    //当前登陆用户   而参数user为该条评论的所有者
    var loginUser = sessionStorage["UserName"];
    if (loginUser == 'null') {
        alert("请先登陆！");
        return;
    }
    if (loginUser == owner || loginUser == user) {
        if (confirm("确认是否删除？")) {
            $.ajax({
                url: "../Message/Delete",
                data: { "Id": id },
                dataType: "text",
                async: false,
                type: "post",
                success: function (data) {
                    message();
                }
            });
        }
    }
    else {
        alert("无权删除他人评论！");
    }
}
function answer(content, user, f) {
    var loginUser = sessionStorage["UserName"];
    if (loginUser == 'null') {
        alert("请先登陆！");
        return;
    }
    var str = content.split('&hc').join('\n');
    var str = str.split('#').join('@' + user + '\n');
    if (str.indexOf('@') == -1)
        $("#txt").val("@" + user + "  \n" + str + "\n#");
    else
        $("#txt").val(str + "\n#");
}
function submit() {
    var user = sessionStorage["UserName"];
    if (user == 'null') {
        alert("请先登陆！");
        return;
    }
    var content = $("#txt").val();
    $.ajax({
        url: "../Message/Insert",
        data: { "SendUser": user, "Contents": content, "ArticleId": id },
        dataType: "text",
        async: false,
        type: "post",
        success: function (data) {
            $("#txt").val('');
            message();
        }
    });
}