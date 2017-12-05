function SumarColumna() {
    debugger
    var total = 0;

    var resultValS = $(tablaServicios).dataTable().api().column(4).data();
    for (var i = 0, len = resultValS.length; i < len; i++) {
        total += parseInt(resultValS[i]);
    }

    var resultValM = $(tablaMedicinas).dataTable().api().column(4).data();
    for (var i = 0, len = resultValM.length; i < len; i++) {
        total += parseInt(resultValM[i]);
    }
    $('#total').val(total);
}

$(document).ready(function () {
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
                    var newRow = [idservicio, servicio, parseInt(txtCantidad) + parseInt(rows[i][2]), lblPrecioServicio, parseInt(lblTotalServicio) + parseInt(rows[i][4])];
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
            } if (servicio != "" && txtCantidad != "") {
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
        } else if (servicio != "" && txtCantidad != "") {
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
        debugger
        table.row('.selected').remove().draw(false)
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
});
function setJsonS(rows) {
    var json = '[';

    for (var i = 0, len = rows.length; i < len; i++) {
        if (i == 0) {
            json += '{"Id_Cita":"0",' +
            '"Id_Servicio":"' + rows[i][0] + '"' +
            ',"Cantidad":"' + rows[i][2] + '"' +
            ',"Precio_Total":"' + rows[i][4] + '"' +
            ',"Id_Detalle_Servicio":"' + rows[i][5] + '"}';
        }
        else {
            json += ',{"Id_Cita":"0",' +
            '"Id_Servicio":"' + rows[i][0] + '"' +
            ',"Cantidad":"' + rows[i][2] + '"' +
            ',"Precio_Total_Servicio":"' + rows[i][4] + '"' +
            ',"Costo":"' + rows[i][3] + '"' +
            ',"Id_Detalle_Servicio":"' + rows[i][5] + '"}';
        }
    }
    json += ']';
    return json;
}
function setJsonM(rows) {
    var json = '[';
    debugger
    for (var i = 0, len = rows.length; i < len; i++) {
        if (i == 0) {
            json += '{"Id_Cita":"0",' +
            '"Id_Producto":"' + rows[i][0] + '"' +
            ',"Cantidad":"' + rows[i][2] + '"' +
            ',"Precio_Total":"' + rows[i][4] + '"' +
            ',"Id_Detalle_Medicina":"' + rows[i][5] + '"}';
        }
        else {
            json += ',{"Numero_Factura":"0",' +
            '"Id_Producto":"' + rows[i][0] + '"' +
            ',"Cantidad":"' + rows[i][2] + '"' +
            ',"Precio_Total":"' + rows[i][4] + '"' +
            ',"Id_Detalle_Medicina":"' + rows[i][5] + '"}';
        }
    }
    json += ']';
    return json;
}

$('#addCita').click(function () {
    var rowsS = $('#tablaServicios').dataTable().api().rows().data();
    var rowsM = $('#tablaMedicinas').dataTable().api().rows().data();
    rowsS.toArray();
    rowsM.toArray();
    $.ajax({
        type: "POST",
        url: "/Cita/Detalles?rowsS=" + setJsonS(rowsS.toArray())+"&&rowsM=" + setJsonM(rowsM.toArray()),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            debugger
            $("#crearFactura").submit();
        },
        error: function (response) {
            debugger
            alert('Error: ' + xhr.statusText);
        }
    });
});

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

    $('#addRow').on('click', function () {
        debugger
        var ddlProductos = $('#ddl_Productos')[0];
        var producto = ddlProductos.options[ddlProductos.selectedIndex].innerHTML;
        var idproducto = ddlProductos.options[ddlProductos.selectedIndex].value;
        var txtCantidad = $('#txt_cantidad')[0].value;
        var lblPrecio = $('#lbl_precioUnitario')[0].innerHTML;
        var lblTotalProducto = $('#lbl_totalProducto')[0].innerHTML;
        var idDetalle = 0;
        var rows = $(tablaMedicinas).dataTable().api().rows().data();
        if (rows.length >= 1) {
            for (var i = 0, len = rows.length; i < len; i++) {
                if (rows[i][0] == idproducto) {
                    var newRow = [idproducto, producto, parseInt(txtCantidad) + parseInt(rows[i][2]), lblPrecio, parseInt(lblTotalProducto) + parseInt(rows[i][4])];
                    for (x = 0, l = $('#tablaMedicinas').dataTable().api().rows()[0].length; x < l; x++) {
                        if ($('#tablaMedicinas').dataTable().api().row(x).data()[0] == idproducto) {
                            $(tablaMedicinas).dataTable().api().row(x).data(newRow).draw();
                            $("#alerta_Error").hide();
                            $('#ddl_Productos').val("");
                            $("#lbl_precioUnitario")[0].innerText = "";
                            $("#lbl_totalProducto")[0].innerText = "";
                            $("#txt_cantidad").val("");
                            SumarColumna();
                            return;
                        }
                    }
                }
            } if (producto != "" && txtCantidad != "") {
                t.row.add([
                    idproducto,
                    producto,
                    txtCantidad,
                    lblPrecio,
                    lblTotalProducto,
                    idDetalle
                ]).draw(false);
                $("#alerta_Error").hide();
                $('#ddl_Productos').val("");
                $("#lbl_precioUnitario")[0].innerText = "";
                $("#lbl_totalProducto")[0].innerText = "";
                $("#txt_cantidad").val("");
            } else {
                $("#alerta_Error").show();

            }
        } else if (producto != "" && txtCantidad != "") {
            t.row.add([
                idproducto,
                producto,
                txtCantidad,
                lblPrecio,
                lblTotalProducto,
                idDetalle
            ]).draw(false);
            $("#alerta_Error").hide();
            $('#ddl_Productos').val("");
            $("#lbl_precioUnitario")[0].innerText = "";
            $("#lbl_totalProducto")[0].innerText = "";
            $("#txt_cantidad").val("");
        } else {
            $("#alerta_Error").show();
        }
        SumarColumna();
    });

    var table = $('#tablaMedicinas').DataTable();
    $('#tablaMedicinas tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
    $('#removeRow').click(function () {
        table.row('.selected').remove().draw(false)
        SumarColumna();
    });

    $('#ddl_Productos').change(function () {
        $('#txt_cantidad').val("");
        $('#alerta_Error1').hide();
        var idProducto = $(ddl_Productos)[0].value;
        if (idProducto != "") {
            $.ajax({
                type: "POST",
                url: '/Facturas/SelectPrecio',
                data: '{id: "' + idProducto + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#lbl_precioUnitario")[0].innerText = response;
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
            $("#lbl_precioUnitario")[0].innerText = "0";
            $("#lbl_totalProducto")[0].innerText = "0";
            $("#txt_cantidad").val("");
        }

    });

    $('#txt_cantidad').change(function () {
        var idProducto = $(ddl_Productos)[0].value;
        if (idProducto != "") {
            var PrecioUnitario = $("#lbl_precioUnitario").text();
            var TotalProducto = $("#lbl_totalProducto").text();
            var Cantidad = $("#txt_cantidad").val();
            TotalProducto = PrecioUnitario * Cantidad;
            $("#lbl_totalProducto")[0].innerText = TotalProducto;
        } else {
            $("#alerta_Error1").show();
        }

    });

});
