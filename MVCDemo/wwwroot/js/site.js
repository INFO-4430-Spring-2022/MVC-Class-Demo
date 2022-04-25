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
if (autocomplete !== null) {
    autocomplete.onkeyup = handleKeyUp;

    function handleKeyUp(evt) {
        evt = evt || window.event;
        var searchText = this.value;
        $.ajax({
            url: autoCompleteUrl
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
}

var outputter = document.getElementById("divOutput");
 if (outputter !== null ) outputter.onclick = autoSelects;

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

var login = document.getElementById("frmLogin");
//alert(login);
if (typeof login !== null) {
    // we have a login form.
    login.addEventListener("submit", function () {
        alert("form being submitted.");
    });
}

var themer = document.getElementById("ddlTheme");
if (typeof themer !== null) {
    themer.addEventListener("change", function () {
        var newStyle = this.value;
        var stylerID = this.getAttribute("data-style");
        if (typeof stylerID !== "undefined" && stylerID !== "") {
            setTheme(stylerID,newStyle);
        }
    });
}

function setTheme(styID,color) {
    var styler = document.getElementById(styID);
    var stylePath = styler.href;
    var refParts = stylePath.split("/");
    // change the style.
    refParts[refParts.length - 1] = "theme-" + color + ".css";
    styler.href = refParts.join("/");
    if (localStorage) {
        localStorage.setItem("theme", JSON.stringify({ themer: styID, color: color }));
    }
}

// check if storage is supported.
if (localStorage) {
    var themeSet = localStorage.getItem("theme");
    // check to see if theme is already set.
    if (themeSet !== null) {
        // preset theme exists
        var themeSettings = JSON.parse(themeSet);
        setTheme(themeSettings.themer, themeSettings.color);
    }
}