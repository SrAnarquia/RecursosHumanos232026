const ReclutamientoOverlayEdit = {
    edit(id) {

        document.getElementById('reclutamientoOverlayEdit')?.remove();

        fetch(`/DatosReclutamientoes/Edit/${id}`)
            .then(r => r.text())
            .then(html => {

                document.body.insertAdjacentHTML('beforeend', html);

                // 🔥 FORZAR VISIBILIDAD
                const overlay = document.getElementById('reclutamientoOverlayEdit');
                if (overlay) {
                    overlay.style.display = 'flex';
                }
            });
    },
    close() {
        document.getElementById('reclutamientoOverlayEdit')?.remove();
    }
};
