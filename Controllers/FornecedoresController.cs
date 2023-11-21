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
    public class FornecedoresController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IFornecedorService _FornecedorService;
        private readonly ICidadeService _cidadeService;
        private readonly IEstadoService _estadoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IPaisService _paisService;

        public FornecedoresController(ILogger<UsuariosController> logger,
                                    IFornecedorService FornecedorService,
                                    ICidadeService cidadeService,
                                    IEstadoService estadoService,
                                    ICategoriaService categoriaService,
                                    IPaisService paisService)
        {
            _logger = logger;
            _FornecedorService = FornecedorService;
            _cidadeService = cidadeService;
            _estadoService = estadoService;
            _categoriaService = categoriaService;
            _paisService = paisService;
        }

        // GET: UsuariosController
        public ActionResult Index()
        {
            ViewBag.Fornecedores = _FornecedorService.GetAllFornecedor();

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
            ViewBag.Paises = _paisService.GetAllPaises();
            ViewBag.Categorias = _categoriaService.GetAllCategorias();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fornecedor Fornecedor)
        {
            try
            {
                var cidades = _cidadeService.GetAllCidades();
                ViewBag.Cidades = cidades;
                Fornecedor.Cidade = cidades.FirstOrDefault(x => x.CidadeID == Fornecedor.CidadeID);

                var estados = _estadoService.GetAllEstados();
                ViewBag.Estados = estados;
                Fornecedor.Estado = estados.FirstOrDefault(x => x.EstadoID == Fornecedor.EstadoID);

                var paises = _paisService.GetAllPaises();
                ViewBag.Paises = paises;
                Fornecedor.Pais = paises.FirstOrDefault(x => x.PaisID == Fornecedor.Pais.PaisID);

                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                Fornecedor.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == Fornecedor.CategoriaID);

                _FornecedorService.CreateNewFornecedor(Fornecedor);

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
            var fornecedor = _FornecedorService.GetFornecedorById(ID);
            ViewBag.Fornecedor = fornecedor;

            var cidades = _cidadeService.GetAllCidades();
            ViewBag.Cidades = cidades;
            ViewBag.Cidade = cidades.FirstOrDefault(x => x.CidadeID == fornecedor.CidadeID);

            var estados = _estadoService.GetAllEstados();
            ViewBag.Estados = estados;
            ViewBag.Estado = estados.FirstOrDefault(x => x.EstadoID == fornecedor.EstadoID);

            var paises = _paisService.GetAllPaises();
            ViewBag.Paises = paises;
            ViewBag.Pais = paises.FirstOrDefault(x => x.PaisID == fornecedor.Pais?.PaisID);

            var categorias = _categoriaService.GetAllCategorias();
            ViewBag.Categorias = categorias;
            ViewBag.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == fornecedor.CategoriaID);

            return View();
        }


        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fornecedor Fornecedor)
        {
            try
            {
                var cidades = _cidadeService.GetAllCidades();
                ViewBag.Cidades = cidades;
                var cidade = cidades.FirstOrDefault(x => x.CidadeID == Fornecedor.CidadeID);

                Fornecedor.Cidade = cidade;
                ViewBag.Cidade = cidade;

                var estados = _estadoService.GetAllEstados();
                ViewBag.Estados = estados;
                var estado = estados.FirstOrDefault(x => x.EstadoID == Fornecedor.EstadoID);

                Fornecedor.Estado = estado;
                ViewBag.Estado = estado;

                var paises = _paisService.GetAllPaises();
                ViewBag.Paises = paises;
                var pais = paises.FirstOrDefault(x => x.PaisID == Fornecedor.Pais?.PaisID);

                Fornecedor.Pais = pais;
                ViewBag.Pais = pais;

                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                var categoria = categorias.FirstOrDefault(x => x.CategoriaID == Fornecedor.CategoriaID);

                Fornecedor.Categoria = categoria;
                ViewBag.Categoria = categoria;

                _FornecedorService.UpdateFornecedor(Fornecedor);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                ViewBag.Fornecedor = Fornecedor;
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
                _FornecedorService.DeleteFornecedor(id);
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
