<?php
	// We just expell expired tokens from the database
	// Which is done within START_SCRIPT so we just let it do its
	// thing without any more lines of code required.
	require("config.php");
	START_SCRIPT();