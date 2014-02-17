$(function() {
    'use strict';
    var $success;

    $success = $('.alert');
    $success.on('click', '.close', function (event) {
        event.preventDefault();
        $success.fadeOut('400', function () {
            $(this).remove();
        });
    });

    console.log("HEJ");
    var anchor = $('.image a');
    

    anchor.each(function (index) {
        var href = $(this).attr('href');
        console.log(href);
        console.log(decodeURIComponent(location.search));

        console.log($(this));

        if (href === decodeURIComponent(location.search)) {
            $(this).parent().addClass('current');
        }
    });
});