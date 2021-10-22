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

namespace RelatorioVM.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            var viewModel = new ExemploSimplesViewModel() { 
                FilialCodigo = 1,
                FilialNome = "Nome da filial",
                PessoaCodigo = 5236,
                DataFinal = DateTime.Now.Date,
                DataInicial = DateTime.Now.Date,
                Pessoa = new PessoaViewModel() { 
                    Codigo = 1,
                    Nome = "Testes"
                },
                Itens = new List<ExemploSimplesItemViewModel>(),                
            };

            for (int i = 0; i < 100; i++)
            {
                viewModel.Itens.Add(new ExemploSimplesItemViewModel()
                {
                    Data = DateTime.Now,
                    FilialCodigo = new Random().Next(1, 10),
                    PessoaCodigo = new Random().Next(1, 10),
                    Valor = (decimal)new Random().NextDouble() * 100m,
                    Pessoa = new PessoaViewModel() { 
                        Codigo = new Random().Next(),
                        Nome = "Teste"
                    },
                    Ativo = new Random().Next(1, 3) == 1,
                    Situacao = (TipoSituacao)new Random().Next(0, 4)
                });
            }

            var relatorioConstrutor = host.Services.GetService(typeof(IRelatorioVM)) as IRelatorioVM;            

            var relatorio = relatorioConstrutor
                .Filtros(viewModel, opcoes => {
                    opcoes
                        .Ignorar(x => x.Itens)
                        .Nome(x => x.DataFinal, "Data final")
                        .Nome(x => x.PessoaCodigo, "Pessoa")
                        .ComplementarValor(x => x.FilialCodigo, x => x.FilialNome)
                        .ComplementarValor(x => x.PessoaCodigo, x => x.Pessoa.Nome)
                        .FaixaDeValor(x => x.DataInicial, x => x.DataFinal);
                })
                .AdicionarTabela(viewModel.Itens, tabela => {
                    tabela
                        .Titulo("Descrição da tabela de testes")
                        .Agrupar(agrupar => 
                            agrupar
                                .Coluna(x => x.FilialCodigo)
                         )
                        .Totalizar()
                        .ComplementarValor(x => x.PessoaCodigo, x => x.Pessoa);
                })      
                .AdicionarComponenteCustomizado(new ComponenteCustomizado())
                .Titulo("Teste de relatório")
                .Construir();

            var cronometro = new Stopwatch();
            cronometro.Start();
            var html = relatorio.GerarHtml();
            cronometro.Stop();
            Console.WriteLine($"Tempo gerando relatório: {cronometro.ElapsedMilliseconds}. Tamanho html: {html.Length}");
            
            var pathHtml = Path.Combine(Path.GetTempPath(), Path.GetTempFileName().Replace(".tmp", ".html"));
            File.WriteAllText(pathHtml, html);
            new Process{StartInfo = new ProcessStartInfo(pathHtml){UseShellExecute = true}}.Start();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.UtilizarRelatorioVM(options => {
                        options.ConfigurarConteudo(conteudo =>
                        {
                            conteudo.Zebrado = false;
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
