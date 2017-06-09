$(function(){
    var temp = {
        user: USER,
        person: PERSON
    }
    var USER;
    var PERSON;

    $(window).bind("load", function () {
        window.location.search.substr(1);
        //alert(JSON.stringify(temp));
    });

    $("#sign_button").bind("click", function send() {
        var user = {
            Login: $("#login").val(),
            Password: $("#pass").val(),
        }
        $.ajax ({
            url: 'api/web/user/login',
            type: "POST",
            data: JSON.stringify(user),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(userData){
                if(userData.UserId != null)
                {
                    USER = userData;
                    $.ajax({
                        url: 'api/web/person',
                        type: "POST",
                        data: JSON.stringify({ UserId: userData.UserId }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function(personData){
                            if(personData == null)
                            {
                                PERSON = null;
                            }
                            var authorized = $(".authorized").toArray();
                            var unauthorized = $(".unauthorized").toArray();
                            $(authorized).switchClass("authorized", "unauthorized");
                            $(unauthorized).switchClass("unauthorized", "authorized");
                        }
                    });
                }
            }
        });        
    });
})