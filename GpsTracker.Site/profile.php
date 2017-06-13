<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>Profile</title>
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" /> 
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/profile/profile.css">
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
					<p class="label" id="message"></p>
					<div class="form-group">
						<label for="login" class="col-lg-2">Login</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="login" placeholder="Enter new login"></div>
					</div>
					<div class="form-group">
						<label for="password" class="col-lg-2">Password</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="password" placeholder="Enter new password"></div>
					</div>
				</form>
				<form class="form-horizontal hidden" id="person_form">
					<p class="label" id="message"></p>
					<div class="form-group">
						<label for="first_name" class="col-lg-2">First name</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="first_name" placeholder="Enter first name"></div>
					</div>
					<div class="form-group">
						<label for="middle_name" class="col-lg-2">Middle name</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="middle_name" placeholder="Enter middle name"></div>
					</div>
					<div class="form-group">
						<label for="last_name" class="col-lg-2">Last name</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="last_name" placeholder="Enter lastname"></div>
					</div>
					<div class="form-group">
						<label class="col-lg-2">Gender</label>
						<div class="col-lg-8">
							<label class="radio-inline">
						      <input type="radio" name="gender" value="true">Male
						    </label>
						    <label class="radio-inline">
						      <input type="radio" name="gender" value="false">Female
						    </label>
					    </div>
					</div>
					<div class="form-group form-inline">
						<label class="col-lg-2">Birthday</label>
						<div class="input-group col-lg-8" id="date_input">
			              <input name="day" placeholder="Day" class="form-control" type="text" style="width: 55px">
			              <select name="month" class="form-control" style="width: 115px">
						    <option value="January">January</option>
						    <option value="February">February</option>
						    <option value="March">March</option>
						    <option value="April">April</option>
						    <option value="May">May</option>
						    <option value="June">June</option>
						    <option value="July">July</option>
						    <option value="August">August</option>
						    <option value="September">September</option>
						    <option value="October">October</option>
						    <option value="November">November</option>
						    <option value="December">December</option>
						  </select>
			              <input name="year" placeholder="Year" class="form-control" type="text" style="width: 55px">
			             </div>
					</div>
					<div class="form-group">
						<label for="email" class="col-lg-2">E-mail</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="email" placeholder="examle@example.com"></div>
					</div>
					<div class="form-group">
						<label for="Phone" class="col-lg-2">Phone</label>
						<div class="col-lg-8"><input type="text" class="form-control" id="phone" placeholder="+380123456789"></div>
					</div>
					<div class="form-group">
						<label class="col-lg-2">Photo</label>
						<div class="col-lg-8">
							<input type="file" name="picture" id="picture" title=""/>
							<img class="img-circle" id="small-avatar" src="">
							<img class="img-circle" id="medium-avatar" src="">
						</div>
					</div>
				</form>
			</div>
			<button class="btn-lg btn" id="save_button">Save</button>
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
</body>
</html>