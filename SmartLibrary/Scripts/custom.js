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

// Áp dụng Select2 cho các dropdown
$('.select2').select2({
    placeholder: "Chọn một hoặc nhiều mục",
    allowClear: true,
    width: 'resolve'
});

$(document).ready(function () {
    $(".btn-save").on("click", function () {
        $(this).html('<i class="fas fa-spinner fa-spin"></i> Đang lưu...').prop("disabled", true);
        $("form").submit();
    });
});


