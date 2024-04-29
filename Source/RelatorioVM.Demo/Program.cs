using iText.Html2pdf;
using Microsoft.Extensions.Hosting;
using RelatorioVM.Demo.ComponentesCustomizados;
using RelatorioVM.Demo.Modelos;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WkHtmlToPdfDotNet;
using iText.IO.Font;
using iText.Layout.Font;
using System.Drawing;

namespace RelatorioVM.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            Random random = new Random();

            #region Inicializar dados ViewModel
            var viewModel = new ExemploSimplesViewModel()
            {
                FilialCodigo = 1,
                FilialNome = "Nome da filial",
                PessoaCodigo = 5236,
                DataFinal = DateTime.Now.Date,
                DataInicial = null,
                Pessoa = new PessoaViewModel()
                {
                    Codigo = 1,
                    Nome = "Testes",
                    Endereco = "Endereco de testes"
                },
                OperacaoCodigo = 5,
                OperacaoNome = "VENDAS",
                Usuario = "TESTES",
                Itens = new List<ExemploSimplesItemViewModel>(),
            };

            #region Inicializar itens
            for (int i = 0; i < 100; i++)
            {
                viewModel.Itens.Add(new ExemploSimplesItemViewModel()
                {
                    Data = DateTime.Now,
                    FilialCodigo = random.Next(1, 10),
                    PessoaCodigo = random.Next(1, 2000),
                    Valor = (decimal)random.NextDouble() * 100m,
                    Pessoa = new PessoaViewModel()
                    {
                        Codigo = random.Next(1, 999999),
                        Nome = "Teste",
                        Endereco = $"Endereco de testes, {random.Next(1, 9999)}"
                    },
                    Ativo = random.Next(1, 3) == 1,
                    Situacao = (TipoSituacao)random.Next(0, 4),
                    Municipio = (new List<string>() { "UMA CIDADE", "OUTRA CIDADE", "MAIS UMA OUTRA CIDADE", "UMA OUTRA CIDADE QUALQUER" })[new Random().Next(0, 3)],
                    Estado = (new List<string>() { "SC", "RS", "SP", "RJ" })[new Random().Next(0, 3)],
                    Cor = (new List<Color>() { Color.PowderBlue, Color.Empty, Color.Firebrick, Color.FloralWhite })[new Random().Next(0, 3)],
                    Produtos = new List<ProdutoViewModel>()                    
                });

                if (i < 10) {
                    viewModel.Itens.Last().CorFundoLinha = "bae3f5";
                }

                for (int j = 0; j < random.Next(2, 300); j++)
                    viewModel.Itens[i].Produtos.Add(new ProdutoViewModel() { 
                        Codigo = j + 1,
                        Nome = "PRODUTO DE TESTES",
                        Valor = (decimal)random.NextDouble() * 100m
                    });
            }
            #endregion

            #endregion

            var relatorioConstrutor = host.Services.GetService(typeof(IRelatorioVM)) as IRelatorioVM;

            var fonteTeste = TipoFonteEscrita.SystemUI;

            var relatorio = relatorioConstrutor
                .Filtros(viewModel, opcoes =>
                {
                    opcoes
                        .Ignorar(x => x.Itens)
                        .Nome(x => x.DataFinal, "Data final")
                        .Nome(x => x.PessoaCodigo, "Pessoa")
                        .ComplementarValor(x => x.FilialCodigo, x => x.FilialNome)
                        .ComplementarValor(x => x.PessoaCodigo, x => x.Pessoa.Nome)
                        .ComplementarValor(x => x.OperacaoCodigo, x => x.OperacaoNome)
                        .FaixaDeValor(x => x.DataInicial, x => x.DataFinal)
                        .Nome(x => x.Usuario, "Usuário");
                })
                .AdicionarTabelaVertical(viewModel.Itens[0], tabela =>
                {
                    tabela
                        .Titulo("Tabela exibindo valores na vertical")
                        .ComplementarValor(x => x.PessoaCodigo, x => x.Pessoa);
                })
                .AdicionarTabelaVertical(viewModel.Itens[1], tabela =>
                {
                    tabela
                        .Titulo("Tabela exibindo valores na vertical, com quantidade de colunas personalizada")
                        .QuantidadeColunasVerticais(3)
                        .ComplementarValor(x => x.PessoaCodigo, x => x.Pessoa);
                })
                .AdicionarTabela(viewModel.Itens, tabela =>
                {
                    tabela
                        .Titulo("Tabela exibindo valores na horizontal")
                        .Ignorar(x => x.PessoaCodigo)
                        .Ignorar(x => x.CorFundoLinha)
                        //.ComplementarValor(x => x.PessoaCodigo, x => x.Pessoa)                        
                        .ComplementarValor(x => x.Municipio, x => x.Estado)
                       // .TabelaVertical(x => x.Pessoa, true)
                       /* .TabelaHorizontal(x => x.Produtos, false, tabelaProdutos => {
                            tabelaProdutos
                                .ComplementarValor(x => x.Codigo, x => x.Nome)
                                .Coluna(x => x.Valor, coluna => coluna.DefinirPrefixoColuna("R$"))
                                .Totalizar(total => total.Coluna(x => x.Valor, x => x.Valor))
                                .Titulo("Produtos")
                                .Fracionar(2, TipoOrientacaoFracionamento.Horizontal);
                        })*/
                        .Coluna(x => x.Valor, coluna => coluna.DefinirPrefixoColuna("R$"))
                        .Coluna(x => x.Municipio, coluna => coluna
                            .DefinirSeparador("/")
                            .DefinirCondensado(true)
                            .PermitirQuebraDeLinha(false)
                            .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Direita)
                            .DefinirCorFundoConteudo(Color.Orchid))
                        /*.Agrupar(agrupar =>
                            agrupar
                                .Coluna(x => x.FilialCodigo, coluna => coluna.OcultarNoTotal())
                                .Coluna(x => x.Ativo)
                         )
                        .Totalizar(opcoes => {
                            opcoes
                                .Coluna(x => x.Valor, x => x.Valor, coluna => {
                                    coluna
                                        .Titulo("Valor total");
                                })
                                .Coluna(x => x.Municipio, x => 1, coluna => {
                                    coluna
                                        .Titulo("Quantidade");
                                });
                        })*/
                        .Formatar(opcoes => {
                            opcoes
                                .DefinirCorFundoLinhaConteudo(x => x.CorFundoLinha);
                        });
                })
                //.AdicionarLinhaHorizontal()
                //.AdicionarComponenteCustomizado(new ComponenteCustomizado())
                .Titulo($"Teste de relatório - Fonte {fonteTeste}")

                .Configurar(configuracao =>
                {
                    configuracao.ConfigurarFormatacao(formatacao =>
                    {
                        formatacao
                            .DefinirQuantidadeCasasDecimais(3)                            
                            .DefinirValorNulavelParaOTipo<int>("-1")
                            .DefinirValorNulavelParaOTipo<DateTime>("--/--/----")
                            .UsarFonte(TipoFonteEscrita.ArialNarrow)
                            .ConfigurarFonteConteudo(fonte =>
                            {
                                fonte.Tamanho = 10;
                                fonte.Nome = fonteTeste;
                            })
                            .ConfigurarFonteTitulo(fonte =>
                            {
                                fonte.Tamanho = 20;
                            });
                    });

                })
                .Construir();


            var cronometro = new Stopwatch();
            cronometro.Start();
            var html = relatorio.GerarHtml();
            cronometro.Stop();
            Console.WriteLine($"Tempo gerando relatório: {cronometro.ElapsedMilliseconds}. Tamanho html: {html.Length}");

            var pathHtml = Path.Combine(Path.GetTempPath(), Path.GetTempFileName().Replace(".tmp", ".html"));
            File.WriteAllText(pathHtml, html);
            new Process { StartInfo = new ProcessStartInfo(pathHtml) { UseShellExecute = true } }.Start();

           // html = File.ReadAllText(@"C:\Users\Marciel\Downloads\TesteRelatorio.html");
            //CriarArquivoPDFUsandoDinkToPDF(html);
            //CriarArquivoPDFUsandoIText(html);
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.UtilizarRelatorioVM(options =>
                    {
                        options.ConfigurarConteudo(conteudo =>
                        {
                            conteudo.Zebrado = true;
                        });
                        options                           
                            .ConfigurarFormatacao(formato => {
                                formato.DefinirQuantidadeCasasDecimais(3);
                            })
                            .ConfigurarCabecalho(cabecalho => {
                                cabecalho
                                    .Esquerda().ImprimirTexto("Nome do meu cliente")
                                    .Direita().ImprimirNumeroDePaginas();
                            })
                            .ConfigurarRodape(rodape => {
                                rodape
                                    .Esquerda().ImprimirTexto("Nome da minha empresa")
                                    .Centro().ImprimirDataHora()
                                    .Direita().ImprimirTexto("Site da minha empresa");
                            });
                    }));

        private static void CriarArquivoPDFUsandoDinkToPDF(string html) {
            var converter = new SynchronizedConverter(new PdfTools());
            var arquivo = Path.Combine(Path.GetTempPath(), Path.GetTempFileName().Replace(".tmp", ".pdf"));
            var caminhoArquivoHeader = GravarHtmlHeader();
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    Out = arquivo,
                    DPI = 320,
                },
                Objects = {
                    new ObjectSettings() {
                        Encoding = System.Text.Encoding.UTF8,
                        PagesCount = true,                        
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { HtmlUrl = caminhoArquivoHeader },
                        FooterSettings = { HtmlUrl = caminhoArquivoHeader }
                    }
                }
            };
            converter.Convert(doc);
            new Process { StartInfo = new ProcessStartInfo(arquivo) { UseShellExecute = true } }.Start();
        }

        private static void CriarArquivoPDFUsandoIText(string html)
        {
            
            var arquivo = Path.Combine(Path.GetTempPath(), Path.GetTempFileName().Replace(".tmp", ".pdf"));

            using (var memoryStream = new MemoryStream())
            {

                HtmlConverter.ConvertToPdf(html, memoryStream);
                File.WriteAllBytes(arquivo, memoryStream.ToArray());
            }

            new Process { StartInfo = new ProcessStartInfo(arquivo) { UseShellExecute = true } }.Start();
        }

        private static string GravarHtmlHeader() {
            var header = @"<?xml version=""1.0"" encoding=""utf-8""?>
<!DOCTYPE html>
  <html><head><script>
  function subst() {
      var vars = {};
      var query_strings_from_url = document.location.search.substring(1).split('&');
      for (var query_string in query_strings_from_url) {
          if (query_strings_from_url.hasOwnProperty(query_string)) {
              var temp_var = query_strings_from_url[query_string].split('=', 2);
              vars[temp_var[0]] = decodeURI(temp_var[1]);
          }
      }
      var css_selector_classes = ['page', 'frompage', 'topage', 'webpage', 'section', 'subsection', 'date', 'isodate', 'time', 'title', 'doctitle', 'sitepage', 'sitepages'];
      for (var css_class in css_selector_classes) {
          if (css_selector_classes.hasOwnProperty(css_class)) {
              var element = document.getElementsByClassName(css_selector_classes[css_class]);
              for (var j = 0; j < element.length; ++j) {
                  element[j].textContent = vars[css_selector_classes[css_class]];
              }
          }
      }
  }
  </script></head><body style=""border:0; margin: 0;"" onload=""subst()"">
  <table style=""border-bottom: 1px solid black; width: 100%"">
    <tr>
      <td class=""section""></td>
      <td style=""text-align:right"">
        Página <span class=""page""></span> / <span class=""topage""></span>
      </td>
    </tr>
  </table>
  </body></html>
";
            var arquivo = Path.Combine(Path.GetTempPath(), Path.GetTempFileName().Replace(".tmp", ".html"));
            File.WriteAllText(arquivo, header);
            return arquivo;
        }
    }
}