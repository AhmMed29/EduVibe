// Sign In/Out and Permissions

function saveToken(responseData) {
    // set : set at browser
    localStorage.setItem("accessToken", responseData.accessToken);
    localStorage.setItem("userEmail", responseData.email);
    localStorage.setItem("userName", responseData.userName);
    localStorage.setItem("userRoles", JSON.stringify(responseData.roles));
}

function logout() {
    localStorage.clear();
    window.location.href = "login.html";
}

function requireAuth() {
    const token = localStorage.getItem("accessToken");
    if (!token) {
        window.location.href = "login.html";
    }
}

function isAdmin() {
    // getItem : get from browser storage
    const roles = JSON.parse(localStorage.getItem("userRoles") || "[]");
    return roles.includes("Admin");
}

function hideIfNotAdmin() {
    if (!isAdmin()) {
        document.querySelectorAll(".admin-only").forEach(el => {
            el.style.display = "none";
        });
    }
}