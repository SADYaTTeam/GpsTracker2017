$.ajax({
    url: 'api/web/marker',
    type: "POST",
    data: JSON.stringify(getUser()),
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    success: function (data) {
        var trueData;
        drawMarkers(trueData);
    }
});
     
               