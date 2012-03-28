var UsersTemplate  = {
    RowTemplate: jQuery.template("user_row_template",
"<div class='user-row' id='row_${Id}'>                 \
    <div class='avatar'><img src='https://secure.gravatar.com/avatar/${GravatarLink}' alt='User avatar'></img></div>   \
    <div id='name-container'>                                                                                               \
        <div id='fullname'>${FullName}</div>                                                                              \
        <div id='emailaddress'>${EmailAddress}</div>                                                                     \
    </div>                                                                                                                \
    <div id='icons-container'>                                                                                             \
    <span {{if !IsAccountOwner}}class='users-delete'{{else}}class='disabled-delete'{{/if}} title='Delete'></span>          \
    <span class='users-edit' title='Edit'></span>                                                                           \
    <span class='users-change-password' title='Change Password'></span>                                                     \
    <span {{if !IsAccountOwner}}{{if Enabled}}class='users-enabled'title='User Enabled'{{else}}class='users-disabled'title='User Disabled'{{/if}}{{/if}}></span>    \
    <div {{if Enabled}}class='users-status-enabled'{{else}}class='users-status-disabled'{{/if}}></div>                      \
    </div>                                                                                                                  \
</div>")
};
