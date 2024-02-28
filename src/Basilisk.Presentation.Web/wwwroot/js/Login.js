const LoginButton = document.querySelector("button");
(function () {
    AddEventListenerLogin();
}())

function AddEventListenerLogin() {
    $("button").on("click", function (event) {
        event.preventDefault();
        LoadingLogin();
        LoginButtonFunction();
    });

}
function LoadingLogin() {
    $("#LoginButton").css({
        "cursor": "not-allowed",
        "background": "#636e72",
        "opacity": "0.4"
    }).html('Loading');
}

function LoginButtonFunction() {
    var dto = CollectLoginForm();

    $.ajax({
        type: "POST",
        url: "http://localhost:5125/Auth/Login",
        contentType: "application/json",
        data: JSON.stringify(dto),
        success: function (response) {
            //save it in localStorage
            localStorage.setItem("accessToken", response.token);
            //Save it in cookie
            var expirationDate = new Date();
            expirationDate.setDate(expirationDate.getDate() + 7); 
            document.cookie = `accessToken=${response.token}; expires=${expirationDate.toUTCString()}; path=/`;

            $("#form-login").submit();
        },
        error: function (xhr, status, error) {
            console.error("Error:", xhr.status, xhr.statusText);
            $("#form-login").submit();
            $("#LoginButton").removeAttr("style");
        }
    });

}

function CollectLoginForm() {
    let dto = {
        username: $(`[name="Username"]`).value,
        password: $(`[name="Password"]`).value,
    };
    return dto;
}