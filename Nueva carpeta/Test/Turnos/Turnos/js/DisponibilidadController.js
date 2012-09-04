function DisponibilidadController(contenedor) {
    this.SystemType = 'DisponibilidadController';
    var self = this;
    this.datosSinGuardar = false;

    var actualizarHorario = function ($input) {
        var valor = $input.val();

        if (validarHorario($input)) {
            var rango = new Rango(valor);
            var infoNuevoValor = verificarValor($input, rango);
            intercambiar($input, infoNuevoValor); //ordenar el nuevo rango en la fila                    
            self.datosSinGuardar = true;
        }

        agregarFilaVacia();
    };

    var agregarFilaVacia = function () {
        var $tabla = $('#disponibilidad', contenedor);

        var ultimaFilaVacia = $tabla.find('tr:last td').toArray().every(function (td) {
            return $(td).find('input').val() == '';
        });

        if (!ultimaFilaVacia) {
            var $nuevaFila = $('<tr> \
                    <td><input value=""></td> \
                    <td><input value=""></td> \
                    <td><input value=""></td> \
                    <td><input value=""></td> \
                    <td><input value=""></td> \
                    <td><input value=""></td> \
                    <td><input value=""></td> \
                </tr>');
            $nuevaFila.find('input').blur(actualizarHorarioConBlur).keypress(actualizarHorarioConEnter);
            $tabla.find('tbody').append($nuevaFila);
        }
    };
    
    var verificarValor = function ($input, rango) {
        var fila = $input.parent().parent().prevAll().length;
        var col = $input.parent().prevAll().length;
        var $tbody = $input.parent().parent().parent();


        var horariosColumna = $tbody.find('tr td:nth-child(' + (col + 1) + ') input').toArray();
        horariosColumna.forEach(function (horario, i) {
            horariosColumna[i] = $(horario).val();
        });

        var solapa = rango.seSolapaConAlguno(fila, horariosColumna);
        var vaEnLaFila = rango.ordenar(fila, horariosColumna);
        return { seSolapa: solapa, intercambiar: { columna: col, fila: fila, vaEnLaFila: vaEnLaFila} };
    };

    var validarHorario = function ($input) {
        var rango = new Rango();

        var todoBien = rango.asignar($input.val());
        if (todoBien) {
            var infoNuevoValor = verificarValor($input, rango);
            todoBien = !infoNuevoValor.seSolapa;
            if (todoBien) {
                $("#error", contenedor).text('');
                $input.toggleClass('invalid-input', false);
            }
            else {
                $input.toggleClass('invalid-input', true);
                $("#error", contenedor).text('*Ese horario se solapa con uno existente.');
            }

        } else {
            $input.toggleClass('invalid-input', true);
            $("#error", contenedor).text('*Ingrese un rango válido.');
        }

        return todoBien;
    };

    var intercambiar = function ($input, infoNuevoValor) {
        var $tbody = $input.parent().parent().parent();
        var intercambio = infoNuevoValor.intercambiar;
        var valorNuevo = $input.val();

        if (intercambio.vaEnLaFila != intercambio.fila) {
            var $input2 = $tbody.find('tr:nth-child(' + (intercambio.vaEnLaFila + 1) + ') td:nth-child(' + (intercambio.columna + 1) + ') input');
            var valor = $input2.val();
            $input.val(valor);
            $input2.val(valorNuevo);
        }
    };

    var actualizarHorarioConEnter = function (e) {
        if (e.which == 13)
            actualizarHorario($(this));
    };

    var actualizarHorarioConBlur = function () {
        actualizarHorario($(this));
    };
    
    this.inicializar = function () {
        $('td input', contenedor).blur(actualizarHorarioConBlur).keypress(actualizarHorarioConEnter);
    };
};