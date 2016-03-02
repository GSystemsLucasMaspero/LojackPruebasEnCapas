$(function () {
    $("#grid").jqGrid({
        url: "/Entidad/GetPositions",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['ID','Fecha y Hora', 'Latitud', 'Longitud', 'Velocidad'],
        colModel: [
            { key: true, hidden: true, name: 'ID', index: 'ID', editable: true },
            { key: false, name: 'Fecha y Hora', index: 'TaskName', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } },
            { key: false, name: 'Latitud', index: 'TaskDescription', editable: true },
            { key: false, name: 'Longitud', index: 'TargetDate', editable: true },
            { key: false, name: 'Velocidad', index: 'Severity', editable: true }
        ],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Recorrido',
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
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            // Edit options
            zIndex: 100,
            url: '/Entidad/EditPosition',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // Add options
            zIndex: 100,
            url: "/Entidad/CreatePosition",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // Delete options
            zIndex: 100,
            url: "/Entidad/DeletePosition",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Esta seguro que desea eliminar esta posición?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});