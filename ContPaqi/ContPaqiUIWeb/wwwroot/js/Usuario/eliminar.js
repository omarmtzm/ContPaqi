let usuario = new Object();

$(document).ready(function () {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "rtl": false,
        "positionClass": "toast-bottom-full-width",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": 300,
        "hideDuration": 1000,
        "timeOut": 5000,
        "extendedTimeOut": 1000,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    if (document.getElementById("hdEstatus").value == "False") {
        document.getElementById("estatus").checked = false;
    } else {
        document.getElementById("estatus").checked = true;
    }
});

function EliminaUsuario() {
    usuario.usuarioID = document.getElementById("usuarioID").value;

    var formData = new FormData();
    formData.append("usuarioID", usuario.usuarioID);

    var baseUrl = $('base').attr('href');
    var url = baseUrl + "Usuario/Delete";
    var method = "POST"
    var sendInfo = formData
    var dataType = "Json"
    var async = false

    var respuesta = callAjax(url, method, sendInfo, dataType, async)

    if (respuesta != null) {
        if (!respuesta.result) {
            ShowMessage(respuesta.message, "Error")
        } else {
            window.location.href = baseUrl + "Usuario/Index";
        }
    }
}

function callAjax(url, method, sendInfo, dataType, async) {
    var ajaxResponse

    $.ajax({
        type: method,
        url: url,
        data: sendInfo,
        contentType: false,
        dataType: dataType,
        async: async,
        processData: false,
        cache: false,
        success: function (data) {
            ajaxResponse = data;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            ShowMessage("Estado: " + ajaxOptions + ", error EliminaUsuario: " + thrownError, "Error")
        }
    });

    return ajaxResponse
}

function ShowMessage(mensaje, tipoMensaje) {

    switch (tipoMensaje) {
        case "Information":
            toastr.info(mensaje, "Information:")
            break
        case "Failure":
            toastr.error(mensaje, "Failure:")
            break
        case "Error":
            toastr.error(mensaje, "Error:")
            break
        case "Success":
            toastr.success(mensaje, "Success:")
            break
        default:
            toastr.info(mensaje, "Information:")
            break
    }

    document.getElementById("mensaje").value = "";
}