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

var markers = [
    ['Title A', 49.802830, 24.001280, 1],
    ['Title B', 49.832361, 24.018122, 2],
    ['Title C', 49.835098, 24.008206, 3],
    ['Title D', 49.773674, 24.010949, 4]
];

function drawMarkers() {
    var marker, i;
    for (i = 0; i < markers.length; i++) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(markers[i][1], markers[i][2]),
            map: map,
            fillColor: '#00FF00',
            fillOpacity: 0.35
        });
        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');

        //Geocoding
        geocoder = new google.maps.Geocoder;
        infowindow = new google.maps.InfoWindow;
        google.maps.event.addListener(marker, 'click', function () {
            geocodeLatLng(this.getPosition(), geocoder, map, infowindow); 
        });
    };
}

function geocodeLatLng(latlng, geocoder, map, infowindow) {
    geocoder.geocode({ 'location': latlng }, function (results, status) {
        if (status === 'OK') {
            if (results[1]) {
                map.setZoom(15);
                var marker = new google.maps.Marker({
                    position: latlng,
                    map: map
                });
                marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
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

function initMap() {
    // Create the map.
    map = new google.maps.Map(document.getElementById('googleMap'), {
        zoom: 15,
        center: { lat: 49.802929, lng: 24.003286 },
        mapTypeId: 'terrain'
    });

    // Construct the circle for each value in citymap.
    // Note: We scale the area of the circle based on the population.
    for (var zone in zones) {
        // Add the circle for this city to the map.
        var zineCircle = new google.maps.Circle({
            strokeColor: '#FF0000',
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: '#FF0000',
            fillOpacity: 0.35,
            map: map,
            center: zones[zone].center,
            //radius: Math.sqrt(zones[zone].population) * 100
            radius: 50
        });
    }
    drawMarkers();
}