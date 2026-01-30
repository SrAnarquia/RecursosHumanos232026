const IncidenteOverlayDelete = {

    async open(idIncidente) {

        const response = await fetch(`/Empleados/DeleteIncidente?id=${idIncidente}`);
        const html = await response.text();

        let container = document.getElementById("modalIncidenteContainer");
        if (!container) {
            container = document.createElement("div");
            container.id = "modalIncidenteContainer";
            document.body.appendChild(container);
        }

        container.innerHTML = html;
    },

    close() {
        const overlay = document.getElementById("incidenteOverlayDelete");
        if (overlay) {
            overlay.style.display = "none";
            overlay.remove();
        }
    }
};

// SUBMIT AJAX
document.addEventListener("submit", async function (e) {

    if (e.target.id !== "formDeleteIncidente") return;

    e.preventDefault();

    const form = e.target;

    const response = await fetch(form.action, {
        method: "POST",
        body: new FormData(form),
        headers: { "X-Requested-With": "XMLHttpRequest" }
    });

    const result = await response.json();

    if (result.success) {
        IncidenteOverlayDelete.close();
        location.reload();
    }
});
