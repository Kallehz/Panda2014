$(document).ready(function () {
    $('form').submit(function (e) {
        var selectorPrefix = "#" + this.id;
        var text = $(selectorPrefix + " #lineText").val();
        var id = $(selectorPrefix + " #lineID").val();
        var timeStart = $(selectorPrefix + " #lineStart").val();
        var timeStop = $(selectorPrefix + " #lineStop").val();
        if (text == "" || timeStart =="" || timeStop =="") {
            $(selectorPrefix + " div[id=\"submit_div\"] #edit_msg").html("None of the fields can be empy");
            $(selectorPrefix + " div[id=\"submit_div\"] #edit_msg").fadeIn(600);
            $(selectorPrefix + " div[id=\"submit_div\"] #edit_msg").css({ display: "inline-table", color: "#eb6864" });
        }
        else {
            $.ajax({
                type: 'post',
                url: '/Subtitle/UpdateSubtitleLine',
                data: { id: id, text: text, timeStart: timeStart, timeStop: timeStop },
                success: function () {
                    var selector = "input[value=\"" + id + "\"] ~ div[id=\"submit_div\"] #edit_msg";
                    $(selector).css({ display: "none" });
                    $(selector).html("Your changes have been saved");
                    $(selector).fadeIn(600);
                    $(selector).css({ display: "inline-table", color: "lightgreen" });
                    $(selector).fadeOut(2000);
                }
            });
        }
        e.preventDefault();
    });
});