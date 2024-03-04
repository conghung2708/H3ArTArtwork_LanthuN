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
            { data: 'applicationUser.fullName', "width": "10%" }, // Accessing displayOrder within the category object
            {
                data: 'artworkID',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                      <a onClick=Delete('/creator/artwork/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}