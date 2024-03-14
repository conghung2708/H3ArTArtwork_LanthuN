﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/creator/artwork/getall' },
        "columns": [
            { data: 'title', "width": "20%" },  // Adjusted width to 20%
            { data: 'description', "width": "30%" },  // Adjusted width to 30%
            { data: 'applicationUser.fullName', "width": "15%" },  // Adjusted width to 15%
            { data: 'price', "width": "10%" },  // Adjusted width to 10%
            { data: 'category.categoryName', "width": "15%" },  // Adjusted width to 15%
            {
                data: 'artworkId',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                     <a href="/creator/artwork/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                     <a onClick=Delete('/creator/artwork/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "10%"  // Adjusted width to 10%
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
                    if (data.success) {
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, error) {
                    dataTable.ajax.reload();
                    toastr.error(xhr.responseJSON.message);
                }
            })
        }
    })
}