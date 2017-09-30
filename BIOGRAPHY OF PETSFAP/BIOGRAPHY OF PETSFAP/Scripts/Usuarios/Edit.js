$(document).ready(function () {
    $('#editarUsuario').submit(function (e) {
        e.preventDefault();
        var ddlEmpleados = $('#Id_Empleado')[0];
        var empleado = ddlEmpleados.options[ddlEmpleados.selectedIndex].innerHTML;
        var ddlRoles = $('#Id_Rol')[0];
        var rol = ddlRoles.options[ddlRoles.selectedIndex].innerHTML;
        if (empleado == "") {
            $('#Empleado-error').show();
            $('#Empleado-error').text("El campo de Empleado es requerido");
        } else {
            $('#Empleado-error').hide();
        }
        if (rol == "") {
            $('#Rol-error').show();
            $('#Rol-error').text("El campo de Rol es requerido");
        } else {
            $('#Rol-error').hide();
        }
    });
});