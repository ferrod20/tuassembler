function Recurso(recurso) {
    this.SystemType = 'Recurso';

    this.id = recurso ? recurso.id : null;
    this.nombre = recurso ? recurso.nombre : null;
    this.especialidad = recurso ? recurso.especialidad : null;
    this.habilitado = recurso ? recurso.habilitado : null;
    this.foto = recurso ? recurso.foto : null;
    this.email = recurso ? recurso.email : null;

    var disponibilidad = {};
    var excepciones = [];

    this.agregarExcepcion = function (fecha, desde, hasta) {
        var excepcion = { fecha: fecha, desde: desde, hasta: hasta };
        excepciones.push(excepcion);
        return excepcion;
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