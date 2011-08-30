
function SetAutoCompleteForContractor(nameOfTxtContractorName, nameOfTxtContractorID) {
    $("#" + nameOfTxtContractorName).autocomplete({
        select: function (event, ui) { $("#" + nameOfTxtContractorID).val(ui.item.id); },
        source: function (request, response) {
            $.ajax({
                //                url: '<%= Url.Content("~/Listing/ListContractor") %>', type: "POST", dataType: "json",
                url: '/Listing/ListContractor', type: "POST", dataType: "json",
                data: { searchText: request.term, maxResults: 10 },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Name, value: item.Name, id: item.ID }
                    }))
                }
            })
        }
    });
}


function AjaxEdit(ID, divID, editUrl) {
    $.ajax({
        url: editUrl, type: "GET",
        data: { id: ID },
        success: function (data) {

            $('#' + divID).empty().append(data);
        }

    })

}

function AjaxSave(divID, editUrl) {
    var inputData = $('#' + divID + ' *').serialize();
    $.ajax({
        url: editUrl, type: "POST",
        data: inputData,
        success: function (data) {

            $('#' + divID).empty().append(data);
        }

    })

}

function AjaxView(timelineID, divID, editUrl) {
    $.ajax({
        url: editUrl, type: "POST",
        data: { id: timelineID },
        success: function (data) {
            $('#' + divID).empty().append(data);
        }

    })

}

function AjaxDelete(ID, divID, editUrl) {
    if (confirm('Delete?')) {
        $.ajax({
            url: editUrl, type: "POST",
            data: { id: ID },
            success: function (data) {
                //$('#' + divID).empty().append(data);
                $('#' + divID).remove();
            }

        })
    }
}

function AjaxDelete2(ID, divID, editUrl, divID2) {
    if (confirm('Delete?')) {
        $.ajax({
            url: editUrl, type: "POST",
            data: { id: ID },
            success: function (data) {
                //$('#' + divID).empty().append(data);
                $('#' + divID).remove();
                $('#' + divID2).remove();
            }

        })
    }
}

//Begin Preview image on client drive
/***** CUSTOMIZE THESE VARIABLES *****/
// width to resize large images to
var maxWidth = 500;
// height to resize large images to
var maxHeight = 500;
// valid file types
var fileTypes = ["bmp", "gif", "png", "jpg", "jpeg"];
// the id of the preview image tag
var outImage = "previewField";
// what to display when the image is not valid
var defaultPic = "spacer.gif";
/***** DO NOT EDIT BELOW *****/
function preview(what, index, outImageName) {
    var source = what.value;
    var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
    for (var i = 0; i < fileTypes.length; i++) {
        if (fileTypes[i] == ext) {
            break;
        }
    }
    globalPic = new Image();
    if (i < fileTypes.length) {

        //Obtenemos los datos de la imagen de firefox
        try {
            globalPic.src = what.files[0].getAsDataURL();
        } catch (err) {
            globalPic.src = source;
        }
    } else {
        globalPic.src = defaultPic;
        alert("Invalid picture type. Must be:nn" + fileTypes.join(", "));
    }
    setTimeout('applyChanges(' + index + ',"' + outImageName + '")', 200);
}

var globalPic;
function applyChanges(index, outImageName) {
    var field = document.getElementById(outImageName + index);
    var x = parseInt(globalPic.width);
    var y = parseInt(globalPic.height);
    if (x > maxWidth) {
        y *= maxWidth / x;
        x = maxWidth;
    }
    if (y > maxHeight) {
        x *= maxHeight / y;
        y = maxHeight;
    }
    field.style.display = (x < 1 || y < 1) ? "none" : "";
    field.src = globalPic.src;
    field.width = x;
    field.height = y;
}

