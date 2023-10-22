using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;

namespace HojeEuCaso.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuarioSistemaService _usuarioSistemaService;
        private readonly ICidadeService _cidadeService;
        private readonly IEstadoService _estadoService;

        public UsuariosController(ILogger<UsuariosController> logger,
                                    IUsuarioSistemaService usuarioSistemaService,
                                    ICidadeService cidadeService,
                                    IEstadoService estadoService)
        {
            _logger = logger;
            _usuarioSistemaService = usuarioSistemaService;
            _cidadeService = cidadeService;
            _estadoService = estadoService;
        }

        // GET: UsuariosController
        public ActionResult Index()
        {
            ViewBag.UsuariosSistema = _usuarioSistemaService.GetAllUsuarioSistema();
            return View();
        }

        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            ViewBag.Cidades = _cidadeService.GetAllCidades();
            ViewBag.Estados = _estadoService.GetAllEstados();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioSistema usuarioSistema)
        {
            try
            {
                var cidades = _cidadeService.GetAllCidades();
                ViewBag.Cidades = cidades;
                usuarioSistema.Cidade = cidades.FirstOrDefault(x => x.CidadeID == usuarioSistema.CidadeID);

                var estados = _estadoService.GetAllEstados();
                ViewBag.Estados = estados;
                usuarioSistema.Estado = estados.FirstOrDefault(x => x.EstadoID == usuarioSistema.EstadoID);

                _usuarioSistemaService.CreateNewUsuarioSistema(usuarioSistema);

                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int ID)
        {
            var usuarioSistema = _usuarioSistemaService.GetUsuarioSistemaById(ID);
            ViewBag.Usuario = usuarioSistema;



            //ViewBag.Usuario.DataNascimento = ((DateTime)usuarioSistema.DataNascimento).ToString("yyyy-MM-dd");


            var cidades = _cidadeService.GetAllCidades();
            ViewBag.Cidades = cidades;
            ViewBag.Cidade = cidades.FirstOrDefault(x => x.CidadeID == usuarioSistema.CidadeID);

            var estados = _estadoService.GetAllEstados();
            ViewBag.Estados = estados;
            ViewBag.Estado = estados.FirstOrDefault(x => x.EstadoID == usuarioSistema.EstadoID);

            return View();
        }


        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioSistema usuarioSistema)
        {
            try
            {
                var cidades = _cidadeService.GetAllCidades();
                ViewBag.Cidades = cidades;
                var cidade = cidades.FirstOrDefault(x => x.CidadeID == usuarioSistema.CidadeID);

                usuarioSistema.Cidade = cidade;
                ViewBag.Cidade = cidade;

                var estados = _estadoService.GetAllEstados();
                ViewBag.Estados = estados;
                var estado = estados.FirstOrDefault(x => x.EstadoID == usuarioSistema.EstadoID);

                usuarioSistema.Estado = estado;
                ViewBag.Estado = estado;

                _usuarioSistemaService.UpdateUsuarioSistema(usuarioSistema);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.UsuarioSistema = usuarioSistema;
                return View();
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _usuarioSistemaService.DeleteUsuarioSistema(id);
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
