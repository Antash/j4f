<?php

$cookieName = 'GastSessionId';
$sessionId

if (isset($_COOKIE[$cookieName]))
{
	echo 'init' . $_COOKIE[$cookieName];
	header('Location: count.php/?id=' . $_COOKIE[$cookieName], true);
}
else
{
	$link = mysql_connect('192.168.1.2', 'gast', 'okoo2Aephe');
	mysql_select_db('gast');
	
	if (!$link) {
		die('Could not connect: ' . mysql_error());
	}
	#echo 'Connected successfully';
	
	global $sessionId = uniqid();
	$query = sprintf('INSERT INTO sessions VALUES (\'%1$s\',0)', $sessionId);
	#echo $query;
	if (!mysql_query($query)) { 
		echo mysql_error();
	}
	setcookie($cookieName, $sessionId, time()+3600 * 24 * 30);
	
	mysql_close($link);
	
	echo ' ID = ' . $sessionId;
}

#setcookie($cookieName, "", time() - 3600);

?>