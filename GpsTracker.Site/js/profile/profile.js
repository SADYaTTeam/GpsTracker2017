withMap = false;
function showMenu(menu)
{
	var form;
	var forms = $("#user_form, #person_form, #friendlist_form");
	switch(menu){
		case "user":
		{
			form="#user_form";
			break;
		}
		case "person":
		{
			form="#person_form";
			break;
		}
		case "list":
		{
			form="#friendlist_form";
			break;
		}
		default:
		{
			return;
		}
	}
	if(!$("#user_form").hasClass("hidden")){
		$("#user_form").addClass("hidden")
	}
	if(!$("#person_form").hasClass("hidden")){
		$("#person_form").addClass("hidden")
	}
	if(!$("#friendlist_form").hasClass("hidden")){
		$("#friendlist_form").addClass("hidden")
	}
 	$(form).removeClass("hidden");
}	

function fillUserInfo()
{
	if(USER != null)
	{
		$("#login_field").val(USER.Login);
		$("#password_field").val(USER.Password);
	}
}

function fillPersonInfo(person)
{
	if(person != null)
	{
		var date = new Date(person.DateOfBirth);
		$("#first_name").val(person.FirstName);
		$("#middle_name").val(person.MiddleName);
		$("#last_name").val(person.LastName);
		switch(person.Gender)
		{
			case true:
			{
				$("#male").prop("checked", true);
				break;
			}
			case false:
			{
				$("#male").prop("checked", true);
				break;	
			}
			default:
			{
				break;
			}
		}
		$("#day").val(date.getDate());
		$("#month option[value=\""+date.getMonth()+"\"").prop("selected", true);
		$("#year").val(date.getFullYear());
		$("#email").val(person.Email);
		$("#phone").val(person.Phone);
		$("#small-avatar, #medium-avatar").attr("src", "data:image/png;base64," + person.Photo)
	}
}

$(function(){

	$(document).ready(function(){
	    checkCookie(false);
	});

	$("#list_info_button").bind("click",function(){
		showMenu("list");
	});

	$("#user_info_button").bind("click",function(){
		showMenu("user");
	});

	$("#person_info_button").bind("click",function(){
		showMenu("person");
	});

	$("save_button").bind("click", function(){
		
	})

	$("#picture").change(function(evt){
		var tgt = evt.target || window.event.srcElement,
        files = tgt.files;

    if (FileReader && files && files.length) {
        var fr = new FileReader();
        fr.onload = function () {
            $("#small-avatar, #medium-avatar").attr("src", fr.result);
        }
        fr.readAsDataURL(files[0]);
    }
	})
})