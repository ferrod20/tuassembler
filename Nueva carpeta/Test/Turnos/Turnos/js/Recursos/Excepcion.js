function Excepcion(fecha, desde, hasta) {
    this.SystemType = 'Excepcion';
    var self = this;
    this.fecha = fecha ? fecha : null;
    var desdeHasta = desde ? new Rango(desde.replace(':', '.') + '-' + hasta.replace(':', '.')) : null;

    this.inicio = function () {
        return desdeHasta? desdeHasta.inicio : null;
    };

    this.fin = function () {
        return desdeHasta? desdeHasta.fin : null;
    };
    
    this.obtString = function () {
        return self.fecha + (self.todoElDia() ? '' : ' ' +
            desdeHasta.inicio.getHours() + '.' + desdeHasta.inicio.getMinutes() + '-' +
            desdeHasta.fin.getHours() + '.' + desdeHasta.fin.getMinutes());
    };

    this.todoElDia = function () {
        return self.inicio() == null;
    };
}