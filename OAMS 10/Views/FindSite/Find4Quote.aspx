﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.FindSite>" %>
<%@ Import Namespace="OAMS.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Find
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Find Site</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.HiddenFor(r => r.CampaignID) %>
    <table width="100%" id="tblAll">
        <tr>
            <td style="width: 230px;" valign="top" id="SearchPane">
                Geo1:
                <br />
                <%: Html.EditorFor(model => model.Geo1FullName, "AutoCompleteGeo", new { level = 1 }) %>
                <br />
                <br />
                Geo2: <a id="A1" href="javascript:checkAll(document.forms[0].Geo2List, true);ShowAll(document.forms[0].Geo2List,'Geo2ListMore');">
                    All</a>&nbsp;/&nbsp;<a id="A2" href="javascript:checkAll(document.forms[0].Geo2List, false);">Clear</a>
                <div id="geo2List">
                </div>
                <a id="Geo2ListMore" href="javascript:ShowAll(document.forms[0].Geo2List,'Geo2ListMore');"
                    style="display: none;">More...</a>
                <br />
                <br />
                Style List: <a id="lnkCheckAllStyle" href="javascript:checkAll(document.forms[0].StyleList, true);">
                    All</a>&nbsp;/&nbsp;<a id="lnkUnCheckAllStyle" href="javascript:checkAll(document.forms[0].StyleList, false);">Clear</a>
                <br />
                <%
           foreach (var category in (new OAMS.Models.CodeMasterRepository()).Get(CodeMasterType.Type))
           {
                %>
                <div>
                    <input type="checkbox" name="StyleList" value="<%= category.Code %>" checked="checked"
                        id='StyleItem<%= category.ID %>' />
                    <label for='StyleItem<%= category.ID %>'>
                        <%: category.Note %>
                    </label>
                    <% 
string profileImageUrl = "";
if (category.Code == "WMB")
{
    profileImageUrl = Url.Content("~/Content/Image/wallmountedbannee.png");
}
else if (category.Code == "BRL")
{
    profileImageUrl = Url.Content("~/Content/Image/britelite.png");
}
else if (category.Code == "BSH")
{
    profileImageUrl = Url.Content("~/Content/Image/busshelter.png");
}
else if (category.Code == "CMR")
{
    profileImageUrl = Url.Content("~/Content/Image/covermarket.png");
}
else if (category.Code == "ELV")
{
    profileImageUrl = Url.Content("~/Content/Image/elevator.png");
}
else if (category.Code == "ITK")
{
    profileImageUrl = Url.Content("~/Content/Image/itkiosk.png");
}
else if (category.Code == "Billboard")
{
    profileImageUrl = Url.Content("~/Content/Image/billboard.png");
}
else if (category.Code == "BillboardPole")
{
    profileImageUrl = Url.Content("~/Content/Image/billboardpole.png");
}
else if (category.Code == "Other")
{
    profileImageUrl = Url.Content("~/Content/Image/other.png");
}
                    %>
                    <img alt="" border="0" src="<%= profileImageUrl %>" width="20" id="ImgStyleItem<%= category.ID%>" /></div>
                <%
           }
                %>
                <a id="StyleListMore" href="javascript:ShowAllStyle(document.forms[0].StyleList,'StyleListMore');"
                    style="display: none;">More...</a>
                <br />
                <br />
                <%: Html.LabelFor(r => r.Format) %>
                <br />
                <%: Html.CodeMasterDropDownListFor(r => r.Format, false)%>
                <br />
                <%: Html.LabelFor(r => r.RoadType2) %>
                <br />
                <%: Html.CodeMasterDropDownListFor(r => r.RoadType2, false)%>
                <br />
                <%--<%: Html.LabelFor(r => r.InstallationPosition2) %>--%>
                Angle to Road
                <br />
                <%: Html.CodeMasterDropDownListFor(r => r.InstallationPosition2, false)%>
                <br />
                Viewing Distance
                <br />
                <%: Html.CodeMasterDropDownListFor(r => r.ViewingDistance, false)%>
                <br />
                <%--<%: Html.EditorFor(model => model.IsWithinCircle) %>--%>
                <%--<input type="checkbox" name="IsWithinCircle" id="IsWithinCircle" onclick="Click_WithinCircle(this);" />--%>
                <input class="check-box" id="IsWithinCircle" name="IsWithinCircle" type="checkbox"
                    value="true" onclick="Click_WithinCircle(this);" />
                Within
                <input type="text" style="width: 50px;" name="Distance" id="Distance" disabled="disabled"
                    onblur="updateDistanceFromTxt(this);" />
                <%: Html.HiddenFor(r => r.Lat) %>
                <%: Html.HiddenFor(r => r.Long) %>
                <br />
                <div id="divMoreProduct" data-url="'<%= Url.Content("~/Listing/ListProduct")%>'" data-name="'ProductIDList'">
                    <br />
                    Current Product<br />
                </div>
                <br />
                <br />
                <div id="divContractor" data-url="'<%= Url.Content("~/Listing/ListContractor")%>'"
                    data-name="'ContractorList'">
                    <br />
                    Contractor<br />
                </div>
                <br />
                <br />
                <div id="divClient" data-url="'<%= Url.Content("~/Listing/ListClient")%>'" data-name="'ClientList'">
                    <br />
                    Client<br />
                </div>
                <br />
                <br />
                <div id="divCategory" data-url="'<%= Url.Content("~/Listing/ListCats")%>'" data-name="'CatList'">
                    <br />
                    Category<br />
                </div>
                <br />
            </td>
            <td valign="top">
                <input id="btnFind" type="button" onclick="search(this);" value="Find" />
                <a href="javascript:toggleSearchPane();">Show/Hide Search Criteria</a>
                <table width="100%">
                    <tr>
                        <td>
                            <div id="map" style="width: 100%; height: 800px;">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 100%;" style="vertical-align: top;">
                <table id="tblResult" class="display">
                    <thead>
                        <tr>
                            <th>
                                ID
                            </th>
                            <th>
                                Site ID
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                Type
                            </th>
                            <th>
                                Format
                            </th>
                            <th>
                                Address Line 1
                            </th>
                            <th>
                                Address Line 2
                            </th>
                            <th>
                                Size
                            </th>
                            <th>
                                Current Product
                            </th>
                            <th>
                                Current Client
                            </th>
                            <th>
                                Contractor
                            </th>
                            <th>
                                Total
                            </th>
                            <th>
                                Edit
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <% } %>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#divMoreProduct").manyTxt();
            $("#divContractor").manyTxt();
            $("#divClient").manyTxt();
            $("#divCategory").manyTxt();

        });
    </script>
    <script type="text/javascript">

        $('#Geo1FullName').val('<%= OAMS.Models.AppSetting.DefaultGeo1Name %>');
        showGeo2('<%= OAMS.Models.AppSetting.DefaultGeo1Name %>');

        function updateDistanceFromTxt(txt) {

            //alert(txt.value);
            //distanceWidget.pRadiusWidget.set('distance', txt.value);
            distanceWidget.set('distance', txt.value);
            distanceWidget.pRadiusWidget.setSizerChangeFromTxt();
            map.fitBounds(distanceWidget.get('bounds'));

        }

        function Click_WithinCircle(chk) {
            if (chk.checked) {
                $("#Distance").removeAttr('disabled');
                distanceWidget.setVisible(true);
            }
            else {
                $("#Distance").attr('disabled', 'disabled');
                distanceWidget.setVisible(false);
            }
        }

        var oTable;

        function showGeo2(str) {
            //alert(str);
            //var v = $("#geo2List").text();

            $("#Geo2ListMore").hide();

            var div1 = $("#geo2List");
            div1.empty();



            $.ajax({
                url: '<%= Url.Content("~/Listing/ListGeo2") %>', type: "POST", dataType: "json",
                data: { parentFullName: str },
                success: function (data) {

                    var index = 0;
                    $.map(data, function (item) {

                        index++;
                        var divInner = document.createElement('div');
                        var chk = document.createElement('input');
                        chk.type = 'checkbox';
                        chk.name = 'Geo2List';
                        chk.value = item.FullName;
                        chk.id = 'Geo2List' + index;
                        divInner.appendChild(chk);
                        var lbl = document.createElement('label');
                        lbl.innerHTML = item.FullName;
                        lbl.setAttribute('for', 'Geo2List' + index)
                        divInner.appendChild(lbl);
                        div1.append(divInner);
                        //div1.append('<br />');
                    });
                }

            })
        }

        function HideUncheck(lst, btnHideID) {

            var count = 0;

            for (i = 0; i < lst.length; i++) {

                if (lst[i].checked) {

                }
                else {
                    count++;

                    lst[i].style.visibility = 'collapse';
                    lst[i].style.display = 'none';
                    $('label[for=' + lst[i].id + ']').css({ display: "none", visibility: "collapse" });
                }
            }

            if (count == 0) {
                $("#" + btnHideID).hide();
            }
            else {

                $("#" + btnHideID).show();
            }
        }

        function ShowAll(lst, btnMoreID) {
            for (i = 0; i < lst.length; i++) {

                lst[i].style.visibility = 'visible';
                lst[i].style.display = '';
                $('label[for=' + lst[i].id + ']').css({ display: "", visibility: "visible" });
            }

            $("#" + btnMoreID).hide();
        }

        function HideUncheckStyle(lst, btnHideID) {

            var count = 0;

            for (i = 0; i < lst.length; i++) {

                if (lst[i].checked) {

                }
                else {
                    count++;

                    lst[i].style.visibility = 'collapse';
                    lst[i].style.display = 'none';
                    $('label[for=' + lst[i].id + ']').css({ display: "none", visibility: "collapse" });
                    $('#Img' + lst[i].id).css({ display: "none", visibility: "collapse" });
                }
            }

            if (count == 0) {
                $("#" + btnHideID).hide();
            }
            else {

                $("#" + btnHideID).show();
            }
        }

        function ShowAllStyle(lst, btnMoreID) {
            for (i = 0; i < lst.length; i++) {

                lst[i].style.visibility = 'visible';
                lst[i].style.display = '';
                $('label[for=' + lst[i].id + ']').css({ display: "", visibility: "visible" });
                $('#Img' + lst[i].id).css({ display: "", visibility: "visible" });
            }

            $("#" + btnMoreID).hide();
        }

        function getURLParameter(name) {
            return unescape((RegExp(name + '=' + '(.+?)(&|$)').exec(location.search) || [, null])[1]);
        }

        function addResults(json) {

            //var contractID = getURLParameter('ContractID');            
            var quoteID = getURLParameter('QuoteID');


            HideUncheck(document.forms[0].Geo2List, 'Geo2ListMore');
            HideUncheckStyle(document.forms[0].StyleList, 'StyleListMore');
            //  Create a new viewpoint bound
            var bounds = new google.maps.LatLngBounds();



            if (json.length) {
                var profileImageUrl;
                var addTemplate = '<%: Html.ActionLinkWithRoles<OAMS.Controllers.QuoteController>("Add to Quote", r => r.AddSite(0,0), null, new Dictionary<string,object> (){{"href","javascript:Add2Quote(linkAdd2Cam, quoteID, siteDetailID);"},{"id","aAdd2Cam"}},true) %>';
                for (var i = 0, site; site = json[i]; i++) {
                    if (site.CodeType == 'WMB') {
                        profileImageUrl = '<%= Url.Content("~/Content/Image/wallmountedbannee.png") %>';
                    }
                    else if (site.CodeType == 'BRL') {
                        profileImageUrl = '<%= Url.Content("~/Content/Image/britelite.png") %>';
                    }
                    else if (site.CodeType == 'BSH') {
                        profileImageUrl = '<%= Url.Content("~/Content/Image/busshelter.png") %>';
                    }
                    else if (site.CodeType == 'CMR') {
                        profileImageUrl = '<%= Url.Content("~/Content/Image/covermarket.png") %>';
                    }
                    else if (site.CodeType == 'ELV') {
                        profileImageUrl = '<%= Url.Content("~/Content/Image/elevator.png") %>';
                    }
                    else if (site.CodeType == 'ITK') {
                        profileImageUrl = '<%= Url.Content("~/Content/Image/itkiosk.png") %>';
                    }
                    else if (site.CodeType == 'Billboard') {
                        profileImageUrl = '<%= Url.Content("~/Content/Image/billboard.png") %>';
                    }
                    else if (site.CodeType == 'BillboardPole') {
                        profileImageUrl = '<%= Url.Content("~/Content/Image/billboardpole.png") %>';
                    }
                    else if (site.CodeType == 'Other') {
                        profileImageUrl = '<%= Url.Content("~/Content/Image/other.png") %>';
                    }

                    var image = new google.maps.MarkerImage(profileImageUrl
                    //                        ,new google.maps.Size(48, 48)
                    //                        ,new google.maps.Point(0, 0)
                    //                        ,new google.maps.Point(24, 24)
                    //                        ,new google.maps.Size(24, 24)
                              );

                    var pos = new google.maps.LatLng(site.Lat, site.Lng);



                    var marker = new google.maps.Marker({
                        map: map,
                        position: pos,
                        icon: image,
                        zIndex: 10
                    });


                    if (VietnamBounds.contains(marker.position)) {
                        bounds.extend(marker.position);
                    }

                    profileMarkers.push(marker);
                    var html = "";
                    html += "<table><tr><td>";
                    html += "ID: " + site.ID;
                    html += "<br />";
                    html += "Site Code: " + site.Code;
                    html += "<br />";
                    html += "Address: " + site.Address;
                    html += "<br />";
                    html += "Location: " + site.GeoFullName;
                    html += "<br />";
                    html += "Type: " + site.Type;
                    html += "<br />";
                    html += "Orientation: " + site.Orientation;
                    html += "<br />";
                    html += "Size: " + site.Size;
                    html += "<br />";
                    html += "Lighting: " + site.Lighting;
                    html += "<br />";
                    html += "Contractor: " + site.Contractor;
                    html += "<br />";
                    html += "CurrentProduct: " + site.CurrentProduct;
                    html += "</td></tr>";
                    html += "<tr><td>";
                    if (site.AlbumID != '') {
                        html += "<br />";
                        html += '<embed type="application/x-shockwave-flash" src="http://picasaweb.google.com/s/c/bin/slideshow.swf" width="400" height="267" flashvars="host=picasaweb.google.com&hl=en_US&feat=flashalbum&RGB=0x000000&feed=http%3A%2F%2Fpicasaweb.google.com%2Fdata%2Ffeed%2Fapi%2Fuser%2F113917932111131696693%2Falbumid%2F';
                        html += site.AlbumID;
                        html += '%3Falt%3Drss%26kind%3Dphoto%26authkey%3D';
                        html += site.AuthID;
                        html += '%26hl%3Den_US" pluginspage="http://www.macromedia.com/go/getflashplayer"></embed>';
                    }

                    html += "</td></tr>";
                    html += "</table>";

                    bindInfoWindow(marker, map, infoWindow, html);
                    //bindInfoWindow(marker, map, infoWindow, 'asdsa');

                    //                    if (oTable != null) {
                    //                        oTable.fnDestroy();
                    //                    }

                    var tbl = $('#tblResult tbody');
                    tbl.innerHTML = '';
                    //html = [];

                    var rSel = document.createElement('tr');
                    tbl.append(rSel);

                    //ID
                    //                    var cSel = document.createElement('td');
                    //                    rSel.appendChild(cSel);
                    //                    var aSel = document.createElement('a');
                    //                    aSel.href = 'javascript:void(0);';
                    //                    aSel.innerHTML = site.ID;
                    //                    aSel.onclick = generateTriggerCallback(marker, 'click');
                    //                    cSel.appendChild(aSel);



                    var cStyle = document.createElement('td');
                    cStyle.innerHTML = site.ID;
                    rSel.appendChild(cStyle);

                    var cStyle101 = document.createElement('td');
                    cStyle101.innerHTML = site.SiteID;
                    rSel.appendChild(cStyle101);

                    var cStyle102 = document.createElement('td');
                    cStyle102.innerHTML = site.Name;
                    rSel.appendChild(cStyle102);

                    //Type
                    var cStyle1 = document.createElement('td');
                    cStyle1.innerHTML = site.Type;
                    rSel.appendChild(cStyle1);

                    //Format
                    var cStyle2 = document.createElement('td');
                    cStyle2.innerHTML = site.Format;
                    rSel.appendChild(cStyle2);

                    //AddressLine1
                    var cStyle3 = document.createElement('td');
                    cStyle3.innerHTML = site.AddressLine1;
                    rSel.appendChild(cStyle3);

                    //AddressLine2
                    var cStyle4 = document.createElement('td');
                    cStyle4.innerHTML = site.AddressLine2;
                    rSel.appendChild(cStyle4);

                    //Size
                    var cStyle5 = document.createElement('td');
                    cStyle5.innerHTML = site.Size;
                    rSel.appendChild(cStyle5);

                    //CurrentProduct
                    var cStyle6 = document.createElement('td');
                    cStyle6.innerHTML = site.CurrentProduct;
                    rSel.appendChild(cStyle6);

                    //CurrentClient
                    var cStyle7 = document.createElement('td');
                    cStyle7.innerHTML = site.CurrentClient;
                    rSel.appendChild(cStyle7);

                    //Contractor
                    var cStyle8 = document.createElement('td');
                    cStyle8.innerHTML = site.Contractor;
                    rSel.appendChild(cStyle8);

                    //Score
                    var cStyle9 = document.createElement('td');
                    cStyle9.innerHTML = site.Score;
                    rSel.appendChild(cStyle9);

                    //Edit
                    //                    var cEdit = document.createElement('td');
                    //                    cEdit.innerHTML = '<%: Html.ActionLink("Edit", "Edit", "Site", new {id=-1},null) %>';
                    //                    cEdit.innerHTML = cEdit.innerHTML.replace('-1', site.ID);
                    //                    rSel.appendChild(cEdit);

                    //Add2Campaign
                    var cStyle10 = document.createElement('td');
                    rSel.appendChild(cStyle10);

                    var aAdd2Cam = document.createElement('a');
                    //var siteID = site.ID;
                    var siteDetailID = site.ID;
                    aAdd2Cam.href = 'javascript:void(0);';

                    if (site.Added) {
                        aAdd2Cam.innerHTML = 'Added';
                        aAdd2Cam.onclick = null;
                    }
                    else {
                        if (addTemplate != null && addTemplate != '') {
                            aAdd2Cam.innerHTML = 'Add to';
                            aAdd2Cam.onclick = Add2Quote(aAdd2Cam, quoteID, siteDetailID);
                        }
                    }

                    cStyle10.appendChild(aAdd2Cam);
                }

                //oTable = $('#tblResult').dataTable();
                oTable = $('#tblResult').dataTable({ "sDom": 'C<"clear">lfrtip' });

            }
            else {

                var tbl = $('#tblResult tbody');
                tbl.innerHTML = '';

                var rSel = document.createElement('tr');
                tbl.append(rSel);

                var cStyle = document.createElement('td');
                cStyle.innerHTML = "No Site Found";
                rSel.appendChild(cStyle);
            }

            //  Fit these bounds to the map
            map.fitBounds(bounds);

            //$(results).html(html.join(''));
            //$('#results-wrapper').show();
        }

        function Add2Quote(link, quoteID, siteDetailID) {
            var url = '<%= Url.Content("~/Quote/AddSite?QuoteID=") %>' + quoteID + '&SiteDetailID=' + siteDetailID;
            return function () {
                $.ajax({
                    url: url, type: "POST", dataType: "json",
                    success: function (data) {
                        //alert(link.innerHTML);
                        link.innerHTML = 'Added';
                        link.href = "javascript:void(0);";
                        link.onclick = null;
                    }
                });
            };
        }

        function bindInfoWindowToA(marker, map, infoWindow, html, link) {
            google.maps.event.addListener(link, 'click', function () {
                //                infoWindow.setContent(html);
                //                infoWindow.open(map, marker);
                alert('aa');
            });
        }

        function bindInfoWindow(marker, map, infoWindow, html) {

            google.maps.event.addListener(marker, 'click', function () {
                //var s = html + "";

                infoWindow.setContent(html);
                //infoWindow.setContent('asd');
                infoWindow.open(map, marker);
            });
        }

        var distanceWidget;
        var map;
        var geocodeTimer;
        var profileMarkers = [];

        var infoWindow = new google.maps.InfoWindow;

        //var VietnamBounds = new google.maps.LatLngBounds(new google.maps.LatLng(6, 100), new google.maps.LatLng(24, 109));
        var VietnamBounds = new google.maps.LatLngBounds(new google.maps.LatLng('<%= OAMS.Models.AppSetting.MapBoundSWLat %>', '<%= OAMS.Models.AppSetting.MapBoundSWLng %>'),
                                                         new google.maps.LatLng('<%= OAMS.Models.AppSetting.MapBoundNELat %>', '<%= OAMS.Models.AppSetting.MapBoundNELng %>'));
        function init() {
            var mapDiv = document.getElementById('map');
            map = new google.maps.Map(mapDiv, {
                //center: new google.maps.LatLng(10.77250, 106.69808),
                center: new google.maps.LatLng('<%= OAMS.Models.AppSetting.FindMapCenterLat %>', '<%= OAMS.Models.AppSetting.FindMapCenterLng %>'),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            distanceWidget = new DistanceWidget({
                map: map,
                distance: 1, // Starting distance in km.
                maxDistance: 2500, // Twitter has a max distance of 2500km.
                color: '#000',
                activeColor: '#59b',
                sizerIcon: new google.maps.MarkerImage('<%= Url.Content("~/Content/Image/resize-off.png") %>'),
                activeSizerIcon: new google.maps.MarkerImage('<%= Url.Content("~/Content/Image/resize.png") %>')
            });

            google.maps.event.addListener(distanceWidget, 'distance_changed',
      updateDistance);

            google.maps.event.addListener(distanceWidget, 'position_changed',
      updatePosition);

            map.fitBounds(distanceWidget.get('bounds'));

            updateDistance();
            updatePosition();
            addActions();
            distanceWidget.setVisible(false);

            google.maps.event.addListener(map, 'dragstart',
            function () {
                infoWindow.close();
            });
        }



        function updatePosition() {
            if (geocodeTimer) {
                window.clearTimeout(geocodeTimer);
            }

            // Throttle the geo query so we don't hit the limit
            geocodeTimer = window.setTimeout(function () {
                reverseGeocodePosition();
            }, 200);
        }

        function AutoCenter(markers) {
            //  Create a new viewpoint bound
            var bounds = new google.maps.LatLngBounds();
            //  Go through each...
            $.each(markers, function (index, marker) {
                bounds.extend(marker.position);
            });
            //  Fit these bounds to the map
            map.fitBounds(bounds);
        }

        function reverseGeocodePosition() {
            var pos = distanceWidget.get('position');
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': pos }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[1]) {
                        $('#of').html('of ' + results[1].formatted_address);
                        return;
                    }
                }

                $('#of').html('of somewhere');
            });
        }

        function updateDistance() {
            var distance = distanceWidget.get('distance');


            //cast to number to function toFixed() working
            distance = distance * 1;
            //            $('#dist').html(distance.toFixed(2));
            $('#Distance').val(distance.toFixed(2));

        }





        function addActions() {




            var s = $('#s').submit(search);

            $('#close').click(function () {
                $('#cols').removeClass('has-cols');
                google.maps.event.trigger(map, 'resize');
                map.fitBounds(distanceWidget.get('bounds'));
                $('#results-wrapper').hide();

                return false;
            });
        }



        function search(e) {
            if (oTable != null) {
                oTable.fnDestroy();
                oTable = null;
            }
            var tdata = $("form").serialize();

            var quoteID = getURLParameter("QuoteID");

            $.ajax({
                url: '<%= Url.Content("~/FindSite/FindJson4Quote?QuoteID=") %>' + quoteID, type: "POST", dataType: "json",
                data: tdata,
                success: function (data) {
                    clearMarkers();
                    addResults(data);
                }
            })
        }

        function clearMarkers() {
            for (var i = 0, marker; marker = profileMarkers[i]; i++) {
                marker.setMap(null);
            }
            infoWindow.close();
            $("#tblResult tbody tr").remove();
        }

        function clickMarker(marker) {
            marker.click();
        }



        google.maps.event.addDomListener(window, 'load', init);

        function generateTriggerCallback(object, eventType) {
            //alert("as");
            return function () {
                google.maps.event.trigger(object, eventType);
            };
        }



        /**
        * A distance widget that will display a circle that can be resized and will
        * provide the radius in km.
        *
        * @param {Object} opt_options Options such as map, position etc.
        *
        * @constructor
        */
        function DistanceWidget(opt_options) {
            var options = opt_options || {};

            this.setValues(options);

            if (!this.get('position')) {
                this.set('position', map.getCenter());
            }



            // Add a marker to the page at the map center or specified position
            var marker = new google.maps.Marker({
                draggable: true,
                title: 'Move me!'
            });

            marker.bindTo('map', this);
            marker.bindTo('zIndex', this);
            marker.bindTo('position', this);
            marker.bindTo('icon', this);

            this.pCenterMarker = marker;

            // Create a new radius widget
            var radiusWidget = new RadiusWidget(options['distance'] || 50);

            // Bind the radius widget properties.
            radiusWidget.bindTo('center', this, 'position');
            radiusWidget.bindTo('map', this);
            radiusWidget.bindTo('zIndex', marker);
            radiusWidget.bindTo('maxDistance', this);
            radiusWidget.bindTo('minDistance', this);
            radiusWidget.bindTo('color', this);
            radiusWidget.bindTo('activeColor', this);
            radiusWidget.bindTo('sizerIcon', this);
            radiusWidget.bindTo('activeSizerIcon', this);

            this.pRadiusWidget = radiusWidget;

            // Bind to the radius widget distance property
            this.bindTo('distance', radiusWidget);
            // Bind to the radius widget bounds property
            this.bindTo('bounds', radiusWidget);

            var me = this;
            google.maps.event.addListener(marker, 'dblclick', function () {
                // When a user double clicks on the icon fit to the map to the bounds
                map.fitBounds(me.get('bounds'));
            });
        }


        DistanceWidget.prototype = new google.maps.MVCObject();

        DistanceWidget.prototype.setVisible = function (isVisible) {

            this.pCenterMarker.setVisible(isVisible);
            this.pRadiusWidget.setVisible(isVisible);

        };



        /**
        * A radius widget that add a circle to a map and centers on a marker.
        *
        * @param {number} opt_distance Optional starting distance.
        * @constructor
        */
        function RadiusWidget(opt_distance) {
            var circle = new google.maps.Circle({
                strokeWeight: 2
            });

            this.set('distance', opt_distance);
            this.set('active', false);
            this.bindTo('bounds', circle);

            circle.bindTo('center', this);
            circle.bindTo('zIndex', this);
            circle.bindTo('map', this);
            circle.bindTo('strokeColor', this);
            circle.bindTo('radius', this);

            this.pCircle = circle;

            this.addSizer_();
        }
        RadiusWidget.prototype = new google.maps.MVCObject();



        /**
        * Add the sizer marker to the map.
        *
        * @private
        */
        RadiusWidget.prototype.addSizer_ = function () {
            var sizer = new google.maps.Marker({
                draggable: true,
                title: 'Drag me!'
            });

            sizer.bindTo('zIndex', this);
            sizer.bindTo('map', this);
            sizer.bindTo('icon', this);
            sizer.bindTo('position', this, 'sizer_position');

            var me = this;
            google.maps.event.addListener(sizer, 'dragstart', function () {
                me.set('active', true);
            });

            google.maps.event.addListener(sizer, 'drag', function () {
                // Set the circle distance (radius)
                me.setDistance_();
            });

            google.maps.event.addListener(sizer, 'dragend', function () {
                me.set('active', false);
            });

            this.pSizer = sizer;
        };

        RadiusWidget.prototype.setVisible = function (isVisible) {

            //this.pCircle.setVisible(isVisible);

            if (isVisible) {
                this.pCircle.setMap(map);
            } else { this.pCircle.setMap(null); }
            this.pSizer.setVisible(isVisible);

        };

        /**
        * Update the radius when the distance has changed.
        */
        RadiusWidget.prototype.distance_changed = function () {
            this.set('radius', this.get('distance') * 1000);
        };



        /**
        * Update the radius when the min distance has changed.
        */
        RadiusWidget.prototype.minDistance_changed = function () {
            if (this.get('minDistance') &&
      this.get('distance') < this.get('minDistance')) {
                this.setDistance_();
            }
        };


        /**
        * Update the radius when the max distance has changed.
        */
        RadiusWidget.prototype.maxDistance_changed = function () {
            if (this.get('maxDistance') &&
      this.get('distance') > this.get('maxDistance')) {
                this.setDistance_();
            }
        };


        /**
        * Update the stroke color when the color is changed.
        */
        RadiusWidget.prototype.color_changed = function () {
            this.active_changed();
        };


        /**
        * Update the active stroke color when the color is changed.
        */
        RadiusWidget.prototype.activeColor_changed = function () {
            this.active_changed();
        };


        /**
        * Update the active stroke color when the color is changed.
        */
        RadiusWidget.prototype.sizerIcon_changed = function () {
            this.active_changed();
        };


        /**
        * Update the active stroke color when the color is changed.
        */
        RadiusWidget.prototype.activeSizerIcon_changed = function () {
            this.active_changed();
        };


        /**
        * Update the center of the circle and position the sizer back on the line.
        *
        * Position is bound to the DistanceWidget so this is expected to change when
        * the position of the distance widget is changed.
        */
        RadiusWidget.prototype.center_changed = function () {
            var sizerPos = this.get('sizer_position');
            var position;
            if (sizerPos) {
                position = this.getSnappedPosition_(sizerPos);
            } else {
                var bounds = this.get('bounds');
                if (bounds) {
                    var lng = bounds.getNorthEast().lng();
                    position = new google.maps.LatLng(this.get('center').lat(), lng);
                }
            }

            if (position) {
                this.set('sizer_position', position);
            }

            $('#Lat').val(this.get('center').lat());
            $('#Long').val(this.get('center').lng());


        };

        RadiusWidget.prototype.setSizerChangeFromTxt = function () {

            var position;

            var bounds = this.get('bounds');
            if (bounds) {
                var lng = bounds.getNorthEast().lng();
                position = new google.maps.LatLng(this.get('center').lat(), lng);
            }

            if (position) {
                this.set('sizer_position', position);
            }

        };

        /**
        * Update the center of the circle and position the sizer back on the line.
        */
        RadiusWidget.prototype.active_changed = function () {
            var strokeColor;
            var icon;

            if (this.get('active')) {
                if (this.get('activeColor')) {
                    strokeColor = this.get('activeColor');
                }

                if (this.get('activeSizerIcon')) {
                    icon = this.get('activeSizerIcon');
                }
            } else {
                strokeColor = this.get('color');

                icon = this.get('sizerIcon');
            }

            if (strokeColor) {
                this.set('strokeColor', strokeColor);
            }

            if (icon) {
                this.set('icon', icon);
            }
        };


        /**
        * Set the distance of the circle based on the position of the sizer.
        * @private
        */
        RadiusWidget.prototype.setDistance_ = function () {
            // As the sizer is being dragged, its position changes.  Because the
            // RadiusWidget's sizer_position is bound to the sizer's position, it will
            // change as well.
            var pos = this.get('sizer_position');
            var center = this.get('center');
            var distance = this.distanceBetweenPoints_(center, pos);

            if (this.get('maxDistance') && distance > this.get('maxDistance')) {
                distance = this.get('maxDistance');
            }

            if (this.get('minDistance') && distance < this.get('minDistance')) {
                distance = this.get('minDistance');
            }

            // Set the distance property for any objects that are bound to it
            this.set('distance', distance);

            var newPos = this.getSnappedPosition_(pos);
            this.set('sizer_position', newPos);
        };


        /**
        * Finds the closest left or right of the circle to the position.
        *
        * @param {google.maps.LatLng} pos The position to check against.
        * @return {google.maps.LatLng} The closest point to the circle.
        * @private.
        */
        RadiusWidget.prototype.getSnappedPosition_ = function (pos) {
            var bounds = this.get('bounds');
            var center = this.get('center');
            var left = new google.maps.LatLng(center.lat(),
      bounds.getSouthWest().lng());
            var right = new google.maps.LatLng(center.lat(),
      bounds.getNorthEast().lng());

            var leftDist = this.distanceBetweenPoints_(pos, left);
            var rightDist = this.distanceBetweenPoints_(pos, right);

            if (leftDist < rightDist) {
                return left;
            } else {
                return right;
            }
        };


        /**
        * Calculates the distance between two latlng points in km.
        * @see http://www.movable-type.co.uk/scripts/latlong.html
        *
        * @param {google.maps.LatLng} p1 The first lat lng point.
        * @param {google.maps.LatLng} p2 The second lat lng point.
        * @return {number} The distance between the two points in km.
        * @private
        */
        RadiusWidget.prototype.distanceBetweenPoints_ = function (p1, p2) {
            if (!p1 || !p2) {
                return 0;
            }

            var R = 6371; // Radius of the Earth in km
            var dLat = (p2.lat() - p1.lat()) * Math.PI / 180;
            var dLon = (p2.lng() - p1.lng()) * Math.PI / 180;
            var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
    Math.cos(p1.lat() * Math.PI / 180) * Math.cos(p2.lat() * Math.PI / 180) *
    Math.sin(dLon / 2) * Math.sin(dLon / 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var d = R * c;
            return d;
        };

        //        function checkAll_Org(lst) {
        //            if (lst[0].checked) {
        //                for (i = 1; i < lst.length; i++) {
        //                    lst[i].checked = false;
        //                    lst[i].disabled = true;
        //                }
        //            }
        //            else {
        //                for (i = 1; i < lst.length; i++) {
        //                    lst[i].disabled = false;
        //                }
        //            }
        //        }

        function checkAll(lst, checked) {
            for (i = 0; i < lst.length; i++) {
                lst[i].checked = checked;
            }
        }

        //        function unCheck(lst) {
        //            if (!this.checked) {
        //                lst[0].checked = false;
        //            }
        //        }

        function toggleSearchPane() {
            var opt = {};
            if ($('#SearchPane').is(":visible")) {
                //$('#tblAll').toggleColumns(1, opt);
                $('#SearchPane').toggle();
                $('#btnToggleSearchPane').val("Show Search Criteria");
            }
            else {
                //$('#tblAll').toggleColumns(1, opt);
                $('#SearchPane').toggle();
                $('#btnToggleSearchPane').val("Hide Search Criteria");
            }
            google.maps.event.trigger(map, 'resize');
        }
    </script>
</asp:Content>
