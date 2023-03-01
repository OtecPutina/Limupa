let btns = document.querySelectorAll(".Open-Single");
let modalLocation = document.querySelector(".single-product-partial");
btns.forEach(x => {
    x.addEventListener("click", function (e) {
        e.preventDefault();
        let prdId = x.getAttribute("data-id")
        let url = `https://localhost:7183/home/getsingleproduct/${prdId}`;

        fetch(url)
            .then(response => response.text())
            .then(data => {
                modalLocation.innerHTML = data;
            })
    })
})