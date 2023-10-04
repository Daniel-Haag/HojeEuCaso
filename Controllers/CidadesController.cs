using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class CidadesController : Controller
    {
        private readonly ILogger<CidadesController> _logger;
        private readonly ICidadeService _cidadeService;

        public CidadesController(ILogger<CidadesController> logger, ICidadeService cidadeService)
        {
            _logger = logger;
            _cidadeService = cidadeService;
        }

        // GET: CidadesController
        public ActionResult Index()
        {
            ViewBag.Cidades = _cidadeService.GetAllCidades().ToList();
            return View();
        }

        // GET: CidadesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CidadesController/Create
        public ActionResult Create()
        {
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: CidadesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cidade cidade)
        {
            try
            {
                _cidadeService.CreateNewCidade(cidade);
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

        // GET: CidadesController/Edit/5
        public ActionResult Edit(int ID)
        {
            ViewBag.Cidade = _cidadeService.GetCidadeById(ID);
            return View();
        }

        // POST: CidadesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cidade cidade)
        {
            try
            {
                _cidadeService.UpdateCidade(cidade);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.Cidade = cidade;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: CidadesController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _cidadeService.DeleteCidade(id);
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
