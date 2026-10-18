$(document).ready(function () {
	$('.searchable').select2();

	$('.alpha').keypress(function (e) {
		var regex = new RegExp("^[a-zA-Z]+$");
		var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
		if (!regex.test(str)) {
			return false;
		}
	});

	$('.uppercasewithdigit').keypress(function (e) {
		var regex = new RegExp("^[A-Z0-9]+$");
		var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
		if (!regex.test(str)) {
			return false;
		}
	});

	$('.uppercase').keypress(function (e) {
		var regex = new RegExp("^[A-Z]+$");
		var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
		if (!regex.test(str)) {
			return false;
		}
	});

	$('.lowercasewithdigit').keypress(function (e) {
		var regex = new RegExp("^[a-z0-9]+$");
		var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
		if (!regex.test(str)) {
			return false;
		}
	});

	$('.lowercase').keypress(function (e) {
		var regex = new RegExp("^[a-z]+$");
		var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
		if (!regex.test(str)) {
			return false;
		}
	});

	$('.digit').keypress(function (e) {
		var regex = new RegExp("^[0-9]+$");
		var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
		if (!regex.test(str)) {
			return false;
		}
	});

	$('.nospace').keypress(function (e) {
		if (e.which === 32)
			return false;
	});
});