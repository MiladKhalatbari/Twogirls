let collapseContainers = document.querySelectorAll(".collapse-container");
var lastCollapseContent;
var icon;
collapseContainers.forEach(collapseContainer => {
    let collapseToggle = collapseContainer.querySelector(".collapse-toggle");
    collapseToggle.addEventListener("click", function () {
        let collapseContent = collapseContainer.querySelector(".collapse-content");

        if (collapseContent.style.display === "flex") {
            collapseContent.style.display = "none";

            icon = this.querySelector(".fa-caret-down")
            if (icon) {
                icon.classList.add("fa-caret-right");
                icon.classList.remove("fa-caret-down");
            }
        }
        else {
            lastCollapseContent ? lastCollapseContent.style.display = "none" : "";
            if (icon) {
                icon.classList.add("fa-caret-right");
                icon.classList.remove("fa-caret-down");
            }


            collapseContent.style.display = "flex";
            icon = this.querySelector(".fa-caret-right")
            if (icon) {
                icon.classList.remove("fa-caret-right");
                icon.classList.add("fa-caret-down");
            }
            lastCollapseContent = collapseContent;
        }
    });
});



let searchContainer = document.getElementById("header-search-area");
let searchIcon = searchContainer.querySelector(".search-show");
let searchBox = searchContainer.querySelector(".search-area");
let searchBoxClose = searchContainer.querySelector(".fa-close");
searchIcon.addEventListener("click", function () {
    searchBox.style.display = "inline";
    searchIcon.style.display = "none";
});
searchBoxClose.addEventListener("click", function () {
    searchIcon.style.display = "inline";
    searchBox.style.display = "none";
});

let toggleOn = document.getElementById("toggle-on");
let toggleOff = document.getElementById("toggle-off");
let sidebarMenu = document.getElementById("sidebar-menu");
let mainBody = document.querySelector(".main-body");
toggleOn.addEventListener("click", function () {
    sidebarMenu.style.display = "none";
    toggleOff.style.display = "inline";
    toggleOn.style.display = "none";
    mainBody.style.width = "100%";
    mainBody.style.maxWidth = "100%";
});
toggleOff.addEventListener("click", function () {
    toggleOff.style.display = "none";
    toggleOn.style.display = "inline";
    sidebarMenu.style.display = "block";
    mainBody.style.width = "84%";
    mainBody.style.maxWidth = "calc(100% - 14rem)";

});

document.getElementById("second-header-show").addEventListener("click", function () {
    let secondHeader = document.querySelector(".white-header");
    if (secondHeader.style.display === "flex") {
        secondHeader.style.display = "none";
    }
    else {
        secondHeader.style.display = "flex";
    }
});



let toggleCompress = document.getElementById("toggle-compress");
let toggleExpand = document.getElementById("toggle-expand");
toggleCompress.addEventListener("click", function () {
    toggleCompress.style.display = "none";
    toggleExpand.style.display = "inline";
    if(document.cancelFullScreen)
    {
       document.cancelFullScreen();
    }
    else if(document.mozCancelFullScreen)
    {
       document.mozCancelFullScreen();
    }
    else if(document.webkitCancelFullScreen)
    {
       document.webkitCancelFullScreen();
    }
   
});

toggleExpand.addEventListener("click", function () {
    toggleExpand.style.display = "none";
    toggleCompress.style.display = "inline";
    if ((document.fullScreenElement && document.fullScreenElement !== null) || (!document.mozFullScreen && !document.webkitIsFullScreen)) {
        if (document.documentElement.requestFullscreen) {
            document.documentElement.requestFullscreen();
        }
        else if (document.documentElement.mozRequestFullscreen) {
            document.documentElement.mozRequestFullscreen();
        }
        else if (document.documentElement.webkitRequestFullscreen) {
            document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
        }
    }
});

//multiple image uploder
function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#admin-user-avatar-image').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
$("#admin-avatar-user-input").change(function () {
    readURL(this);
});


document.addEventListener('DOMContentLoaded', function () {
    ImgUpload();
});

function ImgUpload() {
    var imgWrap = "";
    var imgArray = [];

    var inputFiles = document.querySelectorAll('.upload__inputfile');

    inputFiles.forEach(function (input) {
        input.addEventListener('change', function (e) {
            imgWrap = this.closest('.upload__box').querySelector('.upload__img-wrap');
            var maxLength = this.dataset.max_length;

            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);
            // Clear the existing display
            imgWrap.innerHTML = "";
            filesArr.forEach(function (f, index) {
                if (!f.type.match('image.*')) {
                    return;
                }

                if (imgArray.length > maxLength) {
                    return false;
                } else {
                    var len = imgArray.filter(function (item) {
                        return item !== undefined;
                    }).length;

                    if (len > maxLength) {
                        return false;
                    } else {
                        imgArray.push(f);

                        var reader = new FileReader();
                        reader.onload = function (e) {
                            var html = "<div class='upload__img-box'> <div style='background-image: url(" + e.target.result + ")' data-number='" + document.querySelectorAll('.upload__img-close').length + "' data-file='" + f.name + "' class='img-bg'></div></div>";
                            imgWrap.insertAdjacentHTML('beforeend', html);
                        }
                        reader.readAsDataURL(f);
                    }

                }
            });
        });
    });

    document.body.addEventListener('click', function (e) {
        if (e.target.classList.contains('upload__img-close')) {
            var file = e.target.parentElement.dataset.file;
            for (var i = 0; i < imgArray.length; i++) {
                if (imgArray[i].name === file) {
                    imgArray.splice(i, 1);
                    break;
                }
            }
            e.target.parentElement.parentElement.remove();
        }
    });

};
//multiple image uploder end

//Delete image AJAX
function DeletePhoto(id) {
    if (confirm('Are you sure you want to remove this Photo?')) {

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Admin/Products/CreateOrEdit?handler=PhotoDelete&id=' + id, true);

        // Get the anti-forgery token value
        var form = document.querySelector('#CreateOrEditProductForm');
        var tokenValue = form.querySelector('input[name="__RequestVerificationToken"]').value;

        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.setRequestHeader('RequestVerificationToken', tokenValue);

        xhr.onload = function () {
            if (xhr.status === 200) {
                // Handle the success response
                console.log(xhr.responseText);
            } else {
                // Handle the error response
                console.log('Error:', xhr.status);
            }
        };

        xhr.onerror = function () {
            // Handle the error response
            console.log('Request failed');
        };

        xhr.send();
    }
}
//Delete image AJAX end
