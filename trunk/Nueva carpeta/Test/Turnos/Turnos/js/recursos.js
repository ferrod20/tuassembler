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


$(document).ready(function () {
    $('#mi-cuenta').click(establecerPantallaMiCuenta);
    $('#recursos-y-disponibilidades').click(establecerPantallaRecursos);
    $('#turnos-tomados').click(establecerPantallaTurnosTomados);
    $('#preguntas-frecuentes').click(establecerPantallaPreguntasFrecuentes);        
});