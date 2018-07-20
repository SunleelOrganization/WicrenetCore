(function () {
    $(function () {
        var validate = $("#myform").validate({
            debug: true, //调试模式取消submit的默认提交功能       
            focusInvalid: true,
            onkeyup: false,
            submitHandler: function (form) {   //表单提交句柄,为一回调函数，带一个参数：form   
                submitForm(form);
            },

            rules: {
                userName: {
                    required: true
                },
                userPwd: {
                    required: true,
                    rangelength: [5, 10]
                },
            },
            messages: {
                userName: {
                    required: "必填"
                },
                userPwd: {
                    required: "不能为空",
                    rangelength: "密码长度为5到10位"
                },
            }

        });
        var submitForm = function (form) {
                $.ajax({
                    type: "POST",
                    url: $(form).attr('action'),
                    data: $(form).serializeArray(),
                    success: function (data) {
                        var result = data;
                        if (result.error) {
                            swal(result.error);
                        }
                        else {
                            $.notify(result.succInfo);
                            window.location.href = result.returnUrl;
                        }
                    }
                })   
        }
    });
})();