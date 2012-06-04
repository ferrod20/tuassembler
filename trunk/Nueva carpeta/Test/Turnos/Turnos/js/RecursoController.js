Turnos = {};

    
function RecursoController() {
    this.SystemType = 'RecursoController';
    var scrollable = null;
    var hayBusqueda = false;
    var busquedaDescendente = true;
    
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
        $("#recurso-id, #recurso-foto, #recurso-nombre, #recurso-especialidad, #recurso-email").val("");
    };

    var mostrarListaDeRecursos = function (recursos) {
        if (!recursos)
            recursos = Turnos.Recursos;

        if (hayBusqueda) {
            var criteria = $('#recursos-busqueda').val();
            recursos = filtrar(recursos, criteria);
        }

        var html = $.tmpl(RecursoPlantilla.FilaPlantilla, recursos);

        var $listaRecursos = $('#recursos-lista').empty().append(html);
        $('.recurso-fila').hide().fadeIn(750);
        agregarManejadores($listaRecursos);
    };

    var confirmarBorrado = function (e) {
        scrollable.next();
        e.stopPropagation();
        var idRecurso = obtFilaId(this);
        $('#recursos-confirmar-borrado').show();            
        $('#recursos-contenedor').hide();
        $("#recurso-id").val(idRecurso);
    };

    var eliminarRecurso = function () {
        var idField = $("#recurso-id").val();
        var id = idField == '' ? 0 : parseInt(idField);

        $.post('../Turno/EliminarRecurso', { id: id }).success(function (data) {
            var filaAEliminar = $('#fila_' + id + '.recurso-fila');
            filaAEliminar.slideUp('slow', filaAEliminar.remove);
            Turnos.Recursos.deleteById(id);
            scrollable.prev();
            mostrarListaDeRecursos();
        });
    };

    var filtrar = function (recursos, textoAFiltrar) {
        textoAFiltrar = textoAFiltrar.toUpperCase();
        return recursos.filter(function (recurso) {
            return !textoAFiltrar || recurso.nombre.toUpperCase().indexOf(textoAFiltrar) >= 0
        });                
    };
  
    var buscar = function () {
        var f = function () {
            var criteria = $('#recursos-busqueda').val();
            hayBusqueda = criteria != '';
            mostrarListaDeRecursos();
        };
        setTimeout(f, 1);
    };

    this.inicializar = function () {
        $.post('../Turno/ObtenerRecursos', { id: 1 }).success(function (data) {
            Turnos.Recursos = data;
            mostrarListaDeRecursos();
        });

        $('.primario').html( $.tmpl(RecursoPlantilla.PlantillaGeneral));

        scrollable = $("#recursos-desplazable").scrollable({onSeek: ocultarPaneles}).data("scrollable");
        ocultarPaneles(null, 0);
        
        $('#recursos-busqueda').keydown(buscar);
        $('#recursos-por-nombre').click(ordenarPorNombre);

        $("#crear-recurso").click(mostrarCrear);
        $("#recursos-formulario-grabar").click(grabar);
        $("#recursos-formulario-cancelar, #elminacion-del-recurso-cancelada").click(scrollable.prev);
        $("#eliminacion-del-recurso-confirmada").click(eliminarRecurso);
    };

    var editar = function (recursoId) {
        var recurso = Turnos.Recursos.getById(recursoId);
        if (recurso) {
            limpiarForm();
            $("#recurso-id").val(recurso.id);
            $("#recurso-nombre").val(recurso.nombre);
            $("#recurso-especialidad").val(recurso.especialidad);
            $("#recurso-email").val(recurso.email);
            $("#recurso-foto").val(recurso.foto);
            $('#recursos-confirmar-borrado').hide();
            $('#recursos-contenedor').show();            
            scrollable.next();
        }
    };

    var mostrarCrear = function () {
        limpiarForm();
        $('#recursos-contenedor').show();
        $('#recursos-confirmar-borrado').hide();
        scrollable.next();
    };

    var grabar = function () {
        var id = $("#recurso-id").val();
        var recurso = {
            id: id ==''?0:parseInt(id),
            nombre: $("#recurso-nombre").val(),
            especialidad: $("#recurso-especialidad").val(),
            habilitado: true,
            foto: $("#recurso-foto").val(),
            email: $("#recurso-email").val()
        };
    
     $.post('../Turno/GrabarRecurso', recurso).success(function (data) {
            Turnos.Recursos.push(data);
            mostrarListaDeRecursos();
            scrollable.prev();
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
        var recursos = Turnos.Recursos;
        recursos.sort(funcionDeOrden);

        var label = $('#recursos-por-nombre');
        var asc = 'asc';
        var desc = 'desc';

        if (busquedaDescendente) {
            recursos.reverse();
            label.addClass(asc).removeClass(desc);
        } else 
            label.addClass(desc).removeClass(asc);
        
        busquedaDescendente = !busquedaDescendente;
        mostrarListaDeRecursos(recursos);
    };

    var activar = function () {
     //   e.stopPropagation();
     //   var recursoId = obtFilaId(this);
    };

    var ocultarPaneles = function (e, index) {
        if (index === 0) {
            $('#recursos-contenedor').hide();
            $('#recursos-confirmar-borrado').hide();
        }
    };    
}