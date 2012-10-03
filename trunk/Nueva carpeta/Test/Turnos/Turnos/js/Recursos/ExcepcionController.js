function ExcepcionController(contenedor) {
    this.SystemType = 'ExcepcionController';
    this.datosSinGuardar = false;
    this.recurso = null;
    var self = this;

    this.editar = function () {
        self.recurso.obtExcepciones().forEach(function (excepcion) {
            agregarExcepcion(excepcion, true);
        });
    };
    
    this.inicializar = function() {
        $("#recursos-agregar-excepcion", contenedor).click(agregarNuevaExcepcion);
        $('#recurso-excepcion-todo-el-dia', contenedor).click(todoElDiaClickeado);
    };

    this.limpiarPantalla = function () {
        limpiarDatosExcepcion();
        $('#recursos-lista-de-excepciones', contenedor).html('');
    };

    var limpiarDatosExcepcion = function () {
        $("#recurso-excepcion-fecha, #recurso-excepcion-hora-desde, #recurso-excepcion-hora-hasta", contenedor).val("");        
        $('#recurso-excepcion-todo-el-dia', contenedor).attr('checked', true);
        $('#recurso-excepciones-desde-hasta', contenedor).slideUp();                
    };

    var eliminarExcepcion = function () {
        var $fila = $(this).parent().parent();

        var excepcion = $fila.find('#nombre').data('excepcion');
        self.recurso.eliminarExcepcion(excepcion);
        $fila.slideUp();

        self.datosSinGuardar = true;
    };

    var agregarExcepcion = function(excepcion, show) {
        var $fila = $.tmpl(RecursoPlantilla.FilaExcepcion, excepcion);
        var $nombre = $fila.find('#nombre');        
        $nombre.data('excepcion', excepcion);
        $fila.find('.eliminar').click(eliminarExcepcion);
        $('#recursos-lista-de-excepciones', contenedor).append($fila);
        if (show)
            $fila.show();
        else
            $fila.slideDown('fast');
    };

    var agregarNuevaExcepcion = function () {
        var fecha = $("#recurso-excepcion-fecha", contenedor).val();
        var todoElDia = $('#recurso-excepcion-todo-el-dia', contenedor).is(':checked');

        var horaDesde = todoElDia ? null : $("#recurso-excepcion-hora-desde", contenedor).val();
        var horaHasta = todoElDia ? null : $("#recurso-excepcion-hora-hasta", contenedor).val();

        var excepcion = self.recurso.agregarExcepcion(fecha, horaDesde, horaHasta);
        agregarExcepcion(excepcion);

        limpiarDatosExcepcion();
        self.datosSinGuardar = true;
    };
    
    var todoElDiaClickeado = function() {
        $('#recurso-excepciones-desde-hasta', contenedor).slideToggle($(this).is(':checked'));        
    };
                
}