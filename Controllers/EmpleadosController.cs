using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using RecursosHumanos.Models;
using RecursosHumanos.Models.ViewModels.Empleados;
using System.Data;
using System.Text;

public class EmpleadosController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public EmpleadosController(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    #region Index

    // ===================== INDEX =====================
    [Authorize]
    public IActionResult Index(PersonalListadoVM filtro, int pagina = 1)
    {
        int pageSize = 10;
        List<PersonalListadoVM> lista = new();

        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("AlertasConnection")))
        {
            using SqlCommand cmd = new SqlCommand("Empleados_datos", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new PersonalListadoVM
                {
                    IdPersonal = Convert.ToInt32(dr["id_personal"]),
                    FotoPersonal = dr["foto_personal"] as byte[],
                    Nombre = dr["nombre"].ToString(),
                    Departamento = dr["descripcion"].ToString(),
                    TipoEmpleado = dr["tipo_empleado"].ToString(),
                    Telefono = dr["telefono"].ToString(),
                    Email = dr["email"].ToString(),
                    Estado = dr["estado"].ToString()
                });
            }
        }

        // ===================== FILTROS =====================
        if (!string.IsNullOrEmpty(filtro.FiltroNombre))
            lista = lista.Where(x => x.Nombre.Contains(filtro.FiltroNombre)).ToList();

        if (!string.IsNullOrEmpty(filtro.FiltroDepartamento))
            lista = lista.Where(x => x.Departamento == filtro.FiltroDepartamento).ToList();

        if (!string.IsNullOrEmpty(filtro.FiltroTipoEmpleado))
            lista = lista.Where(x => x.TipoEmpleado == filtro.FiltroTipoEmpleado).ToList();

        if (!string.IsNullOrEmpty(filtro.FiltroEstado))
            lista = lista.Where(x => x.Estado == filtro.FiltroEstado).ToList();


        var departamentos = lista
            .Select(x => x.Departamento)
            .Distinct()
            .OrderBy(x => x)
            .Select(x => new SelectListItem
            {
                Text = x,
                Value = x
            })
            .ToList();

        var tiposEmpleado = lista
            .Select(x => x.TipoEmpleado)
            .Distinct()
            .OrderBy(x => x)
            .Select(x => new SelectListItem
            {
                Text = x,
                Value = x
            })
            .ToList();

        var estados = lista
            .Select(x => x.Estado)
            .Distinct()
            .OrderBy(x => x)
            .Select(x => new SelectListItem
            {
                Text = x,
                Value = x
            })
            .ToList();





        // ===================== PAGINACIÓN =====================
        int totalRegistros = lista.Count;
        var datosPaginados = lista
            .Skip((pagina - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var vm = new PersonalListadoVM
        {
            Datos = datosPaginados,
            PaginaActual = pagina,
            TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),

            FiltroNombre = filtro.FiltroNombre,
            FiltroDepartamento = filtro.FiltroDepartamento,
            FiltroTipoEmpleado = filtro.FiltroTipoEmpleado,
            FiltroEstado = filtro.FiltroEstado,


            Departamentos = departamentos,
            TiposEmpleado = tiposEmpleado,
            Estados = estados
        };

        return View(vm);
    }
    #endregion



    //Vista Parcial de Resumen: Vacaciones, Incidentes, Portafolio
    #region Resumen

    [Authorize]
    public IActionResult Resumen(int id)
    {
        var vm = new EmpleadoResumenVM();

        // =====================================================
        // CONEXIÓN: ALERTAS (DATOS DEL EMPLEADO)
        // =====================================================
        using (SqlConnection cnAlertas = new SqlConnection(
            _configuration.GetConnectionString("AlertasConnection")))
        {
            cnAlertas.Open();

            using SqlCommand cmd = new SqlCommand("Empleado_DatosPorId", cnAlertas);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdPersonal", id);

            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                vm.IdPersonal = dr["id_personal"] != DBNull.Value
                    ? Convert.ToInt32(dr["id_personal"])
                    : 0;

                vm.Nombre = dr["nombre"]?.ToString() ?? string.Empty;
                vm.Departamento = dr["Departamento"]?.ToString() ?? string.Empty;
                vm.FotoPersonal = dr["foto_personal"] as byte[];

                // No viene en el SP → valor neutro
                vm.FechaIngreso = DateTime.MinValue;
            }
        }

        // =====================================================
        // CONEXIÓN: DEFAULT (CURSOS / INCIDENTES / VACACIONES)
        // =====================================================
        using (SqlConnection cnDefault = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection")))
        {
            cnDefault.Open();

            // ===================== CURSOS =====================
            using (SqlCommand cmd = new SqlCommand("Cursos_Resumen", cnDefault))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPersona", id);

                using SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    vm.TotalCursos = dr["Total"] != DBNull.Value
                        ? Convert.ToInt32(dr["Total"])
                        : 0;

                    vm.CursosTerminados = dr["Terminados"] != DBNull.Value
                        ? Convert.ToInt32(dr["Terminados"])
                        : 0;

                    vm.CursosEnProceso = dr["EnProceso"] != DBNull.Value
                        ? Convert.ToInt32(dr["EnProceso"])
                        : 0;

                    vm.CursosSinIniciar = dr["SinIniciar"] != DBNull.Value
                        ? Convert.ToInt32(dr["SinIniciar"])
                        : 0;
                }
            }

            // ===================== INCIDENTES =====================
            using (SqlCommand cmd = new SqlCommand("Incidentes_Resumen", cnDefault))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPersona", id);

                using SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    vm.TotalIncidentes = dr["Total"] != DBNull.Value
                        ? Convert.ToInt32(dr["Total"])
                        : 0;

                    vm.UltimoIncidente = dr["Ultimo"] != DBNull.Value
                        ? Convert.ToDateTime(dr["Ultimo"])
                        : null;

                    vm.RazonMasComun = dr["RazonMasComun"]?.ToString() ?? string.Empty;
                }
            }

            // ===================== VACACIONES =====================
            using (SqlCommand cmd = new SqlCommand("Vacaciones_Resumen", cnDefault))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPersona", id);

                using SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    vm.DiasVacacionesTomados = dr["DiasTomados"] != DBNull.Value
                        ? Convert.ToInt32(dr["DiasTomados"])
                        : 0;

                    vm.TotalSolicitudesVacaciones = dr["TotalSolicitudes"] != DBNull.Value
                        ? Convert.ToInt32(dr["TotalSolicitudes"])
                        : 0;

                    vm.UltimaVacacion = dr["UltimaVacacion"] != DBNull.Value
                        ? Convert.ToDateTime(dr["UltimaVacacion"])
                        : null;
                }
            }
        }

        return PartialView("_EmpleadoResumen", vm);
    }


    #endregion

    //Index Portafolio
    #region Portafolio
    [Authorize]
    public IActionResult Portafolio(int id, string? curso, DateTime? fechaDesde, DateTime? fechaHasta, int pagina = 1)
    {
        int pageSize = 10; // puedes ajustar el tamaño de página

        var vm = new PersonalPortafolioVM
        {
            FiltroCurso = curso,
            FechaDesde = fechaDesde,
            FechaHasta = fechaHasta,
            Cursos = new List<CursoPersonaVM>()
        };

        // ===================== PERSONA =====================
        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("AlertasConnection")))
        {
            using SqlCommand cmd = new SqlCommand("Empleado_DatosPorId", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdPersonal", id);

            cn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                vm.IdPersonal = id;
                vm.FotoPersonal = dr["foto_personal"] as byte[];
                vm.Nombre = dr["nombre"].ToString();
                vm.Departamento = dr["Departamento"].ToString();
                vm.TipoEmpleado = dr["tipo_empleado"].ToString();
                vm.Curp = dr["curp"].ToString();
                vm.Telefono = dr["telefono"].ToString();
                vm.Email = dr["email"].ToString();
                vm.Estado = dr["estado"].ToString();
            }
        }

        // ===================== CURSOS =====================
        var cursosTemp = new List<CursoPersonaVM>();
        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection")))
        {
            using SqlCommand cmd = new SqlCommand("Entrenamiento_PersonaCursos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdPersonal", id);

            cn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cursosTemp.Add(new CursoPersonaVM
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    NombreCurso = dr["NombreCurso"].ToString(),
                    Descripcion = dr["Descripcion"].ToString(),
                    FechaInicio = dr["FechaInicio"] as DateTime?,
                    FechaFinalizacion = dr["FechaFinalizacion"] as DateTime?,
                    Estatus = dr["Estatus"].ToString(),
                    Diploma = dr["Diploma"].ToString()
                });
            }
        }

        // ===================== FILTROS =====================
        if (!string.IsNullOrEmpty(curso))
        {
            cursosTemp = cursosTemp
                .Where(c => c.NombreCurso.Contains(curso, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (fechaDesde.HasValue)
            cursosTemp = cursosTemp
                .Where(c => c.FechaInicio >= fechaDesde.Value)
                .ToList();

        if (fechaHasta.HasValue)
            cursosTemp = cursosTemp
                .Where(c => c.FechaFinalizacion <= fechaHasta.Value)
                .ToList();

        // ===================== PAGINACIÓN =====================
        int totalRegistros = cursosTemp.Count;
        vm.TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize);
        vm.PaginaActual = pagina;

        vm.Cursos = cursosTemp
            .Skip((pagina - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // ===================== NUEVO CURSO (OVERLAY) =====================
        vm.NuevoCurso = new CursoPersonaCreateVM
        {
            IdPersona = id,
            Estatus = _context.EstatusCursos
                .Select(x => new SelectListItem
                {
                    Text = x.Estatus,
                    Value = x.Id.ToString()
                })
                .ToList()
        };

        return View(vm);
    }

    #endregion

    //Index Incidentes
    #region Incidentes

    [Authorize]
    public IActionResult Incidentes(int id,string? nombreIncidente,DateTime? fechaDesde,DateTime? fechaHasta,int pagina = 1)
    {
        int pageSize = 10;

        var vm = new PersonalIncidentesVM
        {
            FiltroNombreIncidente = nombreIncidente,
            FechaDesde = fechaDesde,
            FechaHasta = fechaHasta

        };

        // ===================== PERSONA =====================
        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("AlertasConnection")))
        {
            using SqlCommand cmd = new SqlCommand("Empleado_DatosPorId", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdPersonal", id);

            cn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                vm.IdPersonal = id;
                vm.FotoPersonal = dr["foto_personal"] as byte[];
                vm.Nombre = dr["nombre"].ToString();
                vm.Departamento = dr["Departamento"].ToString();
                vm.TipoEmpleado = dr["tipo_empleado"].ToString();
                vm.Telefono = dr["telefono"].ToString();
                vm.Email = dr["email"].ToString();
                vm.Estado = dr["estado"].ToString();
            }
        }

        // ===================== INCIDENTES =====================
        var incidentesTemp = new List<IncidenteVM>();

        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection")))
        {
            using SqlCommand cmd = new SqlCommand("Incidentes_Persona", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdPersona", id);

            cn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                incidentesTemp.Add(new IncidenteVM
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    NombreIncidente = dr["NombreIncidente"].ToString(),
                    Descripcion = dr["Descripcion"].ToString(),
                    Razon = dr["Razon"].ToString(),
                    FechaIncidente = Convert.ToDateTime(dr["FechaIncidente"]),
                    Evidencia = dr["Evidencia"].ToString()
                });
            }
        }

        // ===================== FILTROS =====================
        if (!string.IsNullOrEmpty(nombreIncidente))
            incidentesTemp = incidentesTemp
                .Where(x => x.NombreIncidente.Contains(nombreIncidente, StringComparison.OrdinalIgnoreCase))
                .ToList();

        if (fechaDesde.HasValue)
            incidentesTemp = incidentesTemp
                .Where(x => x.FechaIncidente >= fechaDesde.Value)
                .ToList();

        if (fechaHasta.HasValue)
            incidentesTemp = incidentesTemp
                .Where(x => x.FechaIncidente <= fechaHasta.Value)
                .ToList();

        // ===================== PAGINACIÓN =====================
        int totalRegistros = incidentesTemp.Count;
        vm.TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize);
        vm.PaginaActual = pagina;

        vm.Incidentes = incidentesTemp
            .Skip((pagina - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // ===================== CREATE OVERLAY =====================
        vm.NuevoIncidente = new IncidenteCreateVM
        {
            IdPersona = id
        };

        return View(vm);
    }
    #endregion


    //Index Vacaciones
    #region Vacaciones
    [Authorize]
    public IActionResult Vacaciones(
        int id,
        DateTime? fechaDesde,
        DateTime? fechaHasta,
        int pagina = 1)
    {
        int pageSize = 10;

        var vm = new PersonalVacacionesVM
        {
            FechaDesde = fechaDesde,
            FechaHasta = fechaHasta,
            PaginaActual = pagina,
            Vacaciones = new List<VacacionVM>()
        };

        // ===================== PERSONA =====================
        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("AlertasConnection")))
        {
            using SqlCommand cmd = new SqlCommand("Empleado_DatosPorId", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdPersonal", id);

            cn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                vm.IdPersonal = id;
                vm.FotoPersonal = dr["foto_personal"] as byte[];
                vm.Nombre = dr["nombre"]?.ToString() ?? string.Empty;
                vm.Departamento = dr["Departamento"]?.ToString() ?? string.Empty;
                vm.TipoEmpleado = dr["tipo_empleado"]?.ToString() ?? string.Empty;
                vm.Telefono = dr["telefono"]?.ToString() ?? string.Empty;
                vm.Email = dr["email"]?.ToString() ?? string.Empty;
                vm.Estado = dr["estado"]?.ToString() ?? string.Empty;
            }
        }

        // ===================== VACACIONES =====================
        var vacacionesTemp = new List<VacacionVM>();

        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection")))
        {
            using SqlCommand cmd = new SqlCommand("Vacaciones_PorUsuario", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", id);

            cn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                int estatusDb = dr["Estatus"] != DBNull.Value
                    ? Convert.ToInt32(dr["Estatus"])
                    : 0;

                vacacionesTemp.Add(new VacacionVM
                {
                    Id = dr["Id"] != DBNull.Value
                        ? Convert.ToInt32(dr["Id"])
                        : 0,

                    Detalles = dr["Detalles"]?.ToString(),

                    FechaInicio = dr["FechaInicio"] != DBNull.Value
                        ? Convert.ToDateTime(dr["FechaInicio"])
                        : (DateTime?)null,

                    FechaFinalizacion = dr["FechaFinalizacion"] != DBNull.Value
                        ? Convert.ToDateTime(dr["FechaFinalizacion"])
                        : (DateTime?)null,

                    FechaCreacion = dr["FechaCreacion"] != DBNull.Value
                        ? Convert.ToDateTime(dr["FechaCreacion"])
                        : DateTime.MinValue,

                    Estatus = estatusDb switch
                    {
                        1 => "Aprobado",
                        2 => "Rechazado",
                        _ => "Pendiente"
                    }
                });
            }
        }

        // ===================== FILTROS =====================
        if (fechaDesde.HasValue)
        {
            vacacionesTemp = vacacionesTemp
                .Where(v => v.FechaInicio.HasValue &&
                            v.FechaInicio.Value.Date >= fechaDesde.Value.Date)
                .ToList();
        }

        if (fechaHasta.HasValue)
        {
            vacacionesTemp = vacacionesTemp
                .Where(v => v.FechaFinalizacion.HasValue &&
                            v.FechaFinalizacion.Value.Date <= fechaHasta.Value.Date)
                .ToList();
        }

        // ===================== PAGINACIÓN =====================
        int totalRegistros = vacacionesTemp.Count;
        vm.TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize);

        vm.Vacaciones = vacacionesTemp
            .Skip((pagina - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return View(vm);
    }
    #endregion






    //Vista de Incidentes:Crear nuevo
    #region CreatesIncidentes

    #region CreateGet
    [Authorize]
    [HttpGet]
    public IActionResult CreateIncidente(int idPersona)
    {
        var razones = _context.RazonesIncidentes
            .Select(x => new SelectListItem
            {
                Text = x.Razon,
                Value = x.Id.ToString()
            })
            .ToList();

        var vm = new IncidenteCreateVM
        {
            IdPersona = idPersona,
            FechaIncidente = DateTime.Today,
            Razones = razones
        };

        return PartialView("_CreateIncidente", vm);
    }



    #endregion

    #region CreatePost

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateIncidente(IncidenteCreateVM model)
    {
        // ===================== DROPDOWN RAZONES =====================
        model.Razones = _context.RazonesIncidentes
            .Select(x => new SelectListItem
            {
                Text = x.Razon,
                Value = x.Id.ToString()
            })
            .ToList();

        // ===================== VALIDACIÓN =====================
        if (!ModelState.IsValid)
        {
            return PartialView("_CreateIncidente", model);
        }

        string evidenciaPath = null;

        // ===================== GUARDAR ARCHIVO =====================
        if (model.EvidenciaFile != null && model.EvidenciaFile.Length > 0)
        {
            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".mp4" };
            var extension = Path.GetExtension(model.EvidenciaFile.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("EvidenciaFile", "Formato de archivo no permitido");
                return PartialView("_CreateIncidente", model);
            }

            string uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "UploadedFiles"
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid() + extension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.EvidenciaFile.CopyTo(fileStream);
            }

            evidenciaPath = "/UploadedFiles/" + uniqueFileName;
        }

        // ===================== GUARDAR EN DB =====================
        _context.Incidentes.Add(new Incidente
        {
            IdPersona = model.IdPersona,
            NombreIncidente = model.NombreIncidente,
            Descripcion = model.Descripcion,
            IdRazon = model.IdRazon.Value,
            FechaIncidente = model.FechaIncidente,
            Evidencia = evidenciaPath,
            FechaCreacion = DateTime.Now
        });

        _context.SaveChanges();

        // ===================== RESPUESTA OVERLAY =====================
        return Json(new { success = true });
    }

    #endregion


    #endregion


    //Vista de Portafolio:Crear nuevo
    #region CreatesPortafolio

    //Portafolio
    #region CrearEmpleadoCurso GET (OVERLAY)
    [Authorize]
    [HttpGet]
    public IActionResult CreateCurso(int idPersona)
    {
        var estatus = _context.EstatusCursos
            .Select(x => new SelectListItem
            {
                Text = x.Estatus,
                Value = x.Id.ToString()
            })
            .ToList();

        var vm = new CursoPersonaCreateVM
        {
            IdPersona = idPersona,
            Estatus = estatus
        };

        return PartialView("_CreateCurso", vm);
    }



    #endregion

    #region CrearEmpleadoCurso POST
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCurso(CursoPersonaCreateVM model)
    {
        // Traemos siempre el dropdown de estatus
        model.Estatus = _context.EstatusCursos
            .Select(x => new SelectListItem
            {
                Text = x.Estatus,
                Value = x.Id.ToString()
            })
            .ToList();

        // Validación de modelo
        if (!ModelState.IsValid)
        {
            return PartialView("_CreateCurso", model);
        }

        string diplomaPath = null;

        // ===================== GUARDAR ARCHIVO =====================
        if (model.DiplomaFile != null && model.DiplomaFile.Length > 0)
        {
            // Carpeta dentro de wwwroot
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FileUploaded");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Nombre único para evitar colisiones
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.DiplomaFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.DiplomaFile.CopyTo(fileStream);
            }

            // Guardamos ruta relativa para la base de datos
            diplomaPath = "/FileUploaded/" + uniqueFileName;
        }

        // ===================== GUARDAR EN DB =====================
        _context.CursosPersonas.Add(new CursosPersona
        {
            IdPersona = model.IdPersona,
            NombreCurso = model.NombreCurso,
            Descripcion = model.Descripcion,
            FechaInicio = model.FechaInicio,
            FechaFinalizacion = model.FechaFinalizacion,
            IdEstatus = model.IdEstatus.Value,
            Diploma = diplomaPath, // <-- ruta guardada
            FechaCreacion = DateTime.Now
        });

        _context.SaveChanges();

        // Retornar éxito al overlay
        return Json(new { success = true });
    }


    #endregion





    #endregion




    #region Edits


    #region Edit GET 
    [Authorize]
    [HttpGet]
    public IActionResult EditCurso(int id)
    {
        var curso = _context.CursosPersonas.FirstOrDefault(x => x.Id == id);
        if (curso == null) return NotFound();

        var vm = new CursoPersonaEditVM
        {
            Id = curso.Id,
            IdPersona = curso.IdPersona.Value,
            NombreCurso = curso.NombreCurso,
            Descripcion = curso.Descripcion,
            FechaInicio = curso.FechaInicio,
            FechaFinalizacion = curso.FechaFinalizacion,
            IdEstatus = curso.IdEstatus,
            Diploma = curso.Diploma,
            Estatus = _context.EstatusCursos
                .Select(x => new SelectListItem
                {
                    Text = x.Estatus,
                    Value = x.Id.ToString()
                }).ToList()
        };

        return PartialView("_EditCurso", vm);
    }

    #endregion


    #region Edit POST
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditCurso(CursoPersonaEditVM model)
    {
        model.Estatus = _context.EstatusCursos
            .Select(x => new SelectListItem
            {
                Text = x.Estatus,
                Value = x.Id.ToString()
            }).ToList();

        if (!ModelState.IsValid)
            return PartialView("_EditCurso", model);

        var curso = _context.CursosPersonas.FirstOrDefault(x => x.Id == model.Id);
        if (curso == null) return NotFound();

        // Actualizar archivo si viene uno nuevo
        if (model.DiplomaFile != null && model.DiplomaFile.Length > 0)
        {
            string uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FileUploaded");
            Directory.CreateDirectory(uploads);

            string fileName = Guid.NewGuid() + Path.GetExtension(model.DiplomaFile.FileName);
            string path = Path.Combine(uploads, fileName);

            using var fs = new FileStream(path, FileMode.Create);
            model.DiplomaFile.CopyTo(fs);

            curso.Diploma = "/FileUploaded/" + fileName;
        }

        curso.NombreCurso = model.NombreCurso;
        curso.Descripcion = model.Descripcion;
        curso.FechaInicio = model.FechaInicio;
        curso.FechaFinalizacion = model.FechaFinalizacion;
        curso.IdEstatus = model.IdEstatus.Value;

        _context.SaveChanges();

        return Json(new { success = true });
    }



    #endregion

    #endregion



    //Vista de Portafolio:Eliminar
    #region DeletesPortafolio

    #region Delete GET
    [Authorize]
    [HttpGet]
    public IActionResult DeleteCurso(int id)
    {
        var curso = (from c in _context.CursosPersonas
                     join e in _context.EstatusCursos
                         on c.IdEstatus equals e.Id
                     where c.Id == id
                     select new CursoPersonaDeleteVM
                     {
                         Id = c.Id,
                         IdPersona = c.IdPersona.Value,
                         NombreCurso = c.NombreCurso,
                         Descripcion = c.Descripcion,
                         FechaInicio = c.FechaInicio,
                         FechaFinalizacion = c.FechaFinalizacion,
                         Estatus = e.Estatus
                     }).FirstOrDefault();

        if (curso == null)
            return NotFound();

        return PartialView("_DeleteCurso", curso);
    }


    #endregion



    #region Delete POST
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteCurso(CursoPersonaDeleteVM model)
    {
        var curso = _context.CursosPersonas.FirstOrDefault(x => x.Id == model.Id);
        if (curso == null)
            return NotFound();

        // 🔥 eliminar archivo si existe
        if (!string.IsNullOrEmpty(curso.Diploma))
        {
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                curso.Diploma.TrimStart('/')
            );

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        _context.CursosPersonas.Remove(curso);
        _context.SaveChanges();

        return Json(new { success = true });
    }


    #endregion

    #endregion




    //Vista de Incidentes:Eliminar
    #region DeletesIncidentes

    #region DeleteIncidenteGet
    // ===================== GET =====================
    [Authorize]
    [HttpGet]
    public IActionResult DeleteIncidente(int id)
    {
        var incidente = (from i in _context.Incidentes
                         join r in _context.RazonesIncidentes
                            on i.IdRazon equals r.Id
                         where i.Id == id
                         select new IncidenteDeleteVM
                         {
                             Id = i.Id,
                             IdPersona = i.IdPersona.Value,
                             NombreIncidente = i.NombreIncidente,
                             Descripcion = i.Descripcion,
                             Razon = r.Razon,
                             FechaIncidente = i.FechaIncidente.Value,
                             Evidencia = i.Evidencia
                         }).FirstOrDefault();

        if (incidente == null)
            return NotFound();

        return PartialView("_DeleteIncidente", incidente);
    }
    #endregion

    #region DeleteIncidentePost
    // ===================== POST =====================
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteIncidente(IncidenteDeleteVM model)
    {
        var incidente = _context.Incidentes
            .FirstOrDefault(x => x.Id == model.Id);

        if (incidente == null)
            return NotFound();

        // 🔥 eliminar evidencia si existe
        if (!string.IsNullOrEmpty(incidente.Evidencia))
        {
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                incidente.Evidencia.TrimStart('/')
            );

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        _context.Incidentes.Remove(incidente);
        _context.SaveChanges();

        return Json(new { success = true });
    }


    #endregion


    #endregion






    #region Details
    [Authorize]
    [HttpGet]
    public IActionResult DetailsCurso(int id)
    {
        var curso = (from c in _context.CursosPersonas
                     join e in _context.EstatusCursos
                         on c.IdEstatus equals e.Id
                     where c.Id == id
                     select new CursoPersonaDetailsVM
                     {
                         Id = c.Id,
                         NombreCurso = c.NombreCurso,
                         Descripcion = c.Descripcion,
                         FechaInicio = c.FechaInicio,
                         FechaFinalizacion = c.FechaFinalizacion,
                         Estatus = e.Estatus,
                         Diploma = c.Diploma
                     }).FirstOrDefault();

        if (curso == null)
            return NotFound();

        return PartialView("_DetailsCurso", curso);
    }

    #endregion




    //Exportar a Excel
    #region Export CSV
    [Authorize]
    public IActionResult Export(
        string? filtroNombre,
        string? filtroDepartamento,
        string? filtroTipoEmpleado,
        string? filtroEstado)
    {
        var empleados = new List<PersonalListadoVM>();

        // ===================== EMPLEADOS (ALERTAS) =====================
        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("AlertasConnection")))
        {
            using SqlCommand cmd = new SqlCommand("Empleados_datos", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                empleados.Add(new PersonalListadoVM
                {
                    IdPersonal = Convert.ToInt32(dr["id_personal"]),
                    Nombre = dr["nombre"]?.ToString(),
                    Departamento = dr["descripcion"]?.ToString(),
                    TipoEmpleado = dr["tipo_empleado"]?.ToString(),
                    Telefono = dr["telefono"]?.ToString(),
                    Email = dr["email"]?.ToString(),
                    Estado = dr["estado"]?.ToString()
                });
            }
        }

        // ===================== FILTROS =====================
        if (!string.IsNullOrEmpty(filtroNombre))
            empleados = empleados.Where(x => x.Nombre.Contains(filtroNombre)).ToList();

        if (!string.IsNullOrEmpty(filtroDepartamento))
            empleados = empleados.Where(x => x.Departamento == filtroDepartamento).ToList();

        if (!string.IsNullOrEmpty(filtroTipoEmpleado))
            empleados = empleados.Where(x => x.TipoEmpleado == filtroTipoEmpleado).ToList();

        if (!string.IsNullOrEmpty(filtroEstado))
            empleados = empleados.Where(x => x.Estado == filtroEstado).ToList();

        // ===================== CSV =====================
        var sb = new StringBuilder();

        sb.AppendLine("Nombre,Departamento,TipoEmpleado,Telefono,Email,Estado,TotalIncidentes,TotalCursos,TotalVacaciones");

        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection")))
        {
            cn.Open();

            foreach (var e in empleados)
            {
                int totalIncidentes = 0;
                int totalCursos = 0;
                int totalVacaciones = 0;

                // ===== INCIDENTES =====
                using (SqlCommand cmd = new SqlCommand("Incidentes_Resumen", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPersona", e.IdPersonal);

                    using SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        totalIncidentes = dr["Total"] != DBNull.Value
                            ? Convert.ToInt32(dr["Total"])
                            : 0;
                    }
                }

                // ===== CURSOS =====
                using (SqlCommand cmd = new SqlCommand("Cursos_Resumen", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPersona", e.IdPersonal);

                    using SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        totalCursos = dr["Total"] != DBNull.Value
                            ? Convert.ToInt32(dr["Total"])
                            : 0;
                    }
                }

                // ===== VACACIONES =====
                using (SqlCommand cmd = new SqlCommand("Vacaciones_Resumen", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPersona", e.IdPersonal);

                    using SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        totalVacaciones = dr["DiasTomados"] != DBNull.Value
                            ? Convert.ToInt32(dr["DiasTomados"])
                            : 0;
                    }
                }

                // ===== CSV ROW =====
                sb.AppendLine(
                    $"\"{e.Nombre}\"," +
                    $"\"{e.Departamento}\"," +
                    $"\"{e.TipoEmpleado}\"," +
                    $"\"{e.Telefono}\"," +
                    $"\"{e.Email}\"," +
                    $"\"{e.Estado}\"," +
                    $"{totalIncidentes}," +
                    $"{totalCursos}," +
                    $"{totalVacaciones}"
                );
            }
        }

        byte[] buffer = Encoding.UTF8.GetBytes(sb.ToString());

        return File(
            buffer,
            "text/csv",
            $"Reporte_Empleados_{DateTime.Now:yyyyMMdd_HHmm}.csv"
        );
    }
    #endregion



}
