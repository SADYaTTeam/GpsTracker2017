function showFriendList(userId){
	if(userId != null)
	{
		$.ajax({
                url: 'api/web/log/sos',
                type: "POST",
                data: JSON.stringify({ UserId: userId }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                }
            });
		setInterval(function(){

		}, 5000);
	}
}