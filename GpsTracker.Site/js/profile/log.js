var lastUserLogIndex = -1;
var lastSosLogIndex = -1;
var lastZoneLogIndex = -1;
var sosLog = new Array();
var userLog = new Array();
var zoneLog = new Array();
var sosTemp = new Array();
var userTemp = new Array();
var zoneTemp = new Array();
var indexes = new Array();

function showLog(userId){
	if(userId != null)
	{
		setInterval(function(){
			$.ajax({
                url: 'api/web/log/sos',
                type: "POST",
                data: JSON.stringify({ UserId: userId }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                	if(lastSosLogIndex < data.length - 1){
                		indexes = new Array();
	                	data.forEach(function(item, i, data){
	                		if(!isInArray(item.DeviceId, indexes))
	                		{
	                			indexes.push(item.DeviceId);
	                		}
	                	})
	                	sosLog = data.slice(lastSosLogIndex + 1);
	                	lastSosLogIndex = data.length - 1;
	                	$.ajax({
		                url: 'api/web/user/deviceId',
		                type: "POST",
		                data: JSON.stringify(indexes.map(function(item){return {DeviceId: item};})),
		                dataType: "json",
		                contentType: "application/json; charset=utf-8",
		                success: function (data) {
		                	if(data != null)
		                	{
		                		sosLog.forEach(function(item, i, soslog){
		                			data.forEach(function(dataItem, j, data){
		                					if(item.DeviceId == dataItem.DeviceId){
		                						$('#sos_log tbody tr:last').after('<tr><td class= "col-xs-1">SOS</td><td class= "col-xs-6">'+item.Message+'</td><td class= "col-xs-2">'+dataItem.Login+'</td><td class= "col-xs-2">'+item.DeviceId+'</td><td class= "col-xs-1">'+item.EventDate+'</td></tr>/n');
		                					}
		                				});
		                		});
		                	}
		                	sosLog = new Array();
		                }
		                });
		            }
	           }
            });
		},1000);
		setInterval(function(){
			$.ajax({
                url: 'api/web/log/zone',
                type: "POST",
                data: JSON.stringify({ UserId: userId }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                	if(lastZoneLogIndex < data.length - 1){
                		indexes = new Array();
	                	data.forEach(function(item, i, data){
	                		if(!isInArray(item.DeviceId, indexes))
	                		{
	                			indexes.push(item.DeviceId);
	                		}
	                	})
	                	zoneLog = data.slice(lastZoneLogIndex + 1);
	                	lastZoneLogIndex = data.length - 1;
	                	$.ajax({
		                url: 'api/web/user/deviceId',
		                type: "POST",
		                data: JSON.stringify(indexes.map(function(item){return {DeviceId: item};})),
		                dataType: "json",
		                contentType: "application/json; charset=utf-8",
		                success: function (data) {
		                	if(data != null)
		                	{
		                		zoneLog.forEach(function(item, i, soslog){
		                			data.forEach(function(dataItem, j, data){
		                					if(item.DeviceId == dataItem.DeviceId){
		                						$('#zones_log tbody tr:last').after('<tr><td class= "col-xs-6">'+item.Message+'</td><td class= "col-xs-2">'+dataItem.Login+'</td><td class= "col-xs-3">'+item.DeviceId+'</td><td class= "col-xs-1">'+item.EventDate+'</td></tr>/n');
		                					}
		                				});
		                		});
		                	}
		                	zoneLog = new Array();
		                }
		                });
		            }
	           }
            });
		},3000);
		setInterval(function(){
			$.ajax({
                url: 'api/web/log/user',
                type: "POST",
                data: JSON.stringify({ UserId: userId }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                	if(lastUserLogIndex < data.length - 1){
                		indexes = new Array();
	                	data.forEach(function(item, i, data){
	                		if(!isInArray(item.DeviceId, indexes))
	                		{
	                			indexes.push(item.DeviceId);
	                		}
	                	})
	                	userLog = data.slice(lastUserLogIndex + 1);
	                	lastUserLogIndex = data.length - 1;
	                	$.ajax({
		                url: 'api/web/user/deviceId',
		                type: "POST",
		                data: JSON.stringify(indexes.map(function(item){return {DeviceId: item};})),
		                dataType: "json",
		                contentType: "application/json; charset=utf-8",
		                success: function (data) {
		                	if(data != null)
		                	{
		                		userLog.forEach(function(item, i, soslog){
		                			data.forEach(function(dataItem, j, data){
		                					if(item.DeviceId == dataItem.DeviceId){
		                						$('#user_info_log tbody tr:last').after('<tr><td class= "col-xs-6">'+item.Message+'</td><td class= "col-xs-2">'+dataItem.Login+'</td><td class= "col-xs-3">'+item.DeviceId+'</td><td class= "col-xs-1">'+item.EventDate+'</td></tr>/n');
		                					}
		                				});
		                		});
		                	}
		                	userLog = new Array();
		                }
		                });
		            }
	           }
            });
		},5000);
	}
}

function isInArray(value, array) {
  return array.indexOf(value) > -1;
}