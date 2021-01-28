var movie = movie || {};

movie.delete = (id) => {
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
                    url: `/Movie/Delete/${id}`,
                    method: "GET",
                    contentType: 'json',
                    success: function (data) {
                        if (data.delMovie > 0) {
                            window.location.href = "/Movie/Index";
                        }
                    }
                });
            }
        }
    });
}

movie.dataTable = () => {
    $('#tbMovie').dataTable({
        "columnDefs": [
            {
                "targets": 0,
                "searchable": false,
            },
            {
                "targets": 3,
                "orderable": false,
                "searchable": false
            },
            {
                "targets": 4,
                "orderable": false,
                "searchable": false
            },
            {
                "targets": 5,
                "orderable": false,
                "searchable": false
            }
        ]
    }
    );
}
movie.changeFinish = (id, isStatus) => {
    bootbox.confirm({
        title: "Cảnh báo!!!",
        message: "<p class=\"text-danger\">Bạn có muốn thay đổi trạng thái của bộ phim này không?</p>",
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
                    url: `/Movie/ChangeIsFinish/${id}/${isStatus}`,
                    method: "GET",
                    contentType: 'json',
                    success: function (data) {
                        if (data.change > 0) {
                            bootbox.alert("<p class='text-success'>Thay đổi trạng thái thành công</p>")
                            window.location.href = "/Movie/Index";
                        }
                    }
                });
            }
        }
    });
}

movie.uploadFile = () => {
    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#imgShow').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]); // convert to base64 string
        }
    }

    $(".custom-file-input").change(function () {
        readURL(this);
    });
}

movie.select2 = () => {
    $('.js-example-basic-multiple').select2();
}

$(document).ready(function () {
    movie.dataTable();
    movie.uploadFile();
    movie.select2();
});