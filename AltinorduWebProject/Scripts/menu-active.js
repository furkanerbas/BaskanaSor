$(function () {
    setNavigation();
});

function setNavigation() {
    var path = window.location.pathname;
    path = path.replace(/\/$/, "");
    path = decodeURIComponent(path);
    if (path == '') {
        path = '/Contact/Chart';
    }

    $(".nav li a").each(function () {
        var href = $(this).attr('href');
        if (path  === href) {
            $(this).addClass('active');
        }
    });
}