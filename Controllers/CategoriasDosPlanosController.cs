using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class CategoriasDosPlanosController : Controller
    {
        private readonly ILogger<CategoriasDosPlanosController> _logger;
        private readonly ICategoriasDosPlanosService _categoriasDosPlanosService;
        private readonly ICategoriaService _categoriaService;
        private readonly IPlanoService _planoService;

        public CategoriasDosPlanosController(ILogger<CategoriasDosPlanosController> logger,
                                    ICategoriasDosPlanosService categoriasDosPlanosService,
                                    ICategoriaService categoriaService,
                                    IPlanoService planoService)
        {
            _logger = logger;
            _categoriasDosPlanosService = categoriasDosPlanosService;
            _categoriaService = categoriaService;
            _planoService = planoService;
        }

        // GET: CategoriasDosPlanossController
        public ActionResult Index()
        {
            ViewBag.CategoriasDosPlanos = _categoriasDosPlanosService.GetAllCategoriasDosPlanos();

            return View();
        }

        // GET: CategoriasDosPlanossController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriasDosPlanossController/Create
        public ActionResult Create()
        {
            ViewBag.CategoriasDosPlanoss = _categoriasDosPlanosService.GetAllCategoriasDosPlanos();
            ViewBag.Categorias = _categoriaService.GetAllCategorias();
            ViewBag.Planos = _planoService.GetAllPlanos();

            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: CategoriasDosPlanossController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriaDosPlanos categoriasDosPlanos)
        {
            try
            {
                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                categoriasDosPlanos.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == categoriasDosPlanos.CategoriaID);

                var planos = _planoService.GetAllPlanos();
                ViewBag.Planos = planos;
                categoriasDosPlanos.Plano = planos.FirstOrDefault(x => x.PlanoID == categoriasDosPlanos.PlanoID);

                _categoriasDosPlanosService.CreateNewCategoriasDosPlanos(categoriasDosPlanos);

                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: CategoriasDosPlanossController/Edit/5
        public ActionResult Edit(int ID)
        {
            ViewBag.Categorias = _categoriaService.GetAllCategorias();
            ViewBag.Planos = _planoService.GetAllPlanos();

            ViewBag.CategoriaDosPlanos = _categoriasDosPlanosService.GetCategoriasDosPlanosById(ID);
            return View();
        }

        // POST: CategoriasDosPlanossController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriaDosPlanos categoriaDosPlanos)
        {
            try
            {
                var categorias = _categoriaService.GetAllCategorias();
                categoriaDosPlanos.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == categoriaDosPlanos.CategoriaID);
                ViewBag.Categorias = categorias;

                var planos = _planoService.GetAllPlanos();
                categoriaDosPlanos.Plano = planos.FirstOrDefault(x => x.PlanoID == categoriaDosPlanos.PlanoID);
                ViewBag.Planos = planos;

                _categoriasDosPlanosService.UpdateCategoriasDosPlanos(categoriaDosPlanos);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.CategoriaDosPlanos = categoriaDosPlanos;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: CategoriasDosPlanossController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _categoriasDosPlanosService.DeleteCategoriasDosPlanos(id);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!" + e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
