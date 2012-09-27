function RecursosController() {
    this.SystemType = 'RecursosController';
    var contenedor = $('.primario');    
    var scrollable = null;
    var hayBusqueda = false;
    var busquedaDescendente = true;
    var recursoController = new RecursoController(contenedor);
    var disponibilidadController = new DisponibilidadController(contenedor);
    var excepcionController = new ExcepcionController(contenedor);
    var recursos;
    var api = app.api.recursos;
    
    var obtFilaId = function (item) {
        var rowid = $(item).parents(".recurso-fila").attr("id");
        var id = parseInt(rowid.split("_")[1], 10);
        return id;
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
    
    var limpiarForm = function() {
        $("#recurso-id, #recurso-foto, #recurso-nombre, #recurso-especialidad, #recurso-email", contenedor).val("");
        excepcionController.limpiarPantalla();
    };

    var mostrarListaDeRecursos = function () {
        if (hayBusqueda) {
            var criteria = $('#recursos-busqueda', contenedor).val();
            recursos = filtrar(criteria);
        }

        var html = $.tmpl(RecursoPlantilla.FilaPlantilla, recursos);

        var $listaRecursos = $('#recursos-lista', contenedor).empty().append(html);
        $('.recurso-fila', contenedor).hide().fadeIn(750);
        agregarManejadores($listaRecursos);
    };

    var confirmarBorrado = function (e) {
        scrollable.next();
        e.stopPropagation();
        var idRecurso = obtFilaId(this);
        $('#recursos-confirmar-borrado', contenedor).show();
        $('#recursos-contenedor', contenedor).hide();
        $("#recurso-id", contenedor).val(idRecurso);
    };

    var eliminarRecurso = function () {
        var idField = $("#recurso-id", contenedor).val();
        var id = idField == '' ? 0 : parseInt(idField);
        api.eliminarRecurso(id, function() {
        var filaAEliminar = $('#fila_' + id + '.recurso-fila', contenedor);
        filaAEliminar.slideUp('slow', filaAEliminar.remove);
        recursos.deleteById(id);
        scrollable.prev();
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

    this.inicializar = function () {
        api.obtenerRecursos(1, function(data) {
            recursos = data;
            mostrarListaDeRecursos();
        });
        
        contenedor.html($.tmpl(RecursoPlantilla.PlantillaGeneral));

        scrollable = $("#recursos-desplazable", contenedor).scrollable({ onSeek: ocultarPaneles }).data("scrollable");
        ocultarPaneles(null, 0);

        $('#recursos-busqueda', contenedor).keydown(buscar);
        $('#recursos-por-nombre', contenedor).click(ordenarPorNombre);

        $("#crear-recurso", contenedor).click(mostrarCrear);
        $("#recursos-formulario-grabar", contenedor).click(grabar);
        $("#recursos-formulario-cancelar", contenedor).click(cancelar);
        $("#elminacion-del-recurso-cancelada", contenedor).click(scrollable.prev);
        $("#eliminacion-del-recurso-confirmada", contenedor).click(eliminarRecurso);

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
        cambiarAVistaDetalle();
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
            mostrarListaDeRecursos();
            cambiarAVistaDeLista();
            app.mostrarAcierto('El recurso se ha grabado correctamente.');
        });        
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