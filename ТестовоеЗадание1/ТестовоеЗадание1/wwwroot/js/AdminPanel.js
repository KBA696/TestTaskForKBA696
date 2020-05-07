function open() {
    $.ajax({
        url: "/admi",
        cache: false,
        success: function (html) {
            $("#content").html(html);
        }
    });
}
function open1() {
    $.ajax({
        url: "/admi",
        data: ({ "login": $('#loginField').val(), "pas": $('#passwordField').val() }),
        cache: false,
        success: function (html) {
            $("#content").html(html);
        }
    });
}