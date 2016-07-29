<?php

	require("config.php");
	START_SCRIPT();
	
	$response = "";
	if(empty($_GET['g'])) {
		$response = json_encode( array (
			'Error' 		=> true,
			'Error_Message' => 'No GUID found in request'
		));
	} else {
		$GUID_STATE			  = GUID_GETDATA($_GET['g'], $GUID);
		
		if(!$GUID_STATE) {
			$response = json_encode( array (
				'Error' 		=> true,
				'Error_Message' => 'GUID does not exist!'
			));
		} else {
			$response = json_encode( array ( 'code' => $GUID['token']; ) );
		}
	}
	
	// Return JSON
	header('Content-Type: application/json');
	die($response);