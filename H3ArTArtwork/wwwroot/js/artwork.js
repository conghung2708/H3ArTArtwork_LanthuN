﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/creator/artwork/getall' },
        "columns": [
            {
                data: 'title',
                "render": function (data, type, row) {
                    if (row.isPremium) {
                        return `${data} <span class="badge rounded-pill bg-info text-dark">Premium</span>`;
                    } else {
                        return data;
                    }
                },
                "width": "15%"
            },
            {
                data: 'imageUrl',
                "render": function (data) {
                    return `<img src="${data}" width="100px" height="auto">`;
                },
                width: "10%"
            },
            { data: 'description', "width": "20%" },
            { data: 'applicationUser.fullName', "width": "10%" },
            { data: 'price', "width": "1%" },
            { data: 'category.categoryName', "width": "5%" }, // Accessing displayOrder within the category object //need to fix here
            // Corrected here
            {
                data: 'isBought',
                "render": function (data, type, row) {
                    if (data) {
                        return `<div class="text-center"><a href="/Admin/Order/GetOrderDetail?artworkId=${row.artworkId}" class="btn btn-success mx-auto">View Order</a></div>`;
                    } else {
                        return `<div class="w-100 btn-group" role="group">
                            <a href="/creator/artwork/upsert?id=${row.artworkId}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                            <a onClick="Delete('/creator/artwork/delete/${row.artworkId}')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                        </div>`;
                    }
                },
                "width": "15%"
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