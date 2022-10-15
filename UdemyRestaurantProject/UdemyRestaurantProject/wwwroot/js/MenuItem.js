$(document).ready(function () {
    $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/MenuItem",
            "type": "GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "foodType.name", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function(data){
                    return `<div class="w-100 btn-group">
                                <a href="/Admin/MenuItems/upsert?id=${data}"
                                class="btn btn-success text-white mx-2"><i class="bi bi-pencil-square"></i></a>
                                <a href="/Admin/MenuItems/upsert?id=${data}"
                                class="btn btn-danger text-white mx-2"><i class="bi bi-trash3-fill"></i></a>
                            </div>`
            },
                "width": "30%"
            },
        ],
        "width":"100%"
    });
});