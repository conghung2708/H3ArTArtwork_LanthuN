var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Moderator/report/GetAllReportArtwork' },
        "columns": [
            { data: 'reportArtworkID', "width": "25%" },
            { data: 'artworkID', "width": "25%" },
            { data: 'artwork.title', "width": "10%" },
            {
                data: 'artwork.imageUrl',
                "render": function (data) {
                    return `<img src="${data}" width="100px" height="auto">`;
                },
                width: "10%"
            },
            { data: 'applicationUser.fullName', "width": "10%" },
            {
                data: 'artwork.reportedConfirm',
                "render": function (data, type, row) {
                    if (data == 0 || data == null) {
                        return `<div class="w-75 btn-group" role="group">
                            <a onClick=Hide('${row.artworkID}') class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="bi bi-unlock-fill"></i>Unhidden</a>
                        </div>`;
                    } else {
                        return `<div class="w-75 btn-group" role="group">
                            <a onClick=Hide('${row.artworkID}')  class="btn btn-danger text-white" style="cursor:pointer; width:100px;"> <i class="bi bi-lock-fill"></i>Hidden</a>
                        </div>`;
                    }
                },
                "width": "25%"
            }
        ]
    });
}

function Hide(id) {
    $.ajax({
        type: "POST",
        url: '/Moderator/Report/Hide',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            } else {
                toastr.error(data.message);
            }
        },
        error: function () {
            toastr.error('An error occurred while processing your request.');
        }
    });
}
