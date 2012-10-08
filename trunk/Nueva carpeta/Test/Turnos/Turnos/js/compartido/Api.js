function Api() {
    this.SystemType = 'Api';

    var urlBase = '../Turno/';

    var recursosApi = function () {
        this.urlBase = function () {
            return urlBase;
        };

        this.eliminarRecurso = function (idRecurso, recursoEliminado) {
            $.post(urlBase + 'EliminarRecurso', { idCliente: 1, idRecurso: idRecurso }).success(recursoEliminado);
        };

        this.obtenerRecursos = function(id, recursoObtenido) {
            $.post(urlBase + 'ObtenerRecursos', { idCliente: 1 }).success(recursoObtenido);
        };

        this.grabarRecurso = function (recurso, recursoGrabado) {
            $.post(urlBase + 'GrabarRecurso', recurso).success(recursoGrabado);
        };

        this.obtenerRecurso = function (idRecurso, recursoObtenido) {
            $.post(urlBase + 'ObtenerRecurso', { idCliente: 1, idRecurso: idRecurso }).success(recursoObtenido);
        };
    };

    this.recursos = new recursosApi();
};
