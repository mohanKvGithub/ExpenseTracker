function initializeDataTable({ tableId, emptyText, layout, ajaxUrl, ajaxData, columns, columnDefs, initComplete, rowCallback,select }) {
    var options = {
        language: {
            paginate: {
                previous: "‹",
                next: "›"
            },
            infoFiltered: "",
            emptyTable: `<div class='text-center text-xs font-weight-bold'>${emptyText}</div>`,
        },
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, 'All']],
        stateSave: false,
        autoWidth: false,
        processing: false,
        serverSide: true,
        paging: true,
        ordering: false,
        searching: true,
        responsive: false,
        select: select,
        ajax: {
            url: ajaxUrl,
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            ajaxData: ajaxData,
            error: function (xhr, error, thrown) {
                console.error("DataTable AJAX Error:", xhr.responseText);
            }
        },
        columns: columns,
        columnDefs: columnDefs,
    };
    if (initComplete) {
        options.initComplete = initComplete;
    }

    if (rowCallback) {
        options.rowCallback = rowCallback;
    }

    var dtTable = $('#' + tableId).DataTable(options);
    return dtTable;
}