var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable()
{
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns":
            [
            { "data": "title", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role = "group">
                        <a id="editar-btn" href="/Admin/Product/Upsert?id=${data}"  
                        class= "btn mx-2"><i class="bi bi-pen"></i> Editar</a>

                          <a id="remover-btn" onClick=Delete('/Admin/Product/Delete/${data}')
                           class= "btn mx-2"> <i class="bi bi-trash3"></i> Remover</a>
                    </div >
                    `
                },
                "width": "15%"
            }
            ]
    });  
}

function Delete(url)
{
    Swal.fire({
        title: 'Tem a certeza ?',
        text: "Você não poderá desfazer isto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Remover'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);

                    }
                    else {
                        toastr.error(data.message);
                    }
                }
                })
        }
    })
}