var VacacionesDiasOverlay = (function () {

    function open(idEmpleado) {
        close();

        fetch(`/Empleados/DiasVacacionesEditPartial/${idEmpleado}`)
            .then(res => {
                if (!res.ok) throw new Error();
                return res.text();
            })
            .then(html => {
                document.body.insertAdjacentHTML("beforeend", html);
            })
            .catch(() => { });
    }

    function close() {
        document.querySelectorAll(".reclutamiento-overlay")
            .forEach(o => o.remove());
    }

    function save() {

        const form = document.getElementById("formDiasVacaciones");
        const data = new FormData(form);

        fetch("/Empleados/GuardarDiasVacaciones", {
            method: "POST",
            body: data
        })
            .then(res => {
                if (!res.ok) throw new Error();
                close();
            })
            .catch(() => { });
    }

    return {
        open,
        close,
        save
    };

})();
