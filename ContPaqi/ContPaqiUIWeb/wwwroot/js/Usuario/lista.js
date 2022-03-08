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

    if (document.getElementById("mensaje").value != "") {
        ShowMessage(document.getElementById("mensaje").value, "Information");
    }

    // Setup - add a text input to each footer cell
    $('#listaUsuarios tfoot th').each(function () {
        var title = $(this).text();
        if (title!= "") {
            $(this).html('<input type="text" class="busquedaControl" />');
        }
        
    });

    // DataTable
    var table = $('#listaUsuarios').DataTable({
        initComplete: function () {
            var r = $('#listaUsuarios tfoot tr');
            r.find('th').each(function () {
                $(this).css('padding', 8);
            });
            $('#listaUsuarios thead').append(r);
            $('#search_0').css('text-align', 'center');
        }
    });

    // Apply the search
    table.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });
});

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


function EditaUsuario(idUsuario) {
    var baseUrl = $('base').attr('href');
    window.location.href = baseUrl + "Usuario/Edit?id=" + idUsuario;
}

function EliminaUsuario(idUsuario) {
    var baseUrl = $('base').attr('href');
    window.location.href = baseUrl + "Usuario/Delete?id=" + idUsuario;
}