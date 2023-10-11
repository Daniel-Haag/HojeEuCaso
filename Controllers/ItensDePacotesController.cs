using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class ItensDePacotesController : Controller
    {
        private readonly ILogger<ItensDePacotesController> _logger;
        private readonly IItensDePacotesService _itensDePacotesService;
        private readonly IPacoteService _pacoteService;

        public ItensDePacotesController(ILogger<ItensDePacotesController> logger,
                                    IItensDePacotesService itensDePacotesService,
                                    IPacoteService pacoteService)
        {
            _logger = logger;
            _itensDePacotesService = itensDePacotesService;
            _pacoteService = pacoteService;
        }

        // GET: ItensDePacotesController
        public ActionResult Index()
        {
            ViewBag.ItensDePacotes = _itensDePacotesService.GetAllItensDePacotes();
            return View();
        }

        // GET: ItensDePacotesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItensDePacotesController/Create
        public ActionResult Create()
        {
            ViewBag.Pacotes = _pacoteService.GetAllPacotes();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: ItensDePacotesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItensDePacotes itemDePacote)
        {
            try
            {
                var pacotes = _pacoteService.GetAllPacotes();
                ViewBag.Pacotes = pacotes;

                itemDePacote.Pacote = pacotes.FirstOrDefault(x => x.PacoteID == itemDePacote.PacoteID);

                _itensDePacotesService.CreateNewItensDePacotes(itemDePacote);

                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: ItensDePacotesController/Edit/5
        public ActionResult Edit(int ID)
        {
            var pacotes = _pacoteService.GetAllPacotes();
            ViewBag.Pacotes = pacotes;

            var itemDePacote = _itensDePacotesService.GetItensDePacotesById(ID);
            ViewBag.ItemDePacote = itemDePacote;
            ViewBag.Pacote = pacotes.FirstOrDefault(x => x.PacoteID == itemDePacote.PacoteID);
            return View();
        }

        // POST: ItensDePacotesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItensDePacotes itemDePacote)
        {
            try
            {
                var pacotes = _pacoteService.GetAllPacotes();
                ViewBag.Pacotes = pacotes;

                var pacote = pacotes.FirstOrDefault(x => x.PacoteID == itemDePacote.PacoteID);
                itemDePacote.Pacote = pacote;
                ViewBag.Pacote = pacote;

                _itensDePacotesService.UpdateItensDePacotes(itemDePacote);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.ItemDePacote = itemDePacote;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: ItensDePacotesController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _itensDePacotesService.DeleteItensDePacotes(id);
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
