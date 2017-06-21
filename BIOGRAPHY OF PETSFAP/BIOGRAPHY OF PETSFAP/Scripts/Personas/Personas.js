$(document).ready(function () {
    $('#tablaPersonas').DataTable();
    $('#liHome')[0].firstChild.className = '';
    $('#liUser')[0].firstChild.className = '';
    $('#liRole')[0].firstChild.className = '';
    $('#liPer')[0].firstChild.className = 'active';
    $('#liMedi')[0].firstChild.className = '';
    $('#liPaci')[0].firstChild.className = '';
});