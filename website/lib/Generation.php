<?php
	include("config.php");
	
	function generateDatabaseConnection() {

		function createDB($dieOnError = true) {
            try {
                $db = new PDO($databaseString, $dbUsername, $dbPassword, $dbOptions);
                if($DATABASE_DEBUG) {
                    $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
                    $db->setAttribute(PDO::ATTR_EMULATE_PREPARES, false);
                }
            } catch(PDOException $ex) {
                if($dieOnError) die("Failed to connect to DB: ". $ex->getMessage());
                else 			return false;
            }

            $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            $db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
            return $db;
        }

	}

	function str_random($length = 16) {
      $pool = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
      return substr(str_shuffle(str_repeat($pool, $length)), 0, $length);
	}
	
	function generateLiveCodingURL($clientID) {

	}
?>