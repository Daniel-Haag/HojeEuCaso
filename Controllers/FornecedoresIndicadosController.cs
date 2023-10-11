using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class FornecedoresIndicadosController : Controller
    {
        private readonly ILogger<FornecedoresIndicadosController> _logger;
        private readonly IFornecedorIndicadoService _fornecedorIndicadoService;
        private readonly IFornecedorService _fornecedorService;

        public FornecedoresIndicadosController(ILogger<FornecedoresIndicadosController> logger,
                                    IFornecedorIndicadoService fornecedorIndicadoService,
                                    IFornecedorService fornecedorService)
        {
            _logger = logger;
            _fornecedorIndicadoService = fornecedorIndicadoService;
            _fornecedorService = fornecedorService;
        }

        // GET: FornecedorIndicadosController
        public ActionResult Index()
        {
            ViewBag.FornecedoresIndicados = _fornecedorIndicadoService.GetAllFornecedorIndicados();
            return View();
        }

        // GET: FornecedorIndicadosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FornecedorIndicadosController/Create
        public ActionResult Create()
        {
            ViewBag.Fornecedores = _fornecedorService.GetAllFornecedor();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: FornecedorIndicadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FornecedorIndicado fornecedorIndicado)
        {
            try
            {
                var fornecedores = _fornecedorService.GetAllFornecedor();
                ViewBag.Fornecedores = fornecedores;

                fornecedorIndicado.FornecedorPai = fornecedores
                    .FirstOrDefault(x => x.FornecedorID == fornecedorIndicado.FornecedorID);

                _fornecedorIndicadoService.CreateNewFornecedorIndicado(fornecedorIndicado);

                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: FornecedorIndicadosController/Edit/5
        public ActionResult Edit(int ID)
        {
            var fornecedores = _fornecedorService.GetAllFornecedor();
            ViewBag.Fornecedores = fornecedores;

            var fornecedorIndicado = _fornecedorIndicadoService.GetFornecedorIndicadoById(ID);
            ViewBag.FornecedorIndicado = fornecedorIndicado;
            ViewBag.Fornecedor = fornecedores.FirstOrDefault(x => x.FornecedorID == fornecedorIndicado.FornecedorID);
            return View();
        }

        // POST: FornecedorIndicadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FornecedorIndicado fornecedorIndicado)
        {
            try
            {
                var fornecedores = _fornecedorService.GetAllFornecedor();
                ViewBag.Fornecedores = fornecedores;

                var fornecedor = fornecedores.FirstOrDefault(x => x.FornecedorID == fornecedorIndicado.FornecedorID);
                fornecedorIndicado.FornecedorPai = fornecedor;
                ViewBag.Fornecedor = fornecedor;

                _fornecedorIndicadoService.UpdateFornecedorIndicado(fornecedorIndicado);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.FornecedorIndicado = fornecedorIndicado;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: FornecedorIndicadosController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _fornecedorIndicadoService.DeleteFornecedorIndicado(id);
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
