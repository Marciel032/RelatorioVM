using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Relatorios.Interfaces
{
    public interface ITabelaAgrupadorRelatorioVM<TConteudo>
    {
        ITabelaAgrupadorRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade);
    }
}
