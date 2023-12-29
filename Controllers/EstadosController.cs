using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using HojeEuCaso.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class EstadosController : Controller
    {
        private readonly ILogger<EstadosController> _logger;
        private readonly IEstadoService _EstadoService;
        private readonly IPaisService _paisService;

        public EstadosController(
            ILogger<EstadosController> logger,
            IEstadoService EstadoService,
            IPaisService paisService)
        {
            _logger = logger;
            _EstadoService = EstadoService;
            _paisService = paisService;
        }

        // GET: EstadosController
        public ActionResult Index()
        {
            ViewBag.Estados = _EstadoService.GetAllEstados().ToList();
            return View();
        }

        // GET: EstadosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstadosController/Create
        public ActionResult Create()
        {
            ViewBag.Paises = _paisService.GetAllPaises();

            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: EstadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Estado Estado)
        {
            try
            {
                _EstadoService.CreateNewEstado(Estado);

                TempData["SuccessMessage"] = "Salvo com sucesso!";

                ViewBag.Paises = _paisService.GetAllPaises();

                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: EstadosController/Edit/5
        public ActionResult Edit(int ID)
        {
            var estado = _EstadoService.GetEstadoById(ID);
            ViewBag.Estado = estado;

            var paises = _paisService.GetAllPaises();
            ViewBag.Paises = paises;

            ViewBag.Pais = paises.FirstOrDefault(x => x.PaisID == estado.PaisID);

            return View();
        }

        // POST: EstadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Estado Estado)
        {
            try
            {
                _EstadoService.UpdateEstado(Estado);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.Estado = Estado;

                var paises = _paisService.GetAllPaises();
                ViewBag.Paises = paises;

                ViewBag.Pais = paises.FirstOrDefault(x => x.PaisID == Estado.PaisID);
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: EstadosController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _EstadoService.DeleteEstado(id);
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
