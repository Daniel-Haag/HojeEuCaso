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
            try
            {
                int fornecedorID = int.Parse(HttpContext.Session.GetString("FornecedorID"));
                ViewBag.Pacotes = _pacoteService.GetPacoteByFornecedor(fornecedorID);
                ViewBag.Diretorio = Path.Combine(_webHostEnvironment.WebRootPath);
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

        // GET: PacotesController
        public ActionResult Profile()
        {
            //Buscando os Servicos/Pacotes do Fornecedor Logado!
            int fornecedorID = int.Parse(HttpContext.Session.GetString("FornecedorID"));
            ViewBag.Pacotes = _pacoteService.GetPacoteByFornecedor(fornecedorID);
            ViewBag.Diretorio = Path.Combine(_webHostEnvironment.WebRootPath);

            //Falta definir como será obtida a imagem
            ViewBag.FotoExistente = "/images/backiee-81831.jpg";

            var fornecedor = _fornecedorService.GetFornecedorById(fornecedorID);

            ViewBag.Fornecedor = fornecedor;

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
            //Talvez esse método entre em desuso
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
                //Melhorar estas buscar por todos os registros
                //Não esquecer de delegar todas estas funcionalidades em métodos menores
                SetData();
                TransformAllPercentProps(pacoteDto);

                if (pacoteDto.Foto != null && pacoteDto.Foto.Length > 0)
                {
                    long maxFileSize = 10 * 1024 * 1024; // 10MB

                    if (pacoteDto.Foto.Length > maxFileSize)
                    {
                        ModelState.AddModelError("Foto", "O tamanho da foto excede o limite de 10MB.");
                        return View();
                    }

                    CopyPhotoStream(pacoteDto);
                }

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
        public ActionResult EditarServico(int ID)
        {
            var pacotes = _pacoteService.GetAllPacotes();
            ViewBag.Pacotes = pacotes;

            var categorias = _categoriaService.GetAllCategorias();
            ViewBag.Categorias = categorias;

            var cidades = _cidadeService.GetAllCidades();
            ViewBag.Cidades = cidades;

            var estados = _estadoService.GetAllEstados();
            ViewBag.Estados = estados;

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
            ViewBag.Cidade = cidades.FirstOrDefault(x => x.CidadeID == pacoteAtual.Cidade.CidadeID);
            ViewBag.Estado = estados.FirstOrDefault(x => x.EstadoID == pacoteAtual.Estado.EstadoID);
            ViewBag.ItensDePacotes = _itensDePacotesService.GetItensDePacotesByPacoteId(ID);

            ViewBag.CaminhoImagem = pacoteAtual.CaminhoFoto;
            ViewBag.CaminhoVideo = pacoteAtual.CaminhoVideo;

            var diretorio = Path.Combine(_webHostEnvironment.WebRootPath);

            var caminhoImagem = pacoteAtual.CaminhoFoto?.Replace(diretorio, "~");
            caminhoImagem = caminhoImagem?.Replace("\\", "/");

            var caminhoVideo = pacoteAtual.CaminhoVideo?.Replace(diretorio, "~");
            caminhoVideo = caminhoVideo?.Replace("\\", "/");

            ViewBag.FotoExistente = caminhoImagem;
            ViewBag.VideoExistente = caminhoVideo;
            return View();
        }

        // POST: PacotesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarServico(PacoteComItensDoPacoteDto pacoteDto)
        {
            try
            {
                //Melhorar estas buscas por todos os registros
                SetData();
                TransformAllPercentProps(pacoteDto);

                if (pacoteDto.Foto != null && pacoteDto.Foto.Length > 0)
                {
                    long maxFileSize = 10 * 1024 * 1024; // 10MB

                    if (pacoteDto.Foto.Length > maxFileSize)
                    {
                        ModelState.AddModelError("Foto", "O tamanho da foto excede o limite de 10MB.");
                        return View();
                    }

                    CopyPhotoStream(pacoteDto);
                }

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

                pacote.Fornecedor = _fornecedorService
                    .GetFornecedorById(int.Parse(HttpContext.Session.GetString("FornecedorID")));

                var categorias = _categoriaService.GetAllCategorias();
                ViewBag.Categorias = categorias;
                var categoria = categorias.FirstOrDefault(x => x.CategoriaID == pacote.CategoriaID);

                pacote.Categoria = categoria;
                ViewBag.Categoria = pacote.Categoria;

                _pacoteService.UpdatePacote(pacote);

                UpdateOrDeleteItenDePacote(pacote, itensDePacotes);
                CreateNewItensDePacote(itensDePacotes);

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

        // POST: PacotesController/Delete/5
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

        public decimal RevertFormatPercentage(decimal fractionValue)
        {
            decimal percentageValue = fractionValue * 100;
            return percentageValue;
        }

        private void CopyVideoStream(PacoteComItensDoPacoteDto pacoteDto)
        {
            pacoteDto.CaminhoVideo = Path.Combine(_webHostEnvironment.WebRootPath, "videos", pacoteDto.Video.FileName);

            if (!System.IO.File.Exists(pacoteDto.CaminhoVideo))
            {
                using (var stream = new FileStream(pacoteDto.CaminhoVideo, FileMode.Create))
                {
                    pacoteDto.Video.CopyTo(stream);
                }
            }
        }

        private void CopyPhotoStream(PacoteComItensDoPacoteDto pacoteDto)
        {
            pacoteDto.CaminhoFoto = Path.Combine(_webHostEnvironment.WebRootPath, "images", pacoteDto.Foto.FileName);

            if (!System.IO.File.Exists(pacoteDto.CaminhoFoto))
            {
                using (var stream = new FileStream(pacoteDto.CaminhoFoto, FileMode.Create))
                {
                    pacoteDto.Foto.CopyTo(stream);
                }
            }
        }

        private void MapPacoteDtoForPacoteObject(PacoteComItensDoPacoteDto pacoteDto, out Pacote pacote, out List<ItensDePacotes> itensDePacotes)
        {
            pacote = _mapper.Map<Pacote>(pacoteDto);
            itensDePacotes = _mapper
                                .Map<List<ItensDePacotes>>(pacoteDto.ItensDePacotes);
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
    }
}
