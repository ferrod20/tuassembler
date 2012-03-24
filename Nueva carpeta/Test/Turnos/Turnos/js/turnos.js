$(document).ready(function () {
    var clienteId = 56;
    var fechaI = new Date(); fechaI.setHours(10); fechaI.setMinutes(0); fechaI.setSeconds(0);
    var fechaF = new Date(); fechaF.setHours(23); fechaF.setMinutes(0); fechaF.setSeconds(0);
    var apiCall = $.post('../Turno/ObtenerTurnosLibres', { clienteId: clienteId, fechaI: dateToString(fechaI), fechaF: dateToString(fechaF) });
        
    //Marian: Defintivamente hay que hacer una sola llamada que devuelva los recursos con sus turnos disponibles
    apiCall.done(function (data) {
        setupCalendario(data.recursos, data.disponibilidad);
    });
});

//function obtenerTurnosLibres(listaRecursos, fechaI, fechaF) {
//    var apiCall2 = $.post('../Turno/ObtenerTurnosLibres', { idCliente: 55, fechaI: dateToString(fechaI), fechaF: dateToString(fechaF) });
//    apiCall2.done(function (data) {
//        setupCalendario(listaRecursos, data);
//    });
//}

function resetForm($dialogContent) {
    $dialogContent.find("input").val("");
    $dialogContent.find("textarea").val("");
};

function mostrar(calEvent, $event) {
    calEvent.end = new Date(calEvent.start).addMinutes(40);
    if (calEvent.end.getTime() < new Date().getTime()) {
        $event.css("backgroundColor", "#aaa");
        $event.find(".wc-time").css({ backgroundColor: "#999", border: "1px solid #888" });
    }
};

function nuevo(calEvent, calElement, freeBusyManager, calendar, DomEvent) {
    var isFree = true;
    $.each(freeBusyManager.getFreeBusys(calEvent.start, calEvent.end), function () {
        if (this.getStart().getTime() != calEvent.end.getTime() && this.getEnd().getTime() != calEvent.start.getTime() && !this.getOption('free')) {
            isFree = false;
            return false;
        }
        return true;
    });

    if (isFree) {
        var $dialogContent = $("#event_edit_container");
        resetForm($dialogContent);

        $dialogContent.dialog({
            modal: true,
            title: "Nuevo turno",
            close: function () {
                $dialogContent.dialog("destroy");
                $dialogContent.hide();
                //  $('#calendar').weekCalendar("removeUnsavedEvents");
            },
            buttons: {
                Aceptar: function () {
                     var tel = $dialogContent.find("input[name='telefono']");
                     var mail = $dialogContent.find("input[name='mail']");

                    calEvent.title = 'Turno con Graciela!';
                    calEvent.end = new Date(calEvent.start).addMinutes(40);
                    $(calendar).weekCalendar('updateEvent', calEvent);
                    $dialogContent.dialog("close");
                    var apiCall = $.post('../Turno/ReservarTurno', { clienteId: 55, fechaI: calEvent.start, fechaF: calEvent.end, telefono: tel, mail:mail });
                },
                Cancelar: function () {
                    $(calendar).weekCalendar('removeEvent', calEvent.id);
                    $dialogContent.dialog("close");
                    return false;
                }
            }
        }).show();

        $dialogContent.find(".date_holder").text($('#calendar').weekCalendar("formatDate", calEvent.start));
        return true;
    }
    else {
        $(calendar).weekCalendar('removeEvent', calEvent.id);
        return false;
    }
} ;           
            
function alto() {
    return $(window).height() - $('h1').outerHeight(true);
};

var d = new Date();
d.setDate(d.getDate() - (d.getDay() - 3));
year = d.getFullYear();
month = d.getMonth();
day = d.getDate();

var listaPublicaTurnosDisponibles;


