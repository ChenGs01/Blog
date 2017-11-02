var id = "";
$(function () {
    id = getUrlParam("id");
    loadSelect();
    load();
    $("#submits").click(function () {
        submit();
    });
});
function load() {
    $.ajax({
        url: "../BlogAdmin/SingleData",
        data: { "keyValue": id },
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            $("#cate").find("option[value='" + data.categoryid + "']").attr("selected", true);
            $("#title").val(data.blogtitle);
            $("#customized-buttonpane").html(data.blogcontent);
        }
    });
}
function loadSelect() {
    $.ajax({
        url: "../BlogAdmin/GetCategories",
        dataType: "json",
        async: false,
        type: "post",
        success: function (data) {
            var res = eval(data);
            $.each(res, function (idx, obj) {
                $("#cate").append("<option value='" + obj.id + "'>" + obj.name + "</option>");
            });
        }
    });
}
function submit() {
    var uid = sessionStorage["UserID"];
    $.ajax({
        url: "../BlogAdmin/EditSubmit",
        dataType: "text",
        async: false,
        data: { "Blogtitle": $("#title").val(), "Blogcontent": $("#customized-buttonpane").html(), "Categoryid": $("#cate").val(), "Userid": uid, "keyValue": id },
        type: "post",
        success: function (data) {
            alert(data);
        }
    });
}