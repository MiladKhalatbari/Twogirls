// navbar fixed top in small screen
let scrollPos = 0;
window.addEventListener('scroll', function () {

    if ((document.body.getBoundingClientRect()).top > scrollPos) {

        let header = document.getElementById('myNavbar');
        header.style.position = "fixed";
        header.style.top = '0px'; header.style.left = '0px'; header.style.right = '0px';
    }
    else if (matchMedia('only screen and (max-width: 990px)').matches) {
        let header = document.getElementById('myNavbar');
        header.style.position = "relative";
        header.style.top = '0px';
    }

    scrollPos = (document.body.getBoundingClientRect()).top;
});
//  navbar fixed top in small screen end

// fill inside the heart icon after click (Like)
let heartIcons = document.querySelectorAll('.fa-heart');
heartIcons.forEach(heartIcon => {
    heartIcon.addEventListener("click", function () {
        if (heartIcon.classList.contains("fa-regular")) {
            heartIcon.classList.remove("fa-regular");
            heartIcon.classList.add("fa-solid");
        } else if (heartIcon.classList.contains("fa-solid")) {
            heartIcon.classList.remove("fa-solid");
            heartIcon.classList.add("fa-regular");
        }
    });
});
// fill inside the heart icon after click (Like) end 

// show and hide search fild 
let searchIcon = document.getElementById("searchIcon");
searchIcon.addEventListener('click', searchvisible)
function searchvisible() {

    if (window.getComputedStyle(searchBox).display === "none") {
        searchBox.style.display = "flex";
    }
    else {
        searchBox.style.display = "none";
    }
}
// show and hide search fild end

//collapse only in small screen
let coll = document.getElementsByClassName("collapsible");
let lastContent;
let lastCollapseBtn;
for (let i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        let content = this.nextElementSibling;

        if (content.style.display === "block" && matchMedia('only screen and (max-width: 575px)').matches) {
            content.style.display = "none";
        }
        else if (matchMedia('only screen and (max-width: 575px)').matches) {
            if (lastContent) {
                lastContent.style.display = "none"
                let tagI = lastCollapseBtn.querySelector('.fa-caret-down');
                if (tagI && tagI.classList.contains("fa-rotate-180")) {
                    tagI.classList.remove("fa-rotate-180");
                }
            }
            content.style.display = "block";
            lastContent = content;
            lastCollapseBtn = this;
        }
    });
}
//collapse only in small screen end


//dragable carousel whith both muse down and touch 
let wrappers = document.querySelectorAll(".wrapper");
wrappers.forEach(wrappers => {
    let carousel = wrappers.querySelector(".custom-carousel");
    let firstItem = carousel.querySelectorAll(".custom-carousel-item")[0];
    let arrowIcons = wrappers.querySelectorAll(".arrow");

    const showHideIcons = () => {
        let scrollwidth = carousel.scrollWidth - carousel.clientWidth;
        if (carousel.scrollLeft == 0) { arrowIcons[0].style.setProperty("display", "none", "important"); }
        else { arrowIcons[0].style.setProperty("display", "block", "important"); }
        if (carousel.scrollLeft >= scrollwidth) { arrowIcons[1].style.setProperty("display", "none", "important"); }
        else { arrowIcons[1].style.setProperty("display", "block", "important"); }

    };
    arrowIcons.forEach(Icon => {
        Icon.addEventListener("click", () => {
            firstItemWidth = firstItem.clientWidth;
            carousel.scrollLeft += Icon.classList.contains("fa-angle-left") ? -firstItemWidth : firstItemWidth;
            setTimeout(() => showHideIcons(), 1000);
        })
    })
    let isDragStart = false, prevPageX, prevScrollLeft;
    const dragStart = (e) => {
        isDragStart = true;
        prevPageX = e.pageX || e.touches[0].clientX;
        prevScrollLeft = carousel.scrollLeft;

    }
    const dragging = (e) => {

        if (!isDragStart) return;
        e.preventDefault();
        carousel.classList.add("dragging")
        let positionDiff = (e.pageX || e.touches[0].clientX) - prevPageX;
        carousel.scrollLeft = prevScrollLeft - positionDiff;
        setTimeout(() => showHideIcons(), 1000);
    }
    const dragStop = () => {
        isDragStart = false;
        carousel.classList.remove("dragging")
    }
    carousel.addEventListener("mousedown", dragStart)
    carousel.addEventListener("mousemove", dragging)
    carousel.addEventListener("mouseup", dragStop)
    carousel.addEventListener("mouseleave", dragStop)
    carousel.addEventListener("touchend", dragStop)
    carousel.addEventListener("touchstart", dragStart)
    carousel.addEventListener("touchmove", dragging)

})
//dragable carousel whith both muse down and touch end


const divmenudown = document.getElementById("navbarMenu");
divmenudown.addEventListener("click", menudownclick);

function menudownclick() {
    if (matchMedia('only screen and (max-width: 575px)').matches) {
        const divEYEGLASSES = document.getElementById("divEYEGLASSES");
        const dropdownEYEGLASSES = document.getElementById("dropdownEYEGLASSES");
        divEYEGLASSES.appendChild(dropdownEYEGLASSES);

        const divSUNGLASSES = document.getElementById("divSUNGLASSES");
        const dropdownSUNGLASSES = document.getElementById("dropdownSUNGLASSES");
        divSUNGLASSES.appendChild(dropdownSUNGLASSES);

        const divBRANDS = document.getElementById("divBRANDS");
        const dropdownBRANDS = document.getElementById("dropdownBRANDS");
        divBRANDS.appendChild(dropdownBRANDS);

    }

}


//show secound image in hover and first image after hover (for discounted product carousel)
function secondImg(e, src) {
    setTimeout(() => { e.src = src; }, 500);
}
function FirstImg(e, src) {
    setTimeout(() => { e.src = src }, 500);
}
//show secound image in hover and first image after hover (for discounted product carousel) end

// change language icon in navbar after selecting from dropdown
const languageDropdown = document.getElementById("languageDropdown");
const languages = languageDropdown.querySelectorAll('li');
languages.forEach(language => {
    language.addEventListener("click", function () {
        let languageLogo = language.querySelector('.lang-logo').cloneNode(true);
        if (languageLogo) {
            let languageBtn = document.getElementById("languageBtn")
            languageBtn.replaceChild(languageLogo, languageBtn.childNodes[1]);
        };
    })
});
// change language icon in navbar after selecting from dropdown

let btns = document.querySelectorAll('.add-to-cart');
btns.forEach(btn => {
    btn.addEventListener("click", addItemToCard)
})
function addItemToCard() {
    document.getElementById("basketDropdown").classList.add("show");
}

//document.getElementById('myNavbar').addEventListener('click', function () {
//    if (document.getElementById("basketDropdown").classList.contains("show")) {
//        document.getElementById("basketDropdown").classList.remove("show")
//    }
//})
