$(document).ready(function () {
    $('#tablaUsuarios').DataTable();
    $('#liHome')[0].firstChild.className = '';
    $('#liUser')[0].firstChild.className = 'active';
    $('#liRole')[0].firstChild.className = '';
});