$(document).ready(function () {
    $('form').submit(function (e) {
        e.preventDefault();
        var selectorPrefix = "[name=\"" + this.name + "\"]";
        var text = $(selectorPrefix + " [name=\"lineText\"]").val();
        var id = $(selectorPrefix + " .lineID").val();
        var timeStart = $(selectorPrefix + " [name=\"lineStart\"]").val();
        var timeStop = $(selectorPrefix + " [name=\"lineStop\"]").val();
        if (text == "" || timeStart =="" || timeStop =="") {
            $(selectorPrefix + " div.submit_div .edit_msg").html("None of the fields can be empy");
            $(selectorPrefix + " div.submit_div .edit_msg").fadeIn(600);
            $(selectorPrefix + " div.submit_div .edit_msg").css({ display: "inline-table", color: "#eb6864" });
        }
        else {
            $.ajax({
                type: 'post',
                url: '/Subtitle/UpdateSubtitleLine',
                data: { id: id, text: text, timeStart: timeStart, timeStop: timeStop },
                success: function () {
                    var selector = "input[value=\"" + id + "\"] ~ div.submit_div .edit_msg";
                    $(selector).css({ display: "none" });
                    $(selector).html("Your changes have been saved");
                    $(selector).fadeIn(600);
                    $(selector).css({ display: "inline-table", color: "lightgreen" });
                    $(selector).fadeOut(2000);
                }
            });
        }
        /*
        */
    });
});