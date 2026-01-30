const ReclutamientoOverlayDelete = {
    open(id) {

        document.getElementById('reclutamientoOverlayDelete')?.remove();

        fetch(`/DatosReclutamientoes/DeletePartial/${id}`)
            .then(r => r.text())
            .then(html => {

                document.body.insertAdjacentHTML('beforeend', html);

                // 🔥 FORZAR VISIBILIDAD
                const overlay = document.getElementById('reclutamientoOverlayDelete');
                if (overlay) {
                    overlay.style.display = 'flex';
                }
            });
    },

    close() {
        document.getElementById('reclutamientoOverlayDelete')?.remove();
    }
};
