$(document).ready(function() {

	// Adds title attributes and classnames to list items
	 
	$("ul#nav li a:contains('Sample')").addClass("dashboard").attr('title', 'Sample');
	$("ul#nav li a:contains('Offer')").addClass("pages").attr('title', 'Offer');
	$("ul#nav li a:contains('Draft')").addClass("assets").attr('title', 'Draft');
	$("ul#nav li a:contains('Order')").addClass("comments").attr('title', 'Order');
	$("ul#nav li a:contains('InReview')").addClass("widgets").attr('title', 'InReview');
	$("ul#nav li a:contains('Pending')").addClass("maps").attr('title', 'Pending');
	$("ul#nav li a:contains('Approve')").addClass("search").attr('title', 'Approve');
	$("ul#nav li a:contains('Edited')").addClass("trash").attr('title', 'Edited');
	$("ul#nav li a:contains('Processed')").addClass("settings").attr('title', 'Processed');
	$("ul#nav li a:contains('Shipped')").addClass("widgets").attr('title', 'Shipped');
	$("ul#nav li a:contains('Cancelled')").addClass("maps").attr('title', 'Cancelled');
	$("ul#nav li a:contains('Dispatch Note')").addClass("search").attr('title', 'Dispatch Note');
	$("ul#nav li a:contains('Invoice')").addClass("trash").attr('title', 'Invoice');
	$("ul#nav li a:contains('Linesheets')").addClass("settings").attr('title', 'Linesheets');

	// Animate sidebar navigation
	
	$('ul#nav li:has(ul)').hover(function(){
		$('.flyoutBg').stop().animate({ left : '175px' }, 300);
		$(this).find('ul').stop().animate({'left' : '175px'}, 300);
		}, function() {
		$('.flyoutBg').stop().animate({ left : '25px' }, 300);
		$(this).find('ul').stop().animate({'left' : '25px'}, 300);
	});
});