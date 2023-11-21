using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class PaisesController : Controller
    {
        private readonly ILogger<PaisesController> _logger;
        private readonly IPaisService _paisService;

        public PaisesController(ILogger<PaisesController> logger, 
            IPaisService paisService)
        {
            _logger = logger;
            _paisService = paisService;
        }

        // GET: PaisesController
        public ActionResult Index()
        {
            ViewBag.Paises = _paisService.GetAllPaises().ToList();
            return View();
        }

        // GET: PaisesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaisesController/Create
        public ActionResult Create()
        {
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: PaisesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pais pais)
        {
            try
            {
                _paisService.CreateNewPais(pais);
                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: PaisesController/Edit/5
        public ActionResult Edit(int ID)
        {
            ViewBag.Pais = _paisService.GetPaisById(ID);
            return View();
        }

        // POST: PaisesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pais pais)
        {
            try
            {
                _paisService.UpdatePais(pais);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.Pais = pais;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: PaisesController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _paisService.DeletePais(id);
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
