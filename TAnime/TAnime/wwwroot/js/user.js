var user = user || {};

user.delete = (id) => {
    bootbox.confirm({
        title: "Cảnh báo!!!",
        message: "<p class=\"text-danger\">Bạn có muốn xóa email này không?</p>",
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
                    url: `/User/Delete/${id}`,
                    method: "GET",
                    contentType: 'JSON',
                    success: function (data) {
                        if (data.result != 0) {
                            bootbox.alert("<p class='text-success'>Bạn đã xóa email này!!!</p>");
                            window.location.href = "/User/Index";
                        }
                    }
                });
            }
        }
    });
}


$(document).ready(function () {
    $('#iduser').DataTable(
        {
            "columnDefs": [
                {
                    "targets": 1,
                    "orderable": false,
                    "searchable": false
                },
                {
                    "targets": 7,
                    "orderable": false,
                    "searchable": false
                }
            ]
        }
    );
});