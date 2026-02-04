using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Models;
using RecursosHumanos.Models.ViewModels.Vacaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static RecursosHumanos.Models.Vacacion;

namespace RecursosHumanos.Controllers
{
    public class VacacionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacacionsController(ApplicationDbContext context)
        {
            _context = context;
        }


        #region Index

        [Authorize]
        public async Task<IActionResult> Index(VacacionIndexVM filtro, int pagina = 1)
        {
            int pageSize = 10;

            //Se obtiene de cookie el usuario
            int usuarioId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                );

            var query = _context.Vacacions
                .Include(v => v.IdAprobadoNavigation)
                .Include(v => v.IdRazonNavigation)
                .Where(v=> v.CreadoPor==usuarioId)
                .AsQueryable();

            // ===== FILTROS =====
            if (!string.IsNullOrEmpty(filtro.Nombre))
                query = query.Where(v => v.Nombre.Contains(filtro.Nombre));

            if (!string.IsNullOrEmpty(filtro.Departamento))
                query = query.Where(v => v.Departamento.Contains(filtro.Departamento));

            if (filtro.FechaDesde.HasValue)
                query = query.Where(v => v.FechaInicio >= filtro.FechaDesde);

            if (filtro.FechaHasta.HasValue)
                query = query.Where(v => v.FechaFinalizacion <= filtro.FechaHasta);

            int totalRegistros = await query.CountAsync();

