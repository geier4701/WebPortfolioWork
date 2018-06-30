loadProducts();

var totalMoney = 0.00;
var totalDollars = 0;
var totalQuarters = 0;
var totalDimes = 0;
var totalNickels = 0;
var idToPurchase = 0;

calculateTotalMoney();

$('#makePurchaseBtn').click(function (event) {
    $.ajax ({
        type: 'GET',
        url: `http://localhost:8080/money/${totalMoney}/item/${idToPurchase}`,
        success: function (data, status) {
            $.each(data, function(index, money) {
                totalQuarters = data.quarters;
                totalDimes = data.dimes;
                totalNickels = data.nickels;
            })
            totalDollars = 0.00;
            calculateTotalMoney();
            loadProducts();
            idToPurchase = 0;
            $('#itemSelected').empty();
            displayMessage("THANK YOU!!!");
        },
        error: function(data, status) {
            var jsonResponse = data.responseJSON;
            message = jsonResponse.message;
            idToPurchase = 0;
            $('#itemSelected').empty();
            displayMessage(message);
        }
    });
})

$('#changeReturnBtn').click(function (event) {
    returnChange();
})

$('#addDollarBtn').click(function (event) {
    totalDollars = totalDollars + 1;
    calculateTotalMoney();
})

$('#addQuarterBtn').click(function (event) {
    totalQuarters = totalQuarters + 1;
    calculateTotalMoney();
})

$('#addDimeBtn').click(function (event) {
    totalDimes = totalDimes + 1;
    calculateTotalMoney();
})

$('#addNickelBtn').click(function (event) {
    totalNickels = totalNickels + 1;
    calculateTotalMoney();
})

function calculateTotalMoney() {
    totalMoney = (totalDollars + (totalQuarters * 0.25) + (totalDimes * 0.1) + (totalNickels * 0.05)).toFixed(2);
    totalMoney = parseFloat(totalMoney);
    $('#totalMoneyIn').empty();
    $('#totalMoneyIn').append('<b>$' + totalMoney + '</b>');
}

function loadProducts() {
    var productTable = $('#productTable');

    productTable.empty();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/items',
        success: function(data, status) {
            $.each(data, function (index, product) {
                var id = product.id;
                var name = product.name;
                var price = product.price;
                var quantity = product.quantity;

                var row = `<td style="float:left; width: 33%">${id}. "${name}" <br>Price: $${price}<br><button onclick="selectItem(${id})" class="btn btn-default">Choose</button><br>Quantity Left: ${quantity}</td>`;
                productTable.append(row);
            });
        },
        error: function() {
            $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item'})
                .text('Error calling service'));
        }
    })
}

function displayMessage(message) {
    $('#returnedMessage').empty();
    $('#returnedMessage').append(message);
}

function selectItem (id) {
    idToPurchase = id;
    $('#itemSelected').empty();
    $('#itemSelected').append("Product number: " + id);
}

function returnChange() {
    $('#changeToReturn').empty();
    if (totalDollars != 0){
        totalQuarters = totalQuarters + (totalDollars * 4);
        totalDollars = 0;
    };
    if (totalQuarters != 0){
        $('#changeToReturn').append(`${totalQuarters} Quarters<br>`);
        totalQuarters = 0;
    };
    if (totalDimes != 0){
        $('#changeToReturn').append(`${totalDimes} Dimes<br>`);
        totalDimes = 0;
    };
    if (totalNickels != 0){
        $('#changeToReturn').append(`${totalNickels} Nickels<br>`);
        totalNickels = 0;
    };
    calculateTotalMoney();
    $('#returnedMessage').empty();
}