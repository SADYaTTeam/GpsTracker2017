withMap=true;
$(document).ready(function(){
    checkCookie(true);
});

$("#dropdown_button").bind("click",function(){
	$.ajax({
        url: 'api/web/friendlist',
        type: "POST",
        data: JSON.stringify({UserId:USER.UserId}),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success:function(friends){
        	if(friends != null)
        	{
        		$.each(friends, function(index, item) {
				  $("#history_friendlist").append(new Option(item.Login, item.Login));
				});
				$("#history_friendlist").append(new Option(USER.Login, USER.Login));
        	}
        }
	});
	$("#dropdown_menu").slideDown(400);
})

$("#slideup_button").bind("click",function(){
	$("#history_friendlist option").remove();
	$("#dropdown_menu").slideUp(400);
})

$("#history_search").bind("click",function(){
	var dateFrom = new Date();
	var dateTo = new Date();
	try{
		dateFrom.setDate(parseInt($("#from_day").val(), 10));
		dateFrom.setMonth(parseInt($("#from_month option:selected").val(), 10));
		dateFrom.setFullYear(parseInt($("#from_year").val(), 10));
		dateFrom.setHours(parseInt($("#from_hour").val(), 10));
		dateFrom.setMinutes(parseInt($("#from_minute").val(), 10));
		dateTo.setDate(parseInt($("#to_day").val(), 10));
		dateTo.setMonth(parseInt($("#to_month option:selected").val(), 10));
		dateTo.setFullYear(parseInt($("#to_year").val(), 10));
		dateTo.setHours(parseInt($("#to_hour").val(), 10));
		dateTo.setMinutes(parseInt($("#to_minute").val(), 10));
		$.ajax({
	        url: 'api/web/user/bylogin',
	        type: "POST",
	        data: JSON.stringify({Login:$("#history_friendlist option:selected").text().trim()}),
	        dataType: "json",
	        contentType: "application/json; charset=utf-8",
	        success:function(data){
	        	$.ajax({
			        url: 'api/web/marker/history',
			        type: "POST",
			        data: JSON.stringify({UserId:data.UserId, From:dateFrom, To:dateTo}),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success:function(markers){
			        	if(markers!=null){
				        	mutex = false;
				        	clearMarkers();
				        	drawMarkersHistory(markers, 'http://maps.google.com/mapfiles/ms/icons/green-dot.png');
			        	}
			        	else
			        	{
			        		alert("There're no markers");
			        	}
			        }
			    });
	        }
	    });
	}
	catch(err){
		alert("Wrong input data");
	}
})