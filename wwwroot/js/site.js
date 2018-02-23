// Write your JavaScript code.
$( document ).ready(function(){
    $('#addPhone').on('click', addPhoneNumber);
});

function addPhoneNumber()
{
    $('.staticFormContent').append('<div class="form-group"><div class="row"><div class="col-md-10"><input asp-for="Phones[].Phone" class="form-control" placeholder="Phone Number" type="text"></div></div></div>');
}