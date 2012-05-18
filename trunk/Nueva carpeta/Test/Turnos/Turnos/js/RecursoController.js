Turnos = { };
Turnos.Recursos = new EntityList([
        { "id": 1, "FirstName": "root", "LastName": "user", "nombre": "root user", "Username": "root", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": true, "Administrator": true, "IsDeleted": false, "GravatarFeed": "a52d6d241c3ba99e8eeed063a7c0a664", "EmailAddress": "info@bandit-software.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "a52d6d241c3ba99e8eeed063a7c0a664?s=25", "Settings": null, "Password": null, "ConfirmPassword": null },
        { "id": 101, "FirstName": "testuser", "LastName": "board", "nombre": "testuser board", "Username": "testuser@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "ed53e691ee322e24d8cc843fff68ebc6", "EmailAddress": "testuser@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "ed53e691ee322e24d8cc843fff68ebc6?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, 
        { "id": 102, "FirstName": "testmanager", "LastName": "board", "nombre": "testmanager board", "Username": "testmanager@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "006891a641e5d0bd9c3e2920465713df", "EmailAddress": "testmanager@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "006891a641e5d0bd9c3e2920465713df?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 103, "FirstName": "testreadonly", "LastName": "board", "nombre": "testreadonly board", "Username": "testreadonly@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "446f6f59f1fc3647e18ca5d612d1796d", "EmailAddress": "testreadonly@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "446f6f59f1fc3647e18ca5d612d1796d?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 104, "FirstName": "testnoaccess", "LastName": "board", "nombre": "testnoaccess board", "Username": "testnoaccess@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "96e8d0604839385c032f3841a7452948", "EmailAddress": "testnoaccess@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "96e8d0604839385c032f3841a7452948?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 101000, "FirstName": "sd@asdsa.com", "LastName": "dsfsdf", "nombre": "sd@asdsa.com dsfsdf", "Username": "sd@asdsa.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "eb5674361e42e52970cf4e8e31e0103d", "EmailAddress": "sd@asdsa.com", "DateFormat": "dd/MM/yyyy", "TimeZone": "Dateline Standard Time", "GravatarLink": "eb5674361e42e52970cf4e8e31e0103d?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 101001, "FirstName": "sdasdsa.com", "LastName": "dsfsdf", "nombre": "sdasdsa.com dsfsdf", "Username": "ssd@asdsa.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "9c14aa2d1cc19e66e28bb5e704d8869b", "EmailAddress": "ssd@asdsa.com", "DateFormat": "dd/MM/yyyy", "TimeZone": "Dateline Standard Time", "GravatarLink": "9c14aa2d1cc19e66e28bb5e704d8869b?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 101002, "FirstName": "sdasdsawww.com", "LastName": "dsfswwwwdf", "nombre": "sdasdsawww.com dsfswwwwdf", "Username": "swdwdsd@asdsa.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "e2150b29e48f20c7c702638ca219ee77", "EmailAddress": "swdwdsd@asdsa.com", "DateFormat": "dd/MM/yyyy", "TimeZone": "Dateline Standard Time", "GravatarLink": "e2150b29e48f20c7c702638ca219ee77?s=25", "Settings": null, "Password": null, "ConfirmPassword": null}]);


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
        $("#recurso-nombre").val("");       
    };

    var reestablecerPantalla = function () {
        scrollable.seekTo(0, 0);
    };
    
    var mostrarListaDeRecursos = function (recursos) {
        if (!recursos)
            recursos = Turnos.Recursos.toArray;
        var html = $.tmpl(RecursoPlantilla.FilaPlantilla, recursos);
        var $listaRecursos = $('#recursos-lista').empty().append(html);
        
        $('#fila_' + Turnos.CurrentUserId + ' .eliminar')
            .removeClass('eliminar')
            .addClass('disabled-delete');
        
        agregarManejadores($listaRecursos);        
    };

//    var mostrarListaDeRecursos = function () {
//        var recursos = Turnos.Recursos.toArray();
//        var resultado = '';
//        if (hayBusqueda) {
//            var criteria = $('#recursos-busqueda').val();
//            recursos = filtrar(recursos, criteria);
//        }

//        if (recursos && recursos.length > 0)
//            resultado = $.tmpl(RecursoPlantilla.FilaPlantilla, recursos);

//        var $usersList = $('#recursos-lista').empty().append(resultado);
//        agregarManejadores($usersList);
//    };

    var confirmarBorrado = function (e) {
        e.stopPropagation();
        var idRecurso = obtFilaId(this);
        $('#recursos-confirmar-borrado').show();
        $("#User_Id").val(idRecurso);
        scrollable.next();
    };

    var eliminarRecurso = function () {
        //quitarRecursoDeLaLista(idUser);
        //scrollable.prev();
    };

    var quitarRecursoDeLaLista = function (id) {
        var filaAEliminar = $('#fila_' + id + '.recurso-fila');
        filaAEliminar.slideUp('slow', filaAEliminar.remove);
        Turnos.Recursos.deleteById(id);
    };

    ///Filter users that match filterText
    var filtrar = function (recursos, textoAFiltrar) {
        textoAFiltrar = textoAFiltrar.toUpperCase();
        var resultado = [];
        $.each(recursos, function () {
            if (!textoAFiltrar || this.nombre.toUpperCase().indexOf(textoAFiltrar) >= 0) {
                resultado.push(this);
            }
        });
        return resultado;
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
        $('.primario').html( $.tmpl(RecursoPlantilla.PlantillaGeneral));

        scrollable = $("#recursos-desplazable").scrollable({onSeek: ocultarPaneles}).data("scrollable");
        ocultarPaneles(null, 0);
        mostrarListaDeRecursos();

        $("#recursos-formulario-cancelar").click(scrollable.prev);
        $("#crear-recurso").click(mostrarCrear);
        $("#recursos-formulario-save").click(grabar);
        $('#recursos-busqueda').keydown(buscar);
        $('#recursos-por-nombre').click(ordenarPorNombre);        
        $("#eliminacion-del-recurso-confirmada").click(eliminarRecurso);
        $("#recursos-confirmar-borrado-cancelar").click(scrollable.prev);
    };

    var editar = function (recursoId) {
        var recurso = Turnos.Recursos.obtenerPorId(recursoId);
        if (recurso) {
            $("#recurso-nombre").val(recurso.FirstName);
            $('#recursos-formulario-contenedor').show();
            scrollable.next();
        }
    };

    var mostrarCrear = function () {
        limpiarForm();
        $('#recursos-formulario-contenedor').show();
        scrollable.next();
    };

    var grabar = function () {
        var user = getFormData();
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
        var recursos = Turnos.Recursos.toArray();
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
        e.stopPropagation();
        var recursoId = obtFilaId(this);
    };

    var ocultarPaneles = function (e, index) {
        if (index === 0) {
            $('#recursos-formulario-contenedor').hide();
            $('#recursos-confirmar-borrado').hide();
        }
    };    
}