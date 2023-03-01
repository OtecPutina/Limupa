let addToBasketBtns = document.querySelectorAll(".add-to-basket");


addToBasketBtns.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();

    let url = btn.getAttribute("href");
    fetch(url)
        .then(response => {
            if (response.status == 200) {
            } else {
                window.location.reload(true)
            }
        })
}))