$(document).ready(function () {
    $('form').submit(function (e) {
        var text = $("#" + this.id + " #lineText").val();
        var id = $("#" + this.id + " #lineID").val();
        var timeStart = $("#" + this.id + " #lineStart").val();
        var timeStop = $("#" + this.id + " #lineStop").val();
        $.ajax({
            type: 'post',
            url: '/Subtitle/UpdateSubtitleLine',
            data: { id: id, text: text, timeStart: timeStart, timeStop: timeStop },
        });
        e.preventDefault();
    });
});