using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface ITabelaHorizontalRelatorioVM<TConteudo>: ITabelaRelatorioVM<TConteudo>
    {
        /// <summary>
        /// Define um titulo para a tabela
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> Titulo(string titulo);

        /// <summary>
        /// Não exibe a propriedade informada
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> Ignorar<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao);

        /// <summary>
        /// Altera todas as propriedades para não serem exibidas. Isto permite informar manualmente qual propriedade sera exibida.
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> IgnorarTudo();

        /// <summary>
        /// Define que a propriedade informada será exibida
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> Exibir<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao);

        /// <summary>
        /// Concatena o valor das duas propriedades informadas, resultando em "Valor - Descrição"
        /// </summary>
        /// <typeparam name="TPropriedade"></typeparam>
        /// <typeparam name="TPropriedadeComplemento"></typeparam>
        /// <param name="propriedadeExpressao">Propriedade principal</param>
        /// <param name="propriedadeComplementoExpressao">Propriedade complementar</param>
        /// <param name="ignorar">Faz com que a propriedade complementar não seja exibida nas colunas</param>
        /// <returns></returns>
        ITabelaHorizontalRelatorioVM<TConteudo> ComplementarValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Expression<Func<TConteudo, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true);

        /// <summary>
        /// Adiciona o texto retornado pela função ao complemento do valor da coluna
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao);

        /// <summary>
        /// Adiciona o texto retornado pela função ao complemento do valor da coluna
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TConteudo, TPropriedade, string> funcao);

        /// <summary>
        /// Adiciona automaticamente todas as colunas decimais como totais, e permite configurar manualmente qual coluna deve totalizar.
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> Totalizar(Action<ITabelaTotalRelatorioVM<TConteudo>> opcoes = null);

        /// <summary>
        /// Aplica agrupamento e ordenação dos valores nas colunas informadas
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> Agrupar(Action<ITabelaAgrupadorRelatorioVM<TConteudo>> opcoes);        
    }
}
