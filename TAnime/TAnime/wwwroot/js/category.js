var category = category || {};

category.delete = (id) => {
    bootbox.confirm({
        title: "Cảnh báo!!!",
        message: "<p class=\"text-danger\">Bạn có muốn xóa thể loại phim này không?</p>",
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
                    url: `/Category/Delete/${id}`,
                    method: "GET",
                    contentType: 'json',
                    success: function (data) {
                        if (data.delCategory > 0) {
                            window.location.href = "/Category/Index";
                        }
                    }
                });
            }
        }
    });
}

$(document).ready(function () {
    $('#tbCategory').dataTable({
        "columnDefs": [
            {
                "targets": 0,
                "searchable": false,
            },
            {
                "targets": 2,
                "orderable": false,
                "searchable": false
            },
        ]
    }
    );
});