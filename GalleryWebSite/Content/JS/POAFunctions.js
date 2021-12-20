//-----------------------------------------//
//Scripts Of StainGlassPOA.cshtml && Mosaics.cshtml
//-----------------------------------------//
$(document).ready(function () {
    $(".spanShow").click(function () {
        var vbData = $("#vbData").val(); // holds general info about the page such as location symbol. structure can be redefined
        var spanChoise = $(this).data("category");
        if ($(this).attr('class') == 'currentCat')
            return false;
        $(".currentCat").each(function () { $(this).removeClass().addClass("spanShow"); });
        $(this).removeClass().addClass("currentCat");

        var ptObject = TranslateCatToPt(vbData, spanChoise);

        $.ajax({
            url: '/POA/GetAllByType',
            contentType: 'application/html; charset=utf-8',
            data: { poaType1: ptObject.pt1, poaType2: ptObject.pt2 },
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                $("#imgDiv").html("").slideUp('slow');
                $("#POADiv").height("auto");
                // 'result' is partial view contains model object and runs _ListOfPOA.cshtml
                $("#imgDiv").html(result).slideDown("slow");
            }
        });
    });
    $(".spanShow").first().trigger("click");
});

//-----------------------------------------//
//-- Scripts Of ListOfPOA.cshtml ----------
//-----------------------------------------//
function ImgDivJsFunc(link, extraParam) {
    switch (link) {
        case 'misc':
            BuildMiscDiv(extraParam);
            break;
        case 'perspectives':
            BuildPersDiv(extraParam);
            break;
        case 'resize':
            BuildResizeDiv(extraParam);
            break;
    }
}
function BuildResizeDiv(params) {
    var t = params.split(";")[1];
    if (screen.width <= 800) {
        t = 'הגדלת תמונה זמינה רק בתצוגת מחשב';
        $("#resizeImg").css('display', 'none');
    }
    else {
        $("#resizeImg").attr('src', params.split(";")[0]);
    }

    $("#resizeDiv").dialog({
        modal: true,
        draggable: false,
        resizable: false,
        show: { effect: 'explode', duration: 500 },
        hide: { effect: 'fade', duration: 500 },
        title: t

    });
}
function BuildPersDiv(params) {
    var url = '/POA/GetAllPersByCN';
    var CatN = params.split(";")[0];
    $.getJSON(url, { CN: CatN }, function (data) {
        $("#perspectivesDiv").html("");
        $("#perspectivesDiv").append('<img id="mainPersImage"/>');
        var numOfTrioDivs = Math.floor(data.length / 3) + 1;
        $("#mainPersImage").attr('src', data[0].PerspectivePath.split('~').join(''));
        for (var i = 0; i < numOfTrioDivs; i++) {
            var newDivElementId = 'trioImageCollection' + i;
            var newDivHtmlElement = '<div id=' + '"' + newDivElementId + '" ' + 'class="trioImageCollection"></div>';
            $("#perspectivesDiv").append(newDivHtmlElement);
            var stopIteration;
            if (data.length % 3 > 0 && i == numOfTrioDivs - 1)
                stopIteration = (i * 3) + data.length % 3;
            else
                stopIteration = (i * 3) + 3;
            for (var j = i * 3; j < stopIteration; j++) {
                var newImgHtmlElement = '<img class="imgStyle" src="' + data[j].PerspectivePath.split('~').join('') + '" />';
                $("#" + newDivElementId).append(newImgHtmlElement);
            }
        }

    });

    $('#perspectivesDiv').off('click')
        .on('click', // attaching event handler to dynamically created items
            '.imgStyle',
            function () {
                var imageURL = $(this).attr('src');
                if (imageURL != $('#mainPersImage').attr('src')) {
                    $('#mainPersImage').fadeOut(1000, function () {
                        $(this).attr('src', imageURL);
                    }).fadeIn(1000);
                }
            });

    $("#perspectivesDiv").dialog({
        modal: true,
        draggable: false,
        resizable: false,
        show: { effect: 'explode', duration: 500 },
        hide: { effect: 'fade', duration: 500 },
        height: "auto",
        width: "auto",
        title: params.split(";")[1]
    });
}
function BuildMiscDiv(CatN) {
    var url = '/POA/GetPOAByCN';
    $.getJSON(url, { CN: CatN }, function (data) {
        $(".infoSpan").html("");
        $("#catNum").text(data.CN);
        if (!data.Description) {
            $("#purchaseLink").removeAttr('href');
            $("#purchaseLink").addClass('noPurchaseLink');
            $("#purchaseLink").text(' לצורך רכישה, יש למלא את פרטיך בטופס צור קשר.');
        }
        else {
            if (0 === data.Description.length) {
                $("#purchaseLink").removeAttr('href');
                $("#purchaseLink").addClass('noPurchaseLink');
                $("#purchaseLink").text(' לצורך רכישה, יש למלא את פרטיך בטופס צור קשר.');
            }
            else {
                $("#purchaseLink").text('לרכישה דרך החנות לחץ כאן');
                $("#purchaseLink").attr('href', data.Description);
                $("#purchaseLink").attr('target', '_blank');
                $("#purchaseLink").removeClass('noPurchaseLink');
            }
        }


        //$("#size").text(data.Size);
        //$("#desc").text(data.Description);
        $("#type").text(TranslatePT(data.PT));
        $("#miscImg").attr('src', data.POAPath.split('~').join(''));


        $("#miscDiv").dialog({
            modal: true,
            draggable: false,
            resizable: false,
            show: { effect: 'explode', duration: 500 },
            hide: { effect: 'fade', duration: 500 },
            title: data.Name
        });

    });
}
//----------------------------------------------------------//
//-- Converting Categories symbols to descriptive strings --//
//----------------------------------------------------------//
//ביאור סימולי קטגוריות מעודכן נמצא בקובץ אקסל
//CategoriesSymbols.xlsx
function TranslatePT(pt) {
    switch (pt) {
        case 21:
            return CNT_SGR;
            break;
        case 11:
            return CNT_STJ;
            break;
        case 22:
            return CNT_MR;
            break;
        case 12:
            return CNT_MJ;
            break;
        case 23:
            return CNT_FSR;
            break;
        case 13:
            return CNT_FSJ;
            break;
        default:
            return "";
            break;
    }
}
//---------------------------------------//
//-- Converting Categories to pt codes --//
//---------------------------------------//
function TranslateCatToPt(cat, subCat) {
    var ptObj = { 'pt1': 0, 'pt2': 0 };
    switch (cat) {
        case "mos": // mosaics
            switch (subCat) {
                case "all":
                    ptObj.pt1 = 22;
                    ptObj.pt2 = 12;
                    break;
                case "Judaica":
                    ptObj.pt1 = 12;
                    break;
                case "NJudaica":
                    ptObj.pt1 = 22;
                    break;
                default:
                    break;
            }
            break;

        case "sg": // stain glass
            switch (subCat) {
                case "all":
                    ptObj.pt1 = 21;
                    ptObj.pt2 = 11;
                    break;
                case "Judaica":
                    ptObj.pt1 = 11;
                    break;
                case "NJudaica":
                    ptObj.pt1 = 21;
                    break;
                default:
                    break;
            }
            break;
        case "fs": //fusing
            switch (subCat) {
                case "all":
                    ptObj.pt1 = 23;
                    ptObj.pt2 = 13;
                    break;
                case "Judaica":
                    ptObj.pt1 = 13;
                    break;
                case "NJudaica":
                    ptObj.pt1 = 23;
                    break;
                default:
                    break;
            }
            break;
        default:
            break;
    }
    return ptObj;
}
