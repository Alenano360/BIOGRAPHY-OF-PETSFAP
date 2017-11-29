$(document).ready(function () {
    var t = $('#tablaMedicinas').DataTable({
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
            },
            {
                "targets": [2],
            },
            {
                "targets": [3],
            },
            {
                "targets": [4],
            }
        ]
    });
    var t = $('#tablaServicios').DataTable({
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
            },
            {
                "targets": [2],
            },
            {
                "targets": [3],
            },
            {
                "targets": [4],
            }
        ]
    });
});

$(document).ready(function () {
    var table = $('#tablaServicios').DataTable();
    $('#tablaServicios tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
    $('#removeRowServicio').click(function () {
        if (table.row('.selected').hasClass != "noEliminar") {
            table.row('.selected').remove().draw(false)

        }
    });
});

$('#addRowServicio').on('click', function () {
        debugger
        var ddlServicio = $('#ddl_Servicios')[0];
        var servicio = ddlServicio.options[ddlServicio.selectedIndex].innerHTML;
        var idservicio = ddlServicio.options[ddlServicio.selectedIndex].value;
        var txtCantidad = $('#txt_cantidadServicio')[0].value;
        var lblPrecioServicio = $('#lbl_precioServicio')[0].innerHTML;
        var lblTotalServicio = $('#lbl_totalServicio')[0].innerHTML;
        var idDetalle = 0;
        var rows = $(tablaServicios).dataTable().api().rows().data();
        if (rows.length >= 1) {
            for (var i = 0, len = rows.length; i < len; i++) {
                if (rows[i][0] == idservicio) {
                    var newRow = [idservicio, servicio, parseInt(txtCantidad) + parseInt(rows[i][2]), lblPrecioServicio, parseInt(lblTotalServicio) + parseInt(rows[i][3]), rows[i][5]];
                    for (x = 0, l = $('#tablaServicios').dataTable().api().rows()[0].length; x < l; x++) {
                        if ($('#tablaServicios').dataTable().api().row(x).data()[0] == idservicio) {
                            $(tablaServicios).dataTable().api().row(x).data(newRow).draw();
                            $("#alerta_Error").hide();
                            $('#ddl_Servicios').val("");
                            $("#lbl_precioServicio")[0].innerText = "";
                            $("#lbl_totalServicio")[0].innerText = "";
                            $("#txt_cantidadServicio").val("");
                            SumarColumna();
                            return;
                        }
                    }
                }
            }if (servicio != "" && txtCantidad != "") {
                t.row.add([
                    idservicio,
                    servicio,
                    txtCantidad,
                    lblPrecioServicio,
                    lblTotalServicio,
                    idDetalle
                ]).draw(false);
                $("#alerta_Error").hide();
                $('#ddl_Servicios').val("");
                $("#lbl_precioServicio")[0].innerText = "";
                $("#lbl_totalServicio")[0].innerText = "";
                $("#txt_cantidadServicio").val("");
            } else {
                $("#alerta_Error").show();

            }
        }else if (servicio != "" && txtCantidad != "") {
            t.row.add([
                idservicio,
                servicio,
                txtCantidad,
                lblPrecioServicio,
                lblTotalServicio,
                idDetalle
            ]).draw(false);
            $("#alerta_Error").hide();
            $('#ddl_Servicios').val("");
            $("#lbl_precioServicio")[0].innerText = "";
            $("#lbl_totalServicio")[0].innerText = "";
            $("#txt_cantidadServicio").val("");
        } else {
            $("#alerta_Error").show();
        }
        SumarColumna();
    });
$('#ddl_Servicios').change(function () {
    $('#txt_cantidadServicio').val("");
    $('#alerta_Error1').hide();
    var idServicio = $(ddl_Servicios)[0].value;
    if (idServicio != "") {
        $.ajax({
            type: "POST",
            url: '/Cita/SelectPrecioServicio',
            data: '{id: "' + idServicio + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#lbl_precioServicio")[0].innerText = response;
            },
            failure: function (response) {
                debugger
                alert("Fallo");
            },
            error: function (response) {
                debugger
                alert("Error");
            }
        });
    } else {
        $("#lbl_precioServicio")[0].innerText = "0";
        $("#lbl_totalServicio")[0].innerText = "0";
        $("#txt_cantidadServicio").val("");
    }

});
function SumarColumna() {
    debugger
    var total = 0;
    var resultVal = $(tablaServicios).dataTable().api().column(4).data();
    for (var i = 0, len = resultVal.length; i < len; i++) {
        total += parseInt(resultVal[i]);
    }
    $('#lbl_totalServicio').val(total);
}
$('#txt_cantidadServicio').change(function () {
    var idServicio = $(ddl_Servicios)[0].value;
    if (idServicio != "") {
        var PrecioServicio = $("#lbl_precioServicio").text();
        var TotalServicio = $("#lbl_totalServicio").text();
        var CantidadS = $("#txt_cantidadServicio").val();
        TotalServicio = PrecioServicio * CantidadS;
        $("#lbl_totalServicio")[0].innerText = TotalServicio;
    } else {
        $("#alerta_Error1").show();
    }

});

$('#addCita').click(function () {
    var rows = $('#tablaServicios').dataTable().api().rows().data();
    rows.toArray();
    $.ajax({
        type: "POST",
        url: "/Cita/DetalleServicio?rows=" + setJson(rows.toArray()),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            debugger
            $("#crearCita").submit();
        },
        error: function (response) {
            debugger
            alert('Error: ' + xhr.statusText);
        }
    });
});

function setJson(rows) {
    var json = '[';

    for (var i = 0, len = rows.length; i < len; i++) {
        if (i == 0) {
            json += '{"Numero_Factura":"0",' +
            '"Id_Producto":"' + rows[i][0] + '"' +
            ',"Cantidad":"' + rows[i][2] + '"' +
            ',"Precio_Total_Producto":"' + rows[i][4] + '"' +
            ',"Precio_Unitario":"' + rows[i][3] + '"' +
            ',"Id_Detalle":"' + rows[i][5] + '"}';
        }
        else {
            json += ',{"Numero_Factura":"0",' +
            '"Id_Producto":"' + rows[i][0] + '"' +
            ',"Cantidad":"' + rows[i][2] + '"' +
            ',"Precio_Total_Producto":"' + rows[i][4] + '"' +
            ',"Precio_Unitario":"' + rows[i][3] + '"' +
            ',"Id_Detalle":"' + rows[i][5] + '"}';
        }
    }
    json += ']';
    return json;
}
