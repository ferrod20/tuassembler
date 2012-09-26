function App() {
    this.SystemType = 'App';
    this.api = new Api();
    var notificador = new Notifier();
    var self = this;

    this.init = function() {
        notificador.init();
    };

    this.mostrarInfo = function (msg) {
        return notificador.showInfo(msg);
    };

    this.mostrarError = function (msg) {
        return notificador.showError(msg);
    };

    this.mostrarAdvertencia = function (msg) {
        return notificador.showWarning(msg);
    };
    
    this.mostrarAcierto = function (msg) {
        return notificador.showSuccess(msg);
    };

    this.ventanaDeConfirmacion = function (opts) {
        return $.confirm(opts);
    };

    this.ventanaDeAlerta = function (opts) {
        return $.alert(opts);
    };

    this.ventanaDePregunta = function (opts) {
        return $.prompt(opts);
    };
};