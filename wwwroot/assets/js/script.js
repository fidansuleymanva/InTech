const navbarToggleBtn = document.getElementById("navbar-toggle-btn");
const navbarMenu = document.getElementsByClassName("navbar-menu")[0];
navbarToggleBtn.addEventListener("click", () => {
    navbarMenu.classList.toggle("open");
});

const starRatings = document.querySelectorAll('.star-rating');

starRatings.forEach((starRating) => {
    const stars = starRating.querySelectorAll('input');

    stars.forEach((star, index) => {
        star.addEventListener('click', (e) => {
            const rating = e.target.value;
            const testimonial = e.target.closest('.testimonials-col');
            const testimonialName = testimonial.querySelector('h3').textContent;

            // Burada değerlendirmeyi saklamak için yapılması gereken işlemleri gerçekleştirebilirsiniz
            // Örneğin, değerleri bir diziye veya bir veritabanına kaydedebilirsiniz
            console.log(`Değerlendirme: ${rating} - İsim: ${testimonialName}`);
        });
    });
});




$('.team-carousel').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    infinite: true,
    autoplay: true,
    autoplaySpeed: 2000,
    prevArrow: '<div class="slick-prev">&#8249;</div>',
    nextArrow: '<div class="slick-next">&#8250;</div>'
});






