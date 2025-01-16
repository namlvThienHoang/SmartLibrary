// Hiển thị nút khi cuộn xuống
$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {  // Nút sẽ hiển thị khi cuộn xuống 100px
        $('#backToTop').fadeIn();
    } else {
        $('#backToTop').fadeOut();
    }
});

// Cuộn về đầu trang khi nhấn vào nút
$('#backToTop').click(function () {
    $('html, body').animate({ scrollTop: 0 }, 800); // Cuộn về đầu trang trong 800ms
    return false;
});


document.addEventListener("DOMContentLoaded", function () {
    const themeToggleButton = document.getElementById("theme-toggle");
    const themeIcon = document.getElementById("theme-icon");
    const body = document.body;

    // Ensure that the themeIcon and body elements exist
    if (!themeIcon || !body) return;  // Exit if elements are not found

    // Check if there's a theme preference saved in localStorage
    if (localStorage.getItem("theme") === "dark") {
        body.classList.add("dark-theme");
        themeIcon.classList.remove("fas", "fa-sun");
        themeIcon.classList.add("fas", "fa-moon");
    } else {
        body.classList.add("light-theme");
        themeIcon.classList.remove("fas", "fa-moon");
        themeIcon.classList.add("fas", "fa-sun");
    }

    // Toggle theme on button click
    themeToggleButton.addEventListener("click", function () {
        if (body.classList.contains("light-theme")) {
            body.classList.replace("light-theme", "dark-theme");
            themeIcon.classList.replace("fa-sun", "fa-moon");
            localStorage.setItem("theme", "dark");
        } else {
            body.classList.replace("dark-theme", "light-theme");
            themeIcon.classList.replace("fa-moon", "fa-sun");
            localStorage.setItem("theme", "light");
        }
    });
});

// Áp dụng Select2 cho các dropdown
$('.select2').select2({
    placeholder: "Chọn một hoặc nhiều mục",
    allowClear: true,
    width: 'resolve'
});


