using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Abstractions;
using RecursosHumanos.Models;
using RecursosHumanos.Models.ViewModels.Empleados;
using RecursosHumanos.Models.ViewModels.Login;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecursosHumanos.Controllers
{
    public class CuentaController : Controller
    {

        #region Builder
        //Constructor
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public CuentaController(ApplicationDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        #endregion

        #region LoginGet

        [HttpGet]
        public IActionResult Login() 
        {

            return View();
        
        }
        #endregion

        #region LoginPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModelscs model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // 1️ Validación en DB Default
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Usuario1 == model.Username);

            if (usuario == null || usuario.Contraseña != model.Password)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View(model);
            }

            var nombreCompleto = usuario.Nombre?.Trim();
            int idEmpleadoFinal = usuario.Id; // fallback

            EmpleadosCookiesValidationVM empleadoAlertas = null;

            // 2️ Buscar en ALERTAS
            var connStringAlertas = _configuration.GetConnectionString("AlertasConnection");

            using (SqlConnection conn = new SqlConnection(connStringAlertas))
            using (SqlCommand cmd = new SqlCommand("dbo.Empleados_datos_por_nombre", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        empleadoAlertas = new EmpleadosCookiesValidationVM
                        {
                            IdPersonal = reader.GetInt32(reader.GetOrdinal("id_personal")),
                            NombreCompleto = reader["nombre"].ToString(),
                            Departamento = reader["Departamento"].ToString(),
                            TipoEmpleado = reader["tipo_empleado"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Email = reader["email"].ToString(),
                            EstaActivo = reader["estado"].ToString() == "A",
                            FechaIngreso = reader["fecha_ingreso"] as DateTime?
                        };
                    }
                }
            }

            // 3️ Si existe en ALERTAS → usar id_personal
            if (empleadoAlertas != null)
            {
                idEmpleadoFinal = empleadoAlertas.IdPersonal;
            }

            // 4️ Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Usuario1),
                new Claim(ClaimTypes.NameIdentifier, idEmpleadoFinal.ToString()),
                new Claim("IdPersonal", idEmpleadoFinal.ToString()),
                new Claim("EsAdmin", usuario.EsAdministrador ? "1" : "0"),
                new Claim("TipoUsuario", usuario.TipoUsuario.ToString())
            };

            if (empleadoAlertas != null)
            {
                claims.Add(new Claim("NombreCompleto", empleadoAlertas.NombreCompleto));
                claims.Add(new Claim("Departamento", empleadoAlertas.Departamento ?? ""));
                claims.Add(new Claim("EmpleadoActivo", empleadoAlertas.EstaActivo ? "1" : "0"));
            }

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                "MyCookieAuth",
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                });

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region LogOut
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Se elimina la cookie
        public async Task<IActionResult> Logout() 
        {
            //Elimina la cookie del navegador
            await HttpContext.SignOutAsync("MyCookieAuth");

            //Redirigir a Login
            return RedirectToAction("Login","Cuenta");
        }
        #endregion

    }
}
