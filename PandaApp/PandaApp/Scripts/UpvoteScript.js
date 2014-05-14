$(document).ready(function () {
    $('form[id=\"upvote\"]').submit(function (e) {
        e.preventDefault();
        var id = $("input[name=\"id\"]").val();
        $.ajax({
            type: 'post',
            url: '/Request/Upvote',
            data: { id: id },
        });
        $(this).html("Upvoted!");
        $(this).css({ color: "lightgreen" })
        $(this).fadeOut(1000);
    });
});