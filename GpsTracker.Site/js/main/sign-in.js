$(function () {
    var temp = {
        user: USER,
        person: PERSON
    }
    var USER;
    var PERSON;

    function getUser() {
        return {UserId:USER.UserId};
    }


    function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    $(window).bind("load", function () {

    });

    function authorize() {
        var authorized = $(".authorized").toArray();
        var unauthorized = $(".unauthorized").toArray();
        $(authorized).switchClass("authorized", "unauthorized");
        $(unauthorized).switchClass("unauthorized", "authorized");
    }

    $("#sign_out_button").bind("click", function () {
        authorize();
    });

    $("#sign_button").bind("click", function send() {
        var user = {
            Login: $("#login").val(),
            Password: $("#pass").val(),
        }
        $.ajax({
            url: 'api/web/user/login',
            type: "POST",
            data: JSON.stringify(user),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (userData) {
                if (userData != null) {
                    USER = userData;
                    $.ajax({
                        url: 'api/web/person',
                        type: "POST",
                        data: JSON.stringify({ UserId: userData.UserId }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (personData) {
                            PERSON = personData;
                            $("#login_area").text(USER.Login)
                            authorize();
                            if (PERSON.Photo != null) {
                                document.getElementById("avatar").src = "data:image/png;base64," + PERSON.Photo;
                            }
                        }
                    });
                    document.cookie = "UserId=" + USER.UserId;
                }
                else {
                    $("#login").val("");
                    $("#pass").val("");
                    alert("Wrong login or password");
                }
            }
        });
    });
})