function RecursoController(contenedor) {
    this.SystemType = 'RecursoController';
    var disponibilidadController = new DisponibilidadController(contenedor);
    var excepcionController = new ExcepcionController(contenedor);
    var recurso;
    var api = app.api.recursos;
    
    var limpiarForm = function() {
        $("#recurso-id, #recurso-foto, #recurso-nombre, #recurso-especialidad, #recurso-email", contenedor).val("");
        excepcionController.limpiarPantalla();
    };

    this.inicializar = function () {
        $("#crear-recurso", contenedor).click(mostrarCrear);
        $("#recursos-formulario-grabar", contenedor).click(grabar);
        $("#recursos-formulario-cancelar", contenedor).click(cancelar);
        
        $('#recurso-tabs').tabs({
            fx: { opacity: 'toggle' },
            select: tabSelected
        });
        $('.tabs li').hide();

        disponibilidadController.inicializar();
        excepcionController.inicializar();
    };

    var tabSelected = function (e, ui) {
        switch (ui.index) {
            case 0:
            case 1:

                break;
            case 2:                
                break;
        }
    };
        
    var cancelar = function () {
        if (disponibilidadController.datosSinGuardar || excepcionController.datosSinGuardar)
            app.ventanaDeConfirmacion({ description: "Hay cambios sin guardar. \n\n Desea descartarlos?",
                onAccept: function () {
                    cambiarAVistaDeLista();
                    excepcionController.datosSinGuardar = disponibilidadController.datosSinGuardar = false;
                }
            });
            else 
                cambiarAVistaDeLista();
    };

    var editar = function (recursoId) {
        var recurso = recursos.getById(recursoId);
        if (recurso) {
            limpiarForm();
            $("#recurso-id", contenedor).val(recurso.id);
            $("#recurso-nombre", contenedor).val(recurso.nombre);
            $("#recurso-especialidad", contenedor).val(recurso.especialidad);
            $("#recurso-email", contenedor).val(recurso.email);
            $("#recurso-foto", contenedor).val(recurso.foto);
            $('#recursos-confirmar-borrado', contenedor).hide();
            $('#recursos-contenedor', contenedor).show();
            cambiarAVistaDetalle();
        }
    };

    var mostrarCrear = function () {
        limpiarForm();
        $('#recursos-contenedor', contenedor).show();
        $('#recursos-confirmar-borrado', contenedor).hide();
        //cambiarAVistaDetalle();
    };

    var grabar = function () {
        var id = $("#recurso-id", contenedor).val();
        var recurso = {
            id: id == '' ? 0 : parseInt(id),
            nombre: $("#recurso-nombre", contenedor).val(),
            especialidad: $("#recurso-especialidad", contenedor).val(),
            habilitado: true,
            foto: $("#recurso-foto", contenedor).val(),
            email: $("#recurso-email", contenedor).val()
        };

        api.grabarRecurso(recurso, function(data) {
            recursos.push(data);
    //        mostrarListaDeRecursos();
      //      cambiarAVistaDeLista();
            app.mostrarAcierto('El recurso se ha grabado correctamente.');
        });        
    };
        
}