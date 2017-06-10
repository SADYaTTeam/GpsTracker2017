function show() {
    var x = document.getElementById("about");
    if (x.className.indexOf("w3-show") == -1) {
        x.className += " w3-show";
    } else { 
        x.className = x.className.replace(" w3-show", "");
    }
}

function myMap() {
    var mapOptions = {
        center: new google.maps.LatLng(49.839683, 24.029717),
        zoom: 15,
    }
var map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);
}