function datosTurnosDisponibles(start, end, callback) {
    //--------------------------------------------------------------------------------------------
    // FER: Tendras una solucion mas elegante?
    //Porque json y mvc es una merda, tengo que transformar los formatos fecha que vienen en ISO a formato date javascript!!
    for(var n= 0; n<listaPublicaTurnosDisponibles.length ; n++)
    {
        if (typeof (listaPublicaTurnosDisponibles[n].start) == "string") {
            listaPublicaTurnosDisponibles[n].start = new Date(parseInt(listaPublicaTurnosDisponibles[n].start.substr(6)));
            listaPublicaTurnosDisponibles[n].end = new Date(parseInt(listaPublicaTurnosDisponibles[n].end.substr(6)));
        }
    }
    //--------------------------------------------------------------------------------------------
    callback({
        options: {
            defaultFreeBusy: { free: false },
            timeslotsPerHour: 3,
            timeslotHeight: 30
        },
        freebusys: listaPublicaTurnosDisponibles,
        events: [] 
        });
};
/*
[
    { "start": new Date(2011, 10, 9, 9, 0), "end": new Date(2011, 10, 9, 10, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 10, 0), "end": new Date(2011, 10, 9, 11, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 11, 0), "end": new Date(2011, 10, 9, 12, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 12, 0), "end": new Date(2011, 10, 9, 13, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 13, 0), "end": new Date(2011, 10, 9, 14, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 14, 0), "end": new Date(2011, 10, 9, 15, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 15, 0), "end": new Date(2011, 10, 9, 16, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 16, 0), "end": new Date(2011, 10, 9, 17, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 17, 0), "end": new Date(2011, 10, 9, 18, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 10, 0), "end": new Date(2011, 10, 9, 11, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 11, 0), "end": new Date(2011, 10, 9, 12, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 12, 0), "end": new Date(2011, 10, 9, 13, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 13, 0), "end": new Date(2011, 10, 9, 14, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 14, 0), "end": new Date(2011, 10, 9, 15, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 15, 0), "end": new Date(2011, 10, 9, 16, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 16, 0), "end": new Date(2011, 10, 9, 17, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 17, 0), "end": new Date(2011, 10, 9, 18, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 10, 0), "end": new Date(2011, 10, 9, 11, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 11, 0), "end": new Date(2011, 10, 9, 12, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 12, 0), "end": new Date(2011, 10, 9, 13, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 13, 0), "end": new Date(2011, 10, 9, 14, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 14, 0), "end": new Date(2011, 10, 9, 15, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 15, 0), "end": new Date(2011, 10, 9, 16, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 16, 0), "end": new Date(2011, 10, 9, 17, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 17, 0), "end": new Date(2011, 10, 9, 18, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 10, 0), "end": new Date(2011, 10, 9, 11, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 11, 0), "end": new Date(2011, 10, 9, 12, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 12, 0), "end": new Date(2011, 10, 9, 13, 0), "free": true, userId: [0, 1] }, { "start": new Date(2011, 10, 9, 13, 0), "end": new Date(2011, 10, 9, 14, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 14, 0), "end": new Date(2011, 10, 9, 15, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 15, 0), "end": new Date(2011, 10, 9, 16, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 16, 0), "end": new Date(2011, 10, 9, 17, 0), "free": true, userId: [0, 2] }, { "start": new Date(2011, 10, 9, 17, 0), "end": new Date(2011, 10, 9, 18, 0), "free": true, userId: [0, 2] }
]
*/

function setupCalendario(recursos, disponibilidad) {
    listaPublicaTurnosDisponibles = disponibilidad;
    var $calendar = $('#calendar').weekCalendar({
        timeslotsPerHour: 4,
        scrollToHourMillis: 0,
        height: alto,
        eventRender: mostrar,
        eventNew: nuevo,
        data: datosTurnosDisponibles,
        users: recursos,
        showAsSeparateUser: true,
        displayOddEven: true,
        daysToShow: 1,
        switchDisplay: { '1 dia': 1, '3 dias': 3 },
        headerSeparator: ' ',
        minDate: new Date(),
        maxDate: new Date(year, month, day + 30),
        use24Hour: true,
        businessHours: { start: 0, end: 0, limitDisplay: false },
        buttonText: { today: "Hoy", lastWeek: "Anterior", nextWeek: "Proximo" },
        draggable: function (calEvent, eventElement) { return false; },
        resizable: function (calEvent, eventElement) { return false; },
        displayFreeBusys: true,

        // I18N1
        firstDayOfWeek: $.datepicker.regional['es'].firstDay,
        shortDays: $.datepicker.regional['es'].dayNamesShort,
        longDays: $.datepicker.regional['es'].dayNames,
        shortMonths: $.datepicker.regional['es'].monthNamesShort,
        longMonths: $.datepicker.regional['es'].monthNames,
        dateFormat: "d F Y" // 25 Octubre 2011
    });
    };
    function dateToString(dFecha) {
        return dFecha.getFullYear() + "-" + padZero(dFecha.getMonth()+1,2) + "-" + padZero(dFecha.getDate(),2) + " " + padZero(dFecha.getHours(), 2) + ":" + padZero(dFecha.getMinutes(),2);
    }
    function padZero(num, length) {
        num = String(num);
        length = parseInt(length) || 2;
        while (num.length < length)
            num = "0" + num;
        return num;
    };