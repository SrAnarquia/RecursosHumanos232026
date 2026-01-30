const CursoOverlayDelete = {

    async open(idCurso) {

        const response = await fetch(`/Empleados/DeleteCurso?id=${idCurso}`);
        const html = await response.text();

        document.getElementById("modalCursosContainer").innerHTML = html;
    },

    close() {
        const overlay = document.getElementById("cursoOverlayDelete");
        if (overlay) {
            overlay.style.display = "none";
            overlay.remove(); // opcional, puedes dejar solo hide
        }
    }
};

// Submit por AJAX
document.addEventListener("submit", async function (e) {

    if (e.target.id !== "formDeleteCurso") return;

    e.preventDefault();

    const form = e.target;

    const response = await fetch(form.action, {
        method: "POST",
        body: new FormData(form),
        headers: { "X-Requested-With": "XMLHttpRequest" }
    });

    const result = await response.json();

    if (result.success) {
        CursoOverlayDelete.close();
        location.reload();
    }
});
