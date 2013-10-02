<?php

define("COOKIE_NAME", 'GastSessionId');
define("SID_NAME", 'uid');

session_start();

class Resources
{
    const Gastarbiter = 1;
}

$currUserId = getUserId($_SESSION[SID_NAME]);

function getUserId($sessionId)
{
	$link = dbConnect();	
	$query = sprintf('SELECT id FROM sessions WHERE sessionId=\'%1$s\'', $sessionId);
	$result = mysql_query($query);
	mysql_close($link);
	if (mysql_num_rows($result) > 0)
	{
		return mysql_result($result, 0);
	}
	return 'NULL';
}

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
	$link = mysql_connect('faliot.ru', 'gast', 'okoo2Aephe');
	mysql_select_db('gast');
	
	if (!$link)
	{
		die('Could not connect: ' . mysql_error());
	}
	
	return $link;
}

function initSession($sessionId)
{
	global $currUserId;
	
	$link = dbConnect();
		
	$query = sprintf('INSERT INTO sessions VALUES (NULL,\'%1$s\')', $sessionId);
	mysql_query($query);
	$currUserId = mysql_insert_id();
	$query = sprintf('INSERT INTO userResources VALUES (%1$d,%2$d,0,0)',
		$currUserId, Resources::Gastarbiter);
	mysql_query($query);
	mysql_close($link);
	
	setcookie(COOKIE_NAME, $sessionId, time()+3600 * 24 * 30);
	$_SESSION[SID_NAME] = $sessionId;
	
	echo 0;
}

function loadSession($sessionId)
{
	global $currUserId;
	$currUserId = getUserId($sessionId);
	
	echo getCounts();
}

function init()
{
	if (isset($_COOKIE[COOKIE_NAME]))
	{
		$link = dbConnect();
		
		$query = sprintf('SELECT id FROM sessions WHERE sessionId=\'%1$s\'', $_COOKIE[COOKIE_NAME]);
		$result = mysql_query($query);
		if (mysql_num_rows($result) > 0)
		{
			loadSession($_COOKIE[COOKIE_NAME]);
		}
		else
		{
			initSession($_COOKIE[COOKIE_NAME]);
		}
	}
	else
	{
		initSession(uniqid());
	}
}

function click()
{
	echo addGastarbiter();
}

function addGastarbiter()
{
	global $currUserId;
	
	$link = dbConnect();
	mysql_query(sprintf(
		'UPDATE userResources SET count = count+1 WHERE userId=%1$d and resourceId=%2$d',
		$currUserId, Resources::Gastarbiter));
	$count = mysql_result(mysql_query(sprintf(
		'SELECT count FROM userResources WHERE userId=%1$d and resourceId=%2$d',
		$currUserId, Resources::Gastarbiter)), 0);
	mysql_close($link);
	
	return $count;
}

function getCounts()
{
	global $currUserId;
	
	$link = dbConnect();
	$count = mysql_result(mysql_query(sprintf(
		'SELECT count FROM userResources WHERE userId=%1$d and resourceId=%2$d',
		$currUserId, Resources::Gastarbiter)), 0);
	mysql_close($link);
	
	return $count;
}

?>