using Microsoft.Extensions.Hosting;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Interfaces;
using System.IO;

namespace RelatorioVM.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            var relatorio = host.Services.GetService(typeof(IRelatorioVM)) as IRelatorioVM;
            var bytes = relatorio
                .Configurar(configuracao => configuracao.UsarOrientacao(TipoOrientacao.Retrato))
                .Construir()
                .Gerar();

            var path = Path.Combine(Path.GetTempPath(), Path.GetTempFileName().Replace(".tmp",".pdf"));
            File.WriteAllBytes(path, bytes);            
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AdicionarRelatorioVM(options => {
                        options
                            .UsarOrientacao(TipoOrientacao.Paisagem)
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
