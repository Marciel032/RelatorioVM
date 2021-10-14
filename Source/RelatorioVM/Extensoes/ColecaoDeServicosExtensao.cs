using Microsoft.Extensions.DependencyInjection;
using RelatorioVM.Conversores;
using RelatorioVM.Conversores.Interfaces;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Interfaces;
using RelatorioVM.Infraestruturas;
using RelatorioVM.Relatorios.Fabricas;
using System;

namespace RelatorioVM.Extensoes
{
    public static class ColecaoDeServicosExtensao
    {
        public static IServiceCollection AdicionarRelatorioVM(this IServiceCollection colecaoDeServicos)
        {
            colecaoDeServicos.AddSingleton(typeof(IConversor), new ConversorSincrono());

            colecaoDeServicos.AddSingleton((provedorDeServicos) => { 
                return ConstrutorRelatorioFabrica.Criar(provedorDeServicos.GetRequiredService<IConversor>()); 
            });

            return colecaoDeServicos;
        }
        
        public static IServiceCollection AdicionarRelatorioVM(this IServiceCollection colecaoDeServicos, Action<IConfiguracaoRelatorio> opcoes)
        {
            colecaoDeServicos.AdicionarRelatorioVM();

            opcoes.Invoke(Configuracao.ConfiguracaoRelatorio);

            return colecaoDeServicos;
        }
    }
}
