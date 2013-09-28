<?php

    $clicks = file_get_contents("clicks.txt");
    $clicks++;

    $fp = fopen("clicks.txt", "w+");

    while ( !flock($fp, LOCK_EX) ) {    
        usleep(500000); // Delay half a second
    }

    fwrite($fp, $clicks);
    fclose($fp);
    flock($fp, LOCK_UN);
	
	echo $clicks;
	
	$link = mysql_connect('192.168.1.2', 'gast', 'okoo2Aephe');
	if (!$link) {
		die('Could not connect: ' . mysql_error());
	}
	echo 'Connected successfully';
	mysql_close($link);
?>