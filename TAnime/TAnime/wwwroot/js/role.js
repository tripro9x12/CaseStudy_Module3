var role = role || {};

role.delete = (id) => {
    bootbox.confirm({
        title: "Cảnh báo!!!",
        message: "<p class=\"text-danger\">Bạn có muốn xóa quyền này không?</p>",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Không'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Có'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: `/Role/Delete/${id}`,
                    method: "GET",
                    contentType: 'JSON',
                    success: function (data) {
                        if (data.result != 0) {
                            bootbox.alert("<p class='text-success'>Bạn đã xóa quyền này!!!</p>");
                            window.location.href = "/Role/Index";
                        }
                    }
                });
            }
        }
    });
}