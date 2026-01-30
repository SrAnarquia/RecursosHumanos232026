const VacacionesOverlayDelete = {
    open(id) {
        fetch(`/Vacacions/DeletePartial/${id}`, {
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        })
            .then(r => r.text())
            .then(html => {
                let container = document.getElementById('vacacionesOverlayContainer');
                if (!container) {
                    container = document.createElement('div');
                    container.id = 'vacacionesOverlayContainer';
                    document.body.appendChild(container);
                }
                container.innerHTML = html;
                const overlay = document.getElementById('vacacionesOverlayDelete');
                if (overlay) overlay.style.display = 'flex';
            });
    },
    close() {
        const container = document.getElementById('vacacionesOverlayContainer');
        if (container) container.innerHTML = '';
    }
};
