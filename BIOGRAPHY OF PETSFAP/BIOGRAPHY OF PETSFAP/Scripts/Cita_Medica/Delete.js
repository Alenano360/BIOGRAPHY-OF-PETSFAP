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
    $.ajax({
        type: "GET",
        url: '/Cita/setJsonS',
        success: function (response) {
            debugger
            var lol = response.replace("\"[", "'[").replace("\"", "'").replace(new RegExp("'", 'g'), "\"");
            var obj = $.parseJSON(lol);
            t.rows.add(obj).draw();
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

    function setJsonS(rows) {
        var json = '[';
        debugger
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
});

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
    $.ajax({
        type: "GET",
        url: '/Cita/setJsonM',
        success: function (response) {
            debugger
            var lol = response.replace("\"[", "'[").replace("\"", "'").replace(new RegExp("'", 'g'), "\"");
            var obj = $.parseJSON(lol);
            t.rows.add(obj).draw();
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
});
