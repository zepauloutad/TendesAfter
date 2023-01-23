//libraries
//------------------------------------------------
//AOS
AOS.init();
setTimeout(() => {
    AOS.refresh();
}, 1000);

//carousel - glide.js

var glide = new Glide(".glide", {
    type: "carousel",
    startAt: 0,
    gap: 10,
    autoplay: 4000,
    animationDuration: 2000,
    hoverpause: true,
    focusAt: "center",
});

glide.mount();
