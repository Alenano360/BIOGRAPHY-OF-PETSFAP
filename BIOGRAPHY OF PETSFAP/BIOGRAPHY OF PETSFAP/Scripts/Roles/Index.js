$(document).ready(function () {
    $('#tablaRoles').DataTable();
    $('#liHome')[0].firstChild.className = '';
    $('#liUser')[0].firstChild.className = '';
    $('#liRole')[0].firstChild.className = 'active';
});