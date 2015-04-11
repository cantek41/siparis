$(document).ready(function() {

	// Adds title attributes and classnames to list items
    $("ul#nav li a:contains('Opportunity')").addClass("opportunity").attr('title', 'Opportunity');
	$("ul#nav li a:contains('Sample')").addClass("sample").attr('title', 'Sample');
	$("ul#nav li a:contains('Offer')").addClass("offer").attr('title', 'Offer');
	$("ul#nav li a:contains('Draft')").addClass("draft").attr('title', 'Draft');
	$("ul#nav li a:contains('Order')").addClass("order").attr('title', 'Order');
	$("ul#nav li a:contains('InReview')").addClass("review").attr('title', 'InReview');
	$("ul#nav li a:contains('Pending')").addClass("pending").attr('title', 'Pending');
	$("ul#nav li a:contains('Approve')").addClass("approve").attr('title', 'Approve');
	$("ul#nav li a:contains('Edited')").addClass("edit").attr('title', 'Edited');
	$("ul#nav li a:contains('Processed')").addClass("process").attr('title', 'Processed');
	$("ul#nav li a:contains('Shipping Confirm')").addClass("ship-confirm").attr('title', 'Shipping Confirm');
	$("ul#nav li a:contains('Shipping')").addClass("shipping").attr('title', 'Shipping');
	$("ul#nav li a:contains('Shipped')").addClass("shipped").attr('title', 'Shipped');
	$("ul#nav li a:contains('Cancelled')").addClass("cancel").attr('title', 'Cancelled');
	$("ul#nav li a:contains('Dispatch Note')").addClass("dispatch").attr('title', 'Dispatch Note');
	$("ul#nav li a:contains('Invoice')").addClass("invoice").attr('title', 'Invoice');
	$("ul#nav li a:contains('Linesheets')").addClass("linesheet").attr('title', 'Linesheets');

    //Türkçe Menu

	$("ul#nav li a:contains('rsat')").addClass("opportunity").attr('title', 'Opportunity');
	$("ul#nav li a:contains('Numune')").addClass("sample").attr('title', 'Sample');
	$("ul#nav li a:contains('Teklif')").addClass("offer").attr('title', 'Offer');
	$("ul#nav li a:contains('Taslak')").addClass("draft").attr('title', 'Draft');
	$("ul#nav li a:contains('Sipari')").addClass("order").attr('title', 'Order');
	$("ul#nav li a:contains('nceleniyor')").addClass("review").attr('title', 'InReview');
	$("ul#nav li a:contains('Onay ')").addClass("pending").attr('title', 'Pending');
	$("ul#nav li a:contains('Onayland')").addClass("approved").attr('title', 'Approve');
	$("ul#nav li a:contains('zenleniyor')").addClass("edit").attr('title', 'Edited');
	$("ul#nav li a:contains('lem')").addClass("process").attr('title', 'Processed');
	$("ul#nav li a:contains('Sevk Onay')").addClass("ship-confirm").attr('title', 'Shipped');
	$("ul#nav li a:contains('Sevk Ediliyor')").addClass("shipping").attr('title', 'Shipped');
	$("ul#nav li a:contains('Sevk Edildi')").addClass("shipped").attr('title', 'Shipped');
	$("ul#nav li a:contains('ptal')").addClass("cancel").attr('title', 'Cancelled');
	$("ul#nav li a:contains('rsaliye')").addClass("dispatch").attr('title', 'Dispatch Note');
	$("ul#nav li a:contains('Fatura')").addClass("invoice").attr('title', 'Invoice');
	$("ul#nav li a:contains('neriler')").addClass("linesheet").attr('title', 'Linesheets');
	// Animate sidebar navigation
	
	$('ul#nav li:has(ul)').hover(function(){
		$('.flyoutBg').stop().animate({ left : '175px' }, 300);
		$(this).find('ul').stop().animate({'left' : '175px'}, 300);
		}, function() {
		$('.flyoutBg').stop().animate({ left : '25px' }, 300);
		$(this).find('ul').stop().animate({'left' : '25px'}, 300);
	});
});