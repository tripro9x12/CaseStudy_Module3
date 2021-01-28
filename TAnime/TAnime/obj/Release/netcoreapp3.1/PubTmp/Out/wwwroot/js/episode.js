var episode = episode || {};

episode.delete = (id) => {
    bootbox.confirm({
        title: "Cảnh báo!!!",
        message: "<p class=\"text-danger\">Bạn có muốn xóa bộ phim này không?</p>",
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
                    url: `/Episode/Delete/${id}`,
                    method: "GET",
                    contentType: 'json',
                    success: function (data) {
                        if (data.delEpisode > 0) {
                            window.location.href = "/Episode/Index";
                        }
                    }
                });
            }
        }
    });
}

$(document).ready(function () {
    $('#tbEpisode').dataTable({
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