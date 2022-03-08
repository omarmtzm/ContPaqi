
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

    LoadPage();
});

function LoadPage() {

    // Setup - add a text input to each footer cell
    $('#listaCocktail tfoot th').each(function () {
        var title = $(this).text();
        if (title != "") {
            $(this).html('<input type="text" class="busquedaControl" />');
        }

    });

    // DataTable
    var table = $('#listaCocktail').DataTable({
        initComplete: function () {
            var r = $('#listaCocktail tfoot tr');
            r.find('th').each(function () {
                $(this).css('padding', 8);
            });
            $('#listaCocktail thead').append(r);
            $('#search_0').css('text-align', 'center');
        },
        "columnDefs": [
            { "width": "5%", "targets": 0 },
           
        ]
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
     
    if (document.getElementById("mensaje").value != "") {
        ShowMessage(document.getElementById("mensaje").value, "Information");
    }

    if (document.getElementById("pvMensaje").value != "") {
        ShowMessage(document.getElementById("pvMensaje").value, "Information");
    }    
}

function SearchCocktailByName() {
    var cocktail = document.getElementById("cocktail").value;      

    var baseUrl = $('base').attr('href');
    var formData = new FormData();

    formData.append("cocktail", cocktail);

    var object = {};
    formData.forEach(function (value, key) {
        object[key] = value;
    });
    var json = JSON.stringify(object);

    var ajaxUrl = baseUrl + "Cocktail/ParcialReporteCocktail";

    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: formData,
        processData: false,
        contentType: false,
        success: function (response, status, xhr) {

            if (!response.result) {
                InformationMessage = response.message
                ShowMessage(response.message, "Information");
            }

            if (response.message!= "") {
                ShowMessage(response.message, "Information");
            }

            var table = $('#listaCocktail').DataTable();
            table.destroy();
            $('#listaCocktail tfoot tr').appendTo('#listaCocktail thead');
            $('#dvCocktaildetails').html(response);

            LoadPage();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log('Error en Ajax function SearchCocktailByName')
        }
    });
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