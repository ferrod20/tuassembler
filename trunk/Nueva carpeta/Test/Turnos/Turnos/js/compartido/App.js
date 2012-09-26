function App() {
    this.SystemType = 'App';
    this.api = new Api();
    this.notificador = new Notifier();
    var self = this;

    this.init = function() {
        notificador.init();
    };

    this.mostrarInfo = function (msg) {
        return self.notificador.showInfo(msg);
    };

    this.mostrarError = function (msg) {
        return self.notificador.showError(msg);
    };

    this.mostrarAdvertencia = function (msg) {
        return self.notificador.showWarning(msg);
    };
    
    this.mostrarAcierto = function (msg) {
        return self.notificador.showSuccess(msg);
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