index = 0;
function addMoreFileInput(divId, nameOfFileInput, nameOfNoteList) {

    var divAddMore = $('#' + divId);

    var lbl = document.createElement('label');
    lbl.setAttribute('id', 'lblfile' + index);
    lbl.innerHTML = 'Filename:';

    divAddMore.append(lbl);

    var input = document.createElement('input');
    input.setAttribute('type', 'file');
    input.setAttribute('name', nameOfFileInput);
    input.setAttribute('size', '65');
    input.setAttribute('id', 'file' + index);
    input.setAttribute('onchange', 'preview(this, ' + index + ',"previewField")');

    divAddMore.append(input);

    var lnkDelete = document.createElement('a');
    lnkDelete.setAttribute('id', 'LnkDeleteFile' + index);
    lnkDelete.setAttribute('onclick', "$('#lblfile" + index + "').remove();$('#file" + index + "').remove();$('#previewField" + index + "').remove();$('#LnkDeleteFile" + index + "').remove();$('#note" + index + "').remove();");
    lnkDelete.innerHTML = 'X';
    lnkDelete.setAttribute('style', 'text-decoration:underline;cursor:pointer;');
    lnkDelete.setAttribute('title', 'Remove this Image');
    divAddMore.append(" ").append(lnkDelete);
    divAddMore.append('<br />');

    var inputNote = document.createElement('input');
    inputNote.setAttribute('type', 'text');
    inputNote.setAttribute('name', nameOfNoteList);
    inputNote.setAttribute('size', '65');
    inputNote.setAttribute('id', 'note' + index);

    divAddMore.append(inputNote);
    divAddMore.append('<br />');

    var previewImg = document.createElement('img');
    previewImg.setAttribute('id', 'previewField' + index + '');
    previewImg.setAttribute('alt', 'Graphic will preview here');

    divAddMore.append(previewImg);
    divAddMore.append('<br />');
    index = index + 1;
}

function addMoreFileInput2(divId, nameOfParam, siteDetailID) {

    var divAddMoreOut = $('#' + divId);

    var divAddMore = $(document.createElement('div'));

    divAddMoreOut.append(divAddMore);

    var lbl = document.createElement('label');
    lbl.setAttribute('id', 'lblSiteDetailFile' + index);
    lbl.innerHTML = 'Filename:';

    divAddMore.append(lbl);

    var hiddenIndex = document.createElement('input');
    hiddenIndex.setAttribute('type', 'hidden');
    hiddenIndex.setAttribute('name', nameOfParam + '.Index');
    hiddenIndex.setAttribute('value', index);
    divAddMore.append(hiddenIndex);

    var hiddenSiteDetailID = document.createElement('input');
    hiddenSiteDetailID.setAttribute('type', 'hidden');
    hiddenSiteDetailID.setAttribute('name', nameOfParam + '[' + index + '].SiteDetailID');
    hiddenSiteDetailID.setAttribute('value', siteDetailID);
    divAddMore.append(hiddenSiteDetailID);

    var input = document.createElement('input');
    input.setAttribute('type', 'file');
    input.setAttribute('name', nameOfParam + '[' + index + '].File');
    input.setAttribute('size', '65');
    input.setAttribute('onchange', 'preview(this, ' + index + ',"previewSiteDetailField")');
    divAddMore.append(input);

    var lnkDelete = document.createElement('a');

    lnkDelete.innerHTML = 'X';
    lnkDelete.setAttribute('style', 'text-decoration:underline;cursor:pointer;');
    lnkDelete.setAttribute('title', 'Remove this Image');
    divAddMore.append(" ").append(lnkDelete);
    divAddMore.append('<br />');

    var inputNote = document.createElement('input');
    inputNote.setAttribute('type', 'text');
    inputNote.setAttribute('name', nameOfParam + '[' + index + '].Note');
    inputNote.setAttribute('size', '65');


    divAddMore.append(inputNote);
    divAddMore.append('<br />');

    var previewImg = document.createElement('img');
    previewImg.setAttribute('id', 'previewSiteDetailField' + index + '');
    previewImg.setAttribute('alt', 'Graphic will preview here');

    divAddMore.append(previewImg);
    divAddMore.append('<br />');

    $(lnkDelete).click(function (e) {
        divAddMore.remove();
    });

    index = index + 1;
}

