using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class TipoCategoriaController : Controller
    {
        private readonly ILogger<TipoCategoriaController> _logger;
        private readonly ITipoCategoriaService _TipoCategoriaService;

        public TipoCategoriaController(ILogger<TipoCategoriaController> logger, ITipoCategoriaService TipoCategoriaService)
        {
            _logger = logger;
            _TipoCategoriaService = TipoCategoriaService;
        }

        // GET: TipoCategoriaController
        public ActionResult Index()
        {
            ViewBag.TipoCategorias = _TipoCategoriaService.GetAllTipoCategorias().ToList();
            return View();
        }

        // GET: TipoCategoriaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoCategoriaController/Create
        public ActionResult Create()
        {
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: TipoCategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoCategoria tipoCategoria)
        {
            try
            {
                _TipoCategoriaService.CreateNewTipoCategoria(tipoCategoria);
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

        // GET: TipoCategoriaController/Edit/5
        public ActionResult Edit(int ID)
        {
            ViewBag.TipoCategoria = _TipoCategoriaService.GetTipoCategoriaById(ID);
            return View();
        }

        // POST: TipoCategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoCategoria tipoCategoria)
        {
            try
            {
                _TipoCategoriaService.UpdateTipoCategoria(tipoCategoria);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.TipoCategoria = tipoCategoria;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: TipoCategoriaController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _TipoCategoriaService.DeleteTipoCategoria(id);
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
