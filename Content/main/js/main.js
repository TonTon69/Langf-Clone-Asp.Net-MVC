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
    // load list story
    $('#btnshowdata').click(function LoadData() {
        $.ajax({
            url: '/Home/DisplayListStory?AreaID=' + '1',
            type: 'POST',
            dataType: "json",
            success: function (response) {
                var obj = JSON.parse(response);
                var res = [];
                for (var i in obj)// đưa json vừa convert vào mảng res[]
                {
                    res.push(obj[i]);
                }
                for (var i of res) {
                    console.log(i);
                }

            }
        });
    });
    //Load data area 
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
                        html += `<div class="swiper-slide col-6 col-lg-3 p-2">
                        <a href="/store?name=${data.StoreName}">
                            <div class="slider-box">
                                <div class="logo" style="background-image: url('${data.Image}')">
                                    <div class="mask" >
                                        <div class="slot e-member"></div>
                                        <div class="slot order"></div>
                                        <div class="slot delivery"></div>
                                        <div class="mb-1 rating">
                                            <i class="fas fa-star"></i>
                                            <span>5</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="info">
                                    <div class="position">
                                        ${data}
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
                                            <div class="badge mr-2">
                                                <span>5</span>
                                            </div>
                                            <div class="cmt">
                                                Chất lượng Ok nhưng
                                                hơi ngonn á nha shop
                                                ơi
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
                                 </div>`
                }
                containerArea.html(html);
            }
        });
        // Promise
        /*$.post('/Home/DisplayListStory?AreaID=' + id)
            .then(function (response) {
                var obj = JSON.parse(response);
                var res = [];
                for (var i in obj)// đưa json vừa convert vào mảng res[]
                {
                    res.push(obj[i]);
                }
                for (var i of res) {
                    console.log(i);
                } )
            .catch(function (err) {
                console.log(err);
            })*/
    }
    );
});

