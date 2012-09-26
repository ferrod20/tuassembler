function Api() {
    this.SystemType = 'Api';

    var urlBase = '../Turno/';

    var recursosApi = function () {
        this.urlBase = function () {
            return urlBase;
        };

        this.eliminarRecurso = function (id, recursoEliminado) {
            $.post(urlBase + 'EliminarRecurso', { id: id }).success(recursoEliminado);
        };

        this.obtenerRecursos = function(id, recursoObtenido) {
            $.post(urlBase + 'ObtenerRecursos', { id: 1 }).success(recursoObtenido);
        };

        this.grabarRecurso = function (recurso, recursoGrabado) {
            $.post(urlBase + 'GrabarRecurso', recurso).success(recursoGrabado);
        };
    };

    this.recursos = new recursosApi();
};
