using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Interfaces
{
    public interface ITabelaRelatorioVM<TConteudo>
    {
        ITabelaRelatorioVM<TConteudo> Titulo(string titulo);
        ITabelaRelatorioVM<TConteudo> Totalizar(Action<ITabelaTotalRelatorioVM<TConteudo>> opcoes = null);
    }
}
