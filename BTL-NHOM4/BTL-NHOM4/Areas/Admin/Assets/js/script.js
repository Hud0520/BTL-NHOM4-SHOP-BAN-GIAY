$(document).ready(function(){
	$("#user").click(function(event){
		event.preventDefault();
	})
});


$(".parent-dropdown").click(function(){
	if(!this.hasAttribute('show_drop')){
		$('.parent-dropdown').css('background-color', '#2F4050');
		$(this).css('background-color', '#223649');
		$('.child-dropdown').hide('slow');
		$('.parent-dropdown').removeAttr("show_drop");
		$(this).find('ul.child-dropdown').show('slow');
		$(this).attr('show_drop', '1');
	}
	else{
		$(this).find('ul.child-dropdown').hide('slow');
		$(this).removeAttr("show_drop");
		$(this).css('background-color', '#2F4050');
	}
});

var loadFile = function (event) {
	var image = document.getElementById('output_image');
	image.src = URL.createObjectURL(event.target.files[0]);
};