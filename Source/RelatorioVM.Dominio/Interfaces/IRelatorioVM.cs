using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface IRelatorioVM
    {
        IRelatorioVM Filtros<TFiltro>(TFiltro filtros, Action<IFiltroRelatorioVM<TFiltro>> opcoes = null);
        IRelatorioVM AdicionarTabela<TConteudo>(IEnumerable<TConteudo> conteudo, Action<ITabelaHorizontalRelatorioVM<TConteudo>> opcoes = null);
        IRelatorioVM AdicionarTabelaVertical<TConteudo>(TConteudo conteudo, Action<ITabelaVerticalRelatorioVM<TConteudo>> opcoes = null);
        IRelatorioVM AdicionarTabelaVertical<TConteudo>(IEnumerable<TConteudo> conteudo, Action<ITabelaVerticalRelatorioVM<TConteudo>> opcoes = null);
        IRelatorioVM AdicionarTabelaVertical<TConteudo>(List<TConteudo> conteudo, Action<ITabelaVerticalRelatorioVM<TConteudo>> opcoes = null);
        IRelatorioVM AdicionarComponenteCustomizado(IElementoRelatorioVM elemento);
        IRelatorioVM AdicionarLinhaHorizontal();
        IRelatorioVM Configurar(Action<IConfiguracaoRelatorio> configuracao);
        IRelatorioVM Titulo(string titulo);
        IGeradorRelatorioVM Construir();
    }
}
