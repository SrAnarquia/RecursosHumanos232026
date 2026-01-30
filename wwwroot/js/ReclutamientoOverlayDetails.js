var ReclutamientoOverlayDetails = (function () {

    function open(id) {
        close(); // evita overlays duplicados

        fetch(`/DatosReclutamientoes/DetailsPartial/${id}`)
            .then(res => res.text())
            .then(html => {
                document.body.insertAdjacentHTML("beforeend", html);
            });
    }

    function close() {
        document.querySelectorAll(".reclutamiento-overlay-details")
            .forEach(o => o.remove());
    }

    return {
        open,
        close
    };
})();
