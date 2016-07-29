<?php
	require("config.php");

	START_SCRIPT();
	
	// Generate our GUID
	$guid = GENERATE_GUID();
	$url  = LC_API_GENERATE_AUTH_URL($guid);
	
	// Generate the GUID for the application
	// With the link
	header('Content-Type: application/json');
	
	//$data = array(
	//	'guid' 	=>  $guid,
	//	'url'	=>  $url
	//); die(json_encode($data));
	
	// error whilist displaying url so we generate the json 
	// manually!
	die ('{"guid":"'. $guid. '","url":"'. $url. '"}');
	
		