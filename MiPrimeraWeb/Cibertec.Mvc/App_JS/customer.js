(function (customer) {
    customer.success = successReload;

    customer.pages = 1;
    customer.rowSize = 20;
    /* Atributos para el manejo de hub */
    customer.hub = {};
    customer.ids = [];
    customer.recordInUse = false;

    customer.addCustomer = addCustomerId;
    customer.removeHubCustomer = removeCustomerId;
    customer.validate = validate;

    $(function () {
        connectToHub();
        init(1);
    })

    //init();

    return customer;

    function successReload(option) {
        cibertec.closeModal(option);
        //Actualizar la pagina despues del crud
        elements = document.getElementsByClassName('active');
        activePages = elements[1].children;
        console.log(activePages[0].text);
        //init();
        //getCustomers(activePages[0].text);
        lstTableRows = $('.table >tbody >tr').length - 1;
        console.log(lstTableRows);

        if (option === "delete" && lstTableRows === 1) {
            cant = activePages[0].text;
            init(cant - 1);
        }
        else
            init(activePages[0].text);

        //getCustomers(activePages[0].text);

    }

    function init(numPage) {
        $.get('/Customer/Count/' + customer.rowSize,
            function (data) {
                customer.pages = data;
                $('.pagination').bootpag({
                    total: customer.pages,
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
                    getCustomers(num);
                });
                getCustomers(numPage);
            });
    }

    function getCustomers(cantPages) {
        var url = '/Customer/List/' + cantPages + '/' + customer.rowSize;
        
        $.get(url, function (data) {
            $('.content').html(data);
            console.log(data);
        });
    }

    function addCustomerId(id) {
        customer.hub.server.addCustomerId(id);
    }

    function removeCustomerId(id) {
        customer.hub.server.removeCustomerId(id);
    }

    function connectToHub() {
        customer.hub = $.connection.customerHub;
        customer.hub.client.customerStatus = customerStatus; //este metodo lo llama desde el cliente al servidor
    }

    function customerStatus(customerIds) {
        console.log(customerIds);
        customer.ids = customerIds;
    }

    function validate(id) {
        customer.recordInUse = (customer.ids.indexOf(id) > -1);
        if (customer.recordInUse) {
            $('#inUse').removeClass('hidden');
            $('#btn-save').html("");
        }
    }

})(window.customer = window.customer || {});


