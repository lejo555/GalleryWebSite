$(document).ready(function () {
    playImages();
});

///פונקציה המפעילה את התמונות בשולי הדף
function playImages() {
    var arrOfTypesCount;
    var imgCounter = 1;
    var path;
    $.getJSON('/Home/GetPoaCount', function (data) {
        arrOfTypesCount = data.slice();
        path = BuildRandomPathWithRandomType(arrOfTypesCount);
        SetSrcToImage(imgCounter, path);
    });

    setInterval(function () {
        if (imgCounter < 4) {
            imgCounter++;
        }
        else {
            imgCounter = 1;
        }
        path = BuildRandomPathWithRandomType(arrOfTypesCount);
        SetSrcToImage(imgCounter, path)
    }, 15000);
}
function BuildRandomPathWithRandomType(arrOfTypesC) {
    var randType = Math.floor((Math.random() * (arrOfTypesC.length)) + 1);
    var randImgNum = Math.floor((Math.random() * (arrOfTypesC[randType - 1])) + 1);
    var randPath = '/Content/Images/';
    switch (randType) { // if another pt is added , this is the portion to be updated - another case
        case 1://sg
            randPath += 'sg/' + randImgNum + '.jpg';
            break;
        case 2://ms
            randPath += 'mos/' + randImgNum + '.jpg';
            break;
        case 3://fs
            randPath += 'fs/' + randImgNum + '.jpg';
            break;
        default:
            break;
    }
    return randPath;
}
function SetSrcToImage(imgC, src) {
    var imgSelector = "#img" + imgC;
    $(imgSelector).css('visibility', 'visible');
    $(imgSelector).attr('src', src);
    $(imgSelector).fadeTo(8000, 1).fadeTo(8000, 0);
}
