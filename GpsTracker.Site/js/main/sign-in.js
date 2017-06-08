$("#sign_button").bind("click",function send() {
        var person = {
            Login: $("#login").val(),
            Password:$("#pass").val(),
        }

        $.ajax({
            url: 'gpstrackerservice.azurewebsites.net/api/web/user/login',
            type: 'post',
            dataType: 'json',
            success: function (data) {
                
            },
            data: person
        });
    });