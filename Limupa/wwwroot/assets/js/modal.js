let btns = document.querySelectorAll(".Open-Modal");
let modalLocation = document.querySelector(".product-detail-modal");
btns.forEach(x => {
    x.addEventListener("click", function (e) {
        e.preventDefault();
        let prdId = x.getAttribute("data-id")
        let url = `https://localhost:7183/home/getproduct/${prdId}`;

        fetch(url)
            .then(response => response.text())
            .then(data =>{
            modalLocation.innerHTML = data;
        }) 
    })
})