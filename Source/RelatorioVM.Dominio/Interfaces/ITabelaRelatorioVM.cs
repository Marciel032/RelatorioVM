using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface ITabelaRelatorioVM<TConteudo>
    {
        ITabelaRelatorioVM<TConteudo> Titulo(string titulo);

        /// <summary>
        /// 
        /// </summary>
        ITabelaRelatorioVM<TConteudo> Totalizar(Action<ITabelaTotalRelatorioVM<TConteudo>> opcoes = null);

        /// <summary>
        /// Aplica agrupamento e ordenação dos valores nas colunas informadas
        /// </summary>
        ITabelaRelatorioVM<TConteudo> Agrupar(Action<ITabelaAgrupadorRelatorioVM<TConteudo>> opcoes);

        /// <summary>
        /// Não exibe a coluna informada
        /// </summary>
        ITabelaRelatorioVM<TConteudo> Ignorar<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao);
    }
}
