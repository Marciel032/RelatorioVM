using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface ITabelaRelatorioVM<TConteudo>
    {
        ITabelaRelatorioVM<TConteudo> Titulo(string titulo);
        ITabelaRelatorioVM<TConteudo> Totalizar(Action<ITabelaTotalRelatorioVM<TConteudo>> opcoes = null);
        ITabelaRelatorioVM<TConteudo> Agrupar(Action<ITabelaAgrupadorRelatorioVM<TConteudo>> opcoes);
    }
}
