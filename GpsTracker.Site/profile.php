<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>Profile</title>
	<link rel="shortcut icon" ref="http://gpstrackerservice.azurewebsites.net/img/siteIcon.png"/>
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" /> 
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/profile/profile.css">
	<link rel="stylesheet" type="text/css" href="css/profile/media.css">
	<link rel="stylesheet" type="text/css" href="css/w3.css">
	<link rel="stylesheet" type="text/css" href="css/test.css">
</head>
<body>
	<header>

	<?php include("pages/header.html");?>

		
	</header>



	<section class="container-fluid">
		<div class="row">
			<div class="profile_menu_nav col-lg-2 btn-group-vertical">
				<button class="btn-lg btn-primary btn" id="user_info_button">User info</button>
				<button class="btn-lg btn-primary btn" id="person_info_button">Personal info</button>
				<button class="btn-lg btn-primary btn" id="list_info_button">Friendlist</button>
				<button class="btn-lg btn-primary btn" id="log_info_button">Log</button>
			</div>
			<div class="profile_content col-lg-10">
				<form action="" class="form-horizontal" id="user_form">
					<p class="label message"></p>
					<div class="form-group">
						<label for="login" class="col-lg-2 label-control">Login</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="login_field" placeholder="Enter new login"></div>
					</div>
					<div class="form-group">
						<label for="password" class="col-lg-2 label-control">Password</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="password_field" placeholder="Enter new password"></div>
					</div>
					<button class="btn-lg btn content_button" id="save_button_user" onclick="return false;">Save</button>
				</form>
				<form class="form-horizontal hidden" id="person_form">
					<p class="label message"></p>
					<div class="form-group">
						<label for="first_name" class="col-lg-2 label-control">First name</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="first_name" placeholder="Enter first name"></div>
					</div>
					<div class="form-group">
						<label for="middle_name" class="col-lg-2 label-control">Middle name</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="middle_name" placeholder="Enter middle name"></div>
					</div>
					<div class="form-group">
						<label for="last_name" class="col-lg-2 label-control">Last name</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="last_name" placeholder="Enter lastname"></div>
					</div>
					<div class="form-group">
						<label class="col-lg-2 label-control">Gender</label>
						<div class="col-lg-8">
							<label class="radio-inline">
						      <input type="radio" name="gender" id="male" value="true">Male
						    </label>
						    <label class="radio-inline">
						      <input type="radio" name="gender" id="female" value="false">Female
						    </label>
					    </div>
					</div>
					<div class="form-group form-inline">
						<label class="col-lg-2 label-control">Birthday</label>
						<div class="input-group col-lg-8" id="date_input">
			              <input name="day" placeholder="Day" class="form-control" type="text" style="width: 55px" id="day">
			              <select name="month" class="form-control" style="width: 115px" id="month">
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
			              <input name="year" placeholder="Year" class="form-control" type="text" style="width: 55px" id="year">
			             </div>
					</div>
					<div class="form-group">
						<label for="email" class="col-lg-2 label-control">E-mail</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="email" placeholder="examle@example.com"></div>
					</div>
					<div class="form-group">
						<label for="Phone" class="col-lg-2 label-control">Phone</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="phone" placeholder="+380123456789"></div>
					</div>
					<div class="form-group">
						<label class="col-lg-2 label-control">Photo</label>
						<div class="col-lg-8">
							<input type="file" name="picture" id="picture" target="small-avatar" title=""/>
							<img class="img-circle" id="small-avatar" src="">
							<img class="img-circle" id="medium-avatar" src="">
						</div>
					</div>
					<button class="btn-lg btn content_button" id="save_button_profile" onclick="return false;">Save</button>
				</form>
				<form class="form-horizontal hidden" id="friendlist_form">
					<div class="form-horizontal col-lg-3">
						<div class="form-inline">
							<select class="form-control" id="search_dropdown">
								<option value="" disabled selected>Search by</option>
								<option value="id">Id</option>
								<option value="login">Login</option>
							</select>
							<input type="text" class="form-control" id="search">
						</div>
					  <select multiple class="form-control" id="search_results">
					  </select>
					  <button class="btn-lg btn content_button" id="friendlist_add_button" onclick="return false;">Add</button>
					</div>
					<div class="form-inline col-lg-7">
						<div class="form-hoizontal col-lg-3">
							<label for="requests" class="label-control block">Friend requests</label>
							<select name="requests" id="requests" multiple class="form-control"></select>
							<button class="btn-lg btn content_button" id="friendlist_accept_button" onclick="return false;">Accept</button>
						</div>
						<div class="form-hoizontal col-lg-4">
							<label for="friendlist" class="label-control block">Friendlist</label>
							<select name="friendlist" id="friendlist" multiple class="form-control"></select>
							<button class="btn-lg btn content_button" id="friendlist_delete_button" onclick="return false;">Delete</button>
						</div>
					</div>
				</form>
				<form class="form-horizontal hidden" id="log_form">
					<div class="container col-xs-6">
					  <div class="row">
			          	<label for="user_info_log" class="label-control block">User info log</label>
				        <table class="table table-fixed" id="user_info_log">
				          <thead>
				            <tr>
				              <th class="col-xs-1">Event</th>
				              <th class="col-xs-2">Message</th>
				              <th class="col-xs-1">Login</th>
				              <th class="col-xs-1">Id</th>
				              <th class="col-xs-1">Date</th>
				            </tr>
				          </thead>
				          <tbody>
				          <tr></tr>	
				          </tbody>
				        </table>
					  </div>
					</div>
					<div class="container-fluid col-xs-6">
					  <div class="row">
			          	<label for="sos_log" class="label-control block">Sos log</label>
				        <table class="table table-fixed" id="sos_log">
				          <thead>
				            <tr>
				              <th class="col-xs-1">Event</th>
				              <th class="col-xs-6">Message</th>
				              <th class="col-xs-2">Login</th>
				              <th class="col-xs-2">Id</th>
				              <th class="col-xs-1">Date</th>
				            </tr>
				          </thead>
				          <tbody>
				          	<tr></tr>
				          </tbody>
				        </table>
					  </div>
					</div>
					<div class="container col-xs-12">
					  <div class="row">
			          	<label for="zones_log" class="label-control block">Zones log</label>
				        <table class="table table-fixed" id="zones_log">
				          <thead>
				            <tr>
				              <th class="col-xs-6">Message</th>
				              <th class="col-xs-2">Login</th>
				              <th class="col-xs-3">Id</th>
				              <th class="col-xs-1">Date</th>
				            </tr>
				          </thead>
				          <tbody>
				          <tr></tr>
				          </tbody>
				        </table>
					  </div>
					</div>
				</form>
			</div>
			
		</div>
	</section>

	<footer class="panel panel-footer">
		<?php include("pages/footer.html");?>
	</footer>
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/jquery-ui.min.js"></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="js/profile/friendlist.js" async></script>
	<script src="js/profile/log.js" async></script>
	<script src="js/sign-in.js"></script>
	<script src="js/view_scripts.js"></script>
	<script src="js/profile/profile.js"></script>
	<script> withMap = false </script>
</body>
</html>