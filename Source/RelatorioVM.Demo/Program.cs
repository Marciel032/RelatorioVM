using Microsoft.Extensions.Hosting;
using RelatorioVM.Demo.Modelos;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Interfaces;
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
                Itens = new List<ExemploSimplesItemViewModel>() { 
                    new ExemploSimplesItemViewModel(),
                    new ExemploSimplesItemViewModel()
                }
            };

            var relatorio = host.Services.GetService(typeof(IRelatorioVM)) as IRelatorioVM;

            var bytes = relatorio
                .Filtros(viewModel, opcoes => {
                    opcoes
                        .Ignorar(x => x.PessoaCodigo);
                })
                .AdicionarConteudo(viewModel.Itens)
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
                    services.AdicionarRelatorioVM(options => {
                        options
                            .UsarOrientacao(TipoOrientacao.Retrato)
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
