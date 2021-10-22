using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface IRelatorioVM
    {
        IRelatorioVM Filtros<TFiltro>(TFiltro filtros, Action<IFiltroRelatorioVM<TFiltro>> opcoes = null);
        IRelatorioVM AdicionarTabela<TConteudo>(IEnumerable<TConteudo> conteudo, Action<ITabelaRelatorioVM<TConteudo>> opcoes = null);
        IRelatorioVM AdicionarComponenteCustomizado(IElementoRelatorioVM elemento);
        IRelatorioVM AdicionarLinhaHorizontal();
        IRelatorioVM Configurar(Action<IConfiguracaoRelatorio> configuracao);
        IRelatorioVM Titulo(string titulo);
        IGeradorRelatorioVM Construir();
    }
}
