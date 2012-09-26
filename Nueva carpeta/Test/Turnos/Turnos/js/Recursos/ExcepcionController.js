function ExcepcionController(contenedor) {
    this.SystemType = 'ExcepcionController';
    this.datosSinGuardar = false;
    var self = this;
    
    this.inicializar = function() {
        $("#recursos-agregar-excepcion", contenedor).click(agregarExcepcion);
        $('#recurso-excepcion-todo-el-dia', contenedor).click(todoElDiaClickeado);
    };
    
    var agregarManejadores = function (lista) {
        $(".recurso-fila", lista).click(function () {
            editar(obtFilaId($(".editar",this)));
        });

        $(".editar", lista).click(function (e) {
            e.stopPropagation(); 
            editar(obtFilaId(this));
        });
        
        $('.eliminar', lista).click(confirmarBorrado);        
        $('.users-disabled, .tilde', lista).click(activar);        
    };

    this.limpiarPantalla = function () {
        $("#recurso-excepcion-fecha, #recurso-excepcion-hora-desde, #recurso-excepcion-hora-hasta", contenedor).val("");
        $('#recursos-lista-de-excepciones', contenedor).html('');
    };

    var mostrarListaDeExcepciones = function (excepciones) {
        //if (!excepciones)
          //  excepciones = Turnos.Excepciones;

        var html = $.tmpl(RecursoPlantilla.FilaExcepcion, excepciones);

        var $listaExcepciones = $('#recursos-lista-de-excepciones', contenedor).empty().append(html);
        agregarManejadores($listaExcepciones);
    };

    var eliminar = function () {
        $(this).parent().parent().slideUp();
//        
//        var idField = $("#recurso-id", contenedor).val();
//        var id = idField == '' ? 0 : parseInt(idField);

//        $.post('../Turno/EliminarRecurso', { id: id }).success(function (data) {
//            var filaAEliminar = $('#fila_' + id + '.recurso-fila', contenedor);
//            filaAEliminar.slideUp('slow', filaAEliminar.remove);
//            recursos.deleteById(id);
//            //scrollable.prev();
//            //mostrarListaDeRecursos();
        //            app.mostrarAcierto('El recurso se ha eliminado correctamente.');
//        });
    };
        
    var agregarExcepcion = function () {
        var html = "<div class='recurso-fila' style='display: none;'>                 \
    <div class='nombre-contenedor'>                                                                                               \
        <div id='nombre'>19/2/2012  14hs a 20hs</div>                                                                              \
    </div>                                                                                                                \
    <div class='iconos-contenedor'>                                                                                             \
    <span class='eliminar' title='Eliminar'></span>          \
    </div>                                                                                                                  \
</div>";

        var fila = $(html);
        fila.find('.eliminar').click(eliminar);
        $('#recursos-lista-de-excepciones', contenedor).append(fila);
        fila.slideDown('fast');
    };
    
    var todoElDiaClickeado = function() {
        $('#recurso-excepciones-desde-hasta', contenedor).slideToggle(!$(this).is(':checked'));        
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

        $.post('../Turno/GrabarRecurso', recurso).success(function (data) {
            recursos.push(data);
            //mostrarListaDeRecursos();
            //cambiarAVistaDeLista();
            app.mostrarAcierto('El recurso se ha grabado correctamente.');
        });
    };        
}