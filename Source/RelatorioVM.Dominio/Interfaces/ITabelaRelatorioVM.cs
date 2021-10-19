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

        /// <summary>
        /// Concatena o valor das duas propriedades informadas, resultando em "Valor - Descrição"
        /// </summary>
        /// <typeparam name="TPropriedade"></typeparam>
        /// <typeparam name="TPropriedadeComplemento"></typeparam>
        /// <param name="propriedadeExpressao">Propriedade principal</param>
        /// <param name="propriedadeComplementoExpressao">Propriedade complementar</param>
        /// <param name="ignorar">Faz com que a propriedade complementar não seja exibida nas colunas</param>
        /// <returns></returns>
        ITabelaRelatorioVM<TConteudo> ComplementarValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Expression<Func<TConteudo, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true);

        /// <summary>
        /// Adiciona o texto retornado pela função ao complemento do valor da coluna
        /// </summary>
        ITabelaRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao);

        /// <summary>
        /// Adiciona o texto retornado pela função ao complemento do valor da coluna
        /// </summary>
        ITabelaRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TConteudo, TPropriedade, string> funcao);
    }
}
