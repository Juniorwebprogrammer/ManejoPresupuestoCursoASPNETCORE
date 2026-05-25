using ManejoPresupuesto.Models;
using ManejoPresupuesto.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IRepositorioCategorias repositorioCategorias;
        private readonly IServiciosUsuarios serviciosUsuarios;
        public CategoriasController(
            IRepositorioCategorias repositorioCategorias,
            IServiciosUsuarios serviciosUsuarios
        )
        {
            this.repositorioCategorias = repositorioCategorias;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var categorias = await repositorioCategorias.Obtener(usuarioId);
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Categoría categoría)
        {
            if (!ModelState.IsValid)
            {
                return View(categoría);
            }
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            categoría.UsuarioId = usuarioId;
            await repositorioCategorias.Crear(categoría);
            return RedirectToAction("Index");
        }
    }
}
