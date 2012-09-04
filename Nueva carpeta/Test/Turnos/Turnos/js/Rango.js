function Rango(v) {
    this.SystemType = 'Rango';
    var self = this;
    this.inicio = new Date();
    this.fin = new Date();
    var valor = v;

    var inicializar = function () {
        if(valor) {
            var inicioFin = valor.split('-');
            var inicio = inicioFin[0];
            var fin = inicioFin[1];

            var horaMinutos = inicio.split('.');
            var minutos = horaMinutos.length > 1 ? horaMinutos[1] : 0;
            self.inicio.setHours(horaMinutos[0], minutos);

            horaMinutos = fin.split('.');
            minutos = horaMinutos.length > 1 ? horaMinutos[1] : 0;
            self.fin.setHours(horaMinutos[0], minutos);    
        }
    };

    inicializar();

    this.asignar = function (val) {
        var regEx = /(^([1-9]|[0-2][\d])(\.[0-5][\d])?-([1-9]|[0-2][\d])(\.[0-5][\d])?$)/;
        var valido = val.match(regEx) != null;
        if (valido) {
            valor = val;
            inicializar();
            valido = self.inicio < self.fin;
        }
        return valido;
    };

    this.seSolapaCon = function (otroRango) {
        var disjuntos = otroRango.inicio >= self.fin || otroRango.fin <= self.inicio;
        return !disjuntos;        
    };

    this.seSolapaConAlguno = function (fila, horariosColumna) {
        return horariosColumna.some(function (horario, indice) {
            var otroRango = new Rango(horario);
            return self.seSolapaCon(otroRango) && indice!=fila;
        });    
    };

    this.ordenar = function (fila, horariosColumna) {
        horariosColumna.splice(fila, 1);
        var ubicarEnPos = -1;
        for (var i = 0, j = 1; ubicarEnPos === -1 && j < horariosColumna.length; i++, j++) {
            var anterior = horariosColumna[i];
            var proximo = horariosColumna[j];

            var rangoAnterior = new Rango(anterior);
            var rangoProximo = new Rango(proximo);

            if (self.inicio >= rangoAnterior.fin && self.fin <= rangoProximo.inicio)
                ubicarEnPos = j;
        }

        if (ubicarEnPos === -1) {
            ubicarEnPos = 0;
            if (horariosColumna.length === 1) {
                var rango = new Rango(horariosColumna[0]);
                if (rango.fin <= self.inicio)
                    ubicarEnPos = 1;
            }
        }
        return ubicarEnPos;
    };
}
