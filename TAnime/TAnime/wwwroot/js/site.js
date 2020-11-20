var init = init || {};

init.slideTransition = () =>{
    $('#recipeCarousel').carousel({
        interval: 2000
    });

    $('.carousel .carousel-item').each(function () {
        var next = $(this).next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }
        next.children(':first-child').clone().appendto($(this));

        for (var i = 0; i < 2; i++) {
            next = next.next();
            if (!next.length) {
                next = $(this).siblings(':first');
            }

            next.children(':first-child').clone().appendto($(this));
        }
    });
}


init.slide = () =>{
    $.ajax({
        url: `/Home/IndexOfSlides`,
        method: 'GET',
        dataType: 'JSON',
        success: (response) => {
            //$('#recipeCarousel > .carousel-inner').empty();
            $.each(response.data, (i, v) => {
                console.log(v.MovieId);
                $('#recipeCarousel > .carousel-inner').append(
                    `   
                        <div class="carousel-item product">
                            <a href="/Home/Detail/${v.movieId}">
                                <img class="d-block"
                                     src="images/${v.imageOfVideo}">
                                <div>
                                    <img src="images/overlays-video.png" class="overlay" />
                                </div>
                                <p class="pr-2" style="overflow:hidden;height:35px">${v.movieName}</p>
                            </a>
                        </div>
                    `
                );
            });
            defaultActive();
        }
    });
}


function defaultActive() {
    var ImgItem = document.querySelectorAll('.carousel-item')[0];
    ImgItem.classList.add('active');
}

$(document).ready(() => {
    init.slide();
    init.slideTransition();
})