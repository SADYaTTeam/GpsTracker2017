<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>Not bad tracker</title>
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
	<script src="//code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="js/view_scripts.js"></script>
	<script src="js/main/sign-in.js"></script>
	<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB8xt0oziJPSxsF6_Zm52PKhkT1idPNr40&callback=myMap"></script>
</div>	
</body>
</html>