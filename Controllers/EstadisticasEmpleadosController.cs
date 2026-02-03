using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Models;
using RecursosHumanos.Models.ViewModels.Empleados;
using System.Globalization;


namespace RecursosHumanos.Controllers
{
    public class EstadisticasEmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadisticasEmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(int anio = 0, int mes = 0)
        {
            if (anio == 0) anio = DateTime.Now.Year;

            var vm = new EmpleadosEstadisticasVM
            {
                Anio = anio,
                Mes = mes,
                CursosPorMes = new(),
                VacacionesPorMes = new(),
                RazonesVacaciones = new(),
                IncidentesPorMes = new(),
                RazonesIncidentes = new()
            };

            // ================= CURSOS =================
            var cursos = await _context.Set<MesTotal>()
                .FromSqlRaw("EXEC Entrenamiento_Estadisticas @Anio={0}, @Mes={1}", anio, mes)
                .ToListAsync();

            vm.CursosPorMes = cursos.ToDictionary(x => $"Mes {x.Mes}", x => x.Total);
            vm.TotalCursos = cursos.Sum(x => x.Total);

            // ================= VACACIONES =================
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "Vacaciones_Estadisticas";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Anio", anio));
                cmd.Parameters.Add(new SqlParameter("@Mes", mes));

                await _context.Database.OpenConnectionAsync();
                using var reader = await cmd.ExecuteReaderAsync();

                // ---- 1er resultset: por mes
                while (await reader.ReadAsync())
                    vm.VacacionesPorMes.Add($"Mes {reader.GetInt32(0)}", reader.GetInt32(1));

                // ---- 2do resultset: razones
                await reader.NextResultAsync();
                while (await reader.ReadAsync())
                    vm.RazonesVacaciones.Add(reader.GetString(0), reader.GetInt32(1));
            }

            // ================= INCIDENTES =================
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "Incidentes_Estadisticas";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Anio", anio));
                cmd.Parameters.Add(new SqlParameter("@Mes", mes));

                using var reader = await cmd.ExecuteReaderAsync();

                // ---- 1er resultset: por mes
                while (await reader.ReadAsync())
                    vm.IncidentesPorMes.Add($"Mes {reader.GetInt32(0)}", reader.GetInt32(1));

                // ---- 2do resultset: razones
                await reader.NextResultAsync();
                while (await reader.ReadAsync())
                    vm.RazonesIncidentes.Add(reader.GetString(0), reader.GetInt32(1));
            }

            // ================= FILTROS =================
            vm.Anios = Enumerable.Range(2024, 3)
                .Select(y => new SelectListItem
                {
                    Value = y.ToString(),
                    Text = y.ToString(),
                    Selected = y == anio
                }).ToList();

            vm.Meses = Enumerable.Range(1, 12)
                .Select(m => new SelectListItem
                {
                    Value = m.ToString(),
                    Text = CultureInfo.GetCultureInfo("es-ES")
                        .DateTimeFormat.GetMonthName(m).ToUpper(),
                    Selected = m == mes
                }).ToList();

            return View(vm);
        }


    }
}
