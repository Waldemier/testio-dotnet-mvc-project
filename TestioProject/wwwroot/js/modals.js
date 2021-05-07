// Referrer token modal

$(function () {
    let ShowModalHere = $('#ShowModalHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        let url = $(this).data('url');
        $.get(url).done(function (data) {
            ShowModalHere.html(data);
            ShowModalHere.find('.modal').modal('show');
        })
    })

    /*ShowModalHere.on('click', '[data-save="modal"]', function (event) {
        let form = $(this).parents('.modal').find('form');
        let actionUrl = form.attr('action');
        let sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (data) {
            ShowModalHere.find('.modal');
        })
    })*/
})

// Deleting Account Modal

$(function () {
    let ShowModalHere2 = $('#ShowModalHere2');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        let url = $(this).data('url');
        $.get(url).done(function (data) {
            ShowModalHere2.html(data);
            ShowModalHere2.find('.modal').modal('show');
        })
    })
})