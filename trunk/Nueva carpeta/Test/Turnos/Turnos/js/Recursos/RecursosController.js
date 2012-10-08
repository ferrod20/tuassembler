function RecursosController() {
    this.SystemType = 'RecursosController';
    var self = this;

    var contenedor = $('.primario');    
    var scrollable = null;
    var hayBusqueda = false;
    var busquedaDescendente = true;
    
    var recursoController = new RecursoController(contenedor);
    var recursos;
    var api = app.api.recursos;
    
    var obtFilaId = function (item) {
        var rowid = $(item).parents(".recurso-fila").attr("id");
        var id = parseInt(rowid.split("_")[1], 10);
        return id;
    };

    this.inicializar = function () {        
        api.obtenerRecursos(1, function (data) {
            recursos = data;
            mostrarListaDeRecursos();
        });

        contenedor.html($.tmpl(RecursoPlantilla.PlantillaGeneral));

        scrollable = $("#recursos-desplazable", contenedor).scrollable({ onSeek: ocultarPaneles }).data("scrollable");
        ocultarPaneles(null, 0);

        $('#recursos-busqueda', contenedor).keydown(buscar);
        $('#recursos-por-nombre', contenedor).click(ordenarPorNombre);

        $("#crear-recurso", contenedor).click(mostrarCrear);
        
        recursoController.inicializar();
        $(recursoController).bind('grabacionCancelada', cambiarAVistaDeLista);
        $(recursoController).bind('recursoGrabado', recursoGrabado);
    };

    var agregarManejadores = function (lista) {
        $(".editar, .recurso-fila", lista).click(function (e) {
            e.stopPropagation();
            var recursoId = obtFilaId(this);
            api.obtenerRecurso(recursoId, function (data) {
                var recurso = new Recurso(data);
                editar(recurso);
            });
        });

        $('.eliminar', lista).click(confirmarBorrado);
        $('.users-disabled, .tilde', lista).click(activar);
    };

    var mostrarListaDeRecursos = function () {
        var recursosAMostrar = recursos;
        if (hayBusqueda) {
            var criteria = $('#recursos-busqueda', contenedor).val();
            recursosAMostrar = filtrar(criteria);
        }

        var html = $.tmpl(RecursoPlantilla.FilaPlantilla, recursosAMostrar);

        var $listaRecursos = $('#recursos-lista', contenedor).empty().append(html);
        $('.recurso-fila', contenedor).hide().fadeIn(750);
        agregarManejadores($listaRecursos);
    };

    var confirmarBorrado = function (e) {
        e.stopPropagation();
        var idRecurso = obtFilaId(this);
        $("#recurso-id", contenedor).val(idRecurso);
        
        app.ventanaDeConfirmacion({
            title: 'Confirmar elminación',
            acceptButtonText: "Si",
            cancelButtonText: "Cancelar",
            description:'Eliminando este recurso se removerá toda la información relativa persistida. Continúa con la elminación de todos modos?',
            onAccept: eliminarRecurso            
        });
    };

    var eliminarRecurso = function () {
        var idField = $("#recurso-id", contenedor).val();
        var idRecurso = idField == '' ? 0 : parseInt(idField);
        api.eliminarRecurso(idRecurso, function () {
            var filaAEliminar = $('#fila_' + idRecurso + '.recurso-fila', contenedor);
            filaAEliminar.slideUp('slow', filaAEliminar.remove);
            recursos.deleteById(idRecurso);
            mostrarListaDeRecursos();
            app.mostrarAcierto('El recurso se ha eliminado correctamente.');
        });        
    };

    var filtrar = function (textoAFiltrar) {
        textoAFiltrar = textoAFiltrar.toUpperCase();
        return recursos.filter(function (recurso) {
            return !textoAFiltrar || recurso.nombre.toUpperCase().indexOf(textoAFiltrar) >= 0;
        });
    };
  
    var buscar = function () {
        var f = function () {
            var criteria = $('#recursos-busqueda', contenedor).val();
            hayBusqueda = criteria != '';
            mostrarListaDeRecursos();
        };
        setTimeout(f, 1);
    };

    var cambiarAVistaDeLista = function () {
        scrollable.prev();
        $('.tabs li').fadeOut(function () { $('.tabs').css({ margin: '', 'padding-left': '' }); });        
    };

    var cambiarAVistaDetalle = function() {
        scrollable.next();
        $('.tabs').css({  'margin': '0px', 'padding-left': '58px' });
        $('.tabs li').fadeIn();
        $('#recurso-tabs').tabs('select', 0);
    };

    var mostrarCrear = function () {
        recursoController.mostrarCrear();
        cambiarAVistaDetalle();
    };

    var editar = function(recurso) {
        recursoController.editar(recurso);
        cambiarAVistaDetalle();
    };

    var recursoGrabado = function (evento, recurso, grabado) {
        if(grabado)
            recursos.push(recurso);
        mostrarListaDeRecursos();
        cambiarAVistaDeLista();

        app.mostrarAcierto('El recurso se ha ' + (grabado ? 'grabado' : 'modificado') + ' correctamente.');        
    };
    
    var ordenarPorNombre = function () {
        function sortByName(a, b) {
            var x = a.nombre.toLowerCase();
            var y = b.nombre.toLowerCase();
            return x < y ? -1 : (x > y ? 1 : 0);
        }
        ordenar(sortByName);
    };

    var ordenar = function (funcionDeOrden) {
        recursos.sort(funcionDeOrden);

        var label = $('#recursos-por-nombre', contenedor);
        var asc = 'asc';
        var desc = 'desc';

        if (busquedaDescendente) {
            recursos.reverse();
            label.addClass(asc).removeClass(desc);
        } else 
            label.addClass(desc).removeClass(asc);
        
        busquedaDescendente = !busquedaDescendente;
        mostrarListaDeRecursos();
    };

    var activar = function () {
     //   e.stopPropagation();
     //   var recursoId = obtFilaId(this);
    };

    var ocultarPaneles = function (e, index) {
        if (index === 0) {
            $('#recursos-contenedor', contenedor).hide();
            $('#recursos-confirmar-borrado', contenedor).hide();
        }
    };    
}