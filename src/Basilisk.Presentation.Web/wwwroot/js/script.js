(function(){
    AddLogoutButtonEventListener();
}())

function showYearlyReport() {
    let yearlyReportTable = document.querySelectorAll("#yearly-report");
    let monthlyReportTable = document.querySelectorAll('#monthly-report');

    if (yearlyReportTable[0].style.display == "none") {
        yearlyReportTable[0].style.display = null;
        yearlyReportTable[1].style.display = null;
    } else {
        yearlyReportTable[0].style.display = "none";
        yearlyReportTable[1].style.display = "none";

    }
    monthlyReportTable[0].style.display = "none";
    monthlyReportTable[1].style.display = "none";
}

function showMonthlyReport() {
    let yearlyReportTable = document.querySelectorAll("#yearly-report");
    let monthlyReportTable = document.querySelectorAll('#monthly-report');

    yearlyReportTable[0].style.display = "none";
    yearlyReportTable[1].style.display = "none";

    if (monthlyReportTable[0].style.display === "none") {
        monthlyReportTable[0].style.display = null;
        monthlyReportTable[1].style.display = null;

    } else {
        monthlyReportTable[0].style.display = "none";
        monthlyReportTable[1].style.display = "none";
    }
}

function AddLogoutButtonEventListener() {
    let logoutButton = document.querySelector("#logout-button");
    logoutButton.addEventListener("click", (e) => {
        RemoveJwt();
    })
}
function RemoveJwt() {
    localStorage.removeItem("accessToken");
    document.cookie.removeItem("")
}