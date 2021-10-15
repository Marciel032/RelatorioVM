using Microsoft.Extensions.DependencyInjection;
using RelatorioVM.Configuradores;
using RelatorioVM.Conversores;
using RelatorioVM.ConversoresPdf;
using RelatorioVM.ConversoresPdf.Interfaces;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Infraestruturas;
using RelatorioVM.Relatorios.Fabricas;
using System;

namespace RelatorioVM.Extensoes
{
    public static class ColecaoDeServicosExtensao
    {
        public static IConfiguradorRelatorio UtilizarRelatorioVM(this IServiceCollection colecaoDeServicos)
        {
            colecaoDeServicos.AddSingleton(typeof(IConversorPdf), new ConversorPdfSincrono());

            colecaoDeServicos.AddSingleton((provedorDeServicos) => { 
                return ConstrutorRelatorioFabrica.Criar(provedorDeServicos.GetRequiredService<IConversorPdf>()); 
            });

            ConversorValor.DefinirConversor<DateTime>(new ConversorDataHora());
            ConversorValor.DefinirConversor<decimal>(new ConversorDecimal());

            return new ConfiguradorRelatorio();
        }
        
        public static IConfiguradorRelatorio UtilizarRelatorioVM(this IServiceCollection colecaoDeServicos, Action<IConfiguracaoRelatorio> opcoes)
        {
            var configurador = colecaoDeServicos.UtilizarRelatorioVM();

            opcoes.Invoke(Configuracao.ConfiguracaoRelatorio);

            return configurador;
        }
    }
}
