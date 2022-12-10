function initDataGrid(grdName, order = 1, orderType = 'asc') {
    if (typeof ($.fn.DataTable) === 'undefined') { return; }

    var handleDataTableButtons = function () {
        $('#' + grdName).DataTable({
            dom: '<<"toolbar">B<"clear"><"col-md-12 col-sm-12 col-xs-1"lf><"clear">rtip>',
            buttons: [

                {
                    extend: "copy",
                    text: "Copiar",
                    className: "btn-sm"
                },

                /*{
                    extend: "print",
                    text: "Imprimir",
                    className: "btn-sm"
                },*/

                {
                    extend: "excelHtml5",

                    className: "btn-sm"
                },

            ],

            language: {
                lengthMenu: 'Mostrar _MENU_ filas por hoja',
                zeroRecords: 'Sin resultados',
                info: 'Página _PAGE_ de _PAGES_ - Desde _START_ a _END_ de _TOTAL_ filas',
                infoEmpty: 'Sin resultados de búsqueda',
                infoFiltered: '(_MAX_ filas totales)',
                search: 'Buscar:',
                copy: 'Copiar',
                paginate: {
                    'first': 'Primera',
                    'previous': 'Anterior',
                    'next': 'Siguiente',
                    'last': 'Ultima'
                }
            },

            order: [[order, orderType]]//,

            // responsive: true

        });
        $('#' + grdName)[0].style = 'width: 100%;';
        $('#' + grdName)[0].width = '100%';
    };

    TableManageButtons = function () {
        "use strict";
        return {
            init: function () {
                handleDataTableButtons();
                $("#grd_wrapper div.dt-buttons")[0].classList.remove("dt-buttons");
            }
        };
    }();

    TableManageButtons.init();
    $("div.dataTables_length").css({ 'margin-top': '5px', 'float': 'left' });

};
