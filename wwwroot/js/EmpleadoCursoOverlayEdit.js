const CursoOverlayEdit = {

    async edit(id) {
        const response = await fetch(`/Empleados/EditCurso?id=${id}`);
        const html = await response.text();
        document.getElementById("modalCursosContainer").innerHTML = html;
    },

    close() {
        const overlay = document.getElementById("cursoOverlay");
        if (overlay) overlay.remove();
    }
};

document.addEventListener("submit", async function (e) {

    if (e.target.id !== "formCursoEdit") return;

    e.preventDefault();

    const form = e.target;

    const response = await fetch(form.action, {
        method: "POST",
        body: new FormData(form),
        headers: {
            "X-Requested-With": "XMLHttpRequest"
        }
    });

    const contentType = response.headers.get("content-type");
    const result = await response.text();

    if (contentType && contentType.includes("text/html")) {
        document.getElementById("modalCursosContainer").innerHTML = result;
    } else {
        CursoOverlayEdit.close();
        location.reload();
    }
});
