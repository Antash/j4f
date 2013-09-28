<?php
	
	echo 'Hello ' . htmlspecialchars($_GET["id"]) . '!';
	
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
?>