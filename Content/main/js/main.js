$(document).ready(function () {
    $(window).scroll(function () {
        // scroll navbar
        if (this.scrollY > 20) {
            $(".header .navbar").addClass("sticky");
        } else {
            $(".header .navbar").removeClass("sticky");
        }
        // scroll show button back to top
        if (this.scrollY > 500) {
            $(".back-to-top").addClass("show");
        } else {
            $(".back-to-top").removeClass("show");
        }
    });
    // back to top
    $(".back-to-top").click(function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
    });
    // active link
    $(".areas .list-group .list-group-item").click(function (e) {
        e.preventDefault();
        $(this).addClass("active").siblings().removeClass("active");
    });
    // slider
    var swiper = new Swiper(".swiper-container", {
        loop: true,
        slidesPerView: 5,
        spaceBetween: 0,
        autoplay: {
            delay: 4000,
        },
        navigation: {
            nextEl: ".right-button",
            prevEl: ".left-button",
        },
    });
});
