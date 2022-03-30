// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var cardLinks = document.getElementsByClassName("card-link");

for (var cL of cardLinks) {
    cL.onclick = handleCardClick;
}

function handleCardClick() {
    $.ajax({
        url: "Card"
        , method: "get"
        , success: function (resp) {
            alert(resp);
        }
        , error: function () {
            alert("Oops");
        }
    });
}