$(document).ready(function () {
    $('#Upvote').submit(function (e) {
        var s = $("#" + this.id + " input[name=s]").val();
        $.ajax({
            type: 'post',
            url: '/Request/Upvote',
            data: { s: s },
        });
        e.preventDefault();
    });
});