<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>Not bad tracker</title>
	<link rel="shortcut icon" type="image/png" ref="http://gpstrackerservice.azurewebsites.net/img/siteIcon.png"/>
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" /> 
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<!-- <link rel="stylesheet" type="text/css" href="css/fonts.css"> -->
	<link rel="stylesheet" type="text/css" href="css/main/main.css">
	<link rel="stylesheet" type="text/css" href="css/w3.css">
</head>
<body>
<div class="" id="main_layout">
	<header>
		<?php include("pages/header.html");?>
	</header>

	<section id="googleMap" style="width:100%;height:89vh;"></section>

	<footer class="panel panel-footer">
		<?php include("pages/footer.html");?>
	</footer>

	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/jquery-ui.min.js"></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="js/view_scripts.js"></script>
	<script src="js/main/map.js" async></script>
	<script src="js/sign-in.js"></script>
	<script src="js/draw_map.js"></script>
	<script src="js/main/main.js"></script>
	<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB8xt0oziJPSxsF6_Zm52PKhkT1idPNr40&callback=initMap"></script>
</div>	
</body>
</html>