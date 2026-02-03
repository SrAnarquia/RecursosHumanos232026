using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RecursosHumanos.Models;
using Microsoft.IdentityModel.Abstractions;
using RecursosHumanos.Models.ViewModels.Login;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace RecursosHumanos.Controllers
{
    public class CuentaController : Controller
    {
        private readonly ApplicationDbContext _context;


        #region Builder
        //Constructor
        public CuentaController(ApplicationDbContext context) 
        {

            _context = context;
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


            if (ModelState.IsValid)
            {
                //Creando usuario con la propiedade _context
                var usuario = _context.Usuarios
                    .FirstOrDefault(u => u.Usuario1 == model.Username);

                //Se checa no sea nulo el dato
                if (usuario != null && usuario.Contraseña == model.Password)
                {
                    //Se crean los claims informacion del usuarios
                    var claims = new List<Claim> 
                    {
                        new Claim(ClaimTypes.Name,usuario.Usuario1),
                        new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                        new Claim("EsAdmin", usuario.EsAdministrador ? "1" : "0"),
                        new Claim("TipoUsuario", usuario.TipoUsuario.ToString())



                    };

                    var identity = new ClaimsIdentity(claims,"MyCookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    //Crea la cookie de autenticacion
                    await HttpContext.SignInAsync("MyCookieAuth", principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)

                        });

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "");
            }
            return View(model);


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
