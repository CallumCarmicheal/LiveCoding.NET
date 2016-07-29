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
		$STATE_ID_NOTFOUND	  = 1; // Not made or is invalid!
		$STATE_ID_NOTYETVALID = 2; // Created but waiting for response
		$STATE_ID_VALID		  = 3; // VALID
		$STATE_ID_INVALID	  = 4; // INVALID
		
		$GUID_STATE			  = GUID_GETDATA($_GET['g'], $GUID);
		
		if(!$GUID_STATE) {
			// INVALID
			$response = json_encode( array ( 'state'	=> $STATE_ID_NOTFOUND ));
		} else {
			$VALID = ($GUID['valid'] == 1);
			
			$response = json_encode( array ( 'state'	=> ($VALID ? $STATE_ID_VALID : $STATE_ID_NOTYETVALID) ));
		}
	}
	
	// Return JSON
	header('Content-Type: application/json');
	die($response);