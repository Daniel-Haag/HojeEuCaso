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
        private readonly IEstadoService _estadoService;

        public CidadesController(
            ILogger<CidadesController> logger,
            ICidadeService cidadeService,
            IEstadoService estadoService)
        {
            _logger = logger;
            _cidadeService = cidadeService;
            _estadoService = estadoService;
        }

        // GET: CidadesController
        public ActionResult Index()
        {
            var teste = _cidadeService.GetAllCidades();

            ViewBag.Cidades = _cidadeService.GetAllCidades();
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
            ViewBag.Estados = _estadoService.GetAllEstados();

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

                ViewBag.Estados = _estadoService.GetAllEstados();

                return View();            
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
            var cidade = _cidadeService.GetCidadeById(ID);
            ViewBag.Cidade = cidade;

            var estados = _estadoService.GetAllEstados();
            ViewBag.Estados = estados;

            ViewBag.Estado = estados.FirstOrDefault(x => x.EstadoID == cidade.EstadoID);

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

                var estados = _estadoService.GetAllEstados();
                ViewBag.Estados = estados;

                ViewBag.Estado = estados.FirstOrDefault(x => x.EstadoID == cidade.EstadoID);

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
