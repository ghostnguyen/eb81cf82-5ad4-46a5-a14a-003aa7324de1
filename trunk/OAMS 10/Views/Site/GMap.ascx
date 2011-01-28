<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.Site>" %>
<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script>
<div id="map_canvas" style="width: 500px; height: 500px;">
</div>
<script type="text/javascript">
    
    initialize();

    function initialize() {
        var latlng = new google.maps.LatLng(<%= Model.Lat %>, <%= Model.Lng %>);
        var myOptions = {
            zoom: 15,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        var infoWindow = new google.maps.InfoWindow;

        var marker = new google.maps.Marker({position: latlng, map: map});
        var markers = 
        
        $.ajax({
            url: "/Site/JsonList", type: "POST", dataType: "json",
            success: function (data) {
                    $.map(data, function (item) {
                       var latlng = new google.maps.LatLng(item.Latitude, item.Longitude);
                       var marker = new google.maps.Marker({position: latlng, map: map,title:item.Code});
                       bindInfoWindow(marker, map, infoWindow, item.Note);
                    })
                }
            })


            
    }

     function bindInfoWindow(marker, map, infoWindow, html) {
      google.maps.event.addListener(marker, 'click', function() {
        infoWindow.setContent(html);
        infoWindow.open(map, marker);
      });
    }


    //label: item.Note, value: item.Code, id: item.ID
</script>
