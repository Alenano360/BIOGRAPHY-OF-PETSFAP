$(document).ready(function () {
    $('#tablaPersonas').DataTable();
    var rol = $('#rol').val();
    if (rol == "Cajero") {
        $('#liHome')[0].firstChild.className = '';
        $('#liPer')[0].firstChild.className = 'active';
        $('#liProd')[0].firstChild.className = '';
    }
    else if (rol == "Administrador") {
        $('#liHome')[0].firstChild.className = '';
        $('#liPer')[0].firstChild.className = 'active';
        $('#liUser')[0].firstChild.className = '';
        $('#liMedi')[0].firstChild.className = '';
        $('#liPaci')[0].firstChild.className = '';
        $('#liProd')[0].firstChild.className = '';
        $('#liCita')[0].firstChild.className = '';
    }
    else if (rol == "Doctor") {
        $('#liHome')[0].firstChild.className = '';
        $('#liPer')[0].firstChild.className = 'active';
        $('#liMedi')[0].firstChild.className = '';
        $('#liPaci')[0].firstChild.className = '';
        $('#liProd')[0].firstChild.className = '';
        $('#liCita')[0].firstChild.className = '';

    }
    else if (rol == "Inventario") {
        $('#liHome')[0].firstChild.className = '';
        $('#liPer')[0].firstChild.className = 'active';
        $('#liProd')[0].firstChild.className = '';
    }
});