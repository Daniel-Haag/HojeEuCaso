using AutoMapper;
using HojeEuCaso.Dtos;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.codec.wmf;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using System.Threading.Tasks;
using HojeEuCaso.Services;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using static iTextSharp.text.pdf.AcroFields;

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
        private readonly IClausulaContratoService _clausulasDeContratoService;
        private readonly IPlanoService _planoService;
        private readonly IPaisService _paisService;
        private readonly IFotoServicoService _fotoServicoService;
        private readonly IPlanoFornecedorService _planoFornecedorService;
        private readonly HttpClient _httpClient;

        public ServicosFornecedorController(ILogger<PacotesController> logger,
                                    IPacoteService pacoteService,
                                    ICategoriaService categoriaService,
                                    IMapper mapper,
                                    ICidadeService cidadeService,
                                    IEstadoService estadoService,
                                    IWebHostEnvironment webHostEnvironment,
                                    IFornecedorService fornecedorService,
                                    IItensDePacotesService itensDePacotesService,
                                    IClausulaContratoService clausulasDeContratoService,
                                    IPlanoService planoService,
                                    IPaisService paisService,
                                    IFotoServicoService fotoServicoService,
                                    IPlanoFornecedorService planoFornecedorService,
                                    HttpClient httpClient)
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
            _clausulasDeContratoService = clausulasDeContratoService;
            _planoService = planoService;
            _paisService = paisService;
            _fotoServicoService = fotoServicoService;
            _planoFornecedorService = planoFornecedorService;
            _httpClient = httpClient;
        }

        // GET: PacotesController
        public ActionResult Index()
        {
            //Buscando os Servicos/Pacotes do Fornecedor Logado!
            try
            {
                int fornecedorID = int.Parse(HttpContext.Session.GetString("FornecedorID"));
                var pacotes = _pacoteService.GetPacoteByFornecedor(fornecedorID);
                ViewBag.Pacotes = pacotes;

                List<FotosServicos> fotosServicos = new List<FotosServicos>();

                foreach (var pacote in pacotes)
                {
                    var fotosServicosPacote = _fotoServicoService.GetFotosServicoByServiceId(pacote.PacoteID);

                    foreach (var fotoServicoPacote in fotosServicosPacote)
                    {
                        fotosServicos.Add(fotoServicoPacote);
                    }

                }

                ViewBag.FotosServicos = fotosServicos;

                ViewBag.Diretorio = Path.Combine(_webHostEnvironment.WebRootPath);
            }
            catch (ArgumentNullException ex)
            {
                if (ex.Message == "Value cannot be null. (Parameter 's')" || ex.Message == "Value cannot be null. Arg_ParamName_Name")
                {
                    RedirectToAction("Logout", "Login");
                }
            }

            return View();
        }

        // GET: PacotesController
        public ActionResult Profile()
        {
            //Buscando os Servicos/Pacotes do Fornecedor Logado!
            int fornecedorID = int.Parse(HttpContext.Session.GetString("FornecedorID"));

            ViewBag.Pacotes = _pacoteService.GetPacoteByFornecedor((int)fornecedorID);
            ViewBag.Diretorio = Path.Combine(_webHostEnvironment.WebRootPath);

            var fornecedor = _fornecedorService.GetFornecedorById((int)fornecedorID);

            var diretorio = Path.Combine(_webHostEnvironment.WebRootPath);

            var caminhoImagem = fornecedor.CaminhoFoto?.Replace(diretorio, "~");
            caminhoImagem = caminhoImagem?.Replace("\\", "/");

            ViewBag.FotoExistente = caminhoImagem;

            ViewBag.Fornecedor = fornecedor;

            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            //Talvez esse método entre em desuso
            SetData();

            TempData["SuccessMessage"] = null;
            return View();
        }

        [HttpGet]
        public ActionResult AdicionarServico()
        {
            var fornecedor = _fornecedorService.GetFornecedorById(int.Parse(HttpContext.Session.GetString("FornecedorID")));
            ViewBag.PacotesPorCategoria = _pacoteService.GetPacotesByCategoriaID(fornecedor.CategoriaID).Where(x => x.Fornecedor == null);

            SetData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarServico(PacoteComItensDoPacoteDto pacoteDto)
        {
            try
            {
                //Melhorar estas buscas por todos os registros
                //Não esquecer de delegar todas estas funcionalidades em métodos menores
                SetData();
                TransformAllPercentProps(pacoteDto);

                Pacote pacote;
                List<ItensDePacotes> itensDePacotes;
                MapPacoteDtoForPacoteObject(pacoteDto, out pacote, out itensDePacotes);

                pacote.Cidade = _cidadeService.GetCidadeById(pacoteDto.CidadeID);
                pacote.Estado = _estadoService.GetEstadoById(pacoteDto.EstadoID);
                pacote.Pais = _paisService.GetPaisById(pacoteDto.PaisID);

                pacote.Fornecedor = _fornecedorService
                    .GetFornecedorById(int.Parse(HttpContext.Session.GetString("FornecedorID")));

                //Definir o serviço pela categoria do fornecedor!
                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                pacote.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == pacote.Fornecedor.CategoriaID);

                if (pacoteDto.Fotos != null && pacoteDto.Fotos.Count() >= 1)
                {
                    foreach (var item in pacoteDto.Fotos)
                    {
                        // Verificação de extensão
                        string[] allowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                        string fileExtension = Path.GetExtension(item.FileName).ToLower();

                        if (!allowedImageExtensions.Contains(fileExtension))
                        {
                            TempData["ErrorMessage"] = "Um dos arquivos não é um arquivo de imagem válido.";
                            ModelState.AddModelError("Foto", "Um dos arquivos não é um arquivo de imagem válido.");
                            return RedirectToAction("AdicionarServico");
                        }

                        long maxFileSize = 10 * 1024 * 1024; // 10MB

                        if (item.Length > maxFileSize)
                        {
                            TempData["ErrorMessage"] = "O tamanho da foto excede o limite de 10MB.";

                            ModelState.AddModelError("Foto", "O tamanho da foto excede o limite de 10MB.");
                            return RedirectToAction("AdicionarServico");
                        }
                    }
                }

                if (pacoteDto.Video != null && pacoteDto.Video.Length > 0)
                {
                    // Verificação de extensão
                    string[] allowedVideoExtensions = { ".mp4", ".avi", ".mkv", ".mov" };
                    string videoExtension = Path.GetExtension(pacoteDto.Video.FileName).ToLower();

                    if (!allowedVideoExtensions.Contains(videoExtension))
                    {
                        TempData["ErrorMessage"] = "O arquivo de vídeo não é um arquivo válido.";

                        ModelState.AddModelError("Video", "O arquivo de vídeo não é um arquivo válido.");
                        return RedirectToAction("AdicionarServico");
                    }

                    long maxVideoSize = 10 * 1024 * 1024; // 10MB
                    if (pacoteDto.Video.Length > maxVideoSize)
                    {
                        TempData["ErrorMessage"] = "O tamanho do vídeo excede o limite de 10MB.";

                        ModelState.AddModelError("Video", "O tamanho do vídeo excede o limite de 10MB.");
                        return RedirectToAction("AdicionarServico");
                    }

                    CopyVideoStream(pacoteDto);
                }

                var pacotePiloto = _pacoteService
                    .GetPacoteByTitulo(pacote.Titulo);

                if (pacotePiloto != null && pacotePiloto?.Fornecedor == null)
                {
                    _pacoteService.DeletePacote(pacotePiloto.PacoteID);
                }

                var pacoteID = _pacoteService.CreateNewPacote(pacote);

                foreach (var itemDePacote in itensDePacotes)
                {
                    itemDePacote.PacoteID = pacoteID;
                    _itensDePacotesService.CreateNewItensDePacotes(itemDePacote);
                }

                ViewBag.PacotesPorCategoria = _pacoteService.GetPacotesByCategoriaID(pacote.Fornecedor.CategoriaID);

                //Tratando se o usuário selecionar várias fotos ao mesmo tempo...
                if (pacoteDto.Fotos != null && pacoteDto.Fotos.Count() >= 1)
                {
                    foreach (var item in pacoteDto.Fotos)
                    {
                        var caminhoFoto = Path.Combine(_webHostEnvironment.WebRootPath, "images", HttpContext.Session.GetString("FornecedorID") + "_" + item.FileName);

                        FotosServicos fotoServico = new FotosServicos()
                        {
                            PacoteID = pacoteID,
                            Pacote = pacote,
                            CaminhoFoto = caminhoFoto
                        };

                        if (!System.IO.File.Exists(caminhoFoto))
                        {
                            _fotoServicoService.CreateNewFotoServico(fotoServico);
                            CopyPhotoStream(caminhoFoto, item);
                        }
                    }
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

        [HttpGet]
        public ActionResult EditarServico(int ID)
        {
            try
            {
                var pacotes = _pacoteService.GetAllPacotes();
                ViewBag.Pacotes = pacotes;

                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;

                var cidades = _cidadeService.GetAllCidades();
                ViewBag.Cidades = cidades;

                var estados = _estadoService.GetAllEstados();
                ViewBag.Estados = estados;

                var paises = _paisService.GetAllPaises();
                ViewBag.Paises = paises;

                var pacoteAtual = _pacoteService.GetPacoteById(ID);

                pacoteAtual.ReajusteAnualPorcentagem = RevertFormatPercentage(pacoteAtual.ReajusteAnualPorcentagem);
                pacoteAtual.DescontoSegundaFeira = RevertFormatPercentage(pacoteAtual.DescontoSegundaFeira);
                pacoteAtual.DescontoTercaFeira = RevertFormatPercentage(pacoteAtual.DescontoTercaFeira);
                pacoteAtual.DescontoQuartaFeira = RevertFormatPercentage(pacoteAtual.DescontoQuartaFeira);
                pacoteAtual.DescontoQuintaFeira = RevertFormatPercentage(pacoteAtual.DescontoQuintaFeira);
                pacoteAtual.DescontoSextaFeira = RevertFormatPercentage(pacoteAtual.DescontoSextaFeira);
                pacoteAtual.DescontoSabado = RevertFormatPercentage(pacoteAtual.DescontoSabado);
                pacoteAtual.DescontoDomingo = RevertFormatPercentage(pacoteAtual.DescontoDomingo);

                ViewBag.PacoteAtual = pacoteAtual;
                ViewBag.Categoria = categorias.FirstOrDefault(x => x.CategoriaID == pacoteAtual.CategoriaID);
                ViewBag.Cidade = cidades.FirstOrDefault(x => x.CidadeID == pacoteAtual.Cidade?.CidadeID);
                ViewBag.Estado = estados.FirstOrDefault(x => x.EstadoID == pacoteAtual.Estado?.EstadoID);
                ViewBag.Pais = paises.FirstOrDefault(x => x.PaisID == pacoteAtual.Pais?.PaisID);

                ViewBag.ItensDePacotes = _itensDePacotesService.GetItensDePacotesByPacoteId(ID);

                ViewBag.CaminhoImagem = pacoteAtual.CaminhoFoto;
                ViewBag.CaminhoVideo = pacoteAtual.CaminhoVideo;

                var diretorio = Path.Combine(_webHostEnvironment.WebRootPath);

                var fotosDoServico = _fotoServicoService.GetFotosServicoByServiceId(ID);

                foreach (var fotoServico in fotosDoServico.ToList())
                {
                    var caminhoFoto = fotoServico.CaminhoFoto?.Replace(diretorio, "~");
                    fotoServico.CaminhoFoto = caminhoFoto?.Replace("\\", "/");
                }

                ViewBag.FotosDoServico = fotosDoServico;

                //var caminhoImagem = pacoteAtual.CaminhoFoto?.Replace(diretorio, "~");
                //caminhoImagem = caminhoImagem?.Replace("\\", "/");

                var caminhoVideo = pacoteAtual.CaminhoVideo?.Replace(diretorio, "~");
                caminhoVideo = caminhoVideo?.Replace("\\", "/");

                //ViewBag.FotoExistente = caminhoImagem;
                ViewBag.VideoExistente = caminhoVideo;
                return View();
            }
            catch (Exception e)
            {
                string erro = e.Message;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarServico(PacoteComItensDoPacoteDto pacoteDto)
        {
            try
            {
                //Melhorar estas buscas por todos os registros
                SetData();
                TransformAllPercentProps(pacoteDto);

                if (pacoteDto.Video != null && pacoteDto.Video.Length > 0)
                {
                    long maxVideoSize = 10 * 1024 * 1024; // 10MB
                    if (pacoteDto.Video.Length > maxVideoSize)
                    {
                        ModelState.AddModelError("Video", "O tamanho do vídeo excede o limite de 10MB.");
                        return View();
                    }

                    CopyVideoStream(pacoteDto);
                }

                Pacote pacote;
                List<ItensDePacotes> itensDePacotes;
                MapPacoteDtoForPacoteObject(pacoteDto, out pacote, out itensDePacotes);

                pacote.Cidade = _cidadeService.GetCidadeById(pacoteDto.CidadeID);
                pacote.Estado = _estadoService.GetEstadoById(pacoteDto.EstadoID);
                pacote.Pais = _paisService.GetPaisById(pacoteDto.PaisID);

                pacote.Fornecedor = _fornecedorService
                    .GetFornecedorById(int.Parse(HttpContext.Session.GetString("FornecedorID")));

                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                var categoria = categorias.FirstOrDefault(x => x.CategoriaID == pacote.CategoriaID);

                pacote.Categoria = categoria;
                ViewBag.Categoria = pacote.Categoria;

                //Tratando se o usuário selecionar várias fotos ao mesmo tempo...
                if (pacoteDto.Fotos != null && pacoteDto.Fotos.Count() >= 1)
                {
                    foreach (var item in pacoteDto.Fotos)
                    {
                        var caminhoFoto = Path.Combine(_webHostEnvironment.WebRootPath, "images", HttpContext.Session.GetString("FornecedorID") + "_" + DateTime.Now.ToString() + item.FileName);

                        long maxFileSize = 10 * 1024 * 1024; // 10MB

                        if (item.Length > maxFileSize)
                        {
                            ModelState.AddModelError("Foto", "O tamanho da foto excede o limite de 10MB.");
                            return View();
                        }

                        FotosServicos fotoServico = new FotosServicos()
                        {
                            PacoteID = pacoteDto.PacoteID,
                            CaminhoFoto = caminhoFoto
                        };

                        if (!System.IO.File.Exists(caminhoFoto))
                        {
                            _fotoServicoService.CreateNewFotoServico(fotoServico);
                            CopyPhotoStream(caminhoFoto, item);
                        }
                    }
                }

                _pacoteService.UpdatePacote(pacote);

                UpdateOrDeleteItenDePacote(pacote, itensDePacotes);
                CreateNewItensDePacote(itensDePacotes);

                //var fotosServicos = _fotoServicoService.GetFotosServicoByServiceId(pacote.PacoteID);

                /////Melhorar estas diversas idas ao banco
                //foreach (var fotoServico in fotosServicos)
                //{
                //    _fotoServicoService.UpdateFotoServico(fotoServico);
                //}

                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                //ViewBag.Pacote = pacote;
                return RedirectToAction("EditarServico", pacote.PacoteID);
            }
            catch (Exception e)
            {
                string erro = e.Message;

                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditarPerfilAsync(int ID)
        {
            SetData();

            var fornecedor = _fornecedorService.GetFornecedorById(ID);
            ViewBag.Fornecedor = fornecedor;

            var cidades = _cidadeService.GetAllCidades();
            ViewBag.Cidades = cidades;

            var estados = _estadoService.GetAllEstados();
            ViewBag.Estados = estados;

            var paises = _paisService.GetAllPaises();
            ViewBag.Paises = paises;

            ViewBag.Cidade = cidades.FirstOrDefault(x => x.CidadeID == fornecedor.Cidade.CidadeID);
            ViewBag.Estado = estados.FirstOrDefault(x => x.EstadoID == fornecedor.Estado.EstadoID);
            ViewBag.Pais = paises.FirstOrDefault(x => x.PaisID == fornecedor.Pais.PaisID);

            var diretorio = Path.Combine(_webHostEnvironment.WebRootPath);

            var caminhoImagem = fornecedor.CaminhoFoto?.Replace(diretorio, "~");
            caminhoImagem = caminhoImagem?.Replace("\\", "/");

            ViewBag.FotoExistente = caminhoImagem;

            //Funcionando consumo da api para obter todos os bancos do brasil
            var options = new RestClientOptions("https://brasilapi.com.br")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/banks/v1", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfil(FornecedorDto fornecedorDto)
        {
            try
            {
                SetData();

                if (fornecedorDto.Foto != null && fornecedorDto.Foto.Length > 0)
                {
                    long maxFileSize = 10 * 1024 * 1024; // 10MB

                    if (fornecedorDto.Foto.Length > maxFileSize)
                    {
                        ModelState.AddModelError("Foto", "O tamanho da foto excede o limite de 10MB.");
                        return View();
                    }

                    var caminhoFoto = Path.Combine(_webHostEnvironment.WebRootPath, "images", HttpContext.Session.GetString("FornecedorID") + "_" + fornecedorDto.Foto.FileName);
                    fornecedorDto.CaminhoFoto = caminhoFoto;

                    CopyPhotoStream(caminhoFoto, fornecedorDto.Foto);
                }

                Fornecedor fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);
                _fornecedorService.UpdateFornecedor(fornecedor);

                TempData["SuccessMessage"] = "Atualizado com sucesso!";
                return RedirectToAction("EditarPerfil", fornecedorDto.FornecedorID);
            }
            catch (Exception e)
            {
                string erro = e.Message;

                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

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

        [HttpGet]
        public ActionResult PrintService(int Id)
        {
            var servico = _pacoteService.GetPacoteById(Id);
            var itensDoPacote = _itensDePacotesService.GetItensDePacotesByPacoteId(Id);

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();

                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                document.Open();

                servico.CaminhoFoto = Path.Combine(_webHostEnvironment.WebRootPath, "images", "logoSemFundo.png");

                if (System.IO.File.Exists(servico.CaminhoFoto))
                {
                    Image logo = Image.GetInstance(servico.CaminhoFoto);
                    logo.ScaleAbsolute(195f, 40f);
                    document.Add(logo);
                }

                document.Add(new Paragraph(servico.Titulo));

                Paragraph subtitulo = new Paragraph(servico.SubTitulo);
                subtitulo.Alignment = Element.ALIGN_LEFT;
                subtitulo.SpacingAfter = 10F;

                document.Add(subtitulo);

                PdfContentByte cb = writer.DirectContent;
                cb.SetLineWidth(1);
                cb.SetColorStroke(BaseColor.RED);
                cb.MoveTo(36, document.PageSize.Height - 125);
                cb.LineTo(document.PageSize.Width - 36, document.PageSize.Height - 125);
                cb.Stroke();

                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                Paragraph title = new Paragraph("Detalhes", titleFont);
                title.Alignment = Element.ALIGN_LEFT;
                document.Add(title);

                document.Add(new Paragraph("Quantidade de pessoas:" + servico.QtdMaximaPessoas));

                Paragraph valorParagraph = new Paragraph("Valor:" + servico.Preco);
                valorParagraph.Alignment = Element.ALIGN_LEFT;
                valorParagraph.SpacingAfter = 10F;

                document.Add(valorParagraph);

                PdfContentByte cb2 = writer.DirectContent;
                cb2.SetLineWidth(-2);
                cb2.SetColorStroke(BaseColor.GRAY);
                cb2.MoveTo(26, document.PageSize.Height - 190);
                cb2.LineTo(document.PageSize.Width - 26, document.PageSize.Height - 190);
                cb2.Stroke();

                Font itensDoServicoTituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                Paragraph itensDoServicoParagrafo = new Paragraph("Itens do Serviço:", itensDoServicoTituloFont);
                itensDoServicoParagrafo.Alignment = Element.ALIGN_LEFT;
                document.Add(itensDoServicoParagrafo);

                foreach (var item in itensDoPacote)
                {
                    document.Add(new Paragraph(item.Descricao));

                    Paragraph qtdItemParagraph = new Paragraph(item.Quantidade.ToString());
                    qtdItemParagraph.Alignment = Element.ALIGN_LEFT;
                    qtdItemParagraph.SpacingAfter = 10F;

                    document.Add(new Paragraph(qtdItemParagraph));
                }

                document.Close();

                byte[] pdfBytes = ms.ToArray();
                return File(pdfBytes, "application/pdf", "DetalhesDoServico.pdf");
            }
        }

        [HttpGet]
        public ActionResult Contrato()
        {
            try
            {
                int fornecedorID = int.Parse(HttpContext.Session.GetString("FornecedorID"));

                TempData["SuccessMessage"] = null;

                ViewBag.ClausulasDeContrato = _clausulasDeContratoService.GetClausulasDeContratosByFornecedorID(fornecedorID);

                return View();
            }
            catch (ArgumentNullException ex)
            {
                if (ex.Message == "Value cannot be null. (Parameter 's')")
                {
                    RedirectToAction("Logout", "Login");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Contrato(List<ClausulaContrato> clausulasDeContrato)
        {
            try
            {
                int fornecedorID = int.Parse(HttpContext.Session.GetString("FornecedorID"));
                var fornecedor = _fornecedorService.GetFornecedorById(fornecedorID);

                UpdateOrDeleteClausulasDeContrato(fornecedor, clausulasDeContrato);
                CreateNewClausulasDeContrato(clausulasDeContrato);

                return View();
            }
            catch (ArgumentNullException ex)
            {
                if (ex.Message == "Value cannot be null. (Parameter 's')")
                {
                    RedirectToAction("Logout", "Login");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult MeuPlano()
        {
            try
            {
                TempData["SuccessMessage"] = null;

                int ID = int.Parse(HttpContext.Session.GetString("FornecedorID"));

                var fornecedor = _fornecedorService.GetFornecedorById(ID);

                var planos = _planoService.GetAllPlanos();
                ViewBag.Planos = planos;

                var planoAtual = planos.FirstOrDefault(x => x.PlanoID == fornecedor.Plano?.PlanoID);
                ViewBag.PlanoAtual = planoAtual;

                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;

                return View();
            }
            catch (ArgumentNullException ex)
            {
                if (ex.Message == "Value cannot be null. (Parameter 's')" || ex.Message == "Value cannot be null. Arg_ParamName_Name")
                {
                    RedirectToAction("Logout", "Login");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult DuplicarServico(int ID)
        {
            try
            {
                var servicoExistente = _pacoteService.GetPacoteById(ID);

                if (servicoExistente != null)
                {
                    servicoExistente.PacoteID = 0;
                    servicoExistente.Categoria = null;
                    servicoExistente.Fornecedor = null;
                    servicoExistente.Pais = null;
                    servicoExistente.Estado = null;
                    servicoExistente.Cidade = null;

                    _pacoteService.CreateNewPacote(servicoExistente);

                    TempData["SuccessMessage"] = "Salvo com sucesso!";

                    return RedirectToAction("Index", "ServicosFornecedor");
                }
                else
                {
                    TempData["ErrorMessage"] = "Ocorreu um erro!";
                    return RedirectToAction("Index", "ServicosFornecedor");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return RedirectToAction("Index", "ServicosFornecedor");
            }
        }

        [HttpGet]
        public ActionResult SolicitaDadosPagamentoPlano()
        {
            return View();
        }

        private void SetData()
        {
            ViewBag.Pacotes = _pacoteService.GetAllPacotes();
            ViewBag.Paises = _paisService.GetAllPaises();
            ViewBag.Estados = _estadoService.GetAllEstados();
            ViewBag.Cidades = _cidadeService.GetAllCidades();
        }

        private decimal TransformToPercentDiscount(decimal number)
        {
            number = number / 100;
            return number;
        }

        private void TransformAllPercentProps(PacoteComItensDoPacoteDto pacoteDto)
        {
            pacoteDto.ReajusteAnualPorcentagem = TransformToPercentDiscount(pacoteDto.ReajusteAnualPorcentagem);
            pacoteDto.DescontoSegundaFeira = TransformToPercentDiscount(pacoteDto.DescontoSegundaFeira);
            pacoteDto.DescontoTercaFeira = TransformToPercentDiscount(pacoteDto.DescontoTercaFeira);
            pacoteDto.DescontoQuartaFeira = TransformToPercentDiscount(pacoteDto.DescontoQuartaFeira);
            pacoteDto.DescontoQuintaFeira = TransformToPercentDiscount(pacoteDto.DescontoQuintaFeira);
            pacoteDto.DescontoSextaFeira = TransformToPercentDiscount(pacoteDto.DescontoSextaFeira);
            pacoteDto.DescontoSabado = TransformToPercentDiscount(pacoteDto.DescontoSabado);
            pacoteDto.DescontoDomingo = TransformToPercentDiscount(pacoteDto.DescontoDomingo);
        }

        private decimal RevertFormatPercentage(decimal fractionValue)
        {
            decimal percentageValue = fractionValue * 100;
            return percentageValue;
        }

        private void CopyVideoStream(PacoteComItensDoPacoteDto pacoteDto)
        {
            pacoteDto.CaminhoVideo = Path.Combine(_webHostEnvironment.WebRootPath, "videos", HttpContext.Session.GetString("FornecedorID") + "_" + pacoteDto.Video.FileName);

            if (!System.IO.File.Exists(pacoteDto.CaminhoVideo))
            {
                using (var stream = new FileStream(pacoteDto.CaminhoVideo, FileMode.Create))
                {
                    pacoteDto.Video.CopyTo(stream);
                }
            }
        }

        private void CopyPhotoStream(string caminhoFoto, IFormFile foto)
        {
            using (var stream = new FileStream(caminhoFoto, FileMode.Create))
            {
                foto.CopyTo(stream);
            }
        }

        private void MapPacoteDtoForPacoteObject(PacoteComItensDoPacoteDto pacoteDto, out Pacote pacote, out List<ItensDePacotes> itensDePacotes)
        {
            pacote = _mapper.Map<Pacote>(pacoteDto);
            itensDePacotes = _mapper.Map<List<ItensDePacotes>>(pacoteDto.ItensDePacotes);
        }

        private void CreateNewItensDePacote(List<ItensDePacotes> itensDePacotes)
        {
            if (itensDePacotes != null)
            {
                foreach (var novoItem in itensDePacotes)
                {
                    if (novoItem.ItensDePacotesID == 0)
                    {
                        _itensDePacotesService.CreateNewItensDePacotes(novoItem);
                    }
                }
            }
        }

        private void UpdateOrDeleteItenDePacote(Pacote pacote, List<ItensDePacotes> itensDePacotes)
        {
            var itensDePacotesAntesDaEdicao = _itensDePacotesService.GetItensDePacotesByPacoteId(pacote.PacoteID);

            if (itensDePacotesAntesDaEdicao != null)
            {
                foreach (var item in itensDePacotesAntesDaEdicao)
                {
                    var testeItemDePacote = itensDePacotes
                        .FirstOrDefault(x => x.ItensDePacotesID == item.ItensDePacotesID);

                    if (testeItemDePacote != null)
                    {
                        testeItemDePacote.PacoteID = pacote.PacoteID;
                        testeItemDePacote.Pacote = pacote;

                        _itensDePacotesService.UpdateItensDePacotes(testeItemDePacote);
                    }
                    else if (testeItemDePacote == null)
                    {
                        _itensDePacotesService.DeleteItensDePacotes(item.ItensDePacotesID);
                    }
                }
            }
        }

        private void CreateNewClausulasDeContrato(List<ClausulaContrato> clausulasDeContrato)
        {
            if (clausulasDeContrato != null)
            {
                foreach (var novoItem in clausulasDeContrato)
                {
                    if (novoItem.ClausulaContratoID == 0)
                    {
                        _clausulasDeContratoService.CreateNewClausulaContrato(novoItem);
                    }
                }
            }
        }

        [HttpPost]
        public JsonResult RemoverFotoExistente(int fotoExistenteID)
        {
            try
            {
                var fotoExistente = _fotoServicoService.GetFotoServicoById(fotoExistenteID);

                if (fotoExistente != null)
                {
                    _fotoServicoService.DeleteFotoServico(fotoExistenteID);

                    return Json(new { success = true, message = "Foto removida com sucesso." });
                }
                else
                {
                    return Json(new { success = false, message = "Foto não encontrada." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao remover a foto." });
            }
        }

        [HttpPost]
        public JsonResult DefineCamposServico(string titulo)
        {
            try
            {
                var servico = _pacoteService.GetPacoteByTitulo(titulo);

                if (servico != null)
                {
                    return Json(new
                    {
                        success = true,
                        preco = servico.Preco.ToString("F2"),
                        qtdMaximaEventosDia = servico.QtdMaximaEventosDia,
                        numeroConvidados = servico.QtdMaximaPessoas
                    });
                }
                else if (titulo == "---")
                {
                    return Json(new { success = false, message = "Nenhum título selecionado!" });
                }
                else
                {
                    return Json(new { success = false, message = "Erro ao autocompletar alguns campos do serviço." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao autocompletar alguns campos do serviço." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriaBoletoAsaas(int planoID)
        {
            try
            {
                Plano plano = _planoService.GetPlanoById(planoID);
                plano.Preco = Math.Round(plano.Preco, 2);

                int fornecedorID = int.Parse(HttpContext.Session.GetString("FornecedorID"));
                var fornecedor = _fornecedorService.GetFornecedorById(fornecedorID);
                var dataVencimento = DateTime.Now.AddDays(1);
                string dataFormatada = dataVencimento.ToString("yyyy-MM-dd");

                //Não esquecer de usar estas configurações no appSettings
                var options = new RestClientOptions("https://sandbox.asaas.com/api/v3/payments");
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("access_token", "$aact_YTU5YTE0M2M2N2I4MTliNzk0YTI5N2U5MzdjNWZmNDQ6OjAwMDAwMDAwMDAwMDAwNjYyNDY6OiRhYWNoX2ZkNjdjZWY0LTViMmYtNDU2NS1iMTk2LWYyZWEzOGIyMGRjNw==");

                request.AddJsonBody(new
                {
                    billingType = "BOLETO",
                    customer = fornecedor.AsaasCustomerID,
                    dueDate = dataFormatada,
                    value = plano.Preco
                });

                //Está criando o boleto mas caso esteja inadimplente não cria um novo PlanoForncedor, preciso ver isso
                var response = await client.PostAsync(request);

                if (response.IsSuccessful)
                {
                    string jsonResponse = response.Content;
                    CreateBoletoAsaasResponseDto createBoletoAsaasResponseDto = JsonConvert.DeserializeObject<CreateBoletoAsaasResponseDto>(jsonResponse);

                    DefineNovoPlanoParaFornecedor(fornecedor.FornecedorID, plano.PlanoID, createBoletoAsaasResponseDto.Id);

                    return Ok(response.Content);
                }
                else
                {
                    return BadRequest(response.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                string erro = e.Message;
                return BadRequest(erro);
            }
        }

        private void UpdateOrDeleteClausulasDeContrato(Fornecedor fornecedor, List<ClausulaContrato> clausulasDeContrato)
        {
            var ClausulasContratoAntesDaEdicao = _clausulasDeContratoService.GetClausulasDeContratosByFornecedorID(fornecedor.FornecedorID);

            if (ClausulasContratoAntesDaEdicao != null)
            {
                foreach (var item in ClausulasContratoAntesDaEdicao)
                {
                    var testeClausulaDeContrato = clausulasDeContrato
                        .FirstOrDefault(x => x.ClausulaContratoID == item.ClausulaContratoID);

                    if (testeClausulaDeContrato != null)
                    {
                        _clausulasDeContratoService.UpdateClausulaContrato(testeClausulaDeContrato);
                    }
                    else if (testeClausulaDeContrato == null)
                    {
                        _clausulasDeContratoService.DeleteClausulaContrato(item.ClausulaContratoID);
                    }
                }
            }
        }

        public void DefineNovoPlanoParaFornecedor(int fornecedorID, int planoID, string asaasPaymentID)
        {
            var fornecedor = _fornecedorService.GetFornecedorById(fornecedorID);
            var plano = _planoService.GetPlanoById(planoID);

            var planoFornecedor = _planoFornecedorService.GetAllPlanosFornecedores()
                .FirstOrDefault(x => x.FornecedorID == fornecedor.FornecedorID);

            if (planoFornecedor != null)
            {
                if (!planoFornecedor.Pago)
                {
                    throw new Exception("Não é possível atualizar para um novo plano, o atual encontra-se em inadimplência");
                }

                _planoFornecedorService.DeletePlanoFornecedor(planoFornecedor.PlanoFornecedorID);
            }
            else
            {
                DateTime dataProximaRenovacao = DateTime.Now;

                var novoPlanoFornecedor = new PlanoFornecedor()
                {
                    FornecedorID = fornecedorID,
                    PlanoID = planoID,
                    Pago = false,
                    AsaasPaymentID = asaasPaymentID
                };

                //Criar enumerador para os periodos de renovação
                if (plano.PeriodoRenovacao == "Mensal")
                {
                    novoPlanoFornecedor.DataProximaRenovacao = dataProximaRenovacao.AddMonths(1);
                }
                else if (plano.PeriodoRenovacao == "Trienal")
                {
                    novoPlanoFornecedor.DataProximaRenovacao = dataProximaRenovacao.AddMonths(3);
                }
                else if (plano.PeriodoRenovacao == "Semestral")
                {
                    novoPlanoFornecedor.DataProximaRenovacao = dataProximaRenovacao.AddMonths(6);
                }
                else if (plano.PeriodoRenovacao == "Anual")
                {
                    novoPlanoFornecedor.DataProximaRenovacao = dataProximaRenovacao.AddYears(1);
                }
                else if (plano.PeriodoRenovacao == "Licensiamento")
                {
                    novoPlanoFornecedor.DataProximaRenovacao = null;
                }
                else
                {
                    throw new Exception("Não foi selecionado um período de renovação válido!");
                }

                _planoFornecedorService.CreateNewPlanoFornecedor(novoPlanoFornecedor);
            }
        }

        private void DesfazerItensDoPacoteEPacote(Pacote pacote, List<ItensDePacotes> itensDePacotes, int pacoteID)
        {
            foreach (var itemDePacote in itensDePacotes)
            {
                itemDePacote.PacoteID = pacoteID;
                _itensDePacotesService.DeleteItensDePacotes(itemDePacote.ItensDePacotesID);
            }

            _pacoteService.DeletePacote(pacote.PacoteID);
        }
    }
}
