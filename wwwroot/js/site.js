// Write your JavaScript code.
var i = 0;
$( document ).ready(function(){
    $('#addPhone').on('click', addPhoneNumber);
});

function addPhoneNumber()
{
    i++;
    $('.staticFormContent').append('<div class="form-group"><div class="row"><div class="col-md-10"><input name="Phones['+i+'].Phone" class="form-control phoneNumber" placeholder="Phone Number" type="text"></div></div></div>');
}