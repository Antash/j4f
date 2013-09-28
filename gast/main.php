<?php

session_start();

if(isset($_POST['action']) && !empty($_POST['action']))
{
    $action = $_POST['action'];
    switch($action)
	{
        case 'init' : init(); break;
        case 'click' : click(); break;
    }
}

function init()
{
	$cookieName = 'GastSessionId';
	$sessionId;
	
	if (isset($_COOKIE[$cookieName]))
	{
		$sessionId = $_COOKIE[$cookieName];
		echo $sessionId;
	}
	else
	{
		$link = mysql_connect('192.168.1.2', 'gast', 'okoo2Aephe');
		mysql_select_db('gast');
		
		if (!$link)
		{
			die('Could not connect: ' . mysql_error());
		}
		
		$sessionId = uniqid();
		$query = sprintf('INSERT INTO sessions VALUES (\'%1$s\',0)', $sessionId);
		mysql_query($query);
		
		setcookie($cookieName, $sessionId, time()+3600 * 24 * 30);
		
		mysql_close($link);
		
		echo ' ID = ' . $sessionId;
	}
	
	$_SESSION['uid'] = $sessionId;
	//setcookie($cookieName, "", time() - 3600);
}

function click()
{
	$sessionId = $_SESSION['uid'];
	$clicks = 0;
	$result = mysql_query('UPDATE sessions SET clicks = clicks+1 WHERE id=\'%1$s\'', $sessionId);
	if ($result)
	{
		$clicks = mysql_result(mysql_query('SELECT clicks FROM sessions WHERE id=\'%1$s\'', $sessionId), 0);
	}
	else {$clicks = $_SESSION['uid'];}
	
	echo $clicks;
}

?>