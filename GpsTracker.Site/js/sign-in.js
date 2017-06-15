var USER;
var PERSON;
var withMap;

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires="+d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/profile.php";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i = 0; i < ca.length; i++) {
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

function signIn(url, user, withMap)
{
    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(user),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function(userData)
        {
            if(userData != null)
            {
                USER = userData;
                mutex = true;
                if(withMap)
                {
                    showMarkers(USER);
                }
                else
                {
                    fillUserInfo();
                }
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
                        }
                        if(!withMap)
                        {
                            fillPersonInfo(PERSON);
                        }
                    }
                });
                setCookie("UserId", USER.UserId, 1);
            }
        }
    });
}

function checkCookie(withMap) {
    var index = getCookie("UserId");
    if (index != "") {
        var user = {
            UserId: parseInt(index)
        };
        signIn('api/web/user/id',user,withMap);
    }
}

function authorize()
{
    var authorized = $(".authorized").toArray();
    var unauthorized = $(".unauthorized").toArray();
    $(authorized).switchClass("authorized", "unauthorized");
    $(unauthorized).switchClass("unauthorized", "authorized");
}

$("#sign_out_button").bind("click",function(){
    document.cookie = "";
    PERSON = null;
    USER = null;
    authorize();
    setCookie("UserId", "", 0);
    mutex = false;
    clearMarkers();
});

$("#sign_button").bind("click", function send() {
    var user = {
        Login: $("#login").val(),
        Password: $("#pass").val(),
    }
    signIn('api/web/user/login',user,withMap);
});

function getUser()
{
    if(USER != null)
    {
        return {UserId: USER.UserId};
    }
    return null;
}