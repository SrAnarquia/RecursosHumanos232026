const EmpleadoResumen = {

    open(id) {
        fetch(`/Empleados/Resumen?id=${id}`)
            .then(r => r.text())
            .then(html => {
                let container = document.getElementById('empleadoResumenContainer');

                if (!container) {
                    container = document.createElement('div');
                    container.id = 'empleadoResumenContainer';
                    document.body.appendChild(container);
                }

                container.innerHTML = html;

                const overlay = document.getElementById('empleadoOverlay');
                if (overlay) overlay.classList.add('show');
            });
    },

    close() {
        const overlay = document.getElementById('empleadoOverlay');
        if (overlay) overlay.remove();
    }
};
