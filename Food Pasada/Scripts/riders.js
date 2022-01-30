$(document).ready(function () {
    $("#btn_add").click(function () {
        
        var DataForm = $("#FormData").serialize();

        $.ajax({
            type: "post",
            url: "/Admin/RiderCreate",
            data: DataForm,

            success: function () {
                Swal.fire("Information", "Successfully Save", "success").then((value) =>{
                    window.location.href = "/Admin/RiderTable";
                });   
            }

        })
    })
})

function GetUserId(ID) {
    var url = "/Admin/GetRidersId?id=" + ID;
    $("#Update").modal('show');

    $.ajax({
        type: "GET",
        url: url,

        success: function (response) {
            var obj = JSON.parse(response);

            $('#ids').val(obj.id);
            $('#fName').val(obj.full_name);
            $('#Address').val(obj.address);
            $('#number').val(obj.number);
            $('#email').val(obj.email);
            $('#username').val(obj.username);
            $('#password').val(obj.password);
        }
    })
}

$('#btn_update').click(function () {
    var DataUpdate = $('#FormDataUpdate').serialize();

    $.ajax({
        type: "POST",
        url: "/Admin/RiderUpdate",
        data: DataUpdate,

        success: function (response) {
            if (response.status == true) {
                Swal.fire("Information", "Data Successfully Update!", "success").then((value) => {
                    window.location.href = "/Admin/RiderTable";
                });
                
            }
            else {
                aler("Data Not Successfully Update!!");
                window.location.href = "/Admin/RiderTable";
            }
        }
    })
})

function GetId(id) {
    $('#id_del').val(id);
    $('#Delete').modal('show');
}

$('#btn_del').click(function () {
    var data_id = $('#id_del').val();

    $.ajax({
        type: "POST",
        url: "/Admin/RiderDelete",
        data: { id: data_id },

        success: function (response) {
            if (response.status == true) {
                Swal.fire("Information", "Information Successfull Deleted!", "success").then((value) => {
                    window.location.href = "/Admin/RiderTable";
                });
                
            }
            else {
                alert("Data Problem");
                window.location.href = "/Admin/RiderTable";
            }
        }
    })
})