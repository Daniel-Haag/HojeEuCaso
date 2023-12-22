using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using HojeEuCaso.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class ServicosWedManagerController : Controller
    {
        private readonly ILogger<ServicosWedManagerController> _logger;
        private readonly ICidadeService _cidadeService;
        private readonly IFornecedorService _fornecedorService;
        private readonly IPacoteService _pacoteService;
        private readonly IPaisService _paisService;
        private readonly IEstadoService _estadoService;

        public ServicosWedManagerController(ILogger<ServicosWedManagerController> logger,
            ICidadeService cidadeService,
            IFornecedorService fornecedorService,
            IPacoteService pacoteService,
            IPaisService paisService,
            IEstadoService estadoService)
        {
            _logger = logger;
            _cidadeService = cidadeService;
            _fornecedorService = fornecedorService;
            _pacoteService = pacoteService;
            _paisService = paisService;
            _estadoService = estadoService;
        }

        [HttpGet]
        public ActionResult FazerOrcamento()
        {
            //var fornecedor = _fornecedorService.GetFornecedorById(int.Parse(HttpContext.Session.GetString("FornecedorID")));
            ViewBag.PacotesPorCategoria = _pacoteService.GetPacotesByCategoriaID(5);

            SetData();
            return View();
        }

        // GET: ServicosWedManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServicosWedManagerController/Create
        public ActionResult Create()
        {
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: ServicosWedManagerController/Create
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

        // GET: ServicosWedManagerController/Edit/5
        public ActionResult Edit(int ID)
        {
            ViewBag.Cidade = _cidadeService.GetCidadeById(ID);
            return View();
        }

        // POST: ServicosWedManagerController/Edit/5
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

        // POST: ServicosWedManagerController/Delete/5
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

        private void SetData()
        {
            ViewBag.Pacotes = _pacoteService.GetAllPacotes();
            ViewBag.Paises = _paisService.GetAllPaises();
            ViewBag.Estados = _estadoService.GetAllEstados();
            ViewBag.Cidades = _cidadeService.GetAllCidades();
        }
    }
}
