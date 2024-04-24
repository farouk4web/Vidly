$(document).ready(function () {

    var isAuthenticated = $("#authenticatState").attr("data-auth-state");

    if (isAuthenticated === "False") {
        $(".notAuth").remove();
    }

});