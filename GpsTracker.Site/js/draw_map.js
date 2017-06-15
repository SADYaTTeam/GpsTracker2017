var map;
var geocoder;
var infowindow;

var zones = {
    home: {
        center: { lat: 49.802830, lng: 24.001280 },
    },
    lvivCenter: {
        center: { lat: 49.832361, lng: 24.018122 },
    },
    polithech: {
        center: { lat: 49.835098, lng: 24.008206 },
    },
    auchan: {
        center: { lat: 49.773674, lng: 24.010949 },
    }
};

var markersArray = new Array();

function initMap() {
    // Create the map.
    
    map = new google.maps.Map(document.getElementById('googleMap'), {
        zoom: 15,
        maxZoon: 20,
        minZoom: 3,
        center: { lat: 49.802929, lng: 24.003286 },
        mapTypeId: 'roadmap',
        gestureHandling: 'greedy'
    });

    //  var infoWindow = new google.maps.InfoWindow({map: map});

    // // Try HTML5 geolocation.
    // if (navigator.geolocation) {
    //     navigator.geolocation.getCurrentPosition(function (position) {
    //         var pos = {
    //             lat: position.coords.latitude,
    //             lng: position.coords.longitude
    //         };

    //         map.setCenter(pos);
    //     }, function () {
    //         //handleLocationError(true, map.getCenter());
    //     });
    // } else {
    //     // Browser doesn't support Geolocation
    //     handleLocationError(false, infoWindow, map.getCenter());
    // }

    // drawZones(zones);
    //drawMarkers(markers);
    // drawPath(markers);
}

function drawMarkers(markers) {
    var marker, i;
    for (i = 0; i < markersArray.length; i++) {
        markersArray[i].setMap(null);
    }
    for (i = 0; i < markers.length; i++) {
        
        marker = new window.google.maps.Marker({
            position: new window.google.maps.LatLng(markers[i].Latitude, markers[i].Longitude),
            map: map,
            fillColor: '#00FF00',
            fillOpacity: 0.35
        });
        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');

        //Geocoding markers
        geocoder = new window.google.maps.Geocoder;
        infowindow = new window.google.maps.InfoWindow;
        window.google.maps.event.addListener(marker, 'click', function () {
            geocodeLatLng(this.getPosition(), geocoder, map, infowindow);
       
        });
        markersArray.push(marker);
    };
}

function drawZones(zones) { // Construct the circle for each value in citymap.
    // Note: We scale the area of the circle based on the population.
    for (var zone in zones) {
        // Add the circle for this city to the map.
        var zoneCirle = new google.maps.Circle({
            strokeColor: '#FF0000',
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: '#FF0000',
            fillOpacity: 0.35,
            map: map,
            center: zones[zone].center,
            radius: 50
        });
    }
};

function geocodeLatLng(latlng, geocoder, map, infowindow) {
    geocoder.geocode({ 'location': latlng }, function (results, status) {
        if (status === 'OK') {
            if (results[1]) {
                map.setZoom(15);
                var marker = new google.maps.Marker({
                    position: latlng,
                    map: map
                });
                infowindow.setContent(results[1].formatted_address);
                infowindow.open(map, marker);
            } else {
                window.alert('No results found');
            }
        } else {
            window.alert('Geocoder failed due to: ' + status);
        }
    });
}

function drawPath(markers) {

    var myPath = new Array();
    for (i = 0; i < markers.length; i++) {
        myPath.push({'lat': markers[i][1], 'lng': markers[i][2]});
    }

    var path = new window.google.maps.Polyline({
        path: myPath,
        geodesic: true,
        strokeColor: '#0000FF',
        strokeOpacity: 1.0,
        strokeWeight: 2
    });

    path.setMap(map);
}