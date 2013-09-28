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
	global $sessionId;
	
	if (isset($_COOKIE[$cookieName]))
	{
		$sessionId $_COOKIE[$cookieName];
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

	#setcookie($cookieName, "", time() - 3600);
}

function click()
{
	global $sessionId;
	
	mysql_query('UPDATE sessions SET clicks = clicks+1 WHERE id=\'%1$s\'', $sessionId);
	$clicks = mysql_query('SELECT clicks FROM sessions WHERE id=\'%1$s\'', $sessionId)
	
	echo $clicks;
}

?>