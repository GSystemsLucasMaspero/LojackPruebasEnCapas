$(function () {
    $("#grid").jqGrid({
        url: "/Entidad/GetPositions",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['ID','Fecha y Hora', 'Latitud', 'Longitud', 'Velocidad'],
        colModel: [
            { key: true, hidden: true, name: 'idPosicion', index: 'idPosicion', editable: true },
            { key: false, name: 'fechaPosicion', index: 'fechaPosicion', editable: true, formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "m/d/Y h:i A" } },
            { key: false, name: 'latitud', index: 'latitud', editable: true },
            { key: false, name: 'longitud', index: 'longitud', editable: true },
            { key: false, name: 'velocidad', index: 'velocidad', editable: true }
        ],
        pager: jQuery('#pager'),
        rowNum: 10,
        height: '100%',
        viewrecords: true,
        emptyrecords: 'No hay registros que mostrar',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: false, add: false, del: false, search: false, refresh: true });
});