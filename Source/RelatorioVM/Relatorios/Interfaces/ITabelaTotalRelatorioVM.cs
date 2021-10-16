using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Relatorios.Interfaces
{
    public interface ITabelaTotalRelatorioVM<TConteudo>
    {
        /// <summary>
        /// Define o titulo que sera exibido junto aos totais.
        /// </summary>
        ITabelaTotalRelatorioVM<TConteudo> Titulo(string titulo);
        ITabelaTotalRelatorioVM<TConteudo> Coluna(Expression<Func<TConteudo, decimal>> expressaoPropriedade, Expression<Func<TConteudo, decimal>> expressaoCalculo = null);
        ITabelaTotalRelatorioVM<TConteudo> Coluna(Expression<Func<TConteudo, long>> expressaoPropriedade, Expression<Func<TConteudo, long>> expressaoCalculo = null);

    }
}
