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
				<button class="btn-lg btn-primary btn" id="list_info_button">Friend list</button>
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
			</div>
			
		</div>
	</section>

	<footer class="panel panel-footer">
		<?php include("pages/footer.html");?>
	</footer>
	<script src="js/jquery-3.2.1.min.js"></script>
	<script src="js/jquery-ui.min.js"></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="js/sign-in.js"></script>
	<script src="js/view_scripts.js"></script>
	<script src="js/profile/profile.js"></script>
	<script> withMap = false </script>
</body>
</html>