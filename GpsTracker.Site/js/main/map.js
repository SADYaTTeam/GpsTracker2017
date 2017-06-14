var mutex = true;
var USER; 

function showMarkers(userData)
{
	if(USER == null)
	{
		USER = userData;
	}
 	setInterval(function(){
		if(mutex)
		{
			$.ajax({
			        url: 'api/web/marker',
			        type: "POST",
			        data: JSON.stringify({ UserId: USER.UserId }),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success: function (data) {
			            var trueData = data.map(function(item){
			             return {Longitude:item.Longitude, Latitude:item.Latitude, Info:item.Name};
			            });
			            drawMarkers(trueData);
			        }
			    });
		}
 	}, 1000);
}