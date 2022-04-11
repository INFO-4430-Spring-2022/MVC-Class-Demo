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

var autocomplete = document.getElementById("txtTypeLookup");
autocomplete.onkeyup = handleKeyUp;

function handleKeyUp(evt) {
    evt = evt || window.event;
    var searchText = this.value;
    $.ajax({
        url: "../ThingType/List"
        , data: { search: searchText }
        , success: function (resp) {
            // alert(resp);
            var outer = document.getElementById("divOutput");
            outer.innerHTML = resp;
        }, error: function () {
            alert("Oops");
        }
    });

    //$.ajax({
    //    url: "../ThingType/Find"
    //    , data: {id: 1}
    //    , success: function (resp) {
    //        alert(resp);
    //        if (resp.success) {
    //            //alert("worked");
    //            var outer = document.getElementById("divOutput");
    //            outer.innerHTML = resp.data.firstName;
    //        } else {    
    //            //was an error
    //        }
    //    }, error: function () {
    //        alert("Oops");
    //    }
    //});
}

document.getElementById("divOutput").onclick = autoSelects;

function autoSelects(evt) {
    evt = evt || window.event;

    var eleClicked = evt.target || evt.srcElement;
    var idOfObject = eleClicked.getAttribute("data-id");
    //alert(eleClicked.innerText + " " + idOfObject + " was clicked");
    var actualValue = document.getElementById("TypeID");
    actualValue.value = idOfObject;
    //alert( eleClicked.tagName +  " was clicked");
    //alert( this.tagName +  " was clicked");

}