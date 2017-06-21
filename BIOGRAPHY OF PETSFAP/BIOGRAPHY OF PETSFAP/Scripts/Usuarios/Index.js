$(document).ready(function () {
    $('#tablaUsuarios').DataTable();
    $('#liHome')[0].firstChild.className = '';
    $('#liUser')[0].firstChild.className = 'active';
    $('#liRole')[0].firstChild.className = '';
    $('#liPer')[0].firstChild.className = '';
    $('#liMedi')[0].firstChild.className = '';
    $('#liPaci')[0].firstChild.className = '';
});