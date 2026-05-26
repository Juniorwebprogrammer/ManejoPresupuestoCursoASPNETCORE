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

        public async Task<IActionResult> Editar(int id)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var categoría = await repositorioCategorias.ObtenerPorId(id, usuarioId);
            if (categoría is null)
            {
                return RedirectToAction("Index");
            }
            return View(categoría);
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var categoría = await repositorioCategorias.ObtenerPorId(id, usuarioId);
            if (categoría is null)
            {
                return RedirectToAction("Index");
            }
            await repositorioCategorias.Eliminar(id);
            return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<IActionResult> Editar(Categoría categoríaEditar)
        {
            if (!ModelState.IsValid)
            {
                return View(categoríaEditar);
            }

            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var categoría = await repositorioCategorias.ObtenerPorId(categoríaEditar.Id, usuarioId);

            if (categoría is null)
            {
                return RedirectToAction("Index");
            }

            categoríaEditar.UsuarioId = usuarioId;

            await repositorioCategorias.Actualizar(categoríaEditar);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> BorrarCategoria(int id)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var categoríaDB = await repositorioCategorias.ObtenerPorId(id, usuarioId);
            if (categoríaDB is null)
            {
                return RedirectToAction("Index");
            }
            await repositorioCategorias.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}
