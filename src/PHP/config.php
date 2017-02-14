<?php 
	
	// AUTHENTICATION TYPES || ENUMS
	define("ENUM_AUTHENTICATION_IMPLICIT", 		"T_A_I");
	
	// URL's
	define("LC_API_URL_AUTHORISE", 				"https://www.livecoding.tv/o/authorize/?");
	
	
	// SETTINGS
	define("AUTHENTICATION_TYPE", 				ENUM_AUTHENTICATION_IMPLICIT);
	define("AUTHENTICATION_CLIENT_ID", 			"");
	define("AUTHENTICATION_CLIENT_SECRET", 		"");
	define("AUTHENTICATION_CALLBACK_URL", 		""); 
	define("TOKEN_EXPIRATION_MINS",				120);   // This is how many minutes until inactive and un-used tokens are deleted
													    // Note once the token has been enabled the token will be set to "NOW() + 2 HOURS!"
	define("API_CHECK_SSL_REQUESTS", 			false); // This will check the ssl cert when using CURL, WILL CAUSE ISSUES SOMETIMES! DEFAULT: TRUE!
	
	// MYSQL SETTINGS
	define("MYSQL_USERNAME",	"root");
	define("MYSQL_PASSWORD", 	"");
	define("MYSQL_HOST",		"127.0.0.1");
	define("MYSQL_DATABASE", 	"callumcarmicheal_livecoding_tv");
	
	// Functions
	function DB_CONNECT(&$database) {
		try {
			$un = MYSQL_USERNAME;
			$up = MYSQL_PASSWORD;
			$uh = MYSQL_HOST;
			$ud = MYSQL_DATABASE;
			$db = new PDO("mysql:host=$uh;dbname=$ud", $un, $up);
			
			// Debugging
			$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
			
			$database = $db;
			return true;
		} catch(PDOException $ex) {
			// IF DEBUG
				die("MYSQL ERR: ". $ex->getMessage());
			// ELSE
		
			return false;
		}
	}
	
	function _GENERATE_GUID() {
		if (function_exists('com_create_guid') === true) {
			return trim(com_create_guid(), '{}');
		}

		return sprintf('%04X%04X-%04X-%04X-%04X-%04X%04X%04X', mt_rand(0, 65535), mt_rand(0, 65535), mt_rand(0, 65535), mt_rand(16384, 20479), mt_rand(32768, 49151), mt_rand(0, 65535), mt_rand(0, 65535), mt_rand(0, 65535));
	}
	
	function LC_API_GENERATE_AUTH_URL($GUID) {
		//https://www.livecoding.tv/o/authorize/?
		//scope=read
		//state=
		//redirect_uri=
		//response_type=code
		//client_id=
		
		
		//https://www.livecoding.tv/o/authorize/?
		//scope=read
		//state=1B43E79F-3944-4820-8E4D-06CD0280D409
		//redirect_uri=http://callumcarmicheal.com/LCAPI/apiLiveCallback.php
		//response_type=code
		//client_id=iYbUJNmaANmXDsXJtAyYVAbVCCJmNu3APODlMQTe
		
		$url = LC_API_URL_AUTHORISE;
		$url .= 'scope=read&';
		$url .= 'state='. $GUID. '&';
		$url .= 'redirect_uri='. AUTHENTICATION_CALLBACK_URL. '&';
		$url .= 'response_type=code&';
		$url .= 'client_id='. AUTHENTICATION_CLIENT_ID;
		
		return $url;
	}
	
	// CALLED AT START OF EACH SCRIPT
	// ITS A CRON JOB AS SUCH
	// REMOVES ALL EXPIRED GUID's IN THE
	// CACHE_STORE
	function START_SCRIPT() {
		DB_CONNECT($db);
	
		try {
			// No need to sqli protect because we created the token data.
			$sql_timed_out 	= 
				"DELETE FROM `cache_store` WHERE `cache_store`.`valid` = 0 AND `cache_store`.`created` < DATE_SUB(now(), INTERVAL ". TOKEN_EXPIRATION_MINS. " MINUTE);";
			$sql_expired	=
				"DELETE FROM `cache_store` WHERE cache_store`.`expires` < now();";
				
			$sql = $sql_timed_out. $sql_expired;
			$res = $db->prepare($sql);
			$res->execute();
		} catch(PDOException $ex) {
			// IF DEBUG
				die("MYSQL ERR (GEN_TOK) : ". $ex->getMessage());
		}
	}
	
	// CHECK DATABASE FOR EXISTING GUID
	// THEN RETURN IF THERE IS NONE.
	function GENERATE_GUID() {
		$x=false;
		$dbState = DB_CONNECT($db);
		$token = "";
		
		while(!$x) {
			$token = _GENERATE_GUID();
			
			try {
				// No need to sqli protect because we created the token data.
				$sql = "select id from cache_store WHERE token='$token'";
				$res = $db->prepare($sql);
				$res->execute();
				$result = $res->rowCount();
				
				// Row count == 0  or -1 ...
				if($result == 0) {
					// Insert our token into the database
					GUID_DATABASE_INSERT($token, 0);
					return $token;
				}
				
				// ELSE LET IT LOOP!
			} catch(PDOException $ex) {
				// IF DEBUG
					die("MYSQL ERR (GEN_TOK) : ". $ex->getMessage());
			}
		} return $token;
	}
	
	
	function GUID_DATABASE_INSERT($GUID, $VALID) {
		if($VALID == true)
			$valid = 1;
		if($VALID == false) 
			$valid = 0;
			
		if(!is_numeric ($VALID)) {
			// If its not a number it will be set to false by default!
			$valid = 0; 
		} else {
			// If error parsing into int, just make it 0
			try { $valid = (int)$VALID; } catch (Exception $ex)  {$valid = 0;}
		}
		
		$sql = "
			INSERT INTO `cache_store` (`id`, `guid`, `token`, `valid`, `expires`, `created`) 
			VALUES (NULL, :guid, '', :valid, DATE_ADD(NOW(), INTERVAL ". TOKEN_EXPIRATION_MINS. " MINUTE) , CURRENT_TIMESTAMP);";
		
		$array = array(
			':guid'  => $GUID,
			':valid' => $valid
		);
		
		$dbState = DB_CONNECT($db);
		$res = $db->prepare($sql);
		$res->execute($array);
	}
	
	function GUID_ISVALID($GUID) {
		$dbState = DB_CONNECT($db);
		try {
			// No need to sqli protect because we created the token data.
			$sql = "select id from cache_store WHERE guid=:guid";
			$arr = array( ':guid' => $GUID );
			$res = $db->prepare($sql);
			$res->execute($arr);
			$result = $res->rowCount();
			
			// Row count == 0  or -1 ...
			if($result == 0) 	return false;
			else				return true;
		} catch(PDOException $ex) {
			// IF DEBUG
				die("MYSQL ERR (GUID_ISVALID) : ". $ex->getMessage());
		}
	}
	
	function GUID_GETDATA($GUID, &$data) {
		$dbState = DB_CONNECT($db);
		$state 	 = GUID_ISVALID($GUID);
		
		if(!$state) return false;
		
		try {
			// No need to sqli protect because we created the token data.
			$sql = "select * from cache_store WHERE guid=:guid";
			$arr = array( ':guid' => $GUID );
			$res = $db->prepare($sql);
			$res->execute($arr);
			$result = $res->fetch();
			
			if($result) {
				$data = $result;
				return true;
			} else {
				return false;
			}
		} catch(PDOException $ex) {
			// IF DEBUG
				die("MYSQL ERR (GUID_GETDATA) : ". $ex->getMessage());
		}
	}
	
	function GUID_SET_CODE($GUID, $TOKEN, $REFRESH) {
		$sql = "UPDATE `cache_store` SET `token`=:token, `refresh`=:refresh, `valid`=1, `expires`=DATE_ADD(now(), INTERVAL 36600 SECOND) WHERE `cache_store`.`guid`=:guid";
		$array = array(
			':token' => $TOKEN,
			':refresh' => $REFRESH,
			':guid' => $GUID
		);
		
		$dbState = DB_CONNECT($db);
		
		$res = $db->prepare($sql);
		$res->execute($array);
	}
	
	// cache_store
	// 		id			int			255
	// 		guid 		varchar		40
	//		token		varchar		255
	//		refresh		varchar		255
	//		valid		tinyint		
	//		created		timestamp	
	//		expires		timestamp	