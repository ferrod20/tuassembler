function Rango(valor) {
    this.SystemType = 'Rango';
    var self = this;
    this.inicio = new Date();
    this.fin = new Date();

    var inicializar = function () {
        var inicioFin = valor.split('-');
        var inicio = inicioFin[0];
        var fin = inicioFin[1];
        
        var horaMinutos = inicio.split('.');
        var minutos = horaMinutos.length > 1 ? horaMinutos[1] : 0;        
        self.inicio.setHours(horaMinutos[0],minutos);

        horaMinutos = fin.split('.');
        minutos = horaMinutos.length > 1 ? horaMinutos[1] : 0;        
        self.fin.setHours(horaMinutos[0], minutos);
    };

    inicializar();

    this.estaIncluido = function (otroRango) {
        var disjuntos = otroRango.inicio >= self.fin || otroRango.fin <= self.inicio;
        return !disjuntos;        
    };
    
}

function DisponibilidadController(contenedor) {
    this.SystemType = 'DisponibilidadController';
    var self = this;
    
    var dataTable = null;
    this.datosSinGuardar = false;

    var actualizarHorario = function (value, settings) {
        var pos = dataTable.fnGetPosition(this);
        dataTable.fnUpdate(value, pos[0], pos[1]);
        self.datosSinGuardar = true;
        //this value
    };

    var validarHorario = function (settings, td) {
        var input = $(td).find('input');
        var valor = input.val();
        
        //verificar que valor sea un string válido
        var rango = new Rango(valor);
        //recorrer toda la fila para ver que el nuevo rango no se superponga
        //ordenar el nuevo rango en la fila

        return true;
    };
        
    this.inicializar = function () {
        dataTable = $("#disponibilidad", contenedor).dataTable({
            "bPaginate": false,
            "bFilter": false,
            "bInfo": false,
            "bSort": false
        });
        
        $('td', dataTable.fnGetNodes()).editable(actualizarHorario, {
            onblur: "submit",
            tooltip: "Click para editar...",
            id: 'elementid',
            onsubmit: validarHorario
        });
    };
};