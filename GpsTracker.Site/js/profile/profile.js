withMap = false;
var option = 0;
function showMenu(menu)
{
	var form;
	var forms = $("#user_form, #person_form, #friendlist_form");
	switch(menu){
		case "user":
		{
			form="#user_form";
			option = 0;
			break;
		}
		case "person":
		{
			form="#person_form";
			option = 1;
			break;
		}
		case "list":
		{
			form="#friendlist_form";
			option = 2;
			break;
		}
		case "log":
		{
			form="#log_form";
			option = 3;
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
	if(!$("#log_form").hasClass("hidden")){
		$("#log_form").addClass("hidden")
	}
 	$(form).removeClass("hidden");
}	

function fillUserInfo()
{
	if(USER != null)
	{
		$("#login_field").val(USER.Login.trim());
		$("#password_field").val(USER.Password.trim());
	}
}

function fillPersonInfo(person)
{
	if(person != null)
	{
		var date = new Date(person.DateOfBirth);
		$("#first_name").val(person.FirstName.trim());
		$("#middle_name").val(person.MiddleName.trim());
		$("#last_name").val(person.LastName.trim());
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
		$("#email").val(person.Email.trim());
		$("#phone").val(person.Phone.trim());
		$("#small-avatar, #medium-avatar").attr("src", "data:image/png;base64," + person.Photo);
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

	$("#log_info_button").bind("click",function(){
		showMenu("log");
	});

	$("#friendlist_accept_button").bind("click", function(){
		$.ajax({
			        url: 'api/web/user/bylogin',
			        type: "POST",
			        data: JSON.stringify({Login:$("#requests option:selected").text().trim()}),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success:function(friend){
					$.ajax({
					        url: 'api/web/friendlist/accept',
					        type: "POST",
					        data: JSON.stringify({UserId:USER.UserId, FriendId:friend.UserId}),
					        dataType: "json",
					        contentType: "application/json; charset=utf-8",
					        success: function(data){
					        	if(data.Type == 0){
					        		alert(data.Message);
					        		$("#friendlist option[" + friend.Login + "]").remove();
					        	}
					        	else{
					        		alert(data.Message);
					        	}	
					        }
					    });
				}
			});
	})

	$("#friendlist_delete_button").bind("click",function(){
		$.ajax({
			        url: 'api/web/user/bylogin',
			        type: "POST",
			        data: JSON.stringify({Login:$("#friendlist option:selected").text().trim()}),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success:function(friend){
					$.ajax({
					        url: 'api/web/friendlist/delete',
					        type: "POST",
					        data: JSON.stringify({UserId:USER.UserId, FriendId:friend.UserId}),
					        dataType: "json",
					        contentType: "application/json; charset=utf-8",
					        success: function(data){
					        	if(data.Type == 0){
					        		alert(data.Message);
					        		$("#friendlist option[" + friend.Login + "]").remove();
					        	}
					        	else{
					        		alert(data.Message);
					        	}	
					        }
					    });
				}
			});
	})

	$("#save_button_user").bind("click", function(){
		try{
			if(USER != null)
			{
				temp = USER;
				temp.Login = $("#login_field").val().trim();
				temp.Password = $("#password_field").val().trim();
				$.ajax({
			        url: 'api/web/user/edit',
			        type: "POST",
			        data: JSON.stringify(temp),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success: function(data){
			        	if(data.Type == 0)
			        	{
			        		$(".message").css("color", "green");
			        	}
			        	else
			        	{
			        		$(".message").css("color", "red");	
			        	}
			        	$(".message").html(data.Message);
			        	$("#sign_out_button").trigger("click");
			        }
			    });
			}
		}
		catch(err){
			$(".message").css("color", "red");
			$(".message").html("Not valid data.");
		}
	})	

	$("#friendlist_add_button").bind("click",function(){
		if(USER != null){
			$.ajax({
			        url: 'api/web/user/bylogin',
			        type: "POST",
			        data: JSON.stringify({Login:$("#search_results option:selected").text().trim()}),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success: function(friend){
			        	if(friend != null){
							$.ajax({
						        url: 'api/web/friendlist/add',
						        type: "POST",
						        data: JSON.stringify({UserId:USER.UserId, FriendId:friend.UserId}),
						        dataType: "json",
						        contentType: "application/json; charset=utf-8",
						        success: function(result){
						        	if(result != null)
						        	{
						        		if(result.Type == 0)
						        		{
						        			$("#search").val("");
						        			$("#search_results option").remove();
						        		}
						        		alert(result.Message);
						        		return;
						        	}
						        }
					    	});	      	
					    }
			        }
			    });
		}
	});

	$("#search").change(function(){
		switch($("#search_dropdown").val())
		{
			case "id":
			{
				$.ajax({
			        url: 'api/web/user/regexp/deviceId',
			        type: "POST",
			        data: JSON.stringify({DeviceId:$("#search").val().trim()}),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success: function(data){
			        	if(data != null)
			        	{
			        		$("#search_results option").remove();
			        		data.forEach(function(item, i, data){
			        			$("#search_results").append(new Option(item.Login, item.Login));
			        		});
			        	}
			        }
			    });
				break;
			}
			case "login":
			{
					$.ajax({
			        url: 'api/web/user/regexp/login',
			        type: "POST",
			        data: JSON.stringify({Login:$("#search").val()}),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success: function(data){
			        	if(data != null)
			        	{
			        		$("#search_results option").remove();
			        		data.forEach(function(item, i, data){
			        			$("#search_results").append(new Option(item.Login, item.Login));
			        		});
			        	}
			        }
			    });
				break;
			}
			default:
			{
				break;
			}
		}
	});

	$("#save_button_profile").bind("click", function(){	
		try{
			if(PERSON != null)
			{
				temp = PERSON;
				temp.FirstName = $("#first_name").val().trim();
				temp.MiddleName = $("#middle_name").val().trim();
				temp.LastName = $("#last_name").val().trim();
				temp.Gender = $("#gender_form input[type='radio']:checked").val();
				var date = new Date();
				date.setDate(parseInt($("#day").val(), 10));
				date.setMonth(parseInt($("#month option:selected").val(), 10));
				date.setFullYear(parseInt($("#year").val(), 10));
				temp.DateOfBirth = date.toDateString().trim();
				temp.Email = $("#email").val().trim();
				temp.Phone = $("#phone").val().trim();
				var imgString = $("#small-avatar").attr("src");
				temp.Photo = imgString.substring(imgString.indexOf("base64,") + 7).trim();
				$.ajax({
			        url: 'api/web/person/edit',
			        type: "POST",
			        data: JSON.stringify(temp),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success: function(data)
			        {
			        	if(data.Type == 0)
			        	{
			        		$(".message").css("color", "green");
							$(".message").html(data.Message);
			        		$("#sign_out_button").trigger("click");
			        		checkCookie(withmap);
			        	}
			        	else
			        	{
			        		$(".message").css("color", "red");	
			        		$(".message").html(data.Message);
			        	}
			        }
			    });
			}
		}
		catch(err){
			$(".message").css("color", "red");
			$(".message").html("Not valid data.");
		}
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