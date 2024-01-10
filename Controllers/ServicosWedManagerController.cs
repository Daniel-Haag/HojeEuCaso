using HojeEuCaso.Dtos;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using HojeEuCaso.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;

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
        private readonly ICategoriaService _categoriaService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServicosWedManagerController(ILogger<ServicosWedManagerController> logger,
            ICidadeService cidadeService,
            IFornecedorService fornecedorService,
            IPacoteService pacoteService,
            IPaisService paisService,
            IEstadoService estadoService,
            ICategoriaService categoriaService,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _cidadeService = cidadeService;
            _fornecedorService = fornecedorService;
            _pacoteService = pacoteService;
            _paisService = paisService;
            _estadoService = estadoService;
            _categoriaService = categoriaService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult FazerOrcamento()
        {
            try
            {
                SetData();

                //var fornecedor = _fornecedorService.GetFornecedorById(int.Parse(HttpContext.Session.GetString("FornecedorID")));
                ViewBag.Categorias = _categoriaService.GetAllCategorias();

                //É possível obter os devidos registros dessa forma
                //var teste2 = CatalogoGeografico.Where(x => x.Estado.Pais.PaisID == 1);
                ViewBag.CatalogoGeografico = _cidadeService.GetAllCidades();




                return View();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult FazerOrcamentoFornecedores(OrcamentoDto orcamentoDto)
        {
            //VERIFICAR A AGENDA DO FORNECEDOR

            try
            {

                SetData();

                //Se o objeto não tiver o valor do orçamento, buscar todos os fornecedores
                if (!string.IsNullOrEmpty(orcamentoDto.OrcamentoTexto))
                {
                    string valorNumerico = orcamentoDto.OrcamentoTexto.Replace("R$", "");
                    decimal orcamentoDecimal;
                    List<Categoria> categorias = new List<Categoria>();
                    List<Fornecedor> fornecedores = new List<Fornecedor>();
                    var diretorio = Path.Combine(_webHostEnvironment.WebRootPath);

                    if (decimal.TryParse(valorNumerico, out orcamentoDecimal))
                    {
                        //var fornecedor = _fornecedorService.GetFornecedorById(int.Parse(HttpContext.Session.GetString("FornecedorID")));
                        ViewBag.CatalogoGeografico = _cidadeService.GetAllCidades();

                        var todasCategorias = _categoriaService.GetAllCategorias();
                        var categoriaIDs = orcamentoDto.CategoriasSelecionadas.ToArray();
                        var categoriasFiltradas = todasCategorias
                            .Where(x => categoriaIDs.Contains(x.CategoriaID));
                        var quantidadeDeCategorias = categoriasFiltradas.Count();
                        var servicos = _pacoteService.GetAllPacotes()
                            .Where(x => categoriaIDs.Contains(x.CategoriaID) && x.Fornecedor != null).ToList();

                        foreach (var servico in servicos.ToList())
                        {
                            if (servico.Preco > orcamentoDecimal)
                            {
                                servicos.Remove(servico);
                                continue;
                            }

                            if (orcamentoDto.QtdMaximaPessoas > servico.QtdMaximaPessoas)
                            {
                                servicos.Remove(servico);
                                continue;
                            }

                            if (servico.Pais?.PaisID != orcamentoDto.PaisID)
                            {
                                servicos.Remove(servico);
                                continue;
                            }
                            else if (servico.Estado?.EstadoID != orcamentoDto.EstadoID)
                            {
                                servicos.Remove(servico);
                                continue;
                            }
                            else if (servico.Cidade?.CidadeID != orcamentoDto.CidadeID)
                            {
                                servicos.Remove(servico);
                                continue;
                            }

                            if (servico.Categoria != null)
                            {
                                var categoriaJaIncluida = categorias
                                    .FirstOrDefault(x => x.CategoriaID == servico.CategoriaID);

                                if (categoriaJaIncluida == null)
                                    categorias.Add(servico.Categoria);
                            }

                            if (servico.Fornecedor != null)
                            {
                                var fornecedorJaIncluido = fornecedores
                                    .FirstOrDefault(x => x.FornecedorID == servico.FornecedorID);

                                var caminhoImagem = servico.Fornecedor.CaminhoFoto?.Replace(diretorio, "~");
                                caminhoImagem = caminhoImagem?.Replace("\\", "/");
                                servico.Fornecedor.CaminhoFoto = caminhoImagem;

                                if (fornecedorJaIncluido == null)
                                    fornecedores.Add(servico.Fornecedor);
                            }

                        }

                        foreach (var categoria in categorias)
                        {
                            var fornecedoresCategoria = fornecedores.
                                Where(x => x.CategoriaID == categoria.CategoriaID).ToList();

                            categoria.Fornecedores = fornecedoresCategoria;
                        }

                        ViewBag.Categorias = categorias;

                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }

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
