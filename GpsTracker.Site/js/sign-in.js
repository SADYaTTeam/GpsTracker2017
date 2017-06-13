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

    function authorize()
    {
        var authorized = $(".authorized").toArray();
        var unauthorized = $(".unauthorized").toArray();
        $(authorized).switchClass("authorized", "unauthorized");
        $(unauthorized).switchClass("unauthorized", "authorized");
    }
    
    $("#sign_out_button").bind("click",function(){
        authorize();
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
                if(userData != null)
                {
                    USER = userData;
                    $.ajax({
                        url: 'api/web/person',
                        type: "POST",
                        data: JSON.stringify({ UserId: userData.UserId }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function(personData){
                            PERSON = personData;
                            $("#login_area").text(USER.Login)
                            authorize();
                            if(PERSON.Photo != null)
                            {
                                document.getElementById("avatar").src = "data:image/png;base64," + PERSON.Photo;
                                //$("#avatar").atr("src", "data:image/png;base64," + PERSON.Photo);
                            }
                        }
                    });
                }
                else
                {
                    $("#login").val("");
                    $("#pass").val("");
                    alert("Wrong login or password");
                }
            }
        });        
    });
})