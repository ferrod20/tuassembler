KANBAN = { };
KANBAN.UsersList = new EntityList([
        { "Id": 1, "FirstName": "root", "LastName": "user", "FullName": "root user", "Username": "root", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": true, "Administrator": true, "IsDeleted": false, "GravatarFeed": "a52d6d241c3ba99e8eeed063a7c0a664", "EmailAddress": "info@bandit-software.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "a52d6d241c3ba99e8eeed063a7c0a664?s=25", "Settings": null, "Password": null, "ConfirmPassword": null },
        { "Id": 101, "FirstName": "testuser", "LastName": "board", "FullName": "testuser board", "Username": "testuser@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "ed53e691ee322e24d8cc843fff68ebc6", "EmailAddress": "testuser@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "ed53e691ee322e24d8cc843fff68ebc6?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, 
        { "Id": 102, "FirstName": "testmanager", "LastName": "board", "FullName": "testmanager board", "Username": "testmanager@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "006891a641e5d0bd9c3e2920465713df", "EmailAddress": "testmanager@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "006891a641e5d0bd9c3e2920465713df?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 103, "FirstName": "testreadonly", "LastName": "board", "FullName": "testreadonly board", "Username": "testreadonly@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "446f6f59f1fc3647e18ca5d612d1796d", "EmailAddress": "testreadonly@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "446f6f59f1fc3647e18ca5d612d1796d?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 104, "FirstName": "testnoaccess", "LastName": "board", "FullName": "testnoaccess board", "Username": "testnoaccess@test.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "96e8d0604839385c032f3841a7452948", "EmailAddress": "testnoaccess@test.com", "DateFormat": "MM/dd/yyyy", "TimeZone": null, "GravatarLink": "96e8d0604839385c032f3841a7452948?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 101000, "FirstName": "sd@asdsa.com", "LastName": "dsfsdf", "FullName": "sd@asdsa.com dsfsdf", "Username": "sd@asdsa.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "eb5674361e42e52970cf4e8e31e0103d", "EmailAddress": "sd@asdsa.com", "DateFormat": "dd/MM/yyyy", "TimeZone": "Dateline Standard Time", "GravatarLink": "eb5674361e42e52970cf4e8e31e0103d?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 101001, "FirstName": "sdasdsa.com", "LastName": "dsfsdf", "FullName": "sdasdsa.com dsfsdf", "Username": "ssd@asdsa.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "9c14aa2d1cc19e66e28bb5e704d8869b", "EmailAddress": "ssd@asdsa.com", "DateFormat": "dd/MM/yyyy", "TimeZone": "Dateline Standard Time", "GravatarLink": "9c14aa2d1cc19e66e28bb5e704d8869b?s=25", "Settings": null, "Password": null, "ConfirmPassword": null }, { "Id": 101002, "FirstName": "sdasdsawww.com", "LastName": "dsfswwwwdf", "FullName": "sdasdsawww.com dsfswwwwdf", "Username": "swdwdsd@asdsa.com", "Role": 0, "WIP": 0, "Enabled": true, "IsAccountOwner": false, "Administrator": false, "IsDeleted": false, "GravatarFeed": "e2150b29e48f20c7c702638ca219ee77", "EmailAddress": "swdwdsd@asdsa.com", "DateFormat": "dd/MM/yyyy", "TimeZone": "Dateline Standard Time", "GravatarLink": "e2150b29e48f20c7c702638ca219ee77?s=25", "Settings": null, "Password": null, "ConfirmPassword": null}]);


function UsersController() {
    this.SystemType = 'UsersController';
    this.selectedUserId = null;
    this.scrollable = null;
    this.isSearch = false;
    this.sortNameDesc = true;

    /*private parts*/
    var thisController = this;
    
    var getRowId = function (item) {
        var rowid = $(item).parents(".user-row").attr("id");
        var id = parseInt(rowid.split("_")[1], 10);
        return id;
    };

    var attachHandlers = function ($list) {
        
        $(".user-row", $list).click(function () {
            thisController.edit(getRowId($(".users-edit",this)));
        });

        $(".users-edit", $list).click(function (e) {
            e.stopPropagation(); 
            thisController.edit(getRowId(this));
        });

        $(".users-change-password", $list).click(function (e) {
            e.stopPropagation(); 
            thisController.changePassword(getRowId(this));
        });

        $('.users-delete', $list).click(function (e) {
            e.stopPropagation(); 
            thisController.deleteUserConfirmation(getRowId(this));
        });        
        
        $('.users-disabled, .users-enabled', $list).click(function (e) {
            e.stopPropagation();
            thisController.changeUserState(getRowId(this));
        });        
    };

    var getFormData = function () {
        return {
            Id: $("#User_Id").val(),
            EmailAddress: $("#User_EmailAddress").val(),
            FirstName: $("#User_FirstName").val(),
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
        $("#User_FirstName").val("");
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
            users = KANBAN.UsersList.toArray;
        var html = $.tmpl(UsersTemplate.RowTemplate, users);
        var $usersList = $('#users-list').empty().append(html);
        
        $('#row_' + KANBAN.CurrentUserId + ' .users-delete')
            .removeClass('users-delete')
            .addClass('disabled-delete');
        
        attachHandlers($usersList);        
    };

    this.renderUsersList = function () {
        var users = KANBAN.UsersList.toArray();
        var result = '';
        if (thisController.isSearch) {
            var criteria = $('#users-search').val();
            users = filterUsers(users, criteria);
        }

        if (users && users.length > 0)
            result = $.tmpl(UsersTemplate.RowTemplate, users);

        var $usersList = $('#users-list').empty().append(result);
        attachHandlers($usersList);
    };

    this.deleteUserConfirmation = function (idUser) {       
        $('#users-delete-confirmation').show();

        $("#User_Id").val(idUser);
        thisController.scrollable.next();
    };

    var updateRemainingCreateUsers = function (data) {
        $('#users-remaining').text(data.usedUsersIndicator);                
        $('#users-create-users-message').text(data.createUsersMessage);
        KANBAN.AllowAddNewUsers = data.allowAddNewUsers;
    };
    
    this.deleteUser = function () {
        var idUser = $("#User_Id").val();
        var apiOptions = {
            success: function (result) {
                if (result.ReplyCode === API.ResponseCode.DataDeleteSuccess) {
                    removeUsersFromList(idUser);
                    updateRemainingCreateUsers(result.ReplyData[0]);
                    KANBAN.showSuccess('The User was deleted succesfully');
                }
                else
                    KANBAN.showError(result.ReplyText);
            }
        };

        API.users.deleteUser(idUser, apiOptions);
        thisController.scrollable.prev();
    };

    var removeUsersFromList = function (id) {
        var rowToRemove = $('#row_' + id + '.user-row');
        rowToRemove.slideUp('slow', rowToRemove.remove);
        KANBAN.UsersList.deleteById(id);
    };

    ///Filter users that match filterText
    var filterUsers = function (usersList, filterText) {
        filterText = filterText.toUpperCase();
        var resultList = [];
        $.each(usersList, function () {
            if (!filterText || this.FullName.toUpperCase().indexOf(filterText) >= 0) {
                resultList.push(this);
            }
        });
        return resultList;
    };
  
    this.search = function () {
        var f = function () {
            var criteria = $('#users-search').val();
            thisController.isSearch = criteria != '';
            thisController.renderUsersList();
        };
        setTimeout(f, 1);
    };

    this.init = function () {
        message = $('#users-root');
        this.scrollable = $("#users-scroll").scrollable({onSeek: hidePanels}).data("scrollable");
        hidePanels(null, 0);
        this.showUsersList();

        $("#users-form label.info").tooltip({ position: "bottom center", tip: "#organizational-users-tooltip" });
        $("#organizational-users-tooltip").remove().appendTo("body");

        $("#users-form-cancel").click(function () {
            thisController.scrollable.prev();
        });

        $("#users-create").click(this.showCreate);

        $("#users-form-save").click(this.save);

        $('#users-search').keydown(this.search);
        $('#users-by-name').click(thisController.sortByName);
        $('#users-change-password-cancel').click(function () {
            clearFields();
            thisController.scrollable.prev();
        });
        $('#users-save-new-password').click(this.saveNewPassword);

        $("#users-delete-confirmated").click(thisController.deleteUser);
        $("#users-delete-confirmation-cancel").click(function () {
            thisController.scrollable.prev();
        });
    };

    this.edit = function (userId) {
        var user = KANBAN.UsersList.getById(userId);
        if (user) {
            $("#User_Id").val(user.Id);
            $("#User_EmailAddress").val(user.EmailAddress);
            $("#User_FirstName").val(user.FirstName);
            $("#User_LastName").val(user.LastName);
            $("#User_Enabled").attr("checked", user.Enabled);
            $("#User_Administrator").attr("checked", user.Administrator);
            $("#User_TimeZone").val(user.TimeZone);
            $("#User_DateFormat").val(user.DateFormat);
            $(".users-password").hide();
            $("#users-grantAccess").hide();

            $('#users-form-container').show();
            //$('#User_EmailAddress').select();
            thisController.scrollable.next();
            
        }
    };

    this.showCreate = function () {
        clearForm();
        $(".users-password").show();
        $("#users-grantAccess").show();

        $('#users-form-container').show();
        $('#User_Enabled').attr('checked', KANBAN.AllowAddNewUsers);
        //$('#User_EmailAddress').select();
        thisController.scrollable.next();
        
    };

    this.save = function () {
        var user = getFormData();
        
        var shouldRefresh = false;
        if (KANBAN.CurrentUserId === user.Id) {
            var currentUser = KANBAN.UsersList.getById(KANBAN.CurrentUserId);
            if (currentUser.Administrator != user.Administrator) 
                shouldRefresh = true;
        }
        
        if ($("#users-form").valid()) {
            API.users.save(user)
            .done(function (response) {
                if (shouldRefresh) {
                    window.location.reload();
                    return;
                }
                var userData = KANBAN.UsersList.getById(user.Id);
                var responseData = response.ReplyData[0];
                var remainingCreateUsers;
                if (userData) {
                    remainingCreateUsers = responseData;
                    userData.EmailAddress = user.EmailAddress;
                    userData.FirstName = user.FirstName;
                    userData.LastName = user.LastName;
                    var separator = user.FirstName && user.LastName ? " " : "";
                    userData.FullName = user.FirstName + separator + user.LastName;
                    userData.Enabled = user.Enabled;
                    userData.Administrator = user.Administrator;
                    userData.TimeZone = user.TimeZone;
                    userData.DateFormat = user.DateFormat;
                } else {
                    remainingCreateUsers = responseData.remainingCreateUsers;
                    KANBAN.UsersList.push(responseData.user);
                    KANBAN.tracker.create_user();
                }
                updateRemainingCreateUsers(remainingCreateUsers);
                thisController.scrollable.prev();
                thisController.showUsersList();
                KANBAN.showSuccess(response.ReplyText);
            });
        }
    };
    
    this.sortByName = function () {
        function sortByName(a, b) {
            var x = a.FullName.toLowerCase();
            var y = b.FullName.toLowerCase();
            return x < y ? -1 : (x > y ? 1 : 0);
        }
        sort(sortByName);
    };

    var sort = function (sortFunction) {
        var users = KANBAN.UsersList.toArray();
        users.sort(sortFunction);

        var label = $('#users-by-name');
        var asc = 'asc';
        var desc = 'desc';

        if (thisController.sortNameDesc) {
            users.reverse();
            label.addClass(asc).removeClass(desc);
        } else 
            label.addClass(desc).removeClass(asc);
        
        thisController.sortNameDesc = !thisController.sortNameDesc;
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
                        KANBAN.showSuccess(response.ReplyText);
                    });
            }
        }
    };

    this.changeUserState = function (userId) {
        if (userId) {
            API.users.changeUserState(userId)
                .done(function (response) {
                    var userData = KANBAN.UsersList.getById(userId);
                    userData.Enabled = !userData.Enabled;
                    updateRemainingCreateUsers(response.ReplyData[0]);
                    thisController.showUsersList();
                    KANBAN.showSuccess(response.ReplyText);                    
                });
        }
        };

        var hidePanels = function (e, index) {
            if (index === 0) {
                $('#users-form-container').hide();
                $('#users-change-password').hide();
                $('#users-delete-confirmation').hide();
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