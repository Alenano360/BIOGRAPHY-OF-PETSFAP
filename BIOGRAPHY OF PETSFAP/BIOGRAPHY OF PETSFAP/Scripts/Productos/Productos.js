$(document).ready(function () {
    $('#tablaProductos').DataTable();
    var rol = $('#rol').val();
    if (rol == "Cajero") {
        $('#liPer')[0].firstChild.className = '';
        $('#liProd')[0].firstChild.className = '';
        $('#liFac')[0].firstChild.className = '';
    }
    else if (rol == "Administrador") {
        $('#liPer')[0].firstChild.className = '';
        $('#liUser')[0].firstChild.className = '';
        $('#liPaci')[0].firstChild.className = '';
        $('#liProd')[0].firstChild.className = 'active';
        $('#liCita')[0].firstChild.className = '';
        $('#liCat')[0].firstChild.className = '';
        $('#liFac')[0].firstChild.className = '';
    }
    else if (rol == "Doctor") {
        $('#liPer')[0].firstChild.className = '';
        $('#liPaci')[0].firstChild.className = '';
        $('#liProd')[0].firstChild.className = 'active';
        $('#liCita')[0].firstChild.className = '';
        $('#liCat')[0].firstChild.className = '';
    }
    else if (rol == "Inventario") {
        $('#liProd')[0].firstChild.className = 'active';
    }
});