var friends = new Array();
var requests = new Array();

function showFriendList(userId){
	if(userId != null)
	{
		$.ajax({
                url: 'api/web/friendlist',
                type: "POST",
                data: JSON.stringify({ UserId: userId }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                	if(data !=null)
                	{
                		friends = data;
						$.each(friends, function(index, item) {
						  $("#friendlist").append(new Option(item.Login, item.Login));
						});
                	}
                }
            });
		$.ajax({
                url: 'api/web/friendlist/request',
                type: "POST",
                data: JSON.stringify({ UserId: userId }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                	if(data != null){
                		var temp = data.slice(requests.lenght);
						$.each(temp, function(index, item) {
							requests.push(item);
						  	$("#requests").append(new Option(item.Login, item.Login));
						});
                	}
                	}
                });
	}
}

function clearFriendlist()
{
	$("#requests").empty();
	$("#search_results").empty();
	$("#friendlist").empty();

}