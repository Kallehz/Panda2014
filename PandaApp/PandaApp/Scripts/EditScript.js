$(document).ready(function () {
    $('form').submit(function (e) {
        var text = $("#" + this.id + " #lineText").val();
        var id = $("#" + this.id + " #lineID").val();
        var timeStart = $("#" + this.id + " #lineStart").val();
        var timeStop = $("#" + this.id + " #lineStop").val();
        if (text == "") {
            $("#edit_msg").html("Text field must not be empty");
            $("#edit_msg").fadeIn(600);
            $("#edit_msg").css({ display: "inline-table", color: "#eb6864" });
        }
        else {
            $.ajax({
                type: 'post',
                url: '/Subtitle/UpdateSubtitleLine',
                data: { id: id, text: text, timeStart: timeStart, timeStop: timeStop },
                success: function () {
                    alert(("input[value=\"" + id + "\"]"));
                    $("input[value=\"" + id + "\"] ~ div[id=\"submit_div\"] #edit_msg").css({ display: "none" });
                    $("input[value=\"" + id + "\"] ~ div[id=\"submit_div\"] #edit_msg").html("Your changes have been saved");
                    $("input[value=\"" + id + "\"] ~ div[id=\"submit_div\"] #edit_msg").fadeIn(600);
                    $("input[value=\"" + id + "\"] ~ div[id=\"submit_div\"] #edit_msg").css({ display: "inline-table", color: "lightgreen" });
                    $("input[value=\"" + id + "\"] ~ div[id=\"submit_div\"] #edit_msg").fadeOut(2000);
                }
            });
        }
        e.preventDefault();
    });
});