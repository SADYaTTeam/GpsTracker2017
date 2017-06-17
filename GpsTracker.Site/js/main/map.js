var mutex = true;
var USER; 
var sosMarkers = new Array();
var markers = new Array();

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
			            markers = drawMarkers(trueData, 'http://maps.google.com/mapfiles/ms/icons/green-dot.png', markers);
			        }
			    });
			$.ajax({
			        url: 'api/web/sos',
			        type: "POST",
			        data: JSON.stringify({ UserId: USER.UserId }),
			        dataType: "json",
			        contentType: "application/json; charset=utf-8",
			        success: function (data) {
			        	if(data!=null)
			            var trueData = data.map(function(item){
			             return {Longitude:item.Longitude, Latitude:item.Latitude, Info:"Sos from " + USER.Login};
			            });
			            sosMarkers = drawMarkers(trueData, 'http://maps.google.com/mapfiles/ms/icons/red-dot.png', sosMarkers);
			        }
			    });
		}
 	}, 5000);
}

function clearMarkers()
{
	if(sosMarkers != null)
	{
		for (i = 0; i < sosMarkers.length; i++) {
            sosMarkers[i].setMap(null);
        }
	}
	if(markers != null)
	{
		for (i = 0; i < markers.length; i++) {
            markers[i].setMap(null);
        }
	}
}