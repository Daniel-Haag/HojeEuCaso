using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using RestSharp;
using HojeEuCaso.Dtos;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;

namespace HojeEuCaso.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IFornecedorService _fornecedorService;
        private readonly IFornecedorIndicadoService _fornecedorIndicadoService;
        private readonly ICidadeService _CidadeService;
        private readonly IEstadoService _estadoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IPaisService _paisService;

        public FornecedoresController(ILogger<UsuariosController> logger,
                                    IFornecedorService FornecedorService,
                                    ICidadeService CidadeService,
                                    IEstadoService estadoService,
                                    ICategoriaService categoriaService,
                                    IPaisService paisService,
                                    IFornecedorIndicadoService fornecedorIndicadoService)
        {
            _logger = logger;
            _fornecedorService = FornecedorService;
            _CidadeService = CidadeService;
            _estadoService = estadoService;
            _categoriaService = categoriaService;
            _paisService = paisService;
            _fornecedorIndicadoService = fornecedorIndicadoService;
        }

        // GET: UsuariosController
        public ActionResult Index()
        {
            ViewBag.Fornecedores = _fornecedorService.GetAllFornecedor();

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
            ViewBag.Cidades = _CidadeService.GetAllCidades();
            ViewBag.Estados = _estadoService.GetAllEstados();
            ViewBag.Paises = _paisService.GetAllPaises();
            ViewBag.Categorias = _categoriaService.GetAllCategorias();
            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fornecedor fornecedor)
        {
            try
            {
                var fornecedorExistente = _fornecedorService.GetFornecedorByEmail(fornecedor.Email);

                if (fornecedorExistente != null)
                {
                    TempData["ErrorMessage"] = "Email já cadastrado!";
                    return RedirectToAction("Create");
                }

                var Cidades = _CidadeService.GetAllCidades();
                ViewBag.Cidades = Cidades;
                fornecedor.Cidade = Cidades.FirstOrDefault(x => x.CidadeID == fornecedor.CidadeID);

                var estados = _estadoService.GetAllEstados();
                ViewBag.Estados = estados;
                fornecedor.Estado = estados.FirstOrDefault(x => x.EstadoID == fornecedor.EstadoID);

                var paises = _paisService.GetAllPaises();
                ViewBag.Paises = paises;
                fornecedor.Pais = paises.FirstOrDefault(x => x.PaisID == fornecedor.PaisID);

                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                fornecedor.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == fornecedor.CategoriaID);

                fornecedor.AsaasCustomerID = CriaClienteAsaas(fornecedor).Result;

                _fornecedorService.CreateNewFornecedor(fornecedor);

                if (fornecedor.IDIdentificacao > 0)
                {
                    var fornecedorPai = _fornecedorService.GetFornecedorById(fornecedor.IDIdentificacao);

                    var fornecedorIndicado = new FornecedorIndicado()
                    {
                        FornecedorID = fornecedorPai.FornecedorID,
                        FornecedorPai = fornecedorPai,
                        NomeFornecedor = fornecedor.Nome,
                        TotalAReceber = 0
                    };

                    _fornecedorIndicadoService.CreateNewFornecedorIndicado(fornecedorIndicado);
                }

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
            var fornecedor = _fornecedorService.GetFornecedorById(ID);
            ViewBag.Fornecedor = fornecedor;

            var Cidades = _CidadeService.GetAllCidades();
            ViewBag.Cidades = Cidades;
            ViewBag.Cidade = Cidades.FirstOrDefault(x => x.CidadeID == fornecedor.CidadeID);

            var estados = _estadoService.GetAllEstados();
            ViewBag.Estados = estados;
            ViewBag.Estado = estados.FirstOrDefault(x => x.EstadoID == fornecedor.EstadoID);

            var paises = _paisService.GetAllPaises();
            ViewBag.Paises = paises;
            ViewBag.Pais = paises.FirstOrDefault(x => x.PaisID == fornecedor.PaisID);

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
                var Cidades = _CidadeService.GetAllCidades();
                ViewBag.Cidades = Cidades;
                var Cidade = Cidades.FirstOrDefault(x => x.CidadeID == Fornecedor.CidadeID);

                Fornecedor.Cidade = Cidade;
                ViewBag.Cidade = Cidade;

                var estados = _estadoService.GetAllEstados();
                ViewBag.Estados = estados;
                var estado = estados.FirstOrDefault(x => x.EstadoID == Fornecedor.EstadoID);

                Fornecedor.Estado = estado;
                ViewBag.Estado = estado;

                var paises = _paisService.GetAllPaises();
                ViewBag.Paises = paises;
                var pais = paises.FirstOrDefault(x => x.PaisID == Fornecedor.PaisID);

                Fornecedor.Pais = pais;
                ViewBag.Pais = pais;

                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                var categoria = categorias.FirstOrDefault(x => x.CategoriaID == Fornecedor.CategoriaID);

                Fornecedor.Categoria = categoria;
                ViewBag.Categoria = categoria;

                _fornecedorService.UpdateFornecedor(Fornecedor);
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
                _fornecedorService.DeleteFornecedor(id);
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return RedirectToAction("Index");
            }
        }

        private async static Task<string> CriaClienteAsaas(Fornecedor fornecedor)
        {
            try
            {
                var options = new RestClientOptions("https://sandbox.asaas.com/api/v3/customers");
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("access_token", "$aact_YTU5YTE0M2M2N2I4MTliNzk0YTI5N2U5MzdjNWZmNDQ6OjAwMDAwMDAwMDAwMDAwNjYyNDY6OiRhYWNoX2ZkNjdjZWY0LTViMmYtNDU2NS1iMTk2LWYyZWEzOGIyMGRjNw==");
                request.AddJsonBody(new
                {
                    name = fornecedor.Nome,
                    cpfCnpj = fornecedor.CPFCNPJResponsavelConta,
                    email = fornecedor.Email,
                    phone = fornecedor.Telefone,
                    mobilePhone = fornecedor.Telefone
                });
                var response = await client.PostAsync(request);

                string jsonResponse = response.Content;
                CreateCustomerAsaasResponseDto createCustomerAsaasResponseDto = JsonConvert.DeserializeObject<CreateCustomerAsaasResponseDto>(jsonResponse);
                string customerID = createCustomerAsaasResponseDto.id;

                return customerID;
            }
            catch (Exception e)
            {
                string erro = e.Message;
            }

            return null;
        }
    }
}
