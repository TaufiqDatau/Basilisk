const token = getCookie("accessToken");
const roleKey = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
var payload = JSON.parse(window.atob(token.split('.')[1]));
(function () {
    GetAllSuppliers();
    addSearchButtonListener();
    addContactButtonListener();
    addCloseButtonListener();
    addSubmitFormListener();
    addSubmitDeleteButtonListener();
    addInsertButtonListener();

}())
function getCookie(name) {
    const cookies = document.cookie.split('; ');

    for (const cookie of cookies) {
        const [cookieName, cookieValue] = cookie.split('=');

        if (cookieName === name) {
            return decodeURIComponent(cookieValue);
        }
    }

    return null;
}
function addContactButtonListener() {
    $('.contact-button').click(function (event) {
        let supplierId = $(this).attr('data-id');
        $.ajax({
            url: `http://localhost:5125/api/v1/suppliers/${supplierId}`,
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            success: function ({ id, companyName, contactPerson, jobTitle, address, city, phone, email }) {
                $('.contact-dialog .address').text(address);
                $('.contact-dialog .city').text(city);
                $('.contact-dialog .phone').text(phone);
                $('.contact-dialog .email').text(email);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.contact-dialog').addClass('popup-dialog--opened');
            }
        });
    });
}

function addCloseButtonListener() {
    $('.close-button').click(function (event) {
        $('.modal-layer').removeClass('modal-layer--opened');
        $('.popup-dialog').removeClass('popup-dialog--opened');
        $('.popup-dialog input').val("");
        $('.popup-dialog textarea').val("");
        $('.popup-dialog .validation-message').text("");
    });
}

function addInsertButtonListener() {
    $('.create-button').click(function (event) {
        event.preventDefault();
        console.log(payload[roleKey]);
        $('.modal-layer').addClass('modal-layer--opened');
        if (payload[roleKey] == 'Administrator') {
            $('.form-dialog').addClass('popup-dialog--opened');
        } else {
            $('.denied-dialog').addClass('popup-dialog--opened');
        }
    });
}

function addUpdateButtonListener() {
    $('.update-button').click(function (event) {
        event.preventDefault();
        let supplierId = $(this).attr('data-id');
        $.ajax({
            url: `http://localhost:5125/api/v1/suppliers/${supplierId}`,
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            success: function (response) {
                $('.modal-layer').addClass('modal-layer--opened');
                if (payload[roleKey] == 'Administrator') {
                    populateInputForm(response);
                    $('.form-dialog').addClass('popup-dialog--opened');
                } else {
                    $('.denied-dialog').addClass('popup-dialog--opened');
                }
            }
        })
    });
}

function populateInputForm({ id, companyName, contactPerson, jobTitle, address, city, phone, email }) {
    $('.form-dialog .id').val(id);
    $('.form-dialog .companyName').val(companyName);
    $('.form-dialog .contactPerson').val(contactPerson);
    $('.form-dialog .jobTitle').val(jobTitle);
    $('.form-dialog .address').val(address);
    $('.form-dialog .city').val(city);
    $('.form-dialog .phone').val(phone);
    $('.form-dialog .email').val(email);
}

function addSubmitFormListener() {
    $('.form-dialog button').click(function (event) {
        event.preventDefault();
        let dto = collectInputForm();
        let requestMethod = (dto.id === 0) ? 'POST' : 'PUT';
        $.ajax({
            method: requestMethod,
            url: `http://localhost:5125/api/v1/suppliers`,
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(dto),
            contentType: 'application/json',
            success: function (response) {
                $(".validation-message").text("");
                location.reload();
            },
            error: function ({ status, responseJSON }) {
                $(".validation-message").text("");
                if (status === 400) {

                    writeValidationMessage(responseJSON.errors);
                }
            }
        });
    })
}

function collectInputForm() {
    let id = $('.form-dialog .id').val()
    let dto = {
        id: (id === "") ? 0 : id,
        companyName: $('.form-dialog .companyName').val(),
        contactPerson: $('.form-dialog .contactPerson').val(),
        jobTitle: $('.form-dialog .jobTitle').val(),
        address: $('.form-dialog .address').val(),
        city: $('.form-dialog .city').val(),
        phone: $('.form-dialog .phone').val(),
        email: $('.form-dialog .email').val()
    };
    return dto;
}

