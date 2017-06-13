function showMenu(option)
{
	var form;
	var forms = $("#user_form, #person_form, #friendlist_form");
	switch(option){
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

$(function(){
	$("#list_info_button").bind("click",function()
	{
		showMenu("list");
	});

	$("#user_info_button").bind("click",function()
	{
		showMenu("user");
	});

	$("#person_info_button").bind("click",function()
	{
		showMenu("person");
	});
})