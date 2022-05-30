

let usuario = new Object();
let AddedComponents = [];

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

});

function CreaUsuario() {
    usuario.nombreUsuario = document.getElementById("nombreUsuario").value;
    usuario.codigoUsuario = document.getElementById("codigoUsuario").value;
    usuario.password = document.getElementById("password").value;
    usuario.estatus = document.getElementById("estatus").checked;
    usuario.correoElectronico = document.getElementById("correoElectronico").value;

    var formData = new FormData();
    formData.append("nombreUsuario", usuario.nombreUsuario);
    formData.append("codigoUsuario", usuario.codigoUsuario);
    formData.append("password", usuario.password);
    formData.append("estatus", usuario.estatus);
    formData.append("correoElectronico", usuario.correoElectronico);

    var baseUrl = $('base').attr('href');
    var url = baseUrl + "Usuario/Create";
    var method = "POST"
    var sendInfo = formData
    var dataType = "Json"
    var async = false

    var respuesta = callAjax(url, method, sendInfo, dataType, async)

    if (respuesta != null) {
        if (!respuesta.result) {
            ShowMessage(respuesta.message, "Error")
        } else {
            ShowMessage(respuesta.message, "Success")
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
            ShowMessage("Estado: " + ajaxOptions + ", error Crea Usuario: " + thrownError, "Error")
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

function mostrarPassword() {
    var cambio = document.getElementById("password");
    if (cambio.type == "password") {
        cambio.type = "text";
        $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
    } else {
        cambio.type = "password";
        $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
    }
}