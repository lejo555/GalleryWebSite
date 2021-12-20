$(document).ready(function () {
    $("#galleryli")
        .mouseover(
            function () {
                $("#primary_nav_wrap").addClass('bm-nav');
            })
        .mouseout(
            function () {
                $("#primary_nav_wrap").removeClass('bm-nav');
            });

    var vbData = $("#vbData").val(); // holds general info about the page such as location symbol. structure can be redefined
    switch (vbData) {
        case "home":
            $("#locationSpan2").text("דף הבית");
            $("#homeli").addClass("current-menu-item");
            break;
        case "contact":
            $("#locationSpan2").text("צור קשר");
            $("#contactli").addClass("current-menu-item");
            break;
        case "mos":
            $("#locationSpan2").text("גלריית התמונות >> יצירות פסיפס");
            $("#galleryli").addClass("current-menu-item");
            break;
        case "sg":
            $("#locationSpan2").text("גלריית התמונות >> יצירות ויטראז");
            $("#galleryli").addClass("current-menu-item");
            break;
        case "fs":
            $("#locationSpan2").text("גלריית התמונות >> יצירות פיוזינג");
            $("#galleryli").addClass("current-menu-item");
            break;
        default:
            $("#locationSpan2").text("מחרוזת מיקום לא נמצאה");
            break;
    }

});

