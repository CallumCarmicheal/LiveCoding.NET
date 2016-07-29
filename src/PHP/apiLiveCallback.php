<?php

	require("config.php");
	START_SCRIPT();
	
	
	// Callback script
	// This is where oAUTH will send the 
	// variables to the server for.
	
	
	// SSSSH THIS CODE WAS NOT TOTALLY TAKEN FROM ANOTHER LIVESTREAMING SERVICE I TOTALLY WAS NOT MAKING
	
	
	class Logger {
		function LogCalls($filePrepend = "", $folderLocation = "GETME") {
			// Get current working directory
			if(\strcmp($folderLocation, "GETME") == 0)
				$folderLocation = getcwd(). "/debug.output/";
			
			$fileName = "";
			/* Generate File Location */ {
				// Check if directory exists
				if(!\file_exists($folderLocation))  {
					\mkdir($folderLocation, 0777);
					
					// Add our htAccess so no one can view the files
					$hta = '
						<Files ~ "\.bin$">
							Order Allow,Deny
							Deny from all
						</Files>
						
						<Files ~ "\.dbg$">
							Order Allow,Deny
							Deny from all
						</Files>';
					
					file_put_contents($folderLocation. ".htaccess", $hta);
				}
				
				// Append fileName to FolderLocation
				$fileName = $folderLocation. $filePrepend . ".";
			}
			
			/* Count files in Directory */ {
				$files = new \FilesystemIterator($folderLocation, \FilesystemIterator::SKIP_DOTS);
				$fileName .= \iterator_count($files);
			}
			
			$reqMethod = $_SERVER['REQUEST_METHOD'];
			/* Append The Request */ {
				$fileName .= ".". $reqMethod;
			}
			
			/* Append file extension */ {
				$fileName .= ".bin";
			}
			
			$fileBuffer = "";
			/* Generate File Contents */ {
				ob_start();
				
				echo "REQUEST TIME: "; {
					// Set default timezone -.-
					date_default_timezone_set("EUROPE/London");
					echo date("Y-m-d H:i:s\n\n");
				}
				
				echo "SERVER DATA: \n"; {
					print_r($_SERVER);
				}
				
				echo "REQUEST DATA: \n"; {
					if($reqMethod == "GET") 
						print_r($_GET);
					if ($reqMethod == "POST")
						print_r($_POST);
					if ($reqMethod == "REQUEST")
						print_r($_POST);
				}
				
				$fileBuffer = ob_get_clean();
			}
			
			/* Finally write to the File */ {
				if(!file_exists($fileName)) {
					$fp = fopen($fileName,"w");
					fwrite($fp, ""); fclose($fp);
				}
				
				file_put_contents($fileName, $fileBuffer);
			}
		}
		
		function LogCallsIF($condition, $filePrepend = "", $folderLocation = "GETME") {
			if($condition) {
				$this->LogCalls($filePrepend, $folderLocation);
			}
		}
		
		function LogText($folderLocation, $fileLocation, $isAppend, $msg, $appendDate = true) {
			/* Create the folder if it does not exist (ADD HTACCESS) */ {
				if(!\file_exists($folderLocation))
					\mkdir($folderLocation, 0777);
					
				// Add our htAccess so no one can view the files
				$hta = '<Files ~ "\.bin$">
							Order Allow,Deny
							Deny from all
						</Files>
						
						<Files ~ "\.dbg$">
							Order Allow,Deny
							Deny from all
						</Files>';
					
				file_put_contents($folderLocation. "/.htaccess", $hta);
			}
			
			$fileName = "";
			/* Generate file Name */ {
				$fileName = $folderLocation. "/". $fileLocation. ".dbg";
			}
			
			$fileData = "";
			/* Generate our Data for the File */ {
				if($appendDate) {
					date_default_timezone_set("EUROPE/London");
					$fileData .= date("[Y-m-d H:i:s] ");
				} 
				
				$fileData .= $msg;
			}
			
			/* Now write our data to the File */ {
				if(!file_exists($fileName)) {
					$fp = fopen($fileName,"w");
					fwrite($fp, $fileData); fclose($fp);
				} else {
					if($isAppend) {
						$tmpFileData = $fileData;
						$fileData = file_get_contents($fileName);
						$fileData .= "\n";
						$fileData .= $tmpFileData;
						file_put_contents($fileName, $fileData, FILE_APPEND);
					} else {
						file_put_contents($fileName, $fileData);
					}
				}
			}
		}
	}
	
	$logger = new Logger();
	$logger->LogCalls("apiLiveCallback");
	
	/*
	 * 	EXAMPLE DATA DATA (GET): 
	 * 	Array (
	 *	  [state] => A87BBFC1-270F-4F38-8F7E-450C6A889E03
	 *	  [code]  => CM51K0nXIc5Fm884vKk79D63u8dDGV
	 *	)
	 */
	 
	 /**
     * [postUrlContents description].
     *
     * @param       $url
     * @param       $query
     * @param array $headers
     *
     * @return mixed|null
	 * @url https://github.com/OzanKurt/LiveCoding-API/blob/8e5690b660fcc2a22cb636eb711ef6c540a695c2/src/LiveCoding.php
     */
    function postUrlContents($url, $query, $headers = []) {
        $query['client_id'] = AUTHENTICATION_CLIENT_ID;
        $response = null;
        try {
            $curl = curl_init();
            $timeout = 5;
            curl_setopt($curl, CURLOPT_HTTPHEADER, $headers);
            curl_setopt($curl, CURLOPT_URL, $url);
            curl_setopt($curl, CURLOPT_POST, count($query));
            curl_setopt($curl, CURLOPT_POSTFIELDS, http_build_query($query));
            curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);
            curl_setopt($curl, CURLOPT_CONNECTTIMEOUT, $timeout);
			curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, API_CHECK_SSL_REQUESTS);
            $response = curl_exec($curl);
            if (false === $response) {
                throw new \Exception(curl_error($curl), curl_errno($curl));
            }
            curl_close($curl);
        } catch (\Exception $e) {
            trigger_error(
                sprintf(
                    'Curl failed with error #%d: %s',
                    $e->getCode(),
                    $e->getMessage()
                ), E_USER_ERROR
            );
        }
        return $response;
    }
	 
	 
	if(!isset($_GET)) { die("INVALID REQUEST!"); }
	if(empty($_GET))  { die("INVALID REQUEST!"); }
	
	$LIVECODING_TOKEN_URL 	= "https://www.livecoding.tv/o/token/";
	
	/*
	grant_type=authorization_code
	code=Php4iJpXGpDT8lCqgBcbfQ2yzhB0Av
	client_id=vCk6rNsC
	redirect_uri=http://localhost/externalapp" 
	-u"vCk6rNsC:sfMxcHUuNnZ"
	*/
	
	
	$fields = array(
		'code' 			=> $_GET['code'],
		'grant_type' 	=> 'authorization_code',
		'redirect_uri' 	=> AUTHENTICATION_CALLBACK_URL
	);
	
	$tokenRequiredHeaders = [
		'Cache-Control: no-cache',
		'Pragma: no-cache',
		'Authorization: Basic '.base64_encode(AUTHENTICATION_CLIENT_ID.':'.AUTHENTICATION_CLIENT_SECRET),
		'Content-Type: application/x-www-form-urlencoded'
	];
	
	$response = postUrlContents($LIVECODING_TOKEN_URL, $fields, $tokenRequiredHeaders);
	
	//print_r($response);
	//echo "<br>Done!";
	
	
	$jOBJ = json_decode($response);
	
	if(!empty($jOBJ->error)) {
		die("There was a error please regenerate your token! Error: ". $jOBJ->error);
	} else {
		GUID_SET_CODE($_GET['state'], $jOBJ->access_token, $jOBJ->refresh_token);
		die("Please go back to your application and if required manually refresh the token!");
	}
