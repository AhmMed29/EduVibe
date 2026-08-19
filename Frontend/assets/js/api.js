// Here We Put All Endpoints

async function apiRequest(endpoint, method = "GET", body = null) {

    // Receiving the Token
    // local storage (a small storage in the browser)
    const token = localStorage.getItem("accessToken");
    const config = {
        method: method, 
        headers: {
            "Content-Type": "application/json", 
            ...(token && { "Authorization": `Bearer ${token}` })
        },
    };

    if (body) {
        config.body = JSON.stringify(body);
    }

    const response = await fetch(`${API_BASE}${endpoint}`, config);

    if (response.status === 401) {
        localStorage.removeItem("accessToken");
        window.location.href = "login.html";
        return;
    }

    const text = await response.text();
    return text ? JSON.parse(text) : null;
}

const AuthAPI = {
    login: (email, password) => apiRequest("/auth/login", "POST", { email, password }),
    register: (data) => apiRequest("/auth/register", "POST", data),
    requestPasswordReset: (email) => apiRequest("/auth/password-reset", "POST", { email }),
    confirmReset: (email, token, newPassword) => apiRequest("/auth/confirm-reset", "POST", { email, token, newPassword }),
};

// Student Endpoints
const StudentAPI = {
    getAll: (params = {}) => apiRequest(`/student?${new URLSearchParams(params)}`),
    getById: (id) => apiRequest(`/student/${id}`),
    create: (data) => apiRequest("/student", "POST", data),
    update: (id, data) => apiRequest(`/student/${id}`, "PUT", data),
    delete: (id) => apiRequest(`/student/${id}`, "DELETE"),
};

// Course Endpoints
const CourseAPI = {
    getAll: (params = {}) => apiRequest(`/course?${new URLSearchParams(params)}`),
    getById: (id) => apiRequest(`/course/${id}`),
    create: (data) => apiRequest("/course", "POST", data),
    update: (id, data) => apiRequest(`/course/${id}`, "PUT", data),
    delete: (id) => apiRequest(`/course/${id}`, "DELETE"),
};

// Instructor Endpoints
const InstructorAPI = {
    getAll: (params = {}) => apiRequest(`/instructor?${new URLSearchParams(params)}`),
    getById: (id) => apiRequest(`/instructor/${id}`),
    create: (data) => apiRequest("/instructor", "POST", data),
    update: (id, data) => apiRequest(`/instructor/${id}`, "PUT", data),
    delete: (id) => apiRequest(`/instructor/${id}`, "DELETE"),
};

// Department Endpoints
const DepartmentAPI = {
    getAll: (params = {}) => apiRequest(`/department?${new URLSearchParams(params)}`),
    getById: (id) => apiRequest(`/department/${id}`),
    create: (data) => apiRequest("/department", "POST", data),
    update: (id, data) => apiRequest(`/department/${id}`, "PUT", data),
    delete: (id) => apiRequest(`/department/${id}`, "DELETE"),
};