console.log("AlertasDetailsOverlay cargado");

var AlertasOverlayDetails = (function () {

    function open(id) {
        close();

        fetch(`/Alertas/Details?id=${id}`)
            .then(res => res.text())
            .then(html => {
                document.body.insertAdjacentHTML("beforeend", html);
            });
    }

    function close() {
        document.querySelectorAll(".alertas-overlay-details")
            .forEach(o => o.remove());
    }

    return {
        open,
        close
    };

})();
