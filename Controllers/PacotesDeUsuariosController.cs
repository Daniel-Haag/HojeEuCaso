using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class PacotesDeUsuariosController : Controller
    {
        private readonly ILogger<PacotesDeUsuariosController> _logger;
        private readonly IPacotesDeUsuariosService _pacotesDeUsuariosService;
        private readonly IFornecedorService _fornecedorService;

        public PacotesDeUsuariosController(ILogger<PacotesDeUsuariosController> logger,
                                    IPacotesDeUsuariosService pacotesDeUsuariosService,
                                    IFornecedorService fornecedorService)
        {
            _logger = logger;
            _pacotesDeUsuariosService = pacotesDeUsuariosService;
            _fornecedorService = fornecedorService;
        }

        // GET: PacotesDeUsuariosController
        public ActionResult Index()
        {
            ViewBag.PacotesDeUsuarios = _pacotesDeUsuariosService.GetAllPacoteDeUsuarios();
            return View();
        }

        // GET: PacotesDeUsuariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PacotesDeUsuariosController/Create
        public ActionResult Create()
        {
            ViewBag.Fornecedores = _fornecedorService.GetAllFornecedor();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: PacotesDeUsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PacotesDeUsuarios pacoteDeUsuario)
        {
            try
            {
                var fornecedores = _fornecedorService.GetAllFornecedor();
                ViewBag.Fornecedores = fornecedores;

                pacoteDeUsuario.Fornecedor = fornecedores.FirstOrDefault(x => x.FornecedorID == pacoteDeUsuario.FornecedorID);

                _pacotesDeUsuariosService.CreateNewPacoteDeUsuario(pacoteDeUsuario);

                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: PacotesDeUsuariosController/Edit/5
        public ActionResult Edit(int ID)
        {
            var fornecedores = _fornecedorService.GetAllFornecedor();
            ViewBag.Fornecedores = fornecedores;

            var pacoteDeUsuario = _pacotesDeUsuariosService.GetPacoteDeUsuarioById(ID);
            ViewBag.PacoteDeUsuario = pacoteDeUsuario;
            ViewBag.Fornecedor = fornecedores.FirstOrDefault(x => x.FornecedorID == pacoteDeUsuario.FornecedorID);
            return View();
        }

        // POST: PacotesDeUsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PacotesDeUsuarios pacoteDeUsuario)
        {
            try
            {
                var fornecedores = _fornecedorService.GetAllFornecedor(); 
                ViewBag.Fornecedores = fornecedores;

                var fornecedor = fornecedores.FirstOrDefault(x => x.FornecedorID == pacoteDeUsuario.FornecedorID);
                pacoteDeUsuario.Fornecedor = fornecedor;
                ViewBag.Fornecedor = fornecedor;

                _pacotesDeUsuariosService.UpdatePacoteDeUsuario(pacoteDeUsuario);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.PacoteDeUsuario = pacoteDeUsuario;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: PacotesDeUsuariosController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _pacotesDeUsuariosService.DeletePacoteDeUsuario(id);
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
