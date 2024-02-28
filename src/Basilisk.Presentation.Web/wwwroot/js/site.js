function applyMoneyConversion() {
    let moneyConversions = document.querySelectorAll(".moneyConversion");
    if (moneyConversions.length > 0) {
        for (let element of moneyConversions) {
            let convertedValue = Number(element.value).toFixed(2);
            element.value = convertedValue;
        }
    }
}
function applyInputTypeNumber() {
    let numberInputs = document.querySelectorAll("[type=number]");
    if (numberInputs.length > 0) {
        for (let element of numberInputs) {
            if (element.value == "") {
                element.value = 0;
            }
        }
    }
}
function applyDateTimeInput() {
    var dateInputs = document.querySelectorAll("[type=date]");
    if (dateInputs.length > 0) {
        for (let element of dateInputs) {
            if (element.getAttribute("value") != "") {
                let dateValue = new Date(element.getAttribute("value"));
                let formatted = dateValue.toISOString().split('T')[0];
                element.value = formatted;
            }
        }
    }
}
function readOnlyPrimaryKeyOnUpdate() {
    let alternateAction = document.querySelector(".alternate-action table");
    if (alternateAction != null) {
        let actionType = alternateAction.getAttribute("data-action");
        document.querySelector(".alternate-action").setAttribute("action", actionType);
        if (actionType === "update") {
            document.querySelector(".readonly-id").setAttribute("readonly", "readonly");
        }
    }
}
function manageRole() {
    let role = document.querySelector(".role");
    if (role != null) {
        let roleName = role.textContent;
        let formatted = roleName.replace('[', '').replace(']', '');
        role.textContent = formatted;
        let mainBody = document.querySelector(".main-body");
        mainBody.setAttribute("data-role", formatted);
    }
}
function toggleMenu() {
    let toggleMenu = document.querySelector(".toggle-menu");
    let mainBody = document.querySelector(".main-body");
    toggleMenu.addEventListener("click", function (event) {
        mainBody.classList.toggle("change-menu");
    });
}
function responsiveGridColumn() {
    let columnNames = collectColumnName();
    if (columnNames != undefined) {
        let mediaQuery = createMediaQueryString(columnNames);
        let style = document.createElement('style');
        style.textContent = mediaQuery;
        let head = document.querySelector('head');
        head.appendChild(style);
    }
}
function createMediaQueryString(columnNames) {
    let mediaQuery = `@media only screen and (max-width: 960px) {`;
    let columnNumber = 2;
    for (let name of columnNames) {
        let cssString = `
            tbody td:nth-child(${columnNumber})::before{
                content: '${name}: ';
                font-weight:bold;
                color:white;
            }`;
        columnNumber++;
        mediaQuery += cssString;
    }
    mediaQuery += `}`;
    return mediaQuery;
}
function collectColumnName() {
    let thCollection = document.querySelectorAll(".grid-container thead th");
    if (thCollection.length > 0) {
        let columnNames = [];
        for (let th of thCollection) {
            let content = th.textContent;
            columnNames.push(content);
        }
        columnNames.shift();
        return columnNames;
    }
}
function autoFillCustomerAddress() {
    let dropdown = document.querySelector('[action="/order/upsert"] #customerId');
    if (dropdown != null) {
        dropdown.addEventListener('change', function () {
            let customerId = dropdown.value;
            requestCustomerAddress(customerId);
        });
    }
}
function requestCustomerAddress(customerId) {
    let request = new XMLHttpRequest();
    request.open('GET', `http://localhost:7070/order/customerAddress/${customerId}`);
    request.send();
    request.onload = function () {
        let data = JSON.parse(request.response);
        fillFormCustomerAddress(data);
    }
}
function fillFormCustomerAddress({ address, city }) {
    let destinationAddressForm = document.querySelector('#destinationAddress');
    let destinationCityForm = document.querySelector('#destinationCity');
    if (destinationAddressForm != null) {
        destinationAddressForm.value = address;
    }
    if (destinationCityForm != null) {
        destinationCityForm.value = city;
    }
}



(function () {
    applyMoneyConversion();
   // applyInputTypeNumber();
    applyDateTimeInput();
    readOnlyPrimaryKeyOnUpdate();
    manageRole();
    //toggleMenu();
    //responsiveGridColumn();
    autoFillCustomerAddress();
}())