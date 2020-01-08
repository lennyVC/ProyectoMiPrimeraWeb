(function (supplier) {
    supplier.success = successReload;

    supplier.pages = 1;
    supplier.rowSize = 20;
    /* Atributos para el manejo de hub */
    supplier.hub = {};
    supplier.ids = [];
    supplier.recordInUse = false;

    supplier.addSupplier = addSupplierId;
    supplier.removeHubSupplier = removeSupplierId;
    supplier.validate = validate;

    $(function () {
        connectToHub();
        init(1);
    })

    //init();

    return supplier;

    function successReload(option) {
        cibertec.closeModal(option);
        //Actualizar la pagina despues del crud
        elements = document.getElementsByClassName('active');
        activePages = elements[1].children;
        console.log(activePages[0].text);
        //init();
        //getsuppliers(activePages[0].text);
        lstTableRows = $('.table >tbody >tr').length - 1;
        console.log(lstTableRows);

        if (option === "delete" && lstTableRows === 1) {
            cant = activePages[0].text;
            init(cant - 1);
        }
        else
            init(activePages[0].text);

        //getsuppliers(activePages[0].text);

    }

    function init(numPage) {
        $.get('/Supplier/SupplierCount/' + supplier.rowSize,
            function (data) {
                supplier.pages = data;
                $('.pagination').bootpag({
                    total: supplier.pages,
                    page: numPage,
                    maxVisible: 5,
                    leaps: true,
                    firstLastUse: true,
                    first: '← ',
                    last: '→ ',
                    wrapClass: 'pagination',
                    activeClass: 'active',
                    disabledClass: 'disabled',
                    nextClass: 'next',
                    prevClass: 'prev',
                    lastClass: 'last',
                    firstClass: 'first'
                }).on('page', function (event, num) {
                    getsuppliers(num);
                });
                getsuppliers(numPage);
            });
    }

    function getsuppliers(cantPages) {
        var url = '/Supplier/SupplierList/' + cantPages + '/' + supplier.rowSize;

        $.get(url, function (data) {
            $('.content').html(data);
            console.log(data);
        });
    }

    function addSupplierId(id) {
        supplier.hub.server.addSupplierId(id);
    }

    function removeSupplierId(id) {
        supplier.hub.server.removeSupplierId(id);
    }

    function connectToHub() {
        supplier.hub = $.connection.supplierHub;
        supplier.hub.client.supplierStatus = supplierStatus; //este metodo lo llama desde el cliente al servidor
    }

    function supplierStatus(supplierIds) {
        console.log(supplierIds);
        supplier.ids = supplierIds;
    }

    function validate(id) {
        supplier.recordInUse = (supplier.ids.indexOf(id) > -1);
        if (supplier.recordInUse) {
            $('#inUse').removeClass('hidden');
            $('#btn-save').html("");
        }
    }

})(window.supplier = window.supplier || {});


