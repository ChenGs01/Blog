
var UserName = "", cateid = "", catename = "", count = 0, pageCount = 0, page = 1, rows = 20;
$(function () {
    cateid = GetUrlParam("cateid");
    catename = decodeURI(GetUrlParam("catename"));
    $("#title").html(catename);
    Fresh();
});
function Fresh() {
    GetPageInfo();
    pageInfo();
    GetArticleData();
}
function GetUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = encodeURI(window.location.search).substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
function Edit(id) {
    layer.ready(function () {
        layer.open({
            type: 2,
            title: '修改',
            maxmin: true,
            area: ['1000px', '800px'],
            content: '../BlogAdmin/Form?id=' + id,
            end: function () {
                Fresh();
            }
        });
    });
}
function Delete(id) {
    if (confirm("确定是否删除?")) {
        $.ajax({
            url: "../BlogAdmin/Delete",
            data: { "keyValue": id },
            dataType: "text",
            async: false,
            type: "post",
            success: function (data) {
                alert(data);
                Fresh();
            }
        });
    }
}

function Disable(state, id) {
    if (confirm("确定是否改变状态?")) {
        $.ajax({
            url: "../BlogAdmin/Disable",
            data: { "keyValue": id + ',' + state },
            dataType: "text",
            async: true,
            type: "post",
            success: function (data) {
                alert(data);
                Fresh();
            }
        });
    }
}
function GetPageInfo() {
    var keyValue = cateid + "," + sessionStorage["UserName"];
    $.ajax({
        url: "../BlogAdmin/GetRecordsCountInfo",
        data: { "keyValue": keyValue },
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            count = parseInt(data);
            var c = parseInt(count / rows);
            var t = count % rows;
            pageCount = c + t;
            pageInfo();
        }
    });
}
function pageInfo() {
    $("#pageInfo").html("共" + count + "条数据,每页" + rows + "条数据,共" + pageCount + "页,当前第" + page + "页");
}
function GetArticleData() {
    var keyValue = cateid + "," + sessionStorage["UserName"];
    $.ajax({
        url: "../BlogAdmin/GetArticleData",
        data: { "rows": rows, "page": page, "sidx": "Id", "sord": "desc", "records": count, "total": pageCount, "keyValue": keyValue, },
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            $("#tbodys").empty();
            $.each(data, function (idx, obj) {
                var f = parseInt(idx) + 1;
                $("#tbodys").append("<tr class=\"tr\"><td>"+f+"</td><td>" + obj.blogtitle + "</td><td>" + obj.date + "</td><td>" + obj.lasteditdate + "</td><td>" + obj.browamount + "</td><td>" + obj.state + "</td><td><a class=\"aclass\" onclick='Edit(" + obj.id + ")'>修改</a>|<a class=\"aclass\" onclick='Delete(" + obj.id + ")'>删除</a>|<a class=\"aclass\" onclick=\"Disable('" + obj.state + "'," + obj.id + ")\">更改状态</a></td></tr >");
            });
        }
    });
}
function TopPage() {
    page--;
    if (page <= 0)
        page = 1;
    Fresh();
}
function NextPage() {
    page++;
    if (page >= pageCount)
        page = pageCount;
    Fresh();

}