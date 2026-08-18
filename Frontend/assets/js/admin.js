// ====================== arabic & english ======================
const translations = {
    en: {
        dashboard: "Dashboard", students: "Students", courses: "Courses",
        instructors: "Instructors", departments: "Departments",
        statStudents: "Total Students", statCourses: "Total Courses",
        statInstructors: "Total Instructors", statDepartments: "Total Departments",
        search: "Search...", add: "+ Add", actions: "Actions",
        edit: "Edit", delete: "Delete", cancel: "Cancel", save: "Save",
        page: "Page", of: "of", records: "records", prev: "Prev", next: "Next",
        name: "Name", email: "Email", phone: "Phone", department: "Department",
        gender: "Gender", salary: "Salary", credits: "Credits", description: "Description",
        title: "Title", price: "Price/Hour", duration: "Duration (h)",
        fname: "First Name", lname: "Last Name", dob: "Date of Birth",
        city: "City", country: "Country", none: "— None —",
        addStudent: "Add Student", editStudent: "Edit Student",
        addCourse: "Add Course", editCourse: "Edit Course",
        addInstructor: "Add Instructor", editInstructor: "Edit Instructor",
        addDepartment: "Add Department", editDepartment: "Edit Department",
        confirmDelete: "Are you sure you want to delete this item?",
        saved: "Saved successfully", deleted: "Deleted successfully",
        error: "Something went wrong. Please try again.",
        logout: "Logout", welcome: "Welcome"
    },
    ar: {
        dashboard: "لوحة التحكم", students: "الطلاب", courses: "الكورسات",
        instructors: "المدرسين", departments: "الأقسام",
        statStudents: "إجمالي الطلاب", statCourses: "إجمالي الكورسات",
        statInstructors: "إجمالي المدرسين", statDepartments: "إجمالي الأقسام",
        search: "بحث...", add: "+ إضافة", actions: "إجراءات",
        edit: "تعديل", delete: "حذف", cancel: "إلغاء", save: "حفظ",
        page: "صفحة", of: "من", records: "سجل", prev: "السابق", next: "التالي",
        name: "الاسم", email: "الإيميل", phone: "الهاتف", department: "القسم",
        gender: "النوع", salary: "الراتب", credits: "الوحدات", description: "الوصف",
        title: "العنوان", price: "السعر/ساعة", duration: "المدة (س)",
        fname: "الاسم الأول", lname: "الاسم الأخير", dob: "تاريخ الميلاد",
        city: "المدينة", country: "الدولة", none: "— بدون —",
        addStudent: "إضافة طالب", editStudent: "تعديل طالب",
        addCourse: "إضافة كورس", editCourse: "تعديل كورس",
        addInstructor: "إضافة مدرس", editInstructor: "تعديل مدرس",
        addDepartment: "إضافة قسم", editDepartment: "تعديل قسم",
        confirmDelete: "هل أنت متأكد من حذف هذا العنصر؟",
        saved: "تم الحفظ بنجاح", deleted: "تم الحذف بنجاح",
        error: "حدث خطأ. حاول مرة أخرى.",
        logout: "تسجيل الخروج", welcome: "أهلاً"
    }
};

let currentLang = localStorage.getItem("lang") || "en";

function t(key) {
    return translations[currentLang][key] || key;
}

function applyLanguage() {
    document.documentElement.lang = currentLang;
    document.documentElement.dir = currentLang === "ar" ? "rtl" : "ltr";
    document.getElementById("langToggle").textContent = currentLang === "en" ? "عربي" : "EN";
    document.querySelectorAll("[data-i18n]").forEach(el => {
        const key = el.getAttribute("data-i18n");
        el.textContent = t(key);
    });
    document.getElementById("adminUserName").textContent = localStorage.getItem("userName") || "Admin";
    document.getElementById("adminUserEmail").textContent = localStorage.getItem("userEmail") || "";
}

function toggleLang() {
    currentLang = currentLang === "en" ? "ar" : "en";
    localStorage.setItem("lang", currentLang);
    applyLanguage();
    showSection(currentSection);
}

