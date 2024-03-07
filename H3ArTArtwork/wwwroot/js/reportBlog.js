var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Moderator/report/GetAllReportBlog' },
        "columns": [
            { data: 'reportBlogID', "width": "25%" },
            { data: 'blogID', "width": "25%" },
            { data: 'blog.title', "width": "10%" },
            {
                data: 'blog.imageUrl',
                "render": function (data) {
                    return `<img src="${data}" width="100px" height="auto">`;
                },
                width: "10%"
            },
            { data: 'applicationUser.fullName', "width": "10%" }, // Accessing displayOrder within the category object
            {
                data: 'blogID',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                      <a onClick=Delete('/moderator/report/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
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