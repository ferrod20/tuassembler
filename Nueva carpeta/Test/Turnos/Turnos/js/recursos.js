var establecerPantallaMiCuenta = function() {
};

var establecerPantallaRecursos = function () {
    var recursoController = new RecursoController();
    recursoController.inicializar();

};

var establecerPantallaTurnosTomados = function () {
};

var establecerPantallaPreguntasFrecuentes = function () {
};

var Turnos;
var Notificador;

$(document).ready(function () {
    Turnos = {};

    $('#mi-cuenta').click(establecerPantallaMiCuenta);
    $('#recursos-y-disponibilidades').click(establecerPantallaRecursos);
    $('#turnos-tomados').click(establecerPantallaTurnosTomados);
    $('#preguntas-frecuentes').click(establecerPantallaPreguntasFrecuentes);

    Notificador = new Notifier();
    Notificador.init();

});