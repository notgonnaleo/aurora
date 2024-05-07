function startSmartTables() {
    $(document).ready(function () {
        $("#table").DataTable({
            layout: {
                topStart: {
                    buttons: ['copy', 'csv', 'excel', 'pdf', 'print']
                }
            },
            responsive: true,
            paging: true,
            searching: true,
        });
    });
}
