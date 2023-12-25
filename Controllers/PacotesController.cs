using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using HojeEuCaso.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class PacotesController : Controller
    {
        private readonly ILogger<PacotesController> _logger;
        private readonly IPacoteService _pacoteService;
        private readonly ICategoriaService _categoriaService;

        public PacotesController(ILogger<PacotesController> logger,
                                    IPacoteService pacoteService,
                                    ICategoriaService categoriaService)
        {
            _logger = logger;
            _pacoteService = pacoteService;
            _categoriaService = categoriaService;
        }

        // GET: PacotesController
        public ActionResult Index()
        {
            ViewBag.Pacotes = _pacoteService.GetAllPacotes();
            return View();
        }

        // GET: PacotesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PacotesController/Create
        public ActionResult Create()
        {
            ViewBag.Categorias = _categoriaService.GetAllCategorias();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: PacotesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pacote pacote)
        {
            try
            {
                string valorNumerico = pacote.PrecoTexto.Replace("R$", "");
                decimal precoDecimal;

                if (decimal.TryParse(valorNumerico, out precoDecimal))
                {
                    pacote.Preco = precoDecimal;
                    var categorias = _categoriaService.GetAllCategorias();
                    ViewBag.Categorias = categorias;
                    pacote.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == pacote.CategoriaID);
                    pacote.Ativo = true;
                    _pacoteService.CreateNewPacote(pacote);

                    TempData["SuccessMessage"] = "Salvo com sucesso!";
                    return View();
                }

                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: PacotesController/Edit/5
        public ActionResult Edit(int ID)
        {
            var categorias = _categoriaService.GetAllCategorias();
            ViewBag.Categorias = categorias;

            var Pacote = _pacoteService.GetPacoteById(ID);
            ViewBag.Pacote = Pacote;
            ViewBag.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == Pacote.CategoriaID);
            return View();
        }

        // POST: PacotesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pacote pacote)
        {
            try
            {
                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                var categoria = categorias.FirstOrDefault(x => x.CategoriaID == pacote.CategoriaID);

                pacote.Categoria = categoria;
                ViewBag.Categoria = categoria;

                _pacoteService.UpdatePacote(pacote);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.Pacote = pacote;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: PacotesController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _pacoteService.DeletePacote(id);
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
