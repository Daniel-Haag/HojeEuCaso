using AutoMapper;
using HojeEuCaso.Dtos;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using HojeEuCaso.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace HojeEuCaso.Controllers
{
    public class ServicosFornecedorController : Controller
    {
        private readonly ILogger<PacotesController> _logger;
        private readonly IPacoteService _pacoteService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;
        private readonly ICidadeService _cidadeService;
        private readonly IEstadoService _estadoService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFornecedorService _fornecedorService;
        private readonly IItensDePacotesService _itensDePacotesService;

        public ServicosFornecedorController(ILogger<PacotesController> logger,
                                    IPacoteService pacoteService,
                                    ICategoriaService categoriaService,
                                    IMapper mapper,
                                    ICidadeService cidadeService,
                                    IEstadoService estadoService,
                                    IWebHostEnvironment webHostEnvironment,
                                    IFornecedorService fornecedorService,
                                    IItensDePacotesService itensDePacotesService)
        {
            _logger = logger;
            _pacoteService = pacoteService;
            _categoriaService = categoriaService;
            _mapper = mapper;
            _cidadeService = cidadeService;
            _estadoService = estadoService;
            _webHostEnvironment = webHostEnvironment;
            _fornecedorService = fornecedorService;
            _itensDePacotesService = itensDePacotesService;
        }

        public ActionResult AdicionarServico()
        {
            SetData();
            return View();
        }

        // GET: PacotesController
        public ActionResult Index()
        {
            //Buscando os Servicos/Pacotes do Fornecedor Logado!
            int fornecedorID = int.Parse(HttpContext.Session.GetString("FornecedorID"));
            ViewBag.Pacotes = _pacoteService.GetPacoteByFornecedor(fornecedorID);
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
            //Talvez entre em desuso
            SetData();

            TempData["SuccessMessage"] = null;
            return View();
        }

        // POST: PacotesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarServico(PacoteComItensDoPacoteDto pacoteDto)
        {
            try
            {
                //Não esquecer de tratar o campo de preço como valor

                //Não esquecer de delegar todas estas funcionalidades em métodos menores

                pacoteDto.ReajusteAnualPorcentagem = TransformToPercentDiscount(pacoteDto.ReajusteAnualPorcentagem);
                pacoteDto.DescontoSegundaFeira = TransformToPercentDiscount(pacoteDto.DescontoSegundaFeira);
                pacoteDto.DescontoTercaFeira = TransformToPercentDiscount(pacoteDto.DescontoTercaFeira);
                pacoteDto.DescontoQuartaFeira = TransformToPercentDiscount(pacoteDto.DescontoQuartaFeira);
                pacoteDto.DescontoQuintaFeira = TransformToPercentDiscount(pacoteDto.DescontoQuintaFeira);
                pacoteDto.DescontoSextaFeira = TransformToPercentDiscount(pacoteDto.DescontoSextaFeira);
                pacoteDto.DescontoSabado = TransformToPercentDiscount(pacoteDto.DescontoSabado);
                pacoteDto.DescontoDomingo = TransformToPercentDiscount(pacoteDto.DescontoDomingo);

                SetData();

                if (pacoteDto.Foto != null && pacoteDto.Foto.Length > 0)
                {
                    long maxFileSize = 10 * 1024 * 1024; // 10MB
                    if (pacoteDto.Foto.Length > maxFileSize)
                    {
                        ModelState.AddModelError("Foto", "O tamanho da foto excede o limite de 10MB.");
                        return View();
                    }

                    pacoteDto.CaminhoFoto = Path.Combine(_webHostEnvironment.WebRootPath, "images", pacoteDto.Foto.FileName);

                    using (var stream = new FileStream(pacoteDto.CaminhoFoto, FileMode.Create))
                    {
                        pacoteDto.Foto.CopyTo(stream);
                    }
                }

                if (pacoteDto.Video != null && pacoteDto.Video.Length > 0)
                {
                    long maxVideoSize = 10 * 1024 * 1024; // 10MB
                    if (pacoteDto.Video.Length > maxVideoSize)
                    {
                        ModelState.AddModelError("Video", "O tamanho do vídeo excede o limite de 10MB.");
                        return View();
                    }

                    pacoteDto.CaminhoVideo = Path.Combine(_webHostEnvironment.WebRootPath, "videos", pacoteDto.Video.FileName);

                    using (var stream = new FileStream(pacoteDto.CaminhoVideo, FileMode.Create))
                    {
                        pacoteDto.Video.CopyTo(stream);
                    }
                }

                //Receber dto e transformar nos modelos disponíveis...
                Pacote pacote = _mapper.Map<Pacote>(pacoteDto);
                List<ItensDePacotes> itensDePacotes = _mapper
                    .Map<List<ItensDePacotes>>(pacoteDto.ItensDePacotes);

                pacote.Cidade = _cidadeService.GetCidadeById(pacoteDto.CidadeID);
                pacote.Estado = _estadoService.GetEstadoById(pacoteDto.EstadoID);

                pacote.Fornecedor = _fornecedorService
                    .GetFornecedorById(int.Parse(HttpContext.Session.GetString("FornecedorID")));

                //Como fica a definição de categoria???
                //Por enquanto vou definir de forma paleativa
                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                pacote.Categoria = categorias.FirstOrDefault(/*x => x.CategoriaID == pacote.CategoriaID*/);

                //Primeiro criar o pacote para preencher o PacoteID do itensDePacote
                var pacoteID = _pacoteService.CreateNewPacote(pacote);

                foreach (var itemDePacote in itensDePacotes)
                {
                    itemDePacote.PacoteID = pacoteID;
                    _itensDePacotesService.CreateNewItensDePacotes(itemDePacote);
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

        private void SetData()
        {
            ViewBag.Pacotes = _pacoteService.GetAllPacotes();
            ViewBag.Estados = _estadoService.GetAllEstados();
            ViewBag.Cidades = _cidadeService.GetAllCidades();
        }

        private decimal TransformToPercentDiscount(decimal number)
        {
            number = number / 100;
            return number;
        }
    }
}
