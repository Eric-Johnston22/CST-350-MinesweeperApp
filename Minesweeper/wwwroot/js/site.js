// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(document).bind("contextmenu", function (e) {
        e.preventDefault();
    });

    $(document).on("mousedown",".flex-grid button", function (event) {
        switch (event.which) {
            case 1:
                var bn = $(this).val();
                processRequest(bn, "/Game/LeftButtonClick");
                break;
            case 2:
                alert("Middle");
                break;
            case 3:
                var bn = $(this).val();
                processRequest(bn, "/Game/RightButtonClick");
                break;
            default:
                alert("I am not sure what you clicked");
        }
    });

});

function processRequest(buttonNumber, handlerUrl) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: handlerUrl,
        data: { "buttonNumber": buttonNumber },
        success: function (data) {
            if (handlerUrl == "/Game/LeftButtonClick") {
                $(".flex-grid").html(data);
            } else {
                var stringNums = buttonNumber.split(",");
                $("#" + stringNums[0] + "\\," + stringNums[1]).html(data);
            }
        }
    });
}