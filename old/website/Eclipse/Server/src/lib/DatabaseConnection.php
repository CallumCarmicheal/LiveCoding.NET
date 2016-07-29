<?php
	namespace Lib;
	
	include("config.php");
	
	
	class Database {
		private $dbHost 			= "localhost";
		private $dbName 			= "LiveCoding";
		private $dbUsername 		= "root";
		private $dbPassword 		= "usbw";
		private $dbOptions 			= array(
			PDO::MYSQL_ATTR_INIT_COMMAND => 'SET NAMES utf8'
		);
		
		public	$dbTableSessions	= "sessions";
		public 	$dbDebugger			= false;
		public 	$dbDieOnError		= false;
		
		private static function genConnectionSTR() {
			return "mysql:host={$this->$dbHost};dbname={$this->$dbName};charset=utf8";
		}
		
		public static function createDatabase() {
			$db = null;
			
			try {
				$db = new PDO(
						$this->genConnectionSTR(), 
						$this->dbUsername, $this->dbPassword, 
						$this->dbOptions);
				
				if($this->dbDebugger) {
					$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
					$db->setAttribute(PDO::ATTR_EMULATE_PREPARES, false);
				}
			} catch(PDOException $ex) {
				if($this->dbDieOnError) die("Failed to connect to DB: ". $ex->getMessage());
				else 					return false;
			}
			
			$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
			return $db;
		}
	}
	
	
	
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