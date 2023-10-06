using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class PlanosController : Controller
    {
        private readonly ILogger<PlanosController> _logger;
        private readonly IPlanoService _planoService;

        public PlanosController(ILogger<PlanosController> logger, IPlanoService planoService)
        {
            _logger = logger;
            _planoService = planoService;
        }

        // GET: PlanosController
        public ActionResult Index()
        {
            ViewBag.Planos = _planoService.GetAllPlanos().ToList();
            return View();
        }

        // GET: PlanosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlanosController/Create
        public ActionResult Create()
        {
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: PlanosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Plano Plano)
        {
            try
            {
                _planoService.CreateNewPlano(Plano);
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

        // GET: PlanosController/Edit/5
        public ActionResult Edit(int ID)
        {
            ViewBag.Plano = _planoService.GetPlanoById(ID);
            return View();
        }

        // POST: PlanosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Plano Plano)
        {
            try
            {
                _planoService.UpdatePlano(Plano);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.Plano = Plano;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: PlanosController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _planoService.DeletePlano(id);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return RedirectToAction("Index");
            }
        }

        private string FormatPrice(decimal price)
        {
            return string.Format("R$ {0:N2}", price);
        }
    }
}
