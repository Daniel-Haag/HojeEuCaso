using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class ItensDePacotesDeUsuariosController : Controller
    {
        private readonly ILogger<ItensDePacotesDeUsuariosController> _logger;
        private readonly IItensDePacotesDeUsuariosService _ItensDePacotesDeUsuariosService;
        private readonly IPacoteService _pacoteService;

        public ItensDePacotesDeUsuariosController(ILogger<ItensDePacotesDeUsuariosController> logger,
                                    IItensDePacotesDeUsuariosService ItensDePacotesDeUsuariosService,
                                    IPacoteService pacoteService)
        {
            _logger = logger;
            _ItensDePacotesDeUsuariosService = ItensDePacotesDeUsuariosService;
            _pacoteService = pacoteService;
        }

        // GET: ItensDePacotesDeUsuariosController
        public ActionResult Index()
        {
            ViewBag.ItensDePacotesDeUsuarios = _ItensDePacotesDeUsuariosService.GetAllItensDePacotesDeUsuarios();
            return View();
        }

        // GET: ItensDePacotesDeUsuariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItensDePacotesDeUsuariosController/Create
        public ActionResult Create()
        {
            ViewBag.Pacotes = _pacoteService.GetAllPacotes();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: ItensDePacotesDeUsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItensDePacotesDeUsuarios itemDePacote)
        {
            try
            {
                var pacotes = _pacoteService.GetAllPacotes();
                ViewBag.Pacotes = pacotes;

                itemDePacote.Pacote = pacotes.FirstOrDefault(x => x.PacoteID == itemDePacote.PacoteID);

                _ItensDePacotesDeUsuariosService.CreateNewItensDePacotesDeUsuarios(itemDePacote);

                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: ItensDePacotesDeUsuariosController/Edit/5
        public ActionResult Edit(int ID)
        {
            var pacotes = _pacoteService.GetAllPacotes();
            ViewBag.Pacotes = pacotes;

            var itemDePacote = _ItensDePacotesDeUsuariosService.GetItensDePacotesDeUsuariosById(ID);
            ViewBag.ItemDePacote = itemDePacote;
            ViewBag.Pacote = pacotes.FirstOrDefault(x => x.PacoteID == itemDePacote.PacoteID);
            return View();
        }

        // POST: ItensDePacotesDeUsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItensDePacotesDeUsuarios itemDePacote)
        {
            try
            {
                var pacotes = _pacoteService.GetAllPacotes();
                ViewBag.Pacotes = pacotes;

                var pacote = pacotes.FirstOrDefault(x => x.PacoteID == itemDePacote.PacoteID);
                itemDePacote.Pacote = pacote;
                ViewBag.Pacote = pacote;

                _ItensDePacotesDeUsuariosService.UpdateItensDePacotesDeUsuarios(itemDePacote);
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

        // POST: ItensDePacotesDeUsuariosController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _ItensDePacotesDeUsuariosService.DeleteItensDePacotesDeUsuarios(id);
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
