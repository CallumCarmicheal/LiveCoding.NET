<?php 
	// Could be needed so im leaving it here incase
	

	/* ========================== */
    $dbHost 			= "localhost";
    $dbName 			= "LiveCoding";
    $dbUsername 		= "root";
    $dbPassword 		= "usbw";

    $dbTableSessions	= "sessions";

    $dbOptions 			= 
    	array(PDO::MYSQL_ATTR_INIT_COMMAND => 'SET NAMES utf8');
	$databaseString 	= 
		"mysql:host={$dbHost};dbname={$dbName};charset=utf8";
	/* ========================== */
	
?>