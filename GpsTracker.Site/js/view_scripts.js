// This example creates circles on the map, representing populations in North
// America.

// First, create an object containing LatLng and population for each city.
var zones = {
    home: {
        center: { lat: 49.802830, lng: 24.001280},
    },
    lvivCenter: {
        center: { lat: 49.832361, lng: 24.018122},
    },
    polithech: {
        center: { lat: 49.835098, lng: 24.008206},
    },
    auchan: {
        center: { lat: 49.773674, lng: 24.010949},
    }
};

var markers = [
     ['Title A', 49.802830, 24.001280, 1],
     ['Title B', 49.832361, 24.018122, 2],
     ['Title C', 49.835098, 24.008206, 3],
     ['Title D', 49.773674, 24.010949, 4]
];

//function myMap() {
//    var mapOptions = {
//        center: new google.maps.LatLng(49.839683, 24.029717),
//        zoom: 15,
//    }
//    var map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);

//}

function initMap() {
    // Create the map.
    var map = new google.maps.Map(document.getElementById('googleMap'), {
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

    var marker, i;

    for (i = 0; i < markers.length; i++) {  
    marker = new google.maps.Marker({
         position: new google.maps.LatLng(markers[i][1], markers[i][2]),
         map: map,
         fillColor: '#00FF00',
         fillOpacity: 0.35
    });
    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png')
    }
}

function show(id) {
    var x = document.getElementById(id);
    if (x.className.indexOf("w3-show") == -1) {
        x.className += " w3-show";
    } else { 
        x.className = x.className.replace(" w3-show", "");
    }
}

function showLogin() {
    show("sign-in")
    var blur = document.getElementById("main_layout");
    if (blur.className.indexOf("blur") == -1) {
    	blur.className += " blur";
    } else{
    	blur.className = blur.className.replace(" blur", "");
    }
}

function showAbout() {
    show("about")
}

