// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$('span').on('click', function (e) {
    $(e.target).next('ul').toggleClass('show');
    if ($(e.target).text() == '+') {
        $(e.target).text('-');
    } else {
        $(e.target).text('+');
    }
});
