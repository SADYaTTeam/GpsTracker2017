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
	
	<div class="main-content">
		<button id="dropdown_button" class="w3-display-topmiddle dropdown_control authorized" onclick="return false">\/</button>
		<div class="container">
			<div class="row">
				<form class="form-horizontal col-xs-6 w3-display-topmiddle" id="dropdown_menu">
					<button id="slideup_button" class="dropdown_control w3-display-topmiddle" onclick="return false">/\</button>
					<div class="form-group form-inline col-xs-12">
						<label class="col-xs-1 label-control">From</label>
						<div class="input-group col-xs-6" id="date_input">
			              <input name="day" placeholder="Day" class="form-control" type="text" style="width: 55px" id="from_day">
			              <select name="month" class="form-control" style="width: 115px" id="from_month">
						    <option value="0">January</option>
						    <option value="1">February</option>
						    <option value="2">March</option>
						    <option value="3">April</option>
						    <option value="4">May</option>
						    <option value="5">June</option>
						    <option value="6">July</option>
						    <option value="7">August</option>
						    <option value="8">September</option>
						    <option value="9">October</option>
						    <option value="10">November</option>
						    <option value="11">December</option>
						  </select>
			              <input name="year" placeholder="Year" class="form-control" type="text" style="width: 55px" id="from_year">
			              <input name="year" placeholder="Hour" class="form-control" type="text" style="width: 55px" id="from_hour">
			              <input name="year" placeholder="Min" class="form-control" type="text" style="width: 55px" id="from_minute">
			             </div>
					</div>
					<div class="form-group form-inline col-xs-12">
						<label class="col-xs-1 label-control">To</label>
						<div class="input-group col-xs-6" id="date_input">
			              <input name="day" placeholder="Day" class="form-control" type="text" style="width: 55px" id="to_day">
			              <select name="month" class="form-control" style="width: 115px" id="to_month">
						    <option value="0">January</option>
						    <option value="1">February</option>
						    <option value="2">March</option>
						    <option value="3">April</option>
						    <option value="4">May</option>
						    <option value="5">June</option>
						    <option value="6">July</option>
						    <option value="7">August</option>
						    <option value="8">September</option>
						    <option value="9">October</option>
						    <option value="10">November</option>
						    <option value="11">December</option>
						  </select>
			              <input name="year" placeholder="Year" class="form-control" type="text" style="width: 55px" id="to_year">
			              <input name="year" placeholder="Hour" class="form-control" type="text" style="width: 55px" id="to_hour">
			              <input name="year" placeholder="Min" class="form-control" type="text" style="width: 55px" id="to_minute">
			             </div>
					</div>
					<div class="form-group col-xs-5">
						<div class="form-hoizontal">
							<label for="history_friendlist" class="label-control block">Friendlist</label>
							<select name="history_friendlist" id="history_friendlist" multiple class="form-control"></select>
						</div>
					</div>
					<div class="form-group col-xs-6">
						<label class="label-control block">Controls</label>
						<button class="btn-lg btn content_button" id="history_search" onclick="return false;">Show</button>
						<button class="btn-lg btn content_button" id="history_cancel" onclick="return false;">Cancel</button>
					</div>
				</form>
			</div>
		</div>
		<section id="googleMap" style="width:100%;height:89vh;">
		</section>
	</div>

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