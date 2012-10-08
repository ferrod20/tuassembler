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
        var excepcion = new Excepcion(fecha, desde, hasta);
        excepciones.push(excepcion);
        return excepcion;
    };

    this.eliminarExcepcion = function (excepcion) {
        var eliminado = null;
        var indice = excepciones.indexOf(excepcion);

        if (indice)
            eliminado = excepciones.splice(indice, 1);

        return eliminado;
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

    this.recorrerDisponibilidad = function (func) {
        if(disponibilidad && disponibilidad[1] && disponibilidad[1].length) {
            var cantidad = disponibilidad && disponibilidad[1].length;
            for (var i = 0; i < cantidad; i++)
                func([disponibilidad[0][i], disponibilidad[1][i], disponibilidad[2][i], disponibilidad[3][i], disponibilidad[4][i], disponibilidad[5][i], disponibilidad[6][i]]);            
        }

        return self;
    };
}