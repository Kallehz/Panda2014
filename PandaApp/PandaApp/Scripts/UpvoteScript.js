$(document).ready(function () {
    $('form[class=\"upvote\"] button[type=\"submit\"]').click(function (e) {
        var upvotes = parseInt($(this).html()[37]);
        $(this).html(upvotes + 1 + " <span class=\"glyphicon glyphicon-arrow-up\"></span>");
        $(this).children().css({ "color": "rgb(85, 207, 85)" });
        $(this).children().fadeOut(1000);
        var id = $(this).siblings("input").val();
        $.ajax({
            type: 'post',
            url: '/Request/Upvote',
            data: { id: id },
        });
        e.preventDefault();
    });

    $('form[id=\"upvote\"]').submit(function (e) {
        $("[type=\"submit\"]").attr("value", "Upvoted!");
        $("input").css({ "color": "rgb(85, 207, 85)" });
        $(this).fadeOut(3000);
        var id = $("[name=\"id\"").val();
        $(".d_upvotes").html(parseInt($(".d_upvotes").html()) + 1);
        $.ajax({
            type: 'post',
            url: '/Request/Upvote',
            data: { id: id },
        });
        e.preventDefault();
    });     
});