const CursoOverlayDetails = {

    async open(idCurso) {

        const response = await fetch(`/Empleados/DetailsCurso?id=${idCurso}`);
        const html = await response.text();

        const container = document.getElementById("modalCursosContainer");
        container.innerHTML = html;
    },

    close() {
        const overlay = document.getElementById("cursoDetailsOverlay");
        if (overlay) overlay.remove();
    }
};
