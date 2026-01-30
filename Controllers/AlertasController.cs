using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using RecursosHumanos.Models.ViewModels.Empleados;

public class AlertasController : Controller
{
    private readonly IConfiguration _configuration;

    public AlertasController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #region Alertas
    public IActionResult Index(int page = 1)
    {
        int pageSize = 10;
        List<PersonalAlarmaVM> lista = new();

        using (SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("AlertasConnection")))
        {
            using SqlCommand cmd = new SqlCommand("Personal_Alarmas", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(new PersonalAlarmaVM
                {
                    IdPersonal = Convert.ToInt32(dr["id_personal"]),
                    FotoPersonal = dr["foto_personal"] as byte[],
                    Nombre = dr["nombre"].ToString(),
                    NombreDepartamento = dr["nombre_departamento"].ToString(),
                    FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"]),
                    FechaBaja = dr["fecha_baja"] as DateTime?,
                    FechaReingreso = dr["fecha_reingreso"] as DateTime?,
                    Estado = dr["estado"].ToString(),
                    FechaUltimaLiquidacion = dr["fecha_ultima_liquidacion"] as DateTime?,
                    DiasDesdeUltimaLiquidacion = dr["dias_desde_ultima_liquidacion"] as int?
                });
            }
        }

        int totalRegistros = lista.Count;
        int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize);

        ViewBag.PaginaActual = page;
        ViewBag.TotalPaginas = totalPaginas;

        var datosPaginados = lista
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return View(datosPaginados);
    }


    #endregion


    #region Details
    public IActionResult Details(int id)
    {
        PersonalAlarmaDetalleVM model = null;

        using SqlConnection cn = new SqlConnection(
            _configuration.GetConnectionString("AlertasConnection"));

        using SqlCommand cmd = new SqlCommand("Personal_Alertas_Detalles", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdPersonal", id);

        cn.Open();
        using SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            model = new PersonalAlarmaDetalleVM
            {
                IdPersonal = id,
                FotoPersonal = dr["foto_personal"] as byte[],
                Nombre = dr["nombre"].ToString(),
                NombreDepartamento = dr["nombre_departamento"].ToString(),
                Estado = dr["estado"].ToString(),

                FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"]),
                FechaBaja = dr["fecha_baja"] as DateTime?,
                FechaReingreso = dr["fecha_reingreso"] as DateTime?,
                AntiguedadAnios = Convert.ToInt32(dr["antiguedad_anios"]),

                FechaUltimaLiquidacion = dr["fecha_ultima_liquidacion"] as DateTime?,
                DiasDesdeUltimaLiquidacion = dr["dias_desde_ultima_liquidacion"] as int?,
                TotalLiquidaciones = Convert.ToInt32(dr["total_liquidaciones"]),
                TotalViajes = Convert.ToInt32(dr["total_viajes"]),
                TotalPagado = Convert.ToDecimal(dr["total_pagado"]),
                TotalGastos = Convert.ToDecimal(dr["total_gastos"]),
                UltimaFechaPago = dr["ultima_fecha_pago"] as DateTime?,

                TelefonoPersonal = dr["telefono_personal"].ToString(),
                Email = dr["email"].ToString(),
                TelefonoEmergencia = dr["tel_emergencia"].ToString(),
                Direccion = dr["direccion"].ToString()
            };
        }

        return PartialView("_Details", model);
    }

    #endregion 


}