// ====================== Helpers ======================
function esc(str) {
    return String(str ?? "").replace(/[&<>"']/g, c => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c]));
}

function showToast(msg, ok = true) {
    const el = document.getElementById("toast");
    el.textContent = msg;
    el.className = "toast " + (ok ? "toast-success" : "toast-error") + " show";
    setTimeout(() => el.classList.remove("show"), 2500);
}

function debounce(fn, ms = 400) {
    let timer;
    return function (...args) {
        clearTimeout(timer);
        timer = setTimeout(() => fn.apply(this, args), ms);
    };
}

// ====================== State ======================
let currentSection = "dashboard";
let modalEntity = null;
let modalId = null;
let departmentsCache = [];

const state = {
    students:    { page: 1, pageSize: 10, search: "" },
    courses:     { page: 1, pageSize: 10, search: "" },
    instructors: { page: 1, pageSize: 10, search: "" },
    departments: { page: 1, pageSize: 10, search: "" }
};

// ====================== Navigation ======================
function showSection(name) {
    currentSection = name;
    document.querySelectorAll(".admin-section").forEach(s => s.style.display = "none");
    document.getElementById("section-" + name).style.display = "block";
    document.querySelectorAll(".admin-nav-item").forEach(el =>
        el.classList.toggle("active", el.dataset.section === name));
    document.getElementById("pageTitle").textContent = t(name);

    if (name === "dashboard") loadDashboard();
    if (name === "students") loadStudents(1);
    if (name === "courses") loadCourses(1);
    if (name === "instructors") loadInstructors(1);
    if (name === "departments") loadDepartments(1);
}

// ====================== Dashboard ======================
async function loadDashboard() {
    const [st, co, ins, dep] = await Promise.all([
        StudentAPI.getAll({ pageNumber: 1, pageSize: 1 }),
        CourseAPI.getAll({ pageNumber: 1, pageSize: 1 }),
        InstructorAPI.getAll({ pageNumber: 1, pageSize: 1 }),
        DepartmentAPI.getAll({ pageNumber: 1, pageSize: 1 })
    ]);
    document.getElementById("statStudents").textContent = st?.totalRecords ?? "-";
    document.getElementById("statCourses").textContent = co?.totalRecords ?? "-";
    document.getElementById("statInstructors").textContent = ins?.totalRecords ?? "-";
    document.getElementById("statDepartments").textContent = dep?.totalRecords ?? "-";
}

// ====================== Loaders ======================
async function loadStudents(page) {
    const s = state.students;
    s.page = page;
    const res = await StudentAPI.getAll({
        pageNumber: page, pageSize: s.pageSize,
        searchTerm: s.search || undefined
    });
    if (!res) return;
    document.getElementById("studentsBody").innerHTML = res.data.map(st => `
        <tr>
            <td>${esc(st.fullName)}</td>
            <td>${esc(st.email)}</td>
            <td>${esc(st.phoneNumber)}</td>
            <td>${esc(st.departmentName) || "-"}</td>
            <td>${esc(st.gender) || "-"}</td>
            <td class="table-actions">
                <button class="btn-edit" onclick="openModal('student', ${st.id})">${t("edit")}</button>
                <button class="btn-delete" onclick="deleteStudent(${st.id})">${t("delete")}</button>
            </td>
        </tr>`).join("");
    renderPagination("studentsPagination", res, "loadStudents");
}

async function loadCourses(page) {
    const s = state.courses;
    s.page = page;
    const res = await CourseAPI.getAll({
        pageNumber: page, pageSize: s.pageSize,
        searchTerm: s.search || undefined
    });
    if (!res) return;
    document.getElementById("coursesBody").innerHTML = res.data.map(c => `
        <tr>
            <td>${esc(c.title)}</td>
            <td>${esc(c.credits)}</td>
            <td>${esc(c.durationInHours) || "-"}</td>
            <td>${esc(c.pricePerHour) ?? "-"}</td>
            <td>${esc(c.departmentName) || "-"}</td>
            <td class="table-actions">
                <button class="btn-edit" onclick="openModal('course', ${c.id})">${t("edit")}</button>
                <button class="btn-delete" onclick="deleteCourse(${c.id})">${t("delete")}</button>
            </td>
        </tr>`).join("");
    renderPagination("coursesPagination", res, "loadCourses");
}

async function loadInstructors(page) {
    const s = state.instructors;
    s.page = page;
    const res = await InstructorAPI.getAll({
        pageNumber: page, pageSize: s.pageSize,
        searchTerm: s.search || undefined
    });
    if (!res) return;
    document.getElementById("instructorsBody").innerHTML = res.data.map(i => `
        <tr>
            <td>${esc(i.fullName)}</td>
            <td>${esc(i.email)}</td>
            <td>${esc(i.phoneNumber)}</td>
            <td>${esc(i.salary) ?? "-"}</td>
            <td>${esc(i.departmentName) || "-"}</td>
            <td class="table-actions">
                <button class="btn-edit" onclick="openModal('instructor', ${i.id})">${t("edit")}</button>
                <button class="btn-delete" onclick="deleteInstructor(${i.id})">${t("delete")}</button>
            </td>
        </tr>`).join("");
    renderPagination("instructorsPagination", res, "loadInstructors");
}

async function loadDepartments(page) {
    const s = state.departments;
    s.page = page;
    const res = await DepartmentAPI.getAll({
        pageNumber: page, pageSize: s.pageSize,
        searchTerm: s.search || undefined
    });
    if (!res) return;
    document.getElementById("departmentsBody").innerHTML = res.data.map(d => `
        <tr>
            <td>${esc(d.name)}</td>
            <td>${esc(d.description) || "-"}</td>
            <td>${d.courses?.length ?? 0}</td>
            <td>${d.instructors?.length ?? 0}</td>
            <td class="table-actions">
                <button class="btn-edit" onclick="openModal('department', ${d.id})">${t("edit")}</button>
                <button class="btn-delete" onclick="deleteDepartment(${d.id})">${t("delete")}</button>
            </td>
        </tr>`).join("");
    renderPagination("departmentsPagination", res, "loadDepartments");
}

// ====================== Pagination ======================
function renderPagination(elId, res, loaderName) {
    document.getElementById(elId).innerHTML = `
        <span>${t("page")} ${res.pageNumber} ${t("of")} ${res.totalPages} (${res.totalRecords} ${t("records")})</span>
        <div style="display:flex; gap:8px;">
            <button class="page-btn" onclick="${loaderName}(${res.pageNumber - 1})" ${res.hasPreviousPage ? "" : "disabled"}>${t("prev")}</button>
            <button class="page-btn" onclick="${loaderName}(${res.pageNumber + 1})" ${res.hasNextPage ? "" : "disabled"}>${t("next")}</button>
        </div>`;
}

// ====================== Modal: forms ======================
function deptOptions(selectedName) {
    const sel = departmentsCache.find(d => d.name === selectedName)?.id ?? "";
    return `<option value="">${t("none")}</option>` +
        departmentsCache.map(d =>
            `<option value="${d.id}" ${String(sel) === String(d.id) ? "selected" : ""}>${esc(d.name)}</option>`
        ).join("");
}

function buildForm(entity) {
    if (entity === "student") return `
        <div class="form-row">
            <div class="form-group"><label>${t("fname")}</label><input data-modal-field="fname" required></div>
            <div class="form-group"><label>${t("lname")}</label><input data-modal-field="lname" required></div>
        </div>
        <div class="form-group"><label>${t("email")}</label><input type="email" data-modal-field="email" required></div>
        <div class="form-group"><label>${t("phone")}</label><input data-modal-field="phoneNumber" required></div>
        <div class="form-row">
            <div class="form-group"><label>${t("dob")}</label><input type="date" data-modal-field="dateOfBirth" required></div>
            <div class="form-group"><label>${t("gender")}</label>
                <select data-modal-field="gender">
                    <option value="">-</option>
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                </select>
            </div>
        </div>
        <div class="form-group"><label>${t("department")}</label><select data-modal-field="departmentId">${deptOptions()}</select></div>
        <div class="form-row">
            <div class="form-group"><label>${t("city")}</label><input data-modal-field="city"></div>
            <div class="form-group"><label>${t("country")}</label><input data-modal-field="country"></div>
        </div>`;

    if (entity === "course") return `
        <div class="form-group"><label>${t("title")}</label><input data-modal-field="title" required></div>
        <div class="form-group"><label>${t("description")}</label><textarea data-modal-field="description"></textarea></div>
        <div class="form-row">
            <div class="form-group"><label>${t("credits")}</label><input type="number" data-modal-field="credits" required></div>
            <div class="form-group"><label>${t("duration")}</label><input type="number" data-modal-field="durationInHours"></div>
        </div>
        <div class="form-row">
            <div class="form-group"><label>${t("price")}</label><input type="number" step="0.01" data-modal-field="pricePerHour"></div>
            <div class="form-group"><label>${t("department")}</label><select data-modal-field="departmentId" required>${deptOptions()}</select></div>
        </div>`;

    if (entity === "instructor") return `
        <div class="form-row">
            <div class="form-group"><label>${t("fname")}</label><input data-modal-field="fname" required></div>
            <div class="form-group"><label>${t("lname")}</label><input data-modal-field="lname" required></div>
        </div>
        <div class="form-group"><label>${t("email")}</label><input type="email" data-modal-field="email" required></div>
        <div class="form-group"><label>${t("phone")}</label><input data-modal-field="phoneNumber" required></div>
        <div class="form-row">
            <div class="form-group"><label>${t("dob")}</label><input type="date" data-modal-field="dateOfBirth" required></div>
            <div class="form-group"><label>${t("salary")}</label><input type="number" step="0.01" data-modal-field="salary"></div>
        </div>
        <div class="form-group"><label>${t("department")}</label><select data-modal-field="departmentId">${deptOptions()}</select></div>
        <div class="form-row">
            <div class="form-group"><label>${t("city")}</label><input data-modal-field="city"></div>
            <div class="form-group"><label>${t("country")}</label><input data-modal-field="country"></div>
        </div>`;

    if (entity === "department") return `
        <div class="form-group"><label>${t("name")}</label><input data-modal-field="name" required></div>
        <div class="form-group"><label>${t("description")}</label><textarea data-modal-field="description"></textarea></div>`;

    return "";
}

function openModal(entity, id = null) {
    modalEntity = entity;
    modalId = id;
    document.getElementById("modalTitle").textContent = t((id ? "edit" : "add") + entity[0].toUpperCase() + entity.slice(1));
    document.getElementById("modalBody").innerHTML = buildForm(entity);
    document.getElementById("modalOverlay").style.display = "flex";
    if (id) fillForm(entity, id);
}

function closeModal() {
    document.getElementById("modalOverlay").style.display = "none";
    modalEntity = null;
    modalId = null;
}

function fillForm(entity, id) {
    const getters = {
        student: StudentAPI.getById,
        course: CourseAPI.getById,
        instructor: InstructorAPI.getById,
        department: DepartmentAPI.getById
    };
    getters[entity](id).then(dto => {
        if (!dto) return;
        const set = (field, val) => {
            const el = document.querySelector(`[data-modal-field="${field}"]`);
            if (el && val !== undefined && val !== null) el.value = val;
        };

        if (entity === "student") {
            const parts = (dto.fullName || "").split(" ");
            set("fname", parts[0]);
            set("lname", parts.slice(1).join(" "));
            set("email", dto.email);
            set("phoneNumber", dto.phoneNumber);
            set("dateOfBirth", String(dto.dateOfBirth).slice(0, 10));
            set("gender", dto.gender || "");
            set("departmentId", departmentsCache.find(d => d.name === dto.departmentName)?.id ?? "");
            set("city", dto.address?.city);
            set("country", dto.address?.country);
        }
        if (entity === "course") {
            set("title", dto.title);
            set("description", dto.description);
            set("credits", dto.credits);
            set("durationInHours", dto.durationInHours);
            set("pricePerHour", dto.pricePerHour);
            set("departmentId", departmentsCache.find(d => d.name === dto.departmentName)?.id ?? "");
        }
        if (entity === "instructor") {
            const parts = (dto.fullName || "").split(" ");
            set("fname", parts[0]);
            set("lname", parts.slice(1).join(" "));
            set("email", dto.email);
            set("phoneNumber", dto.phoneNumber);
            set("dateOfBirth", String(dto.dateOfBirth).slice(0, 10));
            set("salary", dto.salary);
            set("departmentId", departmentsCache.find(d => d.name === dto.departmentName)?.id ?? "");
            set("city", dto.address?.city);
            set("country", dto.address?.country);
        }
        if (entity === "department") {
            set("name", dto.name);
            set("description", dto.description);
        }
    });
}

// ====================== Modal: payloads & submit ======================
function getField(field) {
    return document.querySelector(`[data-modal-field="${field}"]`)?.value ?? "";
}

function collectPayload() {
    if (modalEntity === "student") return {
        fname: getField("fname"), lname: getField("lname"),
        email: getField("email"), phoneNumber: getField("phoneNumber"),
        dateOfBirth: getField("dateOfBirth"), gender: getField("gender") || null,
        departmentId: +getField("departmentId") || null,
        address: { city: getField("city"), country: getField("country") }
    };
    if (modalEntity === "course") return {
        title: getField("title"), description: getField("description"),
        credits: +getField("credits"), durationInHours: +getField("durationInHours") || null,
        pricePerHour: +getField("pricePerHour") || null,
        departmentId: +getField("departmentId")
    };
    if (modalEntity === "instructor") return {
        fname: getField("fname"), lname: getField("lname"),
        email: getField("email"), phoneNumber: getField("phoneNumber"),
        dateOfBirth: getField("dateOfBirth"), salary: +getField("salary") || null,
        departmentId: +getField("departmentId") || null,
        address: { city: getField("city"), country: getField("country") }
    };
    if (modalEntity === "department") return {
        name: getField("name"), description: getField("description")
    };
    return null;
}

async function submitModal() {
    const payload = collectPayload();
    if (!payload) return;

    const actions = {
        student: { create: StudentAPI.create, update: StudentAPI.update, reload: () => loadStudents(state.students.page) },
        course: { create: CourseAPI.create, update: CourseAPI.update, reload: () => loadCourses(state.courses.page) },
        instructor: { create: InstructorAPI.create, update: InstructorAPI.update, reload: () => loadInstructors(state.instructors.page) },
        department: { create: DepartmentAPI.create, update: DepartmentAPI.update, reload: () => loadDepartments(state.departments.page) }
    };

    const action = actions[modalEntity];
    try {
        const res = modalId
            ? await action.update(modalId, payload)
            : await action.create(payload);

        if (res && res.message && !res.id) {
            showToast(res.message, false);
            return;
        }
        showToast(t("saved"));
        closeModal();
        action.reload();
    } catch (e) {
        showToast(t("error"), false);
    }
}

// ====================== Deletes ======================
async function deleteStudent(id) {
    if (!confirm(t("confirmDelete"))) return;
    await StudentAPI.delete(id);
    showToast(t("deleted"));
    loadStudents(state.students.page);
}
async function deleteCourse(id) {
    if (!confirm(t("confirmDelete"))) return;
    await CourseAPI.delete(id);
    showToast(t("deleted"));
    loadCourses(state.courses.page);
}
async function deleteInstructor(id) {
    if (!confirm(t("confirmDelete"))) return;
    await InstructorAPI.delete(id);
    showToast(t("deleted"));
    loadInstructors(state.instructors.page);
}
async function deleteDepartment(id) {
    if (!confirm(t("confirmDelete"))) return;
    await DepartmentAPI.delete(id);
    showToast(t("deleted"));
    loadDepartments(state.departments.page);
}

// ====================== Init ======================
async function init() {
    requireAuth();
    if (!isAdmin()) {
        window.location.href = "login.html";
        return;
    }

    applyLanguage();

    document.getElementById("langToggle").addEventListener("click", toggleLang);

    document.querySelectorAll(".admin-nav-item").forEach(el => {
        el.addEventListener("click", () => showSection(el.dataset.section));
    });

    const searchIds = {
        studentSearch: "students", courseSearch: "courses",
        instructorSearch: "instructors", departmentSearch: "departments"
    };
    Object.entries(searchIds).forEach(([inputId, key]) => {
        document.getElementById(inputId).addEventListener("input", debounce(e => {
            state[key].search = e.target.value;
            const loaders = { students: loadStudents, courses: loadCourses, instructors: loadInstructors, departments: loadDepartments };
            loaders[key](1);
        }));
    });

    document.getElementById("modalOverlay").addEventListener("click", e => {
        if (e.target === document.getElementById("modalOverlay")) closeModal();
    });

    const depRes = await DepartmentAPI.getAll({ pageNumber: 1, pageSize: 100 });
    departmentsCache = depRes?.data ?? [];

    showSection("dashboard");
}

init();