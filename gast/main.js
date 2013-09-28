function Click()
{
	$.ajax({
		url: 'main.php',
		type: 'POST',
		data: {action: 'click'},
		success: function(data) {
			$('#gastCount').html(data);
			}
	});
}

function InitSession()
{
	$.ajax({
		url: 'main.php',
		type: 'POST',
		data: {action: 'init'},
		success: function(data) { 
			//$('#Test').html(data);
		}
	});
}