function deleteSitePhoto(btn, id) {
    if (confirm('Delete photo?')) {

        var input = document.createElement('input');
        input.setAttribute('type', 'text');
        input.setAttribute('name', 'DeletePhotoList');
        input.setAttribute('value', id);
        input.style.visibility = "hidden";

        $('#divDeletePhotoList').append(input);

        btn.style.visibility = "hidden";
        $('#photo' + id).hide();
    }
}

function deleteSitePhoto2(divDisplay, id, strDeletePhotoList, divDeletePhotoList) {
    if (confirm('Delete photo?')) {

        var input = document.createElement('input');
        input.setAttribute('type', 'text');
        input.setAttribute('name', strDeletePhotoList);
        input.setAttribute('value', id);
        input.style.visibility = "hidden";

        divDeletePhotoList.append(input);
        divDisplay.hide();
    }
}

function UpdateSitePhotoNote(url, sitePhotoID, note) {
    $.ajax({
        url: url, type: "POST", dataType: "json",
        data: { id: sitePhotoID, note: note }
    })
}

function UpdateSiteMonitoringPhotoNote(url, siteMonitoringPhotoID, note) {
    $.ajax({
        url: url, type: "POST", dataType: "json",
        data: { id: siteMonitoringPhotoID, note: note }
    })
}


;
(function ($) {

    $.widget("ui.manyTxt", {

        options: {
            url: "",
            name: ""
        },

        _create: function () {

            var base = this;
            var $el = base.element;

            base.options = $.metadata ? $.extend({}, base.options, $el.metadata()) : base.options;

            var $divMore = $(document.createElement('div'));
            base.element.append($divMore);

            var $btnAddMore = $(document.createElement('a'));
            $btnAddMore.attr('style', 'text-decoration:underline;cursor:pointer;');
            $btnAddMore.text('More...');
            $btnAddMore.click(function (e) {

                var input = document.createElement('input');
                var $input = $(input);

                var value = document.createElement('input');
                var $value = $(value);

                var del = document.createElement('a');
                var $del = $(del);

                $input.attr('type', 'text');
                $input.attr('class', 'text-box single-line');
                $input.blur(function (e) {
                    if ($input.val() == '') {
                        $value.val('');
                    }
                });
                $input.autocomplete({
                    select: function (event, ui) {
                        $value.val(ui.item.id);
                    },
                    source: function (request, response) {
                        $.ajax({
                            url: base.options.url, type: "POST", dataType: "json",
                            data: { searchText: request.term, maxResults: 10 },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return { label: item.Name, value: item.Name, id: item.ID }
                                }))
                            }
                        })
                    }
                });
                $divMore.append($input);
                //$el.append($input);
                //$btnAddMore.before($input)

                $value.attr('type', 'text');
                $value.attr('style', 'display: none;');
                $value.attr('name', base.options.name);
                //$el.append($value);
                //$btnAddMore.before($value)
                $divMore.append($value);

                $del.text('X');
                $del.attr('style', 'text-decoration:underline;cursor:pointer;');
                $del.click(function (e) {
                    $input.remove();
                    $value.remove();
                    $del.remove();
                });
                //$el.append(' ').append($del);
                //$btnAddMore.before($del)
                $divMore.append(' ').append($del);
            });

            base.element.append($btnAddMore);
        },

        destroy: function () {
            this.element.next().remove();
        },

        _setOption: function (option, value) {
            $.Widget.prototype._setOption.apply(this, arguments);
        }
    });

})(jQuery);

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});

function getURLParameter(name) {
    return unescape((RegExp(name + '=' + '(.+?)(&|$)').exec(location.search) || [, null])[1]);
}