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
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Microsoft.AspNetCore.Http.Extensions;

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
    private readonly IFotoServicoService _fotoServicoService;
    private readonly IItensDePacotesService _itensDePacotesService;
    private readonly IUsuarioSistemaService _usuarioSistemaService;
    private readonly IOrcamentoService _orcamentoService;

    public ServicosWedManagerController(ILogger<ServicosWedManagerController> logger,
        ICidadeService cidadeService,
        IFornecedorService fornecedorService,
        IPacoteService pacoteService,
        IPaisService paisService,
        IEstadoService estadoService,
        ICategoriaService categoriaService,
        IWebHostEnvironment webHostEnvironment,
        IFotoServicoService fotoServicoService,
        IItensDePacotesService itensDePacotesService,
        IUsuarioSistemaService usuarioSistemaServiceService,
        IOrcamentoService orcamentoService)
    {
      _logger = logger;
      _cidadeService = cidadeService;
      _fornecedorService = fornecedorService;
      _pacoteService = pacoteService;
      _paisService = paisService;
      _estadoService = estadoService;
      _categoriaService = categoriaService;
      _webHostEnvironment = webHostEnvironment;
      _fotoServicoService = fotoServicoService;
      _itensDePacotesService = itensDePacotesService;
      _usuarioSistemaService = usuarioSistemaServiceService;
      _orcamentoService = orcamentoService;
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
    public ActionResult FazerOrcamentoFornecedores(OrcamentoDto OrcamentoDto)
    {
      //VERIFICAR A AGENDA DO FORNECEDOR
      //Se o objeto não tiver o valor do orçamento, buscar todos os fornecedores

      try
      {

        SetData();

        //Se o objeto não tiver o valor do orçamento, buscar todos os fornecedores
        if (!string.IsNullOrEmpty(OrcamentoDto.OrcamentoTexto))
        {
          string valorNumerico = OrcamentoDto.OrcamentoTexto.Replace("R$", "");
          decimal OrcamentoDecimal;
          List<Categoria> categorias = new List<Categoria>();
          List<Fornecedor> fornecedores = new List<Fornecedor>();
          var diretorio = Path.Combine(_webHostEnvironment.WebRootPath);

          if (decimal.TryParse(valorNumerico, out OrcamentoDecimal))
          {
            //var fornecedor = _fornecedorService.GetFornecedorById(int.Parse(HttpContext.Session.GetString("FornecedorID")));
            ViewBag.CatalogoGeografico = _cidadeService.GetAllCidades();

            var todasCategorias = _categoriaService.GetAllCategorias();
            var categoriaIDs = OrcamentoDto.CategoriasSelecionadas.ToArray();
            var categoriasFiltradas = todasCategorias
                .Where(x => categoriaIDs.Contains(x.CategoriaID));
            var quantidadeDeCategorias = categoriasFiltradas.Count();
            var servicos = _pacoteService.GetAllPacotes()
                .Where(x => categoriaIDs.Contains(x.CategoriaID) && x.Fornecedor != null).ToList();
            var fotosServicos = _fotoServicoService.GetAllFotoServicos();

            ViewBag.FotosServicos = fotosServicos;
            ViewBag.Diretorio = Path.Combine(_webHostEnvironment.WebRootPath);
            ViewBag.DataEvento = OrcamentoDto.DataCasamento;
            ViewBag.QtdMaximaPessoas = OrcamentoDto.QtdMaximaPessoas;

            if (servicos.Count > 0)
            {
              foreach (var servico in servicos.ToList())
              {
                if (servico.Preco > OrcamentoDecimal)
                {
                  servicos.Remove(servico);
                  continue;
                }

                if (OrcamentoDto.QtdMaximaPessoas > servico.QtdMaximaPessoas)
                {
                  servicos.Remove(servico);
                  continue;
                }

                if (servico.Pais?.PaisID != OrcamentoDto.PaisID)
                {
                  servicos.Remove(servico);
                  continue;
                }
                else if (servico.Estado?.EstadoID != OrcamentoDto.EstadoID)
                {
                  servicos.Remove(servico);
                  continue;
                }
                else if (servico.Cidade?.CidadeID != OrcamentoDto.CidadeID)
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

                  var caminhoImagemFornecedor = servico.Fornecedor.CaminhoFoto?.Replace(diretorio, "~");
                  caminhoImagemFornecedor = caminhoImagemFornecedor?.Replace("\\", "/");
                  servico.Fornecedor.CaminhoFoto = caminhoImagemFornecedor;

                  if (fornecedorJaIncluido == null)
                    fornecedores.Add(servico.Fornecedor);
                }

                servico.CaminhoFoto = fotosServicos
                    .FirstOrDefault(x => x.PacoteID == servico.PacoteID)?.CaminhoFoto?
                    .Replace(diretorio, "~");

                servico.CaminhoFoto = servico.CaminhoFoto?.Replace("\\", "/");

                servico.ItensDoPacote = _itensDePacotesService.GetItensDePacotesByPacoteId(servico.PacoteID);
              }

              foreach (var categoria in categorias)
              {
                var fornecedoresCategoria = fornecedores.
                    Where(x => x.CategoriaID == categoria.CategoriaID).ToList();

                categoria.Fornecedores = fornecedoresCategoria;

                foreach (var fornecedor in categoria.Fornecedores)
                {
                  fornecedor.ServicosFornecedor = servicos
                      .Where(x => x.FornecedorID == fornecedor.FornecedorID).ToList();
                }
              }

              ViewBag.Categorias = categorias;
            }
            else
            {
              ViewBag.Categorias = null;
              TempData["ErrorMessage"] = "Não foram encontrados registros com as informações solicitadas!";
            }



            return View();
          }
          else
          {
            return View();
          }
        }
        else
        {
          //Buscar todos os servicos
          return View();
        }
      }
      catch (Exception e)
      {
        TempData["ErrorMessage"] = "Ocorreu um erro!";
        return View();
      }

    }

    [HttpGet]
    public ActionResult GerarOrcamento(string servicosSelecionados, string dataEvento, string qtdMaximaPessoas)
    {
      //Obter os dados do wed manager
      //Wed manager deve ser um fornecedor, então deve ser feita essa alteração o quanto antes...

      //Conforme a confirmação do orçamento, feita pelo noivo, e efetuado o devido pagamento
      //Deve marcar a agenda dos fornecedores aqui conforme a data definida do evento


      //Se não conseguir obter o ID do Wed Manager deve obter o ID do usuário
      //O usuário por sua vez, deve ter algum vínculo único com o Wed Manager
      //Para então obter os dados do respectivo Wed Manager

      //No pagamento que o usuário irá fazer do orçamento, deve revalidar antes, se o fornecedor está realmente livre
      //Naquela data informada

      ViewBag.ServicosSelecionados = servicosSelecionados;
      ViewBag.DataEvento = dataEvento;
      ViewBag.QtdMaximaPessoas = qtdMaximaPessoas;

      decimal valorTotalOrcamento;
      List<Pacote> servicos;

      try
      {
        int? usuarioID = int.Parse(HttpContext?.Session?.GetString("ID"));

        if (usuarioID != null)
        {
          var usuario = _usuarioSistemaService.GetUsuarioSistemaById((int)usuarioID);

          if (usuario != null && usuario.Role == "Wed Manager")
          {
            if (!string.IsNullOrEmpty(servicosSelecionados)
                                && !string.IsNullOrEmpty(dataEvento)
                                && !string.IsNullOrEmpty(qtdMaximaPessoas)
                                && usuario != null)
            {
              PreparaVariaveisOrcamento(servicosSelecionados, out valorTotalOrcamento, out servicos);
              PreparaDadosTelaOrcamentoGerado(dataEvento, qtdMaximaPessoas, usuario, valorTotalOrcamento, servicos);

              //Só deve salvar o orçamento se estiver gerando e não consultando
              var orcamento = new Orcamento()
              {
                UsuarioSistemaID = usuario.UsuarioSistemaID,
                servicosIDs = servicosSelecionados,
                DataEvento = DateTime.Parse(dataEvento),
                QtdConvidados = int.Parse(qtdMaximaPessoas),
                ValorTotalOrcamento = valorTotalOrcamento
              };

              _orcamentoService.CreateNewOrcamento(orcamento);

              return View();
            }
          }
          else
          {
            PreparaVariaveisOrcamento(servicosSelecionados, out valorTotalOrcamento, out servicos);
            PreparaDadosTelaOrcamentoGerado(dataEvento, qtdMaximaPessoas, null, valorTotalOrcamento, servicos);

            return View();
          }
        }
        else
        {
          throw new Exception("Erro ao obter dados!");
        }
      }
      catch (Exception e)
      {
        //Se for este erro, é alguém que não está logado
        if (e.Message == "Value cannot be null. (Parameter 's')")
        {
          TempData["Link"] = this.Request?.GetDisplayUrl();
          return RedirectToAction("Login", "Login");
        }

        throw new Exception("Erro ao obter dados!");
      }

      return View();
    }

    [HttpPost]
    public IActionResult ImprimirOrcamento([FromBody] ImprimirOrcamentoRequest request)
    {
      try
      {
        string htmlOrcamento = request.HtmlOrcamento;

        //// Crie um documento PDF usando o conteúdo HTML recebido
        //var doc = new HtmlToPdfDocument()
        //{
        //    GlobalSettings = {
        //    ColorMode = ColorMode.Color,
        //    PaperSize = PaperKind.A4,
        //},
        //    Objects = {
        //    new ObjectSettings() {
        //        PagesCount = true,
        //        HtmlContent = htmlOrcamento,
        //        WebSettings = { DefaultEncoding = "utf-8" },
        //    }
        //}
        //};

        //byte[] pdf = _converter.Convert(doc);

        //// Salve ou envie o PDF conforme necessário
        //// Por exemplo, você pode salvar o PDF em disco ou enviar para o navegador como um download


        var pdfBytes = GeneratePdf(htmlOrcamento);

        return File(pdfBytes, "application/pdf", "orcamento.pdf");
      }
      catch (Exception ex)
      {
        // Lide com qualquer erro que possa ocorrer durante a geração do PDF
        return Json(new { success = false, message = ex.Message });
      }
    }

    public ActionResult Create()
    {
      TempData["SuccessMessage"] = null;
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Cidade Cidade)
    {
      try
      {
        _cidadeService.CreateNewCidade(Cidade);
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
    public ActionResult Edit(Cidade Cidade)
    {
      try
      {
        _cidadeService.UpdateCidade(Cidade);
        TempData["SuccessMessage"] = "Atualizado com sucesso!";
        ViewBag.Cidade = Cidade;
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

    private void PreparaDadosTelaOrcamentoGerado(string dataEvento, string qtdMaximaPessoas, UsuarioSistema? wedManager, decimal valorTotalOrcamento, List<Pacote> servicos)
    {
      ViewBag.WedManager = wedManager;
      ViewBag.Servicos = servicos;
      ViewBag.DataEvento = DateTime.Parse(dataEvento).ToString("dd/MM/yyyy");
      ViewBag.QtdMaximaPessoas = qtdMaximaPessoas;
      ViewBag.ValorTotalOrcamento = valorTotalOrcamento;
    }

    private void PreparaVariaveisOrcamento(string servicosSelecionados, out decimal valorTotalOrcamento, out List<Pacote> servicos)
    {
      string idsString = servicosSelecionados.Replace("[", "").Replace("]", "").Replace("\"", "");
      string[] idsArray = idsString.Split(',');
      List<int> idsList = idsArray.Select(int.Parse).ToList();
      valorTotalOrcamento = 0;
      servicos = _pacoteService.GetAllPacotes()
          .Where(x => idsList.Contains(x.PacoteID))
          .ToList();
      foreach (var servico in servicos)
      {
        valorTotalOrcamento += servico.Preco;

        var itens = _itensDePacotesService.GetItensDePacotesByPacoteId(servico.PacoteID);
        servico.ItensDoPacote = itens;
      }
    }

    public class ImprimirOrcamentoRequest
    {
      public string HtmlOrcamento { get; set; }
    }

    private byte[] GeneratePdf(string html)
    {
      //var pdfDocument = PdfGenerator.GeneratePdf(html, PageSize.A4);
      //using (var memoryStream = new MemoryStream())
      //{
      //    pdfDocument.Save(memoryStream);
      //    return memoryStream.ToArray();
      //}
      return null;
    }
  }
}
