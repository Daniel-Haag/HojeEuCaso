using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class ClausulasDeContratoController : Controller
    {
        private readonly ILogger<ClausulasDeContratoController> _logger;
        private readonly IClausulaContratoService _clausulaContratoService;
        private readonly ICategoriaService _categoriaService;

        public ClausulasDeContratoController(ILogger<ClausulasDeContratoController> logger,
                                    IClausulaContratoService clausulaContratoService,
                                    ICategoriaService categoriaService)
        {
            _logger = logger;
            _clausulaContratoService = clausulaContratoService;
            _categoriaService = categoriaService;
        }

        // GET: ClausulaContratosController
        public ActionResult Index()
        {
            ViewBag.ClausulasContratos = _clausulaContratoService.GetAllClausulaContratos();
            return View();
        }

        // GET: ClausulaContratosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClausulaContratosController/Create
        public ActionResult Create()
        {
            ViewBag.Categorias = _categoriaService.GetAllCategorias();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: ClausulaContratosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClausulaContrato clausulaContrato)
        {
            try
            {
                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;

                clausulaContrato.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == clausulaContrato.CategoriaID);

                _clausulaContratoService.CreateNewClausulaContrato(clausulaContrato);

                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: ClausulaContratosController/Edit/5
        public ActionResult Edit(int ID)
        {
            var categorias = _categoriaService.GetAllCategorias();
            ViewBag.Categorias = categorias;

            var ClausulaContrato = _clausulaContratoService.GetClausulaContratoById(ID);
            ViewBag.ClausulaContrato = ClausulaContrato;
            ViewBag.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == ClausulaContrato.CategoriaID);

            return View();
        }

        // POST: ClausulaContratosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClausulaContrato clausulaContrato)
        {
            try
            {
                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;

                var categoria = categorias.FirstOrDefault(x => x.CategoriaID == clausulaContrato.CategoriaID);
                clausulaContrato.Categoria = categoria;
                ViewBag.Categoria = categoria;
                
                _clausulaContratoService.UpdateClausulaContrato(clausulaContrato);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.ClausulaContrato = clausulaContrato;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: ClausulaContratosController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _clausulaContratoService.DeleteClausulaContrato(id);
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
