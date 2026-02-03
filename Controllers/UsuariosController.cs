using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Models;

namespace RecursosHumanos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        //Este es el constructor
        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

      
    }
}
