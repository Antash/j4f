<?php

$cookieName = "GastSessionId";

if (isset($_COOKIE[$cookieName]))
{
	echo "init" + $_COOKIE[$cookieName];
	header("Location: click.php/?" + $_COOKIE[$cookieName]);
}
else
{
	$link = mysql_connect('192.168.1.2', 'gast', 'okoo2Aephe');
	if (!$link) {
		die('Could not connect: ' . mysql_error());
	}
	echo 'Connected successfully';
	
	$query = "INSERT INTO sessions VALUES (DEFAULT,0)";
	mysql_query($query);
	$sessionId = mysql_insert_id($link);
	setcookie($cookieName, $sessionId, time()+3600 * 24 * 30);
	
	mysql_close($link);
	
	echo " ID = " + $sessionId;
}

?>