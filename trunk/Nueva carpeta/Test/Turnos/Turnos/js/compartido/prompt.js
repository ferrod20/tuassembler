(function ($) {
    var defaultOptions = {
        title: 'Importante',
        acceptButtonText: "Aceptar",
        cancelButtonText: "Cancelar",
        zIndex: 99999,
        multiline: true,
        expose: {
            maskId: 'promptMask',
            color: '#000',
            loadSpeed: 1000,
            opacity: 0.4,
            '-moz-opacity': 0.40,
            '-khtml-opacity': 0.40
        }
    };

    $.confirm = function (options) {
        //events: onBeforeLoad, onClose, onAccept, onCancel
        //appearence: title, description, acceptButtonText (default: Accept), cancelButtonText (default: Cancel)
        //data: an object to use in accept and cancel functions
        options.acceptButtonText = "Si";
        options.cancelButtonText = "No";
        options.cancelButton = true;
        options.acceptButton = true;
        options.cancelButton = true;
        options.userInput = false;
        create(options);

    };

    $.alert = function (options) {
        if (typeof options == 'string')
            options = { description: options };
        //events: onBeforeLoad, onClose, onAccept
        //appearence: title, description, acceptButtonText (default: Accept)
        //data: an object to use in accept function
        options.acceptButton = true;
        options.cancelButton = false;
        options.userInput = false;
        create(options);
    };

    $.prompt = function (options) {
        //events: onBeforeLoad, onClose, onAccept, onCancel
        //appearence: title, description, acceptButtonText (default: Accept), cancelButtonText (default: Cancel)
        //data: an object to use in accept and cancel functions        
        options.acceptButton = true;
        options.cancelButton = true;
        options.userInput = true;
        create(options);
    };

    var dialog = $('#prompt-dialog');

    var dialogTemplate = jQuery.template("dialog_template",
                    '<div id="prompt-dialog" class="secondary-dialog"> \
    <div class="panel"> \
    <div> \
             <h2>${title}</h2> \
        <div> \
                 <label id="prompt-labelPrompt" for="prompt-dialogValue">${description}</label> \
                 <span id="prompt-message">{{html description}}</span> \
                 {{if multiline}} \
                 <textarea cols="47" rows="2" id="prompt-dialogValue" name="dialogValue"></textarea> \
                 {{else}} \
                 <input type="text" id="prompt-dialogValue" name="dialogValue"/> \
                 {{/if}} \
        </div> \
       <button id="prompt-dialog-accept-button" type="button" class="button">${acceptButtonText}</button> \
       <button id="prompt-dialog-cancel-button" type="button" class="button cancel">${cancelButtonText}</button> \
    </div> \
</div>');

    function createDialog(options) {
        dialog = $($.tmpl(dialogTemplate, options));
        $('body').append(dialog);

        $('#prompt-dialogValue', dialog).attr("tabindex", 1);
        $('#prompt-dialog-accept-button', dialog).attr("tabindex", 2);
        $('#prompt-dialog-cancel-button', dialog).attr("tabindex", 3);
    };

    function setDialogBehavior(options) {
        $('#kb-dialog-header').text(options.title);

        if (options.userInput) {
            $('#prompt-labelPrompt', dialog).show().text(options.description);
            $('#prompt-dialogValue', dialog).show();
            $('#prompt-message', dialog).hide();
        } else {
            $('#prompt-message', dialog).show().html(options.description);
            $('#prompt-labelPrompt', dialog).hide();
            $('#prompt-dialogValue', dialog).hide();
        }

        if (options.acceptButton) {
            $('#prompt-dialog-accept-button', dialog).text(options.acceptButtonText);
            $('#prompt-dialog-accept-button', dialog).show();
        }
        else
            $('#prompt-dialog-accept-button', dialog).hide();

        if (options.cancelButton) {
            $('##prompt-dialog-cancel-button', dialog).text(options.cancelButtonText);
            $('#prompt-dialog-cancel-button', dialog).show();
        }
        else
            $('#prompt-dialog-cancel-button', dialog).hide();

    };

    var cancelFunction;
    function setAcceptAndCancelFunctions(options) {
        if (options.acceptButton) {
            var acceptFunction = options.onAccept ? function () {
                result = "accept";
                var userInput = null;
                if (options.userInput)
                    userInput = $("#prompt-dialogValue").val();
                var close = options.onAccept(userInput, options.data);
                if (close == null || close == true)
                    dialog.overlay().close();
            } : function () {
                result = "accept";
                dialog.overlay().close();
            };
            $("#prompt-dialog-accept-button", dialog).unbind('click').click(acceptFunction);
        }

        if (options.cancelButton) {
            cancelFunction = options.onCancel ? function () {
                result = "cancel";
                var userInput = null;
                if (options.userInput)
                    userInput = $("#prompt-dialogValue").val();
                options.onCancel(userInput, options.data);
                dialog.overlay().close();
            } : function () {
                result = "cancel";
                dialog.overlay().close();
            };
            $("#prompt-dialog-cancel-button", dialog).unbind('click').click(cancelFunction);
        }

    };

    var result;
    function create(options) {
        result = null;

        var opts = $.extend($.extend({}, defaultOptions), options);
        if (!dialog.length)
            createDialog(opts);

        setDialogBehavior(opts);

        if (opts.userInput) {
            $('#prompt-dialogValue').val('');
        }

        setAcceptAndCancelFunctions(opts);

        //check if the expose mask is already loaded to prevent closing it when the dialog is closed
        var expose = $.mask.isLoaded() ? null : opts.expose;

        var $closeButton;
        if (!expose && opts.blockElement) {
            var $container = $(opts.blockElement).block({ message: '', bindEvents: false });
            if ($container.is(".modal-dialog")) {
                $closeButton = $container.find("a.close").hide();
            }
        }

        var overlayInstance = dialog.overlay({
            expose: expose,
            fixed: false,
            closeOnClick: false,
            closeOnEsc: true,
            oneInstance: false,
            onBeforeLoad: opts.onBeforeLoad,
            onLoad: function () {
                if ($('#prompt-dialogValue').is(":visible"))
                    $('#prompt-dialogValue').focus();
                else
                    $('#prompt-dialog-accept-button').focus();
            },
            onClose: function () {
                if (!result && $.isFunction(cancelFunction)) cancelFunction(); //when the prompt is closed without pressing a button.

                if (opts.onClose) {
                    var userInput = null;
                    if (opts.userInput)
                        userInput = $("#prompt-dialogValue").val();
                    opts.onClose(userInput);
                }
                if (!expose && opts.blockElement) {
                    $(opts.blockElement).unblock();
                    if ($closeButton) $closeButton.show();
                }

                dialog.data("overlay", null);
                delete overlayInstance;
            },
            load: true
        });

        dialog.css('z-index', opts.zIndex);
        dialog.overlay().load();

        dialog.height(dialog.find("div.panel>div").height() + 40);
    };

})(jQuery)