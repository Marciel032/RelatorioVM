using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface ITabelaAgrupadorRelatorioVM<TConteudo>
    {
        ITabelaAgrupadorRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade, Action<IColunaAgrupadorRelatorioVM> configuraColuna = null);
        ITabelaAgrupadorRelatorioVM<TConteudo> Totalizar(bool totalizar = true);
    }
}
