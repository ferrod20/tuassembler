function Recurso() {
    this.SystemType = 'Recurso';

    this.id = null;
    this.nombre = null;
    this.especialidad = null;
    this.habilitado = true;
    this.foto = null;
    this.email = null;

    var disponibilidad = {};
    var excepciones = [];

    this.agregarExcepcion = function (fecha, desde, hasta) {
        var excepcion = { fecha: fecha, desde: desde, hasta: hasta };
        excepciones.push(excepcion);
    };

    this.agregarDisponibilidad = function (dia, desde, hasta) {
        if (disponibilidad[dia]) 
            disponibilidad[dia].push({ desde: desde, hasta: hasta });
        else 
            disponibilidad[dia] = [{ desde: desde, hasta: hasta }];
    };

    this.obtExcepciones = function () {
        return excepciones;
    };

    this.obtDisponibilidad = function (dia) {
        var resultado = null;

        if (dia)
            resultado = disponibilidad[dia];
        else
            resultado = disponibilidad;
        
        return resultado;
    };
}