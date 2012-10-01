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

    var eliminarExcepcion = function () {
        var $fila = $(this).parent().parent();

        var excepcion = $fila.find('#nombre').data('excepcion');
        self.recurso.eliminarExcepcion(excepcion);
        $fila.slideUp();

        self.datosSinGuardar = true;
    };

    var agregarExcepcion = function () {
        var fecha = $("#recurso-excepcion-fecha", contenedor).val();
        var todoElDia = $('#recurso-excepcion-todo-el-dia', contenedor).is(':checked');

        var horaDesde = todoElDia ? null : $("#recurso-excepcion-hora-desde", contenedor).val();
        var horaHasta = todoElDia ? null : $("#recurso-excepcion-hora-hasta", contenedor).val();

        var excepcion = self.recurso.agregarExcepcion(fecha, horaDesde, horaHasta);

        var $fila = $(fila);
        var $nombre = $fila.find('#nombre');
        $nombre.html(excepcion.fecha + (todoElDia ? '' : ' ' + excepcion.desde + '-' + excepcion.hasta));
        $nombre.data('excepcion', excepcion);
        $fila.find('.eliminar').click(eliminarExcepcion);
        $('#recursos-lista-de-excepciones', contenedor).append($fila);
        $fila.slideDown('fast');

        limpiarDatosExcepcion();
        self.datosSinGuardar = true;
    };
    
    var todoElDiaClickeado = function() {
        $('#recurso-excepciones-desde-hasta', contenedor).slideToggle($(this).is(':checked'));        
    };
                
}