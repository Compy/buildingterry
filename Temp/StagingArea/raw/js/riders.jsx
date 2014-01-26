var gMap = 0;
var latitude = 0;
var longitude = 0;
var lastLat = 0;
var lastLong = 0;
var directions = 0;

var destMarker = 0;
$(document).ready(function() { 
    $("#declareEmergency").click(function() {
        showAndroidToast("Emergency declared!!");
        Android.postEmergency();
        return false;
    });
    
    $("#btnExit").click(function() {
    	Android.exitApp();
    	return false;
    });
    //navigator.geolocation.getCurrentPosition(showPosition);
    
    //latitude = Android.getLatitude();
    //longitude = Android.getLongitude();
    latitude = 33.993188;
    longitude = -84.069099;
    showMap(latitude, longitude);
    
    $( '#startride' ).bind( 'pageshow',function(event, ui){
        var currCenter = gMap.getCenter();
        google.maps.event.trigger(gMap, 'resize');
        gMap.setCenter(currCenter);
      });
      
    setInterval(function(){updateLocation()},3600);
    
    google.maps.event.addListener(gMap, 'click', function(event) {
	  placeMarker(event.latLng);
	});
});

function showAndroidToast(toast) {
    Android.showToast(toast);
}

function displayStatus()
{
	var status = "";
	// If there is no user status specified in the dropdown, use the custom status
	if ($("#userStatus").val() == "")
	{
		status = $("#inputStat").val();
	}
	else
	{
		status = $("#userStatus").val();
	}
	$("#statusDisplay").html(status);
}

function placeMarker(location) {
  if ( destMarker != 0 ) {
    destMarker.setPosition(location);
  } else {
    destMarker = new google.maps.Marker({
      position: location,
      map: gMap
    });
  }
}

function updateLocation()
{
	latitude = Android.getLatitude();
    longitude = Android.getLongitude();
    
    if (latitude == lastLat && longitude == lastLong) return;
    lastLat = latitude;
    lastLong = longitude;
    
    //showAndroidToast("Got location " + latitude + " " + longitude);
    
    showMap(latitude, longitude);
}

function showPosition(position)
{
	showAndroidToast("Got it.");
    alert("Position: " + position.coords.latitude + ", " + position.coords.longitude);
    
    showMap(position.coords.latitude, position.coords.longitude);
}

function showMap(lat,lng)
{
    var mapcanvas = document.createElement('div');
    mapcanvas.id = 'mapcanvas';
    mapcanvas.style.height = '400px';
    mapcanvas.style.width = '400px';

    document.getElementById("mapDisplay").appendChild(mapcanvas);

    var latlng = new google.maps.LatLng(lat,lng);
    var myOptions = {
      zoom: 15,
      center: latlng,
      mapTypeControl: false,
      navigationControlOptions: {style: google.maps.NavigationControlStyle.SMALL},
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    
    if (gMap == 0)
    {
    	var map = new google.maps.Map(document.getElementById("mapcanvas"), myOptions);

    	var marker = new google.maps.Marker({
        	position: latlng, 
        	map: map, 
        	title:"You are here!"
    	});
    	gMap = map;
    }
    
    gMap.setCenter(latlng);
    
    google.maps.event.trigger(gMap, 'resize');
}

////////////////////////////////////////////////////////////////////////////////

//function returns weather data in form of JSON from wunderground.com
function getWeather(Data){
	
	//store pertinent weather data as variables for html display
	var currentTemp = parseInt(Data.current_observation.feelslike_f);
	var location = Data.current_observation.display_location.full;
	var icon = Data.current_observation.estimated.icon;
	var forecast = Data.forecast.txt_forecast.forecastday[0].fcttext;
		  
	 //create html for display 
	 
     var html = '<h2>'+currentTemp+'&deg;F</h2>';
     html += '<h3>'+ forecast+'</h3>';
     html += '<div id="attribution"><p>Weather data courtesy of: </p><a href="http://www.wunderground.com/"><img src="weatherlogo.png" style="width:70%" /></a></div>';
       
       
       
      
     // 
     document.getElementById("cityWeather").innerHTML = location + " Weather";
      document.getElementById("weather2").innerHTML = html;
	
}//end of getWeather function


/////////////////////////////////////////////////////
