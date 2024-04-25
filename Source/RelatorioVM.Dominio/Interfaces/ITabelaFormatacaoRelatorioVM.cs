using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface ITabelaFormatacaoRelatorioVM<TConteudo>
    {
        void DefinirCorFundoLinhaConteudo(Expression<Func<TConteudo, string>> propriedadeExpressao);
    }
}
