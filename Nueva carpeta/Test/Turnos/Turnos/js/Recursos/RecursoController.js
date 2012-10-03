function RecursoController(contenedor) {
    this.SystemType = 'RecursoController';
    var self = this;
    this.recurso = null;
    
    var disponibilidadController = new DisponibilidadController(contenedor);
    var excepcionController = new ExcepcionController(contenedor);
    var api = app.api.recursos;
    
    this.datosSinGuardar = false;
    
    var limpiarPantalla = function () {
        $("#recurso-id, #recurso-foto, #recurso-nombre, #recurso-especialidad, #recurso-email", contenedor).val("");
        excepcionController.limpiarPantalla();
        disponibilidadController.limpiarPantalla();
    };

    this.inicializar = function () {
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
        if (disponibilidadController.datosSinGuardar || excepcionController.datosSinGuardar || self.datosSinGuardar)
            app.ventanaDeConfirmacion({ description: "Hay cambios sin guardar. \n\n Desea descartarlos?",
                onAccept: function () {
                    self.datosSinGuardar = excepcionController.datosSinGuardar = disponibilidadController.datosSinGuardar = false;
                    $(self).trigger('grabacionCancelada');
                }
            });
        else
            $(self).trigger('grabacionCancelada');            
    };

    this.editar = function (recurso) {
        self.recurso = disponibilidadController.recurso = excepcionController.recurso = recurso;
        if (self.recurso) {
            limpiarPantalla();
            $("#recurso-id", contenedor).val(self.recurso.id);
            $("#recurso-nombre", contenedor).val(self.recurso.nombre);
            $("#recurso-especialidad", contenedor).val(self.recurso.especialidad);
            $("#recurso-email", contenedor).val(self.recurso.email);
            $("#recurso-foto", contenedor).val(self.recurso.foto);
            $('#recursos-confirmar-borrado', contenedor).hide();
            $('#recursos-contenedor', contenedor).show();
            disponibilidadController.editar();
            excepcionController.editar();
        }
    };

    this.mostrarCrear = function () {
        self.recurso = new Recurso();
        limpiarPantalla();
        $('#recursos-contenedor', contenedor).show();
        $('#recursos-confirmar-borrado', contenedor).hide();
    };

    var grabar = function () {
        var id = $("#recurso-id", contenedor).val();

        if (!self.recurso)
            self.recurso = new Recurso();

        self.recurso.id = id == '' ? 0 : parseInt(id);
        self.recurso.nombre = $("#recurso-nombre", contenedor).val();
        self.recurso.especialidad = $("#recurso-especialidad", contenedor).val();
        self.recurso.habilitado = true;
        self.recurso.foto = $("#recurso-foto", contenedor).val();
        self.recurso.email = $("#recurso-email", contenedor).val();

        disponibilidadController.extraerInfo();

        api.grabarRecurso(self.recurso, function () {
            $(self).trigger('recursoGrabado', [self.recurso]);
        });
    };
}