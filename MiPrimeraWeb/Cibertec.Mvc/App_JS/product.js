(function (product) {
    product.success = successReload;

    product.pages = 1;
    product.rowSize = 15;
    /* Atributos para el manejo de hub */
    product.hub = {};
    product.ids = [];
    product.recordInUse = false;

    product.addProduct = addProductId;
    product.removeHubProduct = removeProductId;
    product.validate = validate;

    $(function () {
        connectToHub();
        init(1);
    })

    //init();

    return product;

    function successReload(option) {
        cibertec.closeModal(option);
        //Actualizar la pagina despues del crud
        elements = document.getElementsByClassName('active');
        activePages = elements[1].children;
        console.log(activePages[0].text);
        //init();
        //getproducts(activePages[0].text);
        lstTableRows = $('.table >tbody >tr').length - 1;
        console.log(lstTableRows);

        if (option === "delete" && lstTableRows === 1) {
            cant = activePages[0].text;
            init(cant - 1);
        }
        else
            init(activePages[0].text);

        //getproducts(activePages[0].text);

    }

    function init(numPage) {
        $.get('/Product/ProductCount/' + product.rowSize,
            function (data) {
                product.pages = data;
                $('.pagination').bootpag({
                    total: product.pages,
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
                    getproducts(num);
                });
                getproducts(numPage);
            });
    }

    function getproducts(cantPages) {
        var url = '/Product/ProductList/' + cantPages + '/' + product.rowSize;

        $.get(url, function (data) {
            $('.content').html(data);
            console.log(data);
        });
    }

    function addProductId(id) {
        product.hub.server.addProductId(id);
    }

    function removeProductId(id) {
        product.hub.server.removeProductId(id);
    }

    function connectToHub() {
        product.hub = $.connection.productHub;
        product.hub.client.productStatus = productStatus; //este metodo lo llama desde el cliente al servidor
    }

    function productStatus(productIds) {
        console.log(productIds);
        product.ids = productIds;
    }

    function validate(id) {
        product.recordInUse = (product.ids.indexOf(id) > -1);
        if (product.recordInUse) {
            $('#inUse').removeClass('hidden');
            $('#btn-save').html("");
        }
    }

})(window.product = window.product || {});


