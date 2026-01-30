const VacacionesOverlayCreate = {

    open() {
        fetch('/Vacacions/Create')
            .then(r => r.text())
            .then(html => {
                let container = document.getElementById('vacacionesOverlayContainer');
                if (!container) {
                    container = document.createElement('div');
                    container.id = 'vacacionesOverlayContainer';
                    document.body.appendChild(container);
                }

                container.innerHTML = html;

                const overlay = document.getElementById('vacacionesOverlay');
                if (overlay) overlay.classList.add('show');
            });
    },

    close() {
        const overlay = document.getElementById('vacacionesOverlay');
        if (overlay) overlay.remove();
    }
};


// INTERCEPTA EL SUBMIT DEL FORM DEL PARTIAL
document.addEventListener('submit', function (e) {

    if (e.target.id !== 'formCrearVacacion') return;

    e.preventDefault();

    const form = e.target;

    fetch(form.action, {
        method: 'POST',
        body: new FormData(form),
        credentials: 'same-origin'
    })
        .then(r => {
            VacacionesOverlayCreate.close();
            location.reload(); // 👈 AQUÍ
        })

        .catch(() => {
            // incluso si hay error
            VacacionesOverlayCreate.close();
        });

});
