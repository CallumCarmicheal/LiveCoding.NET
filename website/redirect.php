<?php
	
	$debug = false;

	if($debug) {
		$fi = new FilesystemIterator(__DIR__. "/redirect_output/", FilesystemIterator::SKIP_DOTS);

		$result = "";
		$fileName = __DIR__. "/redirect_output/". "request.". $_SERVER['REQUEST_METHOD']. ".". iterator_count($fi);


		if($_SERVER['REQUEST_METHOD'] === 'POST') {
			ob_start();
				var_dump($_POST);
			$result = ob_get_clean();
		} else if ($_SERVER['REQUEST_METHOD'] === 'GET') {
			ob_start();
				var_dump($_GET);
			$result = ob_get_clean();
		} else {
			$result = "Unknown Request Found";
		}
		
		$status = file_put_contents($fileName, $result);
	}
?>