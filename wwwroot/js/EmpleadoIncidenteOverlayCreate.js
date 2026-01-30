const IncidenteOverlay = {

    async open(idPersona) {

        const response = await fetch(`/Empleados/CreateIncidente?idPersona=${idPersona}`);
        const html = await response.text();

        document.getElementById("modalIncidenteContainer").innerHTML = html;
    },

    close() {
        const overlay = document.getElementById("incidenteOverlay");
        if (overlay) overlay.remove();
    }
};

document.addEventListener("submit", async function (e) {

    if (e.target.id !== "formIncidente") return;

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
        document.getElementById("modalIncidenteContainer").innerHTML = result;
    } else {
        IncidenteOverlay.close();
        location.reload();
    }
});
