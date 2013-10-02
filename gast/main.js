function Click()
{
	$.ajax({
		url: 'main.php',
		type: 'POST',
		data: {action: 'click'},
	});
	$('#gastCount').html(parseInt($('#gastCount').html(), 10)+1);
}

function InitSession()
{
	$.ajax({
		url: 'main.php',
		type: 'POST',
		data: {action: 'init'},
		success: function(data) { 
			$('#gastCount').html(data);
		}
	});
}