            var datos = await query
                .OrderByDescending(v => v.FechaCreacion)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var model = new VacacionIndexVM
            {
                Datos = datos,
                Nombre = filtro.Nombre,
                Departamento = filtro.Departamento,
                FechaDesde = filtro.FechaDesde,
                FechaHasta = filtro.FechaHasta,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize)
            };

            return View(model);
        }
        #endregion

        #region Aprobaciones

        [Authorize]
        public async Task<IActionResult> Aprobaciones(VacacionIndexVM filtro, int pagina = 1)
        {
            int pageSize = 10;

            var query = _context.Vacacions
                .Include(v => v.IdAprobadoNavigation)
                .Include(v => v.IdRazonNavigation)
                .AsQueryable();

            // ===== FILTROS =====
            if (!string.IsNullOrEmpty(filtro.Nombre))
                query = query.Where(v => v.Nombre.Contains(filtro.Nombre));

            if (!string.IsNullOrEmpty(filtro.Departamento))
                query = query.Where(v => v.Departamento.Contains(filtro.Departamento));

            if (filtro.FechaDesde.HasValue)
                query = query.Where(v => v.FechaInicio >= filtro.FechaDesde);

            if (filtro.FechaHasta.HasValue)
                query = query.Where(v => v.FechaFinalizacion <= filtro.FechaHasta);

            int totalRegistros = await query.CountAsync();

            var datos = await query
                .OrderByDescending(v => v.FechaCreacion)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var model = new VacacionIndexVM
            {
                Datos = datos,
                Nombre = filtro.Nombre,
                Departamento = filtro.Departamento,
                FechaDesde = filtro.FechaDesde,
                FechaHasta = filtro.FechaHasta,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize)
            };

            return View(model); // Esto te lleva a la vista Aprobacion.cshtml
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Aprobar(int id)
        {
            var vacacion = await _context.Vacacions
                .Include(v => v.IdAprobadoNavigation)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vacacion == null)
                return NotFound();

            // Crear registro de aprobación
            var aprobacion = new Aprobacion
            {
                IdPersona = 1, // <-- aquí iría el ID del usuario que aprueba
                Estatus = true
            };

            _context.Aprobacions.Add(aprobacion);
            await _context.SaveChangesAsync();

            // Asociar la aprobación con la solicitud
            vacacion.IdAprobado = aprobacion.Id;
            _context.Vacacions.Update(vacacion);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Rechazar(int id)
        {
            var vacacion = await _context.Vacacions
                .Include(v => v.IdAprobadoNavigation)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vacacion == null)
                return NotFound();

            // Crear registro de rechazo
            var aprobacion = new Aprobacion
            {
                IdPersona = 1, // <-- aquí iría el ID del usuario que rechaza
                Estatus = false
            };

            _context.Aprobacions.Add(aprobacion);
            await _context.SaveChangesAsync();

            vacacion.IdAprobado = aprobacion.Id;
            _context.Vacacions.Update(vacacion);
            await _context.SaveChangesAsync();

            return Ok();
        }


        #endregion


        #region DeletesPartial


        #region DeletePartialGet
        [Authorize]

        public async Task<IActionResult> DeletePartial(int? id)
        {
            if (id == null) return NotFound();

            var vacacion = await _context.Vacacions
                .Include(v => v.IdAprobadoNavigation)
                .Include(v => v.IdRazonNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vacacion == null) return NotFound();

            var model = new VacacionDeleteVM
            {
                Datos = vacacion,
                MostrarModal = true
            };

            return PartialView("_VacacionesDeletePartial", model);
        }

        #endregion

        #region DeletePartialPost
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedPartial(int id)
        {
            var vacacion = await _context.Vacacions.FindAsync(id);

            if (vacacion != null)
            {
                _context.Vacacions.Remove(vacacion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }




        #endregion
        #endregion

        #region AprobarVista


        #region aprobarGet
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AprobarGet(int id)
        {
            var vacacion = await _context.Vacacions
                .Include(v => v.IdRazonNavigation)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vacacion == null)
                return NotFound();

            var vm = new VacacionAprobarVM
            {
                Id = vacacion.Id,
                Nombre = vacacion.Nombre,
                Departamento = vacacion.Departamento,
                FechaInicio = vacacion.FechaInicio,
                FechaFinalizacion = vacacion.FechaFinalizacion,
                Razon = vacacion.IdRazonNavigation?.Razon,
                Detalles = vacacion.Detalles
            };

            return PartialView("_AprobarVacacion", vm);
        }

        #endregion


        #region aprobarPost
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AprobarPost(VacacionAprobarVM model)
        {
            var vacacion = await _context.Vacacions
                .FirstOrDefaultAsync(v => v.Id == model.Id);

            if (vacacion == null)
                return NotFound();

            var aprobacion = new Aprobacion
            {
                IdPersona = model.Id, // usuario logueado
                Estatus = true
            };

            _context.Aprobacions.Add(aprobacion);
            await _context.SaveChangesAsync();

            vacacion.IdAprobado = aprobacion.Id;
            _context.Vacacions.Update(vacacion);
            await _context.SaveChangesAsync();

            // 🔁 REDIRECCIÓN
            return RedirectToAction(nameof(Aprobaciones));
        }




        #endregion


        #endregion


        #region RechazarVista

        #region rechazarGet
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RechazarGet(int id)
        {
            var vacacion = await _context.Vacacions
                .Include(v => v.IdRazonNavigation)
                .FirstOrDefaultAsync(v => v.Id==id);


            if (vacacion == null)
                return NotFound();


            var vm = new VacacionRechazarVM
            {
                Id = vacacion.Id,
                Nombre=vacacion.Nombre,
                Departamento = vacacion.Departamento,
                FechaInicio=vacacion.FechaInicio,
                FechaFinalizacion=vacacion.FechaFinalizacion,
                Razon = vacacion.IdRazonNavigation?.Razon,
                Detalles=vacacion.Detalles


            };


            return PartialView("_RechazarVacacion",vm);
        }

        #endregion

        #region rechazarPost

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RechazarPost(VacacionRechazarVM model) 
        {

            var vacacion = await _context.Vacacions
                .FirstOrDefaultAsync(v => v.Id == model.Id);


            if (vacacion==null)
                return NotFound();

            var Aprobacion = new Aprobacion
            {
                IdPersona = model.Id, 
                Estatus = false
            };


            _context.Aprobacions.Add(Aprobacion);
            await _context.SaveChangesAsync();

            vacacion.IdAprobado = Aprobacion.Id;
            _context.Vacacions.Update(vacacion);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Aprobaciones));

        }



        #endregion



        #endregion

        #region CreatesPartial

        #region CreatePartialGet

        [Authorize]
        public IActionResult Create()
        {
            // 1️⃣ Obtener Id del usuario desde la cookie
            int usuarioId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            // 2️⃣ Obtener usuario desde BD
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Id == usuarioId);

            // 3️⃣ Crear el modelo
            var model = new VacacionCreateVM
            {
                Nuevo = new Vacacion
                {
                    // Autorellenado
                    Nombre = usuario?.Usuario1,              // o usuario?.NombreCompleto
                    Departamento = usuario?.Departamento     // puede ser null, no pasa nada
                },

                Razones = Enum.GetValues(typeof(RazonVacaciones))
                    .Cast<RazonVacaciones>()
                    .Select(r => new SelectListItem
                    {
                        Value = ((int)r).ToString(),
                        Text = r.ToString()
                                .Replace("AsuntosPersonales", "Asuntos personales")
                    })
                    .ToList()
            };

            return PartialView("_VacacionesCrearPartial", model);
        }

        #endregion


        #region CreatePartialPost
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VacacionCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    foreach (var e in error.Value.Errors)
                    {
                        Console.WriteLine($"{error.Key}: {e.ErrorMessage}");
                    }
                }

                return PartialView("_VacacionesCrearPartial", model);
            }

            //Se obtiene de cookie el usuario
            int usuarioId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                );

            //Se obtiene el nombre del usuario

            string nombreCompleto = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId).Nombre.ToString();

            //Se agrega quien lo creo dependiendo de la sesion
            model.Nuevo.CreadoPor = usuarioId;

            //Se agrega el nombre de quien lo crea:
            model.Nuevo.Nombre = nombreCompleto;

            model.Nuevo.FechaCreacion = DateTime.Now;
            _context.Vacacions.Add(model.Nuevo);
            await _context.SaveChangesAsync();

            return Ok(); // ✅ devuelvo solo 200 OK
         }
        #endregion

        #endregion


        #region EditsPartial

        #region EditPartialGet

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacacion = await _context.Vacacions.FindAsync(id);
            if (vacacion == null)
            {
                return NotFound();
            }
            ViewData["IdAprobado"] = new SelectList(_context.Aprobacions, "Id", "Id", vacacion.IdAprobado);
            ViewData["IdRazon"] = new SelectList(_context.Razones, "Id", "Id", vacacion.IdRazon);
            return View(vacacion);
        }

        #endregion

        #region EditPartialPost
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Departamento,Detalles,FechaInicio,FechaFinalizacion,IdRazon,IdAprobado,FechaCreacion")] Vacacion vacacion)
        {
            if (id != vacacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacacionExists(vacacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAprobado"] = new SelectList(_context.Aprobacions, "Id", "Id", vacacion.IdAprobado);
            ViewData["IdRazon"] = new SelectList(_context.Razones, "Id", "Id", vacacion.IdRazon);
            return View(vacacion);
        }
        #endregion


        #endregion

        #region DeletesPartial

        #region DeletePartialGet
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacacion = await _context.Vacacions
                .Include(v => v.IdAprobadoNavigation)
                .Include(v => v.IdRazonNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacacion == null)
            {
                return NotFound();
            }

            return View(vacacion);
        }
        #endregion


        #region DeletePartialPost
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacacion = await _context.Vacacions.FindAsync(id);
            if (vacacion != null)
            {
                _context.Vacacions.Remove(vacacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #endregion



        #region ExportToExcel

        //Tipo De vista
        public enum TipoExportVacaciones
        {
            Index,
            Aprobaciones
        }



        //Filtrado Segun vista
        private IQueryable<Vacacion> BuildVacacionesQuery(VacacionIndexVM filtro,TipoExportVacaciones tipo)
        {
            var query = _context.Vacacions
                .Include(v => v.IdAprobadoNavigation)
                .Include(v => v.IdRazonNavigation)
                .AsQueryable();

            // 👉 Index: solo las creadas por el usuario logeado
            if (tipo == TipoExportVacaciones.Index)
            {
                int usuarioId = int.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                );

                query = query.Where(v => v.CreadoPor == usuarioId);
            }

            // ===== FILTROS COMUNES =====
            if (!string.IsNullOrEmpty(filtro.Nombre))
                query = query.Where(v => v.Nombre.Contains(filtro.Nombre));

            if (!string.IsNullOrEmpty(filtro.Departamento))
                query = query.Where(v => v.Departamento.Contains(filtro.Departamento));

            if (filtro.FechaDesde.HasValue)
                query = query.Where(v => v.FechaInicio >= filtro.FechaDesde);

            if (filtro.FechaHasta.HasValue)
                query = query.Where(v => v.FechaFinalizacion <= filtro.FechaHasta);

            return query;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Export(VacacionIndexVM filtro,TipoExportVacaciones tipo)
        {
            var datos = await BuildVacacionesQuery(filtro, tipo)
                .OrderByDescending(v => v.FechaCreacion)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Vacaciones");

            // 🔹 ENCABEZADOS
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Nombre";
            worksheet.Cell(1, 3).Value = "Departamento";
            worksheet.Cell(1, 4).Value = "Fecha Inicio";
            worksheet.Cell(1, 5).Value = "Fecha Fin";
            worksheet.Cell(1, 6).Value = "Razón";
            worksheet.Cell(1, 7).Value = "Estatus";

            var header = worksheet.Range("A1:G1");
            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = XLColor.LightGray;
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // 🔹 DATOS
            int fila = 2;
            foreach (var v in datos)
            {
                worksheet.Cell(fila, 1).Value = v.Id;
                worksheet.Cell(fila, 2).Value = v.Nombre;
                worksheet.Cell(fila, 3).Value = v.Departamento;
                worksheet.Cell(fila, 4).Value = v.FechaInicio;
                worksheet.Cell(fila, 5).Value = v.FechaFinalizacion;
                worksheet.Cell(fila, 6).Value = v.IdRazonNavigation?.Razon;
                worksheet.Cell(fila, 7).Value =
                    v.IdAprobadoNavigation == null
                        ? "Pendiente"
                        : v.IdAprobadoNavigation.Estatus.Value ? "Aprobado" : "Rechazado";

                fila++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            string origen = tipo == TipoExportVacaciones.Index
                ? "MisVacaciones"
                : "Aprobaciones";

            string fileName = $"{origen}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }





        #endregion 




        #region ExistData

        [Authorize]
        private bool VacacionExists(int id)
        {
            return _context.Vacacions.Any(e => e.Id == id);
        }
        #endregion
    }
}
