<?php
session_start();
?>

<html>
    <head>
        <title>Gast</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    </head>
    <body onload='InitSession();'>
	
		<script type="text/JavaScript" src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
		<script type="text/JavaScript" src="main.js"></script>
		
        <input id="btnMain" type='button' onClick='Click();' value='Понаехать!'/>
        <br/>
		<br/>
		Таджиков:
        <div id='gastCount'>
        </div>
    </body>
</html>