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

