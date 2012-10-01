function ExcepcionController(contenedor) {
    this.SystemType = 'ExcepcionController';
    this.datosSinGuardar = false;
    this.recurso = null;
    var self = this;
    var fila = "<div class='recurso-fila' style='display: none;'>                 \
    <div class='nombre-contenedor'>                                                                                               \
        <div id='nombre'></div>                                                                              \
    </div>                                                                                                                \
    <div class='iconos-contenedor'>                                                                                             \
    <span class='eliminar' title='Eliminar'></span>          \
    </div>                                                                                                                  \
</div>";        
    this.inicializar = function() {
        $("#recursos-agregar-excepcion", contenedor).click(agregarExcepcion);
        $('#recurso-excepcion-todo-el-dia', contenedor).click(todoElDiaClickeado);
    };
        
    this.limpiarPantalla = function () {
        limpiarDatosExcepcion();
        $('#recursos-lista-de-excepciones', contenedor).html('');
    };

    var limpiarDatosExcepcion = function () {
        $("#recurso-excepcion-fecha, #recurso-excepcion-hora-desde, #recurso-excepcion-hora-hasta", contenedor).val("");        
        $('#recurso-excepcion-todo-el-dia', contenedor).attr('checked', false);
        $('#recurso-excepciones-desde-hasta', contenedor).slideDown();                
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
        var fecha = $("#recurso-excepcion-fecha", contenedor).val();
        var todoElDia = $('#recurso-excepcion-todo-el-dia', contenedor).is(':checked');

        var horaDesde = todoElDia ? null : $("#recurso-excepcion-hora-desde", contenedor).val();
        var horaHasta = todoElDia ? null : $("#recurso-excepcion-hora-hasta", contenedor).val();

        var excepcion = self.recurso.agregarExcepcion(fecha, horaDesde, horaHasta);

        var $fila = $(fila);
        $fila.find('#nombre').html(excepcion.fecha + (todoElDia ? '' : ' ' + excepcion.desde + '-' + excepcion.hasta));
        $fila.find('.eliminar').click(eliminar);
        $('#recursos-lista-de-excepciones', contenedor).append($fila);
        $fila.slideDown('fast');

        limpiarDatosExcepcion();
    };
    
    var todoElDiaClickeado = function() {
        $('#recurso-excepciones-desde-hasta', contenedor).slideToggle($(this).is(':checked'));        
    };
                
}