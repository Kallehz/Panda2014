$(document).ready(function () {
    $('form[id=\"upvote\"] button[type=\"submit\"]').click(function (e) {
        var upvotes = parseInt($(this).html()[37]);
        $(this).html(upvotes + 1 + " <span class=\"glyphicon glyphicon-arrow-up\"></span>");
        $(this).children().css({ "color": "lightgreen" });
        $(this).children().fadeOut(1000);
        var id = $(this).siblings("input").val();
        $.ajax({
            type: 'post',
            url: '/Request/Upvote',
            data: { id: id },
        });
        e.preventDefault();
    });
});