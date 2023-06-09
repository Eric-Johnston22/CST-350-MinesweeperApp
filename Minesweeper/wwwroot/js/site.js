// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    
    onWelcome("/Game/Welcome");


    $(document).on("click", ".delete-button", function (event) {
        event.preventDefault();
        var gameNumber = $(this).val();
        console.log("Delete button clicked for game #:"+gameNumber);
    });

    $(document).on("click", ".load-button", function (event) {
        event.preventDefault();
        var gameNumber = $(this).val();
        //console.log("Load button clicked for game #:" + gameNumber);

        loadGame(gameNumber);
    });


    $("#SaveGameButton").on("click", (e) => {
        e.preventDefault();
        onSave();
    })

    $("#ShowSavedGamesButton").on("click", (e) => {
        e.preventDefault();
        onLoadGames();
    })

    $(document).bind("contextmenu", function (e) {
        e.preventDefault();
    });

    $(document).on("mousedown", ".flex-grid button", function (event) {
        switch (event.which) {
            case 1:
                var bn = $(this).val();
                var flagged = $(this).hasClass("flagged");
                if (!flagged) {
                    processRequest(bn, "/Game/LeftButtonClick");
                } else {
                    $(".flex-grid button").on("click", function (e) {
                        e.preventDefault();
                    });
                }

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

function onWelcome(handlerUrl) {
    $.ajax({
        method: 'GET',
        url: handlerUrl,
        success: function (data) {
            $("#welcome").html(data);
        }
    });
}

function onSave() {
    $.ajax({
        method: 'GET',
        url: "/Game/SaveGame",
        success: function (data) {
            $("#welcome").html("Your game has been saved in a separate save document. Success: "+data);
        }
    });
}
function onLoadGames() {
    $.ajax({
        method: 'GET',
        url: "/Game/ShowSavedGames",
        success: function (data) {
            $("#welcome").html(data);
            $(".flex-grid").html("");
        }
    });
}

function loadGame(gameNumber) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: "/Game/LoadGame",
        data: { "gameNumber": gameNumber },
        success: function (data) {
            $("#welcome").html("Welcome to loaded game #" + gameNumber);
            $(".flex-grid").html(data);
        }
    });
}