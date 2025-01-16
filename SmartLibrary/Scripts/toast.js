function showToast(title, message, type) {
    const toastTitle = document.getElementById("toastTitle");
    const toastMessage = document.getElementById("toastMessage");
    const toastNotification = document.getElementById("toastNotification");

    // Cập nhật nội dung
    toastTitle.textContent = title;
    toastMessage.textContent = message;

    // Thay đổi kiểu theo `type`
    if (type === "success") {
        toastNotification.classList.add("bg-success", "text-white");
    } else if (type === "error") {
        toastNotification.classList.add("bg-danger", "text-white");
    } else if (type === "warning") {
        toastNotification.classList.add("bg-warning", "text-dark");
    } else {
        toastNotification.classList.add("bg-light", "text-dark");
    }

    // Hiển thị Toast
    const toast = new bootstrap.Toast(toastNotification);
    toast.show();
}
