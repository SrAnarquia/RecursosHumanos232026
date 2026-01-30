const CursoOverlay = {

    async open(idPersona) {

        const response = await fetch(`/Empleados/CreateCurso?idPersona=${idPersona}`);
        const html = await response.text();

        const container = document.getElementById("modalCursosContainer");
        container.innerHTML = html;
    },

    close() {
        const overlay = document.getElementById("cursoOverlay");
        if (overlay) overlay.remove();
    }
};

// 👇 INTERCEPTAR SUBMIT DEL FORMULARIO
document.addEventListener("submit", async function (e) {

    if (e.target.id !== "formCurso") return;

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

    // Si devuelve HTML → hay errores de validación
    if (contentType && contentType.includes("text/html")) {
        document.getElementById("modalCursosContainer").innerHTML = result;
    } else {
        // ✔ Guardado correcto
        CursoOverlay.close();
        location.reload(); // o refresca solo la tabla si luego quieres optimizar
    }
});
