/* eslint-disable */

declare var jQuery;
declare var $;
declare var Resources;

$(document).ready(function () {
    startRating();
    $('#copyright').fadeIn().delay(6000).fadeOut();

    // Smooth scrolling using jQuery easing
    $('a.js-scroll-trigger[href*="#"]:not([href="#"])').click(function () {
        if (location.pathname.replace(/^\//, '') === this.pathname.replace(/^\//, '') && location.hostname === this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                $('html, body').animate({
                    scrollTop: (target.offset().top)
                }, 1250, "easeInOutExpo");
                return false;
            }
        }
    });
    // Closes responsive menu when a scroll trigger link is clicked
    $('.js-scroll-trigger').click(function () {
        $('.navbar-collapse').collapse('hide');
    });
    // Activate scrollspy to add active class to navbar items on scroll
    $('body').scrollspy({
        target: '#sideNav'
    });

    // Show password dialog if necessary
    if ($('#btnShowPasswordModal').length) {
        $('#btnShowPasswordModal').trigger('click');
    }

    initializeTagsinput();
});

function enableSubmit() {
    $('#submitCaptcha').removeAttr('disabled');
}