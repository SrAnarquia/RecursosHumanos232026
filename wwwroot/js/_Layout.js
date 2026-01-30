// Toggle sidebar
document.getElementById("btnToggle").addEventListener("click", function () {
    document.getElementById("sidebar").classList.toggle("collapsed");
});

// Toggle submenus
document.querySelectorAll(".menu-toggle").forEach(button => {
    button.addEventListener("click", function () {
        this.parentElement.classList.toggle("active");
    });
});
