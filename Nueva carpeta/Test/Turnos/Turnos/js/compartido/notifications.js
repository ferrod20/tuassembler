function Notifier() {
    var self = this;
    
    this.INFO = 'infoNotification';
    this.ERROR = 'errorNotification';
    this.WARNING = 'warningNotification';
    this.SUCCESS = 'successNotification';

    this.isOpen = false;
    this.message = "";
    this.options = null;
    
    this.defaultOptions = {
        toggleEffect: 'slide',
        toggleOptions: { direction: 'up' },
        autoHide: true,
        autoHideTime: 10000,
        type: self.INFO,
        block: false
    };
    
    var $notifier, $notifierText, hideTimeout;

    this.showWarning = function (msg) {
        return notify(msg, { type: self.WARNING, autoHide: true });
    };

    this.showInfo = function (msg) {
        return notify(msg, { type: self.WARNING, autoHide: true });
    };

    this.showError = function (msg) {
        return notify(msg, { type: self.ERROR, autoHide: true });
    };

    this.showSuccess = function (msg) {
        return notify(msg, { type: self.SUCCESS, autoHide: true });
    };

    var notify = function (msg, options) {
        if (msg) {
            $notifier = $("#notifications-container");
            $notifierText = $("#notifications-container p");

            self.options = $.extend({}, self.defaultOptions, options);
            self.message = msg;

            if (self.isOpen) {
                $notifier.removeClass(self.WARNING)
                     .removeClass(self.INFO)
                     .removeClass(self.ERROR)
                     .removeClass(self.SUCCESS)
                     .addClass(options.type);

                $notifierText.html(msg);

                if (options.autoHide)
                    hideTimeout = setTimeout(self.hide, options.autoHideTime || self.defaultOptions.autoHideTime);
            }
            else
                self.show();
        }
        return self;
    };

    this.init = function () {
        $("#notifications-container h3.close").click(self.hide);
        self.options = $.extend({ }, self.defaultOptions);
        self.isOpen = true;
        self.hide();
    };

    this.show = function() {
        if (hideTimeout) {
            clearTimeout(hideTimeout);
            hideTimeout = null;
        }

        if (self.isOpen)
            self.hide();

        var opts = self.options;
        $notifierText.html(self.message);       
        $notifier.attr("class", "hidden " + opts.type).toggle(opts.toggleEffect, opts.toggleOptions);
        self.isOpen = true;

        if (opts.autoHide) {
            hideTimeout = setTimeout(function() {
                self.hide();
                hideTimeout = null;
            }, opts.autoHideTime || self.defaultOptions.autoHideTime);
        }
    };

    this.hide = function() {
        if (self.isOpen) {
            var opts = self.options;
            $("#notifications-container").toggle(opts.toggleEffect, opts.toggleOptions);
            self.isOpen = false;
        }
    };
};