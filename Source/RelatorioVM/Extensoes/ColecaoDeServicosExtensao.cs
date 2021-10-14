using Microsoft.Extensions.DependencyInjection;
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
            colecaoDeServicos.AddSingleton((provedorDeServicos) => { return ConstrutorRelatorioFabrica.Criar(); });
            return colecaoDeServicos;
        }
        
        public static IServiceCollection AdicionarRelatorioVM(this IServiceCollection colecaoDeServicos, Action<IConfiguracaoRelatorio> opcoes)
        {
            colecaoDeServicos.AddSingleton((provedorDeServicos) => { return ConstrutorRelatorioFabrica.Criar(); });
            opcoes.Invoke(Configuracao.ConfiguracaoRelatorio);


            return colecaoDeServicos;
        }
    }
}
