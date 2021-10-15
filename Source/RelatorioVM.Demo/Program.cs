using Microsoft.Extensions.Hosting;
using RelatorioVM.Demo.Modelos;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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

            for (int i = 0; i < 150; i++)
            {
                viewModel.Itens.Add(new ExemploSimplesItemViewModel()
                {
                    Data = DateTime.Now,
                    FilialCodigo = new Random().Next(),
                    PessoaCodigo = new Random().Next(),
                    Valor = (decimal)new Random().NextDouble(),
                    Pessoa = new PessoaViewModel() { 
                        Codigo = new Random().Next(),
                        Nome = "Teste"
                    }
                });
            }

            var relatorio = host.Services.GetService(typeof(IRelatorioVM)) as IRelatorioVM;

            var bytes = relatorio
                .Filtros(viewModel, opcoes => {
                    opcoes
                        .Ignorar(x => x.Itens)
                        .Nome(x => x.DataFinal, "Data final")
                        .Nome(x => x.PessoaCodigo, "Pessoa")
                        .ComplementarValor(x => x.FilialCodigo, x => x.FilialNome)
                        .ComplementarValor(x => x.PessoaCodigo, x => x.Pessoa.Nome)
                        .ComplementarValor(x => x.DataInicial, x => x.DataFinal)
                        .Separador(x => x.DataInicial, "até");
                })
                .AdicionarTabela(viewModel.Itens)
                .Titulo("Teste de relatório")
                .Construir()
                .Gerar();

            var path = Path.Combine(Path.GetTempPath(), Path.GetTempFileName().Replace(".tmp",".pdf"));

            File.WriteAllBytes(path, bytes);

            new Process
            {
                StartInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.UtilizarRelatorioVM(options => {
                        options
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
                            });
                    }));
    }
}
