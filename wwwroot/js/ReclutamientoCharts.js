document.addEventListener("DOMContentLoaded", function () {

    if (!window.reclutamientoChartsData) return;

    const data = window.reclutamientoChartsData;

    // ================= CONTACTADOS POR MES =================
    new Chart(document.getElementById('chartContactadosMes'), {
        type: 'line',
        data: {
            labels: data.meses,
            datasets: [{
                label: 'Contactados',
                data: data.contactadosPorMes,
                borderColor: '#0d6efd',
                backgroundColor: 'rgba(13,110,253,0.2)',
                fill: true,
                tension: 0.3
            }]
        }
    });

    // ================= CONTRATADOS POR MES =================
    new Chart(document.getElementById('chartContratadosMes'), {
        type: 'bar',
        data: {
            labels: data.meses,
            datasets: [{
                label: 'Contratados',
                data: data.contratadosPorMes,
                backgroundColor: '#198754'
            }]
        }
    });

    // ================= FUENTES =================
    new Chart(document.getElementById('chartFuentes'), {
        type: 'pie',
        data: {
            labels: data.fuentesLabels,
            datasets: [{
                data: data.fuentesData,
                backgroundColor: [
                    '#0d6efd', '#198754', '#ffc107',
                    '#dc3545', '#6f42c1', '#20c997'
                ]
            }]
        }
    });

    // ================= NO CONTRATADOS =================
    new Chart(document.getElementById('chartNoContratados'), {
        type: 'doughnut',
        data: {
            labels: data.noContratadosLabels,
            datasets: [{
                data: data.noContratadosData,
                backgroundColor: [
                    '#dc3545', '#ffc107', '#6c757d',
                    '#fd7e14', '#adb5bd'
                ]
            }]
        }
    });

});
