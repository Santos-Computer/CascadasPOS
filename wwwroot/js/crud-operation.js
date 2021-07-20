
var detailLink = document.currentScript.getAttribute('modalDetailLink');
var deleteLink = document.currentScript.getAttribute('deleteLink');

var selectedItemId = 0;
var selectedItemName = '';
var rowElement = null;

$(document).on('click', '.item-row', function () {

    $('#btn-edit').attr('disabled', false);
    $('#btn-detail').attr('disabled', false);
    $('#btn-delete').attr('disabled', false);
    selectedItemId = $(this).data('id');
    selectedItemName = $(this).data('name');
    rowElement = this;
    $('.item-row').removeClass('bg-active');
    $(this).addClass('bg-active');

});

$('#btn-detail').click(function () {

    $.get(detailLink, { id: selectedItemId }, function (data) {
        $('#modal-container').html(data);
        $('#detailModal').modal('show');
    });
});

$('#btn-delete').click(function () {

    let __RequestVerificationToken = $("input[name=__RequestVerificationToken]").val();

    console.log(__RequestVerificationToken)
    console.log(deleteLink)

    Swal.fire({
        html: `
                        <i style="font-size: 58px; color:3085d6; " class="fas fa-exclamation-triangle"></i>

                        <p class="mt-4" style="font-size:18px">Do you want to</br>delete this item?</p>
                      `,
        showCancelButton: true,
        confirmButtonColor: '#ff5e57',
        cancelButtonColor: '#575fcf',
        confirmButtonText: '<i class="far fa-trash-alt"></i> Delete',
        cancelButtonText: 'Cancel <i class="fas fa-times"></i>',
        footer: 'You wont be able to recover it'
    }).then((result) => {
        if (result.isConfirmed) {

            $.post(deleteLink, { id: selectedItemId, __RequestVerificationToken }, function (deleted) {
                if (deleted) {
                    $(rowElement).fadeOut();
                    $('#item-name').text(selectedItemName);
                    $('.toast').toast('show');
                }
            })
        }
    })
});