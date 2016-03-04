$(function () {
    $.ajax({
        url: "/Entidad/GetPositions",
        dataType: "json",
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        async: false,
        processData: false,
        cache: false,
        delay: 15,
        jsonReader: {
            data: "records",
        },
        success: function (data) {
            alert(data);  
            var series = new Array();
            for (var i in data) {
                var serie = new Array(data[i].Value, data[i].Item);
                series.push(serie);
            }
            Draw(series);
        },
    });
});
$(function Draw(series) {
    $("#graph").highcharts({
        chart: {
            type: 'spline'
        },
        title: {
            text: 'Velocidad'
        },
        xAxis: {
            categories: '{series.fechaPosicion}'
        },
        yAxis: {
            title: {
                text: 'Velocidad'
            },
        },
        tooltip: {
            crosshairs: true,
            shared: true
        },
        plotOptions: {
            spline: {
                marker: {
                    radius: 4,
                    lineColor: '#666666',
                    lineWidth: 1
                }
            }
        },
        series: [{
            name: 'Velocidad',
            marker: {
                symbol: 'diamond'
            },
            data: series
        }]
    });
});