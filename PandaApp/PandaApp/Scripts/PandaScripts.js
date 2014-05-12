$(document).ready(function () {
    $('form').submit(function (e) {
        var text = $("#" + this.id + " #lineText").val();
        var id = $("#" + this.id + " #lineID").val();
        $.ajax({
            type: 'post',
            url: '/Subtitle/UpdateSubtitleLine',
            data: { id: id, text: text },
        });
        e.preventDefault();
    });
});