Turnos = { };
Turnos.Recursos = new EntityList([
        { "id": 1, "FirstName": "root", "LastName": "user", "nombre": "root user", "Username": "root", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": true, "Administrator": true, "IsDeleted": false, "GravatarFeed": "a52d6d241c3ba99e8eeed063a7c0a664", "EmailAddress": "info@bandit-software.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "a52d6d241c3ba99e8eeed063a7c0a664?s=25", "Settings": null, "Password": null, "ConfirmPassword": null },
        { "id": 101, "FirstName": "testuser", "LastName": "board", "nombre": "testuser board", "Username": "testuser@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "ed53e691ee322e24d8cc843fff68ebc6", "EmailAddress": "testuser@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "ed53e691ee322e24d8cc843fff68ebc6?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, 
        { "id": 102, "FirstName": "testmanager", "LastName": "board", "nombre": "testmanager board", "Username": "testmanager@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "006891a641e5d0bd9c3e2920465713df", "EmailAddress": "testmanager@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "006891a641e5d0bd9c3e2920465713df?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 103, "FirstName": "testreadonly", "LastName": "board", "nombre": "testreadonly board", "Username": "testreadonly@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "446f6f59f1fc3647e18ca5d612d1796d", "EmailAddress": "testreadonly@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "446f6f59f1fc3647e18ca5d612d1796d?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 104, "FirstName": "testnoaccess", "LastName": "board", "nombre": "testnoaccess board", "Username": "testnoaccess@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "96e8d0604839385c032f3841a7452948", "EmailAddress": "testnoaccess@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "96e8d0604839385c032f3841a7452948?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 101000, "FirstName": "sd@asdsa.com", "LastName": "dsfsdf", "nombre": "sd@asdsa.com dsfsdf", "Username": "sd@asdsa.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "eb5674361e42e52970cf4e8e31e0103d", "EmailAddress": "sd@asdsa.com", "DateFormat": "dd/MM/yyyy", "TimeZone": "Dateline Standard Time", "GravatarLink": "eb5674361e42e52970cf4e8e31e0103d?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 101001, "FirstName": "sdasdsa.com", "LastName": "dsfsdf", "nombre": "sdasdsa.com dsfsdf", "Username": "ssd@asdsa.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "9c14aa2d1cc19e66e28bb5e704d8869b", "EmailAddress": "ssd@asdsa.com", "DateFormat": "dd/MM/yyyy", "TimeZone": "Dateline Standard Time", "GravatarLink": "9c14aa2d1cc19e66e28bb5e704d8869b?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 101002, "FirstName": "sdasdsawww.com", "LastName": "dsfswwwwdf", "nombre": "sdasdsawww.com dsfswwwwdf", "Username": "swdwdsd@asdsa.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "e2150b29e48f20c7c702638ca219ee77", "EmailAddress": "swdwdsd@asdsa.com", "DateFormat": "dd/MM/yyyy", "TimeZone": "Dateline Standard Time", "GravatarLink": "e2150b29e48f20c7c702638ca219ee77?s=25", "Settings": null, "Password": null, "ConfirmPassword": null}]);


function RecursoController() {
    this.SystemType = 'RecursoController';
    this.selectedUserId = null;
    this.scrollable = null;
    this.hayBusqueda = false;
    this.busquedaDescendente = true;

    var thisController = this;
    
    var obtFilaId = function (item) {
        var rowid = $(item).parents(".recurso-fila").attr("id");
        var id = parseInt(rowid.split("_")[1], 10);
        return id;
    };

    var attachHandlers = function ($list) {
        
        $(".recurso-fila", $list).click(function () {
            thisController.edit(obtFilaId($(".editar",this)));
        });

        $(".editar", $list).click(function (e) {
            e.stopPropagation(); 
            thisController.edit(obtFilaId(this));
        });
        
        $('.eliminar', $list).click(function (e) {
            e.stopPropagation(); 
            thisController.deleteUserConfirmation(obtFilaId(this));
        });        
        
        $('.users-disabled, .tilde', $list).click(function (e) {
            e.stopPropagation();
            thisController.changeUserState(obtFilaId(this));
        });        
    };

    var getFormData = function () {
        return {
            Id: $("#User_Id").val(),
            EmailAddress: $("#User_EmailAddress").val(),
            FirstName: $("#recurso-nombre").val(),
            LastName: $("#User_LastName").val(),
            Enabled: $("#User_Enabled").is(":checked"),
            Administrator: $("#User_Administrator").is(":checked"),
            TimeZone: $("#User_TimeZone").val(),
            DateFormat: $("#User_DateFormat").val(),
            Password: $("#User_Password").val(),
            ConfirmPassword: $("#User_ConfirmPassword").val(),
            GrantAccessToBoards: $("#User_GrantAccessToBoards").val()
        };
    };

    var clearForm = function() {
        $("#User_Id").val("0");
        $("#User_EmailAddress").val("");
        $("#recurso-nombre").val("");
        $("#User_LastName").val("");
        $("#User_Enabled").attr("checked", false);
        $("#User_Administrator").attr("checked", false);
        $("#User_GrantAccessToBoards").attr("checked", false);
        $("#User_TimeZone").val("");
        $("#User_DateFormat").val("");
        $("#User_Password").val("");
        $("#User_ConfirmPassword").val("");
    };

    this.resetScreen = function () {
        thisController.scrollable.seekTo(0, 0);
    };
    
    this.showUsersList = function (users) {
        if (!users)
            users = Turnos.Recursos.toArray;
        var html = $.tmpl(RecursoPlantilla.FilaPlantilla, users);
        var $usersList = $('#recursos-lista').empty().append(html);
        
        $('#fila_' + Turnos.CurrentUserId + ' .eliminar')
            .removeClass('eliminar')
            .addClass('disabled-delete');
        
        attachHandlers($usersList);        
    };

    this.renderUsersList = function () {
        var users = Turnos.Recursos.toArray();
        var result = '';
        if (thisController.hayBusqueda) {
            var criteria = $('#recursos-busqueda').val();
            users = filterUsers(users, criteria);
        }

        if (users && users.length > 0)
            result = $.tmpl(RecursoPlantilla.FilaPlantilla, users);

        var $usersList = $('#recursos-lista').empty().append(result);
        attachHandlers($usersList);
    };

    this.deleteUserConfirmation = function (idUser) {       
        $('#recursos-confirmar-boarrado').show();

        $("#User_Id").val(idUser);
        thisController.scrollable.next();
    };

    var updateRemainingCreateUsers = function (data) {
        $('#users-remaining').text(data.usedUsersIndicator);                
        $('#crear-recurso-users-message').text(data.createUsersMessage);
        Turnos.AllowAddNewUsers = data.allowAddNewUsers;
    };
    
    this.deleteUser = function () {
        var idUser = $("#User_Id").val();
        var apiOptions = {
            success: function (result) {
                if (result.ReplyCode === API.ResponseCode.DataDeleteSuccess) {
                    removeUsersFromList(idUser);
                    updateRemainingCreateUsers(result.ReplyData[0]);
                    Turnos.showSuccess('The User was deleted succesfully');
                }
                else
                    Turnos.showError(result.ReplyText);
            }
        };

        API.users.deleteUser(idUser, apiOptions);
        thisController.scrollable.prev();
    };

    var removeUsersFromList = function (id) {
        var rowToRemove = $('#fila_' + id + '.recurso-fila');
        rowToRemove.slideUp('slow', rowToRemove.remove);
        Turnos.Recursos.deleteById(id);
    };

    ///Filter users that match filterText
    var filterUsers = function (usersList, filterText) {
        filterText = filterText.toUpperCase();
        var resultList = [];
        $.each(usersList, function () {
            if (!filterText || this.nombre.toUpperCase().indexOf(filterText) >= 0) {
                resultList.push(this);
            }
        });
        return resultList;
    };
  
    this.search = function () {
        var f = function () {
            var criteria = $('#recursos-busqueda').val();
            thisController.hayBusqueda = criteria != '';
            thisController.renderUsersList();
        };
        setTimeout(f, 1);
    };

    this.inicializar = function () {
        

        this.scrollable = $("#recursos-desplazable").scrollable({onSeek: hidePanels}).data("scrollable");
        hidePanels(null, 0);
        this.showUsersList();

        $("#recursos-formulario label.info").tooltip({ position: "bottom center", tip: "#organizational-users-tooltip" });
        $("#organizational-users-tooltip").remove().appendTo("body");

        $("#recursos-formulario-cancelar").click(function () {
            thisController.scrollable.prev();
        });

        $("#crear-recurso").click(this.showCreate);

        $("#recursos-formulario-save").click(this.save);

        $('#recursos-busqueda').keydown(this.search);
        $('#recursos-por-nombre').click(thisController.sortByName);
        $('#users-change-password-cancelar').click(function () {
            clearFields();
            thisController.scrollable.prev();
        });
        $('#users-save-new-password').click(this.saveNewPassword);

        $("#eliminacion-del-recurso-confirmada").click(thisController.deleteUser);
        $("#recursos-confirmar-boarrado-cancelar").click(function () {
            thisController.scrollable.prev();
        });
    };

    this.edit = function (userId) {
        var user = Turnos.Recursos.getById(userId);
        if (user) {
            $("#User_Id").val(user.Id);
            $("#User_EmailAddress").val(user.EmailAddress);
            $("#recurso-nombre").val(user.FirstName);
            $("#User_LastName").val(user.LastName);
            $("#User_Enabled").attr("checked", user.Enabled);
            $("#User_Administrator").attr("checked", user.Administrator);
            $("#User_TimeZone").val(user.TimeZone);
            $("#User_DateFormat").val(user.DateFormat);
            $(".users-password").hide();
            $("#users-grantAccess").hide();

            $('#recursos-formulario-contenedor').show();
            //$('#User_EmailAddress').select();
            thisController.scrollable.next();
            
        }
    };

    this.showCreate = function () {
        clearForm();
        $(".users-password").show();
        $("#users-grantAccess").show();

        $('#recursos-formulario-contenedor').show();
        $('#User_Enabled').attr('checked', Turnos.AllowAddNewUsers);
        //$('#User_EmailAddress').select();
        thisController.scrollable.next();
        
    };

    this.save = function () {
        var user = getFormData();
        
        var shouldRefresh = false;
        if (Turnos.CurrentUserId === user.Id) {
            var currentUser = Turnos.Recursos.getById(Turnos.CurrentUserId);
            if (currentUser.Administrator != user.Administrator) 
                shouldRefresh = true;
        }
        
        if ($("#recursos-formulario").valid()) {
            API.users.save(user)
            .done(function (response) {
                if (shouldRefresh) {
                    window.location.reload();
                    return;
                }
                var userData = Turnos.Recursos.getById(user.Id);
                var responseData = response.ReplyData[0];
                var remainingCreateUsers;
                if (userData) {
                    remainingCreateUsers = responseData;
                    userData.EmailAddress = user.EmailAddress;
                    userData.FirstName = user.FirstName;
                    userData.LastName = user.LastName;
                    var separator = user.FirstName && user.LastName ? " " : "";
                    userData.nombre = user.FirstName + separator + user.LastName;
                    userData.Enabled = user.Enabled;
                    userData.Administrator = user.Administrator;
                    userData.TimeZone = user.TimeZone;
                    userData.DateFormat = user.DateFormat;
                } else {
                    remainingCreateUsers = responseData.remainingCreateUsers;
                    Turnos.Recursos.push(responseData.user);
                    Turnos.tracker.create_user();
                }
                updateRemainingCreateUsers(remainingCreateUsers);
                thisController.scrollable.prev();
                thisController.showUsersList();
                Turnos.showSuccess(response.ReplyText);
            });
        }
    };
    
    this.sortByName = function () {
        function sortByName(a, b) {
            var x = a.nombre.toLowerCase();
            var y = b.nombre.toLowerCase();
            return x < y ? -1 : (x > y ? 1 : 0);
        }
        sort(sortByName);
    };

    var sort = function (sortFunction) {
        var users = Turnos.Recursos.toArray();
        users.sort(sortFunction);

        var label = $('#recursos-por-nombre');
        var asc = 'asc';
        var desc = 'desc';

        if (thisController.busquedaDescendente) {
            users.reverse();
            label.addClass(asc).removeClass(desc);
        } else 
            label.addClass(desc).removeClass(asc);
        
        thisController.busquedaDescendente = !thisController.busquedaDescendente;
        thisController.showUsersList(users);
    };

    var getFormPassword = function () {
        return {
            Id: $('#User_Id').val(),
            NewPassword: $('#User_NewPassword').val(),
            ConfirmNewPassword: $('#User_ConfirmNewPassword').val()            
        };
    };

    this.changePassword = function (userId) {
        clearFields();
        var user = getFormPassword();
        if (user) {
            $("#User_Id").val(userId);

            $('#users-change-password').show();
            
            thisController.scrollable.next();
        }
    };

    this.saveNewPassword = function () {
        if ($("#users-change-password-form").valid()) {
            var user = getFormPassword();

            if (user) {
                API.users.saveNewPassword(user)
                    .done(function (response) {
                        
                        thisController.scrollable.prev();
                        thisController.showUsersList();
                        Turnos.showSuccess(response.ReplyText);
                    });
            }
        }
    };

    this.changeUserState = function (userId) {
        if (userId) {
            API.users.changeUserState(userId)
                .done(function (response) {
                    var userData = Turnos.Recursos.getById(userId);
                    userData.Enabled = !userData.Enabled;
                    updateRemainingCreateUsers(response.ReplyData[0]);
                    thisController.showUsersList();
                    Turnos.showSuccess(response.ReplyText);                    
                });
        }
        };

        var hidePanels = function (e, index) {
            if (index === 0) {
                $('#recursos-formulario-contenedor').hide();
                $('#users-change-password').hide();
                $('#recursos-confirmar-boarrado').hide();
            }
        };

    var clearFields = function () {
        $('#User_NewPassword').val('');
        $('#User_ConfirmNewPassword').val('');
    };
}

function User(name) {
    this.SystemType = 'User';
    this.Id = 0;
    this.Name = name?name:'';     
 }