// Write your Javascript code.
//$('.has-sub').on('click', function (e) {
//	//$(this).toggleClass('submenu');
//	e.stopPropagation();
//})
$('table').on('change', '.selectAll', function (e) {
	$(this).closest('tr').find(".checkbox").not(':disabled').prop('checked', this.checked);
});

//$('table').on('change', '#s', function (e) {
//	if (this.checked ) {
//		$(this).closest('tr').find("#S").prop('checked'), false);
//	}
//});

//if (!document.getElementById(s).checked) {
//	$("#S").checked = true;
//}

//$("#s").on('click', 'input[type="checkbox"'), function() {
//	$(this).closest('tr').find("#S").checked = true;
//}

$('table').on('change', '.s', function (e) {
	if (!this.checked) {
		bootbox.alert({
			title: "Warning",
			message: "Disabling <b>SELECT</b> will also disable <b>UPDATE</b> and <b>DELETE</b>"
		});
		$(this).closest('tr').find(".ds").prop('checked', false);
		$(this).closest('tr').find(".d").prop('checked', false);
		$(this).closest('tr').find(".u").prop('checked', false);
		$(this).closest('tr').find(".dd").prop('checked', false);
		$(this).closest('tr').find(".du").prop('checked', false);
	}
});

$('table').on('change', '.i', function (e) {
	if (!this.checked) {
		$(this).closest('tr').find(".di").prop('checked', false);
	}
});

$('table').on('change', '.d', function (e) {
	if (!this.checked) {
		$(this).closest('tr').find(".dd").prop('checked', false);
	} else {
		bootbox.alert({
			title: "Warning",
			message: "Enabling <b>DELETE</b> will also enable <b>SELECT</b>"
		});
		$(this).closest('tr').find(".s").not(':disabled').prop('checked', true);
	}
});

$('table').on('change', '.u', function (e) {
	if (!this.checked) {
		$(this).closest('tr').find(".du").prop('checked', false);
	} else {
		bootbox.alert({
			title: "Warning",
			message: "Enabling <b>UPDATE</b> will also enable <b>SELECT</b>"
		});
		$(this).closest('tr').find(".s").not(':disabled').prop('checked', true);
	}
});

$('table').on('change', '.ds', function (e) {
	if (this.checked) {
		$(this).closest('tr').find(".s").prop('checked', true);
	}
});

$('table').on('change', '.di', function (e) {
	if (this.checked) {
		$(this).closest('tr').find(".i").prop('checked', true);
	}
});

$('table').on('change', '.dd', function (e) {
	if (this.checked) {
		$(this).closest('tr').find(".d").prop('checked', true);
	} else {
		$(this).closest('tr').find(".s").prop('checked', true);
	}
});

$('table').on('change', '.du', function (e) {
	if (this.checked) {
		$(this).closest('tr').find(".u").prop('checked', true);
	} else {
		$(this).closest('tr').find(".s").prop('checked', true);
	}
});


//$(".alert").on("change", function (e) {
//	if(!e.target.checked)
//		bootbox.alert("TEST");
//});