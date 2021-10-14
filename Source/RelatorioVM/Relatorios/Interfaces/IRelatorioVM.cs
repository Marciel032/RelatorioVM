using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Interfaces
{
    public interface IRelatorioVM
    {
        IRelatorioVM Filtros<TFiltro>(TFiltro filtros, Action<IFiltroRelatorioVM<TFiltro>> opcoes = null);
        IRelatorioVM AdicionarConteudo<TConteudo>(TConteudo conteudo);
        IRelatorioVM Configurar(Action<IConfiguracaoRelatorio> configuracao);
        IRelatorioVM Titulo(string titulo);
        IGeradorRelatorioVM Construir();
    }
}
