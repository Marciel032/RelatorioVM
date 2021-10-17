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
        /// <summary>
        /// Define um totalizador manualmente
        /// </summary>
        /// <typeparam name="TPropriedade"></typeparam>
        /// <param name="expressaoPropriedade">Coluna onde o total é exibido</param>
        /// <param name="expressaoCalculo">Valor calculado para obter o totalizador</param>
        /// <returns></returns>
        ITabelaTotalRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade, Expression<Func<TConteudo, decimal>> expressaoCalculo);

        /// <summary>
        /// Define um totalizador manualmente
        /// </summary>
        /// <typeparam name="TPropriedade"></typeparam>
        /// <param name="expressaoPropriedade">Coluna onde o total é exibido</param>
        /// <param name="expressaoCalculo">Valor calculado para obter o totalizador</param>
        /// <returns></returns>
        ITabelaTotalRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade, Expression<Func<TConteudo, long>> expressaoCalculo);
        
        /// <summary>
        /// Ignora uma coluna especifica
        /// </summary>
        /// <typeparam name="TPropriedade"></typeparam>
        /// <param name="expressaoPropriedade">Coluna a ser ignorada</param>
        /// <returns></returns>
        ITabelaTotalRelatorioVM<TConteudo> Ignorar<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade);

        /// <summary>
        /// Ignora todas as colunas, permitindo configurar manualmente os totais desejados
        /// </summary>
        /// <typeparam name="TPropriedade"></typeparam>
        /// <param name="expressaoPropriedade">Coluna a ser ignorada</param>
        /// <returns></returns>
        ITabelaTotalRelatorioVM<TConteudo> IgnorarTodos();

    }
}
