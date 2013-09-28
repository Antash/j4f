<?php

if(isset($_POST['action']) && !empty($_POST['action']))
{
    $action = $_POST['action'];
    switch($action)
	{
        case 'init' : init(); break;
        case 'click' : click(); break;
    }
}

$sessionId;

function init()
{
	$cookieName = 'GastSessionId';

	if (isset($_COOKIE[$cookieName]))
	{
		echo 'init' . $_COOKIE[$cookieName];
		header('Location: main.php/?id=' . $_COOKIE[$cookieName], true);
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
}

function click()
{
	#echo 'Hello ' . htmlspecialchars($_GET["id"]) . '!';
	
    $clicks = file_get_contents("clicks.txt");
    $clicks++;

    $fp = fopen("clicks.txt", "w+");

    while ( !flock($fp, LOCK_EX) )
	{    
        usleep(500000); // Delay half a second
    }

    fwrite($fp, $clicks);
    fclose($fp);
    flock($fp, LOCK_UN);
	
	echo $clicks;
}

?>