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
                    Estado = (new List<string>() { "SC", "RS", "SP", "RJ" })[new Random().Next(0, 3)]
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
                        //.ComplementarValor(x => x.PessoaCodigo, x => x.Pessoa)                        
                        .ComplementarValor(x => x.Municipio, x => x.Estado)
                        .TabelaVertical(x => x.Pessoa, tabelaV => tabelaV.QuantidadeColunasVerticais(2))
                        .Coluna(x => x.Valor, coluna => coluna.DefinirPrefixoColuna("R$"))
                        .Coluna(x => x.Municipio, coluna => coluna
                            .DefinirSeparador("/")
                            .DefinirCondensado(true)
                            .PermitirQuebraDeLinha(false)
                            .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Direita))
                        .Agrupar(agrupar =>
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
                        });
                })
                .AdicionarLinhaHorizontal()
                .AdicionarComponenteCustomizado(new ComponenteCustomizado())
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
                        /*   options
                               .UsarOrientacao(TipoOrientacao.Retrato)                            
                               .ConfigurarFormatacao(formato => {
                                   formato.CasasDecimais = 3;
                               })
                               .ConfigurarCabecalho(cabecalho => {
                                   cabecalho
                                       .Esquerda().ImprimirTexto("Nome da empresa")
                                       .Centro().ImprimirDataHora()
                                       .Direita().ImprimirNumeroDePaginas();
                               })
                               .ConfigurarRodape(rodape => {
                                   rodape
                                       .Esquerda().ImprimirTexto("Infogen Sistemas")
                                       .Direita().ImprimirTexto("www.infogen.com.br");
                               });*/
                    }));
    }
}