jQuery.fn.dataTableExt.oApi.fnGetColumnData = function (oSettings, iColumn, bUnique, bFiltered, bIgnoreEmpty) {
    // check that we have a column id
    if (typeof iColumn == "undefined") return [];

    // by default we only wany unique data
    if (typeof bUnique == "undefined") bUnique = true;

    // by default we do want to only look at filtered data
    if (typeof bFiltered == "undefined") bFiltered = true;

    // by default we do not wany to include empty values
    if (typeof bIgnoreEmpty == "undefined") bIgnoreEmpty = true;

    // list of rows which we're going to loop through
    var aiRows;

    // use only filtered rows
    if (bFiltered == true) aiRows = oSettings.aiDisplay;
    // use all rows
    else aiRows = oSettings.aiDisplayMaster; // all row numbers

    // set up data array    
    var asResultData = new Array();

    for (var i = 0, c = aiRows.length; i < c; i++) {
        var iRow = aiRows[i];
        var sValue = this.fnGetData(iRow, iColumn);

        // ignore empty values?
        if (bIgnoreEmpty == true && sValue.length == 0) continue;

        // ignore unique values?
        else if (bUnique == true && jQuery.inArray(sValue, asResultData) > -1) continue;

        // else push the value onto the result data array
        else asResultData.push(sValue);
    }

    return asResultData;
};

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

function DisponibilidadController(contenedor) {
    this.SystemType = 'DisponibilidadController';
    var self = this;

    var dataTable = null;
    this.datosSinGuardar = false;

    var actualizarHorario = function (value, settings) {
        var pos = dataTable.fnGetPosition(this);
        var rango = new Rango(value);        
        var infoNuevoValor = verificarValor(pos[0], pos[1], rango);
        intercambiar(value, infoNuevoValor); //ordenar el nuevo rango en la fila                    
        self.datosSinGuardar = true;        
    };

    var verificarValor = function (fila, col, rango) {        
        var horariosColumna = dataTable.fnGetColumnData(col);
        var solapa = rango.seSolapaConAlguno(fila, horariosColumna);
        var vaEnLaFila = rango.ordenar(fila, horariosColumna);
        return { seSolapa: solapa, intercambiar: { columna: col, fila: fila, vaEnLaFila: vaEnLaFila} };
    };

    var validarHorario = function (settings, td) {
        var input = $(td).find('input');
        var valor = input.val();
        var rango = new Rango();

        var todoBien = rango.asignar(valor);
        if (todoBien) {
            var pos = dataTable.fnGetPosition(td);
            var infoNuevoValor = verificarValor(pos[0], pos[1], rango);
            todoBien = !infoNuevoValor.seSolapa;
            if (todoBien) {
                $("#error", contenedor).text('');
                input.toggleClass('invalid-input', false);
                
            }
            else {
                input.toggleClass('invalid-input', true);
                $("#error", contenedor).text('*Ese horario se solapa con uno existente.');
            }

        } else {
            input.toggleClass('invalid-input', true);
            $("#error", contenedor).text('*Ingrese un rango válido.');
        }

        return todoBien;
    };

    var intercambiar = function (valor, infoNuevoValor) {
        var intercambio = infoNuevoValor.intercambiar;

        if(intercambio.vaEnLaFila != intercambio.fila) {
            var valor2 = dataTable.fnGetData(intercambio.vaEnLaFila, intercambio.columna);
            dataTable.fnUpdate(valor2, intercambio.fila, intercambio.columna);
        }
        
        dataTable.fnUpdate(valor, intercambio.vaEnLaFila, intercambio.columna);
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