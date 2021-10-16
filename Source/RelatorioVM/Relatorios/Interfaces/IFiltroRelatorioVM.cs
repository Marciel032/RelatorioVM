using RelatorioVM.Dominio.Configuracoes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Relatorios.Interfaces
{
    public interface IFiltroRelatorioVM<T>
    {
        /// <summary>
        /// Altera o nome do filtro para o valor informado
        /// </summary>
        IFiltroRelatorioVM<T> Nome<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, string nome);

        /// <summary>
        /// Altera o separador que divide campos de codigo e nome, ou periodos
        /// </summary>
        IFiltroRelatorioVM<T> Separador<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, string separador);

        /// <summary>
        /// Não exibe a propriedade informada ao gerar a tabela de filtros
        /// </summary>
        IFiltroRelatorioVM<T> Ignorar<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao);

        /// <summary>
        /// Substitui o valor do filtro pelo valor informado, na propriedade especificada
        /// </summary>
        IFiltroRelatorioVM<T> Valor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, string valor);

        /// <summary>
        /// Substitui o valor do filtro pelo valor retornado na função
        /// </summary>
        IFiltroRelatorioVM<T> Valor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao);

        /// <summary>
        /// Adiciona o texto informado ao valor já contido na propriedade informada
        /// </summary>
        IFiltroRelatorioVM<T> ComplementarValor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, string valor);

        /// <summary>
        /// Concatena o valor das duas propriedades informadas, resultando em "Valor - Descrição"
        /// </summary>
        /// <typeparam name="TPropriedade"></typeparam>
        /// <typeparam name="TPropriedadeComplemento"></typeparam>
        /// <param name="propriedadeExpressao">Propriedade principal</param>
        /// <param name="propriedadeComplementoExpressao">Propriedade complementar</param>
        /// <param name="ignorar">Faz com que a propriedade complementar não seja exibida nos filtros</param>
        /// <returns></returns>
        IFiltroRelatorioVM<T> ComplementarValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Expression<Func<T, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true);

        /// <summary>
        /// Concatena o valor das duas propriedades informadas, usando um separador com texto "Até"
        /// </summary>
        /// <typeparam name="TPropriedade"></typeparam>
        /// <typeparam name="TPropriedadeComplemento"></typeparam>
        /// <param name="propriedadeExpressao">Faixa inicial</param>
        /// <param name="propriedadeComplementoExpressao">Faixa final</param>
        /// <param name="ignorar">Faz com que a propriedade complementar não seja exibida nos filtros</param>
        /// <returns></returns>
        IFiltroRelatorioVM<T> FaixaDeValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Expression<Func<T, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true);

        /// <summary>
        /// Adiciona o texto retornado pela função ao complemento do valor do filtro
        /// </summary>
        IFiltroRelatorioVM<T> ComplementarValor<TPropriedade>(Expression<Func<T, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao);
    }
}
