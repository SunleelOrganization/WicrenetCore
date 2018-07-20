$(function () {

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

    var modalInit = new ModalInit();
    modalInit.Init();

});
var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_departments').bootstrapTable({
            url: '/Home/GetUsersAsync',
            method: 'post',
            dataType: "json",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            toolbar: '#toolbar',
            striped: true,
            cache: false,
            pagination: true,
            sortable: false,
            sortOrder: "asc",
            queryParams: oTableInit.queryParams,
            sidePagination: "server",
            pageNumber: 1,
            pageSize: 10,
            pageList: [10, 25, 50, 100],
            strictSearch: false,
            showColumns: true,
            minimumCountColumns: 2,
            clickToSelect: true,
            height: 500,
            uniqueId: "ID",
            showToggle: true,
            cardView: false,
            detailView: false,
            columns: [{
                checkbox: true
            }, {
                field: 'userName',
                title: '用户名'
            }, {
                field: 'email',
                title: '邮箱'
            }, {
                field: 'age',
                title: '年龄'
            }, {
                field: 'creationTime',
                title: '注册时间',
                formatter: function (value, row, index) {
                    if (value == null) {
                        return "";
                    }
                    var offlineTimeStr = moment(row.creationTime).format("YYYY-M-DD HH:mm:ss");
                    return offlineTimeStr;
                }
            },]
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            userName: $("#search_userName").val(),
            email: $("#search_email").val()
        };
        return temp;
    };
    return oTableInit;
};

var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        //初始化页面上面的按钮事件
        $("#btn_query").bind('click', function () {
            $('#tb_departments').bootstrapTable('refresh');
        });
        $("#btn_add").bind('click', function () {
            $("#title").text("创建用户");
            $("#id").val(0);
            $("#userName").val("");
            $("#email").val("");
            $("#age").val("");
            $("#myModal").modal("show");
        });
        $("#btn_edit").bind('click', function () {
            var data = $('#tb_departments').bootstrapTable('getSelections');
            if (data.length == 0) { $.notify('请选择数据!'); return; }
            if (data.length > 1) { $.notify('请选择一条数据!'); return; }
            $.ajax({
                type: "Get",
                url: "/Home/ViewUser?id=" + data[0].id + "&_t=" + new Date().getTime(),
                success: function (data) {
                    $("#id").val(data.id);
                    $("#userName").val(data.userName);
                    $("#email").val(data.email);
                    $("#age").val(data.age);
                    $("#title").text("编辑用户");
                    $("#myModal").modal("show");

                }
            });
        });

        $("#btn_delete").bind('click', function () {
            var data = $('#tb_departments').bootstrapTable('getSelections');
            if (data.length == 0) { $.notify('请选择数据!'); return; }
            if (data.length > 1) { $.notify('请选择一条数据!'); return; }
            swal({
                title: "你确定吗?",
                text: "此用户将被删除!",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            }).then((willDelete) => {
                    if (willDelete) {
                        $.ajax({
                            type: "Get",
                            url: "/Home/DeleteUserById?id=" + data[0].id,
                            success: function () {
                                $('#tb_departments').bootstrapTable('refresh');                             
                                $.notify('删除成功!');
                            },
                            fail: function () {
                                $.notify('删除失败!');
                            }
                        })
                    }
                });

        });
    };

    return oInit;
};

var ModalInit = function () {
    var oInit = new Object();
    oInit.Init = function () {
        var validate = $("#myform").validate({
            focusInvalid: true,
            onkeyup: false,
            submitHandler: function (form) {   //表单提交句柄,为一回调函数，带一个参数：form   
                submitForm(form);
            },

            rules: {
                userName: {
                    required: true
                },
                email: {
                    required: true,
                },
                age: {
                    required: true,
                },
            },
            messages: {
                userName: {
                    required: "必填"
                },
                email: {
                    required: "不能为空",
                },
                age: {
                    required: "不能为空",
                },
            }

        });
        var submitForm = function (form) {
            $.ajax({
                type: "POST",
                url: $(form).attr('action'),
                data: $(form).serializeArray(),
                success: function (data) {
                    $("#myModal").modal('hide');
                    $('#tb_departments').bootstrapTable('refresh');
                    $.notify('修改成功!');
                }
            })
        }
    }
    return oInit;
};