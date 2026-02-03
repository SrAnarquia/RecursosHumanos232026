using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos.Models;
using RecursosHumanos.Models.ViewModels.Reclutamiento;
using System.Globalization;

namespace RecursosHumanos.Controllers
{
    public class EstadisticasReclutamientoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadisticasReclutamientoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int anio = 0, int mes = 0)
        {
            // 🔒 Validación clave
            if (mes > 0 && anio == 0)
                anio = DateTime.Now.Year;

            var query = _context.DatosReclutamientos
                .Include(x => x.IdEstatusNavigation)
                .AsQueryable();

            if (anio > 0)
                query = query.Where(x => x.FechaContacto.Year == anio);

            if (mes > 0)
                query = query.Where(x => x.FechaContacto.Month == mes);

            var datos = await query.ToListAsync();

            var vm = new ReclutamientoEstadisticasVM
            {
                Anio = anio,
                Mes = mes,

                TotalContratados = datos.Count(x => x.IdEstatusNavigation.Estatus1 == "CONTRATADO"),
                TotalNoContratados = datos.Count(x => x.IdEstatusNavigation.Estatus1 != "CONTRATADO"),

                RazonesNoContratacion = datos

                    .Where(x => x.IdEstatusNavigation.Estatus1 != "CONTRATADO")
                    .Where(x => !string.IsNullOrWhiteSpace(x.Comentarios))
                    .GroupBy(x => x.Comentarios.Trim())
                    .Select(g => new
                    {
                        Razon = g.Key,
                        Total = g.Count()
                    })
                    .OrderByDescending(x => x.Total)
                    .Take(5)
                    .ToDictionary(x => x.Razon, x => x.Total),

                // ===== AÑOS FIJOS =====
                Anios = Enumerable.Range(2025, 4)
                    .Select(y => new SelectListItem
                    {
                        Value = y.ToString(),
                        Text = y.ToString(),
                        Selected = y == anio
                    }).ToList(),

                // ===== MESES EN ESPAÑOL =====
                Meses = Enumerable.Range(1, 12)
                    .Select(m => new SelectListItem
                    {
                        Value = m.ToString(),
                        Text = CultureInfo
                            .GetCultureInfo("es-ES")
                            .DateTimeFormat
                            .GetMonthName(m)
                            .ToUpper(),
                        Selected = m == mes
                    }).ToList()
            };

            return View(vm);
        }
    }
}
