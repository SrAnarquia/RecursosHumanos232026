var ReclutamientoOverlayDetails = (function () {

    function open(id) {
        close();

        fetch(`/DatosReclutamientoes/DetailsPartial/${id}`)
            .then(res => {
                if (!res.ok) throw new Error();
                return res.text();
            })
            .then(html => {
                document.body.insertAdjacentHTML("beforeend", html);
            })
            .catch(() => { });
    }

    function close() {
        document.querySelectorAll(".reclutamiento-overlay")
            .forEach(o => o.remove());
    }

    return {
        open,
        close
    };

})();
