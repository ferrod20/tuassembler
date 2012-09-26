function HomeController() {
    this.SystemType = 'HomeController';
    var recursoController = null;
    
    this.inicializar = function() {
        $('#mi-cuenta').click(establecerPantallaMiCuenta);
        $('#recursos-y-disponibilidades').click(establecerPantallaRecursos);
        $('#turnos-tomados').click(establecerPantallaTurnosTomados);
        $('#preguntas-frecuentes').click(establecerPantallaPreguntasFrecuentes);
    };
    
    var establecerPantallaMiCuenta = function () {
    };

    var establecerPantallaRecursos = function () {
        if (!recursoController)
            recursoController = new RecursoController();
        recursoController.inicializar();
    };

    var establecerPantallaTurnosTomados = function () {
    };

    var establecerPantallaPreguntasFrecuentes = function () {
    };
}

app = new App();

$(document).ready(function () {
    app.init();
    var homeController = new HomeController();
    homeController.inicializar();
});