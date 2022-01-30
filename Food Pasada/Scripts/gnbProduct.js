function AjaxPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/GnB/Create",
        data: new FormData(formData),
        success: function (response) {
            if (response.status == true) {
                Swal.fire("Information", "New Product Save", "success").then((value) => {
                    window.location.href = "/GnB/Product";
                });
                
            }
            else {
                alert("Semething missing!");
                window.location.href = "/GnB/Product";
            }
        }
    }

    if ($(formData).attr('enctype') == 'multipart/form-data') {
        ajaxConfig['contentType'] = false;
        ajaxConfig['processData'] = false;
    }

    $.ajax(ajaxConfig);
    return false;
}

function ajaxPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/GnB/Updates",
        data: new FormData(formData),
        success: function (response) {
            if (response.status == true) {
               
                Swal.fire("Information", "Data Successfully Update", "success").then((value) => {
                    window.location.href = "/GnB/Product";
                });
            }
            else {
                alert("Error!");
                window.location.href = "/GnB/Product";
            }
        }
    }
    if ($(formData).attr('enctype') == 'multipart/form-data') {
        ajaxConfig['contentType'] = false;
        ajaxConfig['processData'] = false;
    }
    $.ajax(ajaxConfig);
    return false;
}


function Del_id(id) {
    $('#ids').val(id);
    $('#Delete').modal('show');
}

$('#btn_del').click(function () {
    var data_id = $('#ids').val();

    $.ajax({
        type: "post",
        url: "/GnB/Delete",
        data: { id: data_id },
        success: function (response) {
            if (response.status == true) {
                
                Swal.fire("Information", "Item Deleted", "success").then((value) => {
                    window.location.href = "/GnB/Product";
                });
            }
            else {
                alert("Error");
                window.location.href = "/GnB/Product";
            }
        }
    })
})



function ShowImage(uploader, previewImage) {
    if (uploader.files && uploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(uploader.files[0]);
    }
}

function ShowImages(uploader, previewImages) {
    if (uploader.files && uploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImages).attr('src', e.target.result);
        }
        reader.readAsDataURL(uploader.files[0]);
    }
}