function writeValidationMessage(errorMessages) {
    for (let field in errorMessages) {
        let messages = errorMessages[field];
        if (Array.isArray(messages) && messages.length > 0) {
            let message = messages[0];
            $(`.form-dialog [data-for="${field}"]`).text(message);
        }

    }
}


function addDeleteButtonListener() {
    $('.delete-button').click(function (event) {
        event.preventDefault();
        let supplierId = $(this).attr('data-id');
        $('.delete-dialog .id').val(supplierId);
        $('.modal-layer').addClass('modal-layer--opened');
        if (payload[roleKey]=="Administrator") {
            $('.delete-dialog').addClass('popup-dialog--opened');
        } else {
            $('.denied-dialog').addClass('popup-dialog--opened');
        }
    });
}

function addSubmitDeleteButtonListener() {
    $('.delete-dialog button').click(function (event) {
        let supplierId = $('.delete-dialog .id').val();
        $.ajax({
            method: "DELETE",
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            url: `http://localhost:5125/api/v1/suppliers/${supplierId}`,
            success: function (response) {
                location.reload();
            },
            error: function (response) {

                $('.delete-dialog').removeClass('popup-dialog--opened');
                $('.denied-dialog').addClass('popup-dialog--opened');

            }
        });
    });
}

function GetAllSuppliers() {
    $.ajax({
        type: "GET",
        url: "http://localhost:5125/api/v1/suppliers",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json"
        },
        success: function (suppliers) {
            FillSupplierTable(suppliers);
            FillPagination(suppliers);

            addUpdateButtonListener();
            addContactButtonListener();
            addDeleteButtonListener();
        },
        error: function (xhr, status, error) {
            console.error("Error:", xhr.status, xhr.statusText);
        }
    });

}
function addSearchButtonListener() {
    const searchButton = document.querySelector(".supplier-search-button");
    searchButton.addEventListener("click", (e) => {
        e.preventDefault();
        SearchSupplier(1);
    })
}
function SearchSupplier(currentPage) {
    let searchValue = document.querySelector("#supplier-name-search").value;
    let tableBody = document.querySelector("#supplier-table-body");
    tableBody.innerHTML = "";
    let request = new XMLHttpRequest();
    request.open("GET", `http://localhost:5125/api/v1/suppliers?name=${searchValue}&currentPage=${currentPage}`);
    request.setRequestHeader("Content-Type", "application/json");
    request.setRequestHeader("Authorization", `Bearer ${token}`);
    request.send();
    request.onload = () => {
        let suppliersInfoViewModel = JSON.parse(request.responseText);

        FillSupplierTable(suppliersInfoViewModel);
        FillPagination(suppliersInfoViewModel);

        addUpdateButtonListener();
        addContactButtonListener();
        addDeleteButtonListener();
    };

}
function FillSupplierTable(suppliers) {
    
}
function FillPagination(suppliers) {
    const paginationClass = document.querySelector(".pagination");
    paginationClass.innerHTML = "";
    paginationClass.innerHTML += `
                      <div>page ${suppliers.currentPage} of ${suppliers.totalPage}</div>
                      <div class="pagination-list"></div>
                `;
    const $tableBody = $("#supplier-table-body");
    $tableBody.empty(); // Clear the contents first

    $.each(suppliers.data, function (index, supplier) {
        $tableBody.append(`
        <tr>
            <td>
                <a class="blue-button delete-button" data-id="${supplier.id}"> DELETE </a>
                <a class="blue-button update-button" data-id="${supplier.id}"> EDIT </a>
                <a class="blue-button contact-button" data-id="${supplier.id}"> CONTACT </a>
            </td>
            <td>${supplier.companyName}</td>
            <td>${supplier.contactPerson}</td>
            <td>${supplier.jobTitle}</td>
        </tr>
    `);
    });


    const PageDiv = document.querySelector(".pagination-list");
    for (let page = 1; page <= suppliers.totalPage; page++) {
        PageDiv.innerHTML += `
                        <a href="#" onClick="PageClick(${page})">
                            <span>${page}</span>
                        </a>
                    `;
    }
}

function PageClick(pageNumber) {
    SearchSupplier(pageNumber);
}