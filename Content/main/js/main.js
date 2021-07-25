$(document).ready(function () {

    setTimeout(function () {
        $('#updateResutl').fadeOut('slow');
    }, 1000);

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
   
    $('.btn-area').click(function (e) {
        let id = $(this).attr("id");
        //callback
        $.ajax({
            url: '/Home/DisplayListStory?AreaID=' + id,
            type: 'POST',
            dataType: "json",
            success: function (response) {
                var obj = JSON.parse(response);
                let html = '';
                let containerArea = $("#container-area");
                obj.forEach((data) => {
                    if (data != null) {
                        var resultOfView = data.Review == "" ? "Chưa có review nào" : `<div class="badge mr-2 "><span>${data.StarOfReview}</span></div> ${data.Review}`;
                        var resultOfStar = data.Review != "" ? ` <div class="mb-1 rating">
                                                                    <i class="fas fa-star"></i>
                                                                    <span>${data.StarOfReview}</span>
                                                                </div>` : "";
                        html += `<div class="swiper-slide col-6 col-lg-3 p-2">
                        <a href="/store/index?storeID=${data.StoreID}">
                            <div class="slider-box">
                                <div class="logo" style="background-image: url('${data.Image}');">
                                    <div class="mask" >
                                        <div class="slot e-member"></div>
                                        <div class="slot order"></div>
                                        <div class="slot delivery"></div>
                                        ${resultOfStar}
                                    </div>
                                </div>
                                <div class="info">
                                    <div class="position">
                                        ${data.Place}
                                    </div>
                                    <div class="stitle">
                                        ${data.StoreName}
                                    </div>
                                    <div class="address">
                                        ${data.Address}
                                    </div>
                                    <div class="review mt-3">
                                        <!-- <i>Chua co review nao!</i> -->
                                        <div class="d-flex align-items-center">
                                            <div class="cmt">
                                                ${resultOfView}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>`
                    }
                })
                if (obj.length == 0) {
                    html += `<div class="alert alert-primary" role="alert">
                                  <strong>Xin lỗi quý khách!</strong> Hiện tại khu vực này chưa có quán ăn!
                                 </div>`;
                    $('#area-see-more').hide();
                }
                containerArea.html(html);
            }
        });
    }
    );
});

