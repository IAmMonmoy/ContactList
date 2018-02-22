// Write your JavaScript code.
$( document ).ready(function(){
    $('#addPhone').on('click', addPhoneNumber);
});

function addPhoneNumber()
{
    $('.staticFormContent').append('<div class="form-group"><div class="row"><div class="col-md-10"><input class="form-control" id="phoneNumber" placeholder="Phone Number" type="text"></div></div></div>');
}