function AjaxPost(formData) {
    var ajaxConfig = {
        type: "post",
        url: "/Qualiteatime/Create",
        data: new FormData(formData),
        success: function (response) {
            if (response.status == true) {
               
                Swal.fire("Information", "New Data Added", "success").then((value) => {
                    window.location.href = "/QualiteaTime/Product";
                });
                
            }
            else {
                alert("Complete the Form");
                window.location.href = "/QualiteaTime/Product";
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
        url: "/QualiteaTime/Updates",
        data: new FormData(formData),
        success: function (response) {
            if (response.status == true) {
                Swal.fire("Information", "Data Successfully Update", "success").then((value) => {
                    window.location.href = "/QualiteaTime/Product";
                });
            }
            else {
                alert("Something Error");
                window.location.href = "/QualiteaTime/Product";
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
    var url = "/QualiteaTime/Delete";
    var data_id = $('#ids').val();

    $.ajax({
        type: "post",
        url: url,
        data: { id: data_id },
        success: function (response) {
            if (response.status == true) {
                Swal.fire("Information", "Product Delete Successfull", "success").then((value) => {
                    window.location.href = "/QualiteaTime/Product";
                });
            }
            else {
                alert("Something error");
                window.location.href = "/QualiteaTime/Product";
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