var mutex = true;
var USER;
var sosMarkers = new Array();
var markers = new Array();
var trueData;

function showMarkers(userData) {
    if (USER == null) {
        USER = userData;
    }
    setInterval(function () {
        if (mutex) {
            $.ajax({
                url: 'api/web/marker',
                type: "POST",
                data: JSON.stringify({ UserId: USER.UserId }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    trueData = data.map(function (item) {
                        return { Longitude: item.Longitude, Latitude: item.Latitude, Info: item.UserId };
                    });
                    markers = drawMarkers(trueData, 'http://maps.google.com/mapfiles/ms/icons/green-dot.png', markers);
                    // $.ajax({
                    //     //TODO Insert url here
                    //     url: 'api/web/marker/logins',
                    //     type: "POST",
                    //     data: JSON.stringify({ UserId: USER.UserId }),
                    //     dataType: "json",
                    //     contentType: "application/json; charset=utf-8",
                    //     success: function (data) {
                    //         if (data != null) {
                    //             CONTENT = new Array();
                    //             //for (var i = 0; i <= markers.length; i++) {
                    //             //    for (var j = 0; j <= data.length; j++) {
                    //             //        if (data[j].UserId === parseInt(markers[i].Info)) {
                    //             //            CONTENT = data[i].Login;
                    //             //        };
                    //             //    }
                    //             //}
                    //             trueData.forEach(function (item, i, trueData) {
                    //                 data.forEach(function (item2, j, data) {
                    //                     if (data[j].UserId === parseInt(trueData[i].Info)) {
                    //                         CONTENT.push(data[j].Login);
                    //                     };
                    //                 });
                    //             });
                    //             
                    //         }
                    //     }
                    // });

                }
            });
            $.ajax({
                url: 'api/web/sos',
                type: "POST",
                data: JSON.stringify({ UserId: USER.UserId }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data != null)
                        var trueData = data.map(function (item) {
                            return { Longitude: item.Longitude, Latitude: item.Latitude, Info: "Sos from " + USER.Login };
                        });
                    sosMarkers = drawMarkers(trueData, 'http://maps.google.com/mapfiles/ms/icons/red-dot.png', sosMarkers);
                }
            });
        }
    }, 5000);
}

function clearMarkers() {
    if (sosMarkers != null) {
        for (i = 0; i < sosMarkers.length; i++) {
            sosMarkers[i].setMap(null);
        }
    }
    if (markers != null) {
        for (i = 0; i < markers.length; i++) {
            markers[i].setMap(null);
        }
    }
}