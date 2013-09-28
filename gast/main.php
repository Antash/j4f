<?php

define("COOKIE_NAME", 'GastSessionId');

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

function dbConnect()
{
	$link = mysql_connect('192.168.1.2', 'gast', 'okoo2Aephe');
	mysql_select_db('gast');
	
	if (!$link)
	{
		die('Could not connect: ' . mysql_error());
	}
	
	return $link;
}

function initSession($sessionId)
{
	$link = dbConnect();
		
	$query = sprintf('INSERT INTO sessions VALUES (\'%1$s\',0)', $sessionId);
	mysql_query($query);
	mysql_close($link);
	
	setcookie(COOKIE_NAME, $sessionId, time()+3600 * 24 * 30);
	$_SESSION['uid'] = $sessionId;
}

function loadSession($sessionId)
{
	$link = dbConnect();
		
	$query = sprintf('SELECT clicks FROM sessions WHERE id=\'%1$s\'', $sessionId);
	$result = mysql_query($query);
	if (mysql_num_rows($result) > 0)
	{
		$clicks = mysql_result($result, 0);
	}
	else
	{
		initSession($sessionId);
		$clicks = 0;
	}
	mysql_close($link);
	$_SESSION['uid'] = $sessionId;
	
	echo $clicks;
}

function init()
{
	if (isset($_COOKIE[COOKIE_NAME]))
	{
		loadSession($_COOKIE[COOKIE_NAME]);
	}
	else
	{
		initSession(uniqid());
	}
}

function click()
{
	$sessionId = $_SESSION['uid'];
	$clicks;
	
	$link = dbConnect();
	mysql_query(sprintf('UPDATE sessions SET clicks = clicks+1 WHERE id=\'%1$s\'', $sessionId));
	$clicks = mysql_result(mysql_query(sprintf('SELECT clicks FROM sessions WHERE id=\'%1$s\'', $sessionId)), 0);
	mysql_close($link);
	
	echo $clicks;
}

?>