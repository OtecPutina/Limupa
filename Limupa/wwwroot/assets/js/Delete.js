let deleteBtns = document.querySelectorAll(".delete-parent");

deleteBtns.forEach(btn => btn.addEventListener("click", function () {
    btn.parentElement.remove()
}))