using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ILogger<CategoriasController> _logger;
        private readonly ICategoriaService _categoriaService;
        private readonly ITipoCategoriaService _tipoCategoriaService;

        public CategoriasController(ILogger<CategoriasController> logger, 
                                    ICategoriaService CategoriaService,
                                    ITipoCategoriaService tipoCategoriaService)
        {
            _logger = logger;
            _categoriaService = CategoriaService;
            _tipoCategoriaService = tipoCategoriaService;
        }

        // GET: CategoriasController
        public ActionResult Index()
        {
            ViewBag.Categorias = _categoriaService.GetAllCategorias();
            return View();
        }

        // GET: CategoriasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriasController/Create
        public ActionResult Create()
        {
            ViewBag.TipoCategorias = _tipoCategoriaService.GetAllTipoCategorias();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: CategoriasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            try
            {
                var tipoCategorias = _tipoCategoriaService.GetAllTipoCategorias();
                ViewBag.TipoCategorias = tipoCategorias;

                categoria.TipoCategoria = tipoCategorias.FirstOrDefault(x => x.TipoCategoriaID == categoria.TipoCategoriaID);

                _categoriaService.CreateNewCategoria(categoria);

                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: CategoriasController/Edit/5
        public ActionResult Edit(int ID)
        {
            var tipoCategorias = _tipoCategoriaService.GetAllTipoCategorias();
            ViewBag.TipoCategorias = tipoCategorias;

            var categoria = _categoriaService.GetCategoriaById(ID);
            ViewBag.Categoria = categoria;
            ViewBag.TipoCategoria = tipoCategorias.FirstOrDefault(x => x.TipoCategoriaID == categoria.TipoCategoriaID);
            return View();
        }

        // POST: CategoriasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            try
            {
                var tipoCategorias = _tipoCategoriaService.GetAllTipoCategorias();
                ViewBag.TipoCategorias = tipoCategorias;

                var tipoCategoria = tipoCategorias.FirstOrDefault(x => x.TipoCategoriaID == categoria.TipoCategoriaID);
                categoria.TipoCategoria = tipoCategoria;
                ViewBag.TipoCategoria = tipoCategoria;

                _categoriaService.UpdateCategoria(categoria);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.Categoria = categoria;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: CategoriasController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _categoriaService.DeleteCategoria(id);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return RedirectToAction("Index");
            }
        }
    }
}
