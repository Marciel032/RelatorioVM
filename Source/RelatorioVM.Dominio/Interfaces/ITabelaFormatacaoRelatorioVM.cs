using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface ITabelaFormatacaoRelatorioVM<TConteudo>
    {
        void DefinirCorFundoLinhaConteudo(Expression<Func<TConteudo, string>> propriedadeExpressao);
        void DefinirCorFundoLinhaConteudo(Expression<Func<TConteudo, Color>> propriedadeExpressao);
        void DefinirCorFundoLinhaConteudo(Expression<Func<TConteudo, TipoCor>> propriedadeExpressao);
    }
}
