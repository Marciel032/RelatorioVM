﻿using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface ITabelaVerticalRelatorioVM<TConteudo>
    {
        /// <summary>
        /// Define um titulo para a tabela
        /// </summary>
        ITabelaVerticalRelatorioVM<TConteudo> Titulo(string titulo);

        /// <summary>
        /// Não exibe a propriedade informada
        /// </summary>
        ITabelaVerticalRelatorioVM<TConteudo> Ignorar<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao);

        /// <summary>
        /// Altera todas as propriedades para não serem exibidas. Isto permite informar manualmente qual propriedade sera exibida.
        /// </summary>
        ITabelaVerticalRelatorioVM<TConteudo> IgnorarTudo();

        /// <summary>
        /// Define que a propriedade informada será exibida
        /// </summary>
        ITabelaVerticalRelatorioVM<TConteudo> Exibir<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao);

        /// <summary>
        /// Permite realizar configurações em uma coluna especifica
        /// </summary>
        ITabelaVerticalRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Action<IColunaRelatorioVM<TConteudo>> coluna);

        /// <summary>
        /// Concatena o valor das duas propriedades informadas, resultando em "Valor - Descrição"
        /// </summary>
        /// <typeparam name="TPropriedade"></typeparam>
        /// <typeparam name="TPropriedadeComplemento"></typeparam>
        /// <param name="propriedadeExpressao">Propriedade principal</param>
        /// <param name="propriedadeComplementoExpressao">Propriedade complementar</param>
        /// <param name="ignorar">Faz com que a propriedade complementar não seja exibida nas colunas</param>
        /// <returns></returns>
        ITabelaVerticalRelatorioVM<TConteudo> ComplementarValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Expression<Func<TConteudo, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true);

        /// <summary>
        /// Adiciona o texto retornado pela função ao complemento do valor da coluna
        /// </summary>
        ITabelaVerticalRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao);

        /// <summary>
        /// Adiciona o texto retornado pela função ao complemento do valor da coluna
        /// </summary>
        ITabelaVerticalRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TConteudo, TPropriedade, string> funcao);

        /// <summary>
        /// Define a quantidade de colunas em que o conteúdo será dividido
        /// </summary>
        ITabelaVerticalRelatorioVM<TConteudo> QuantidadeColunasVerticais(int colunas = 1);

        /// <summary>
        /// Divide os valores em colunas, e os exibe lado a lado
        /// </summary>
        /// <param name="quantidadeColunas">Define a quantidade de dados que será exibido em cada linha.</param>
        /// <param name="orientacao">Define a ordenação dos dados exibidos.</param>
        /// <returns></returns>        
        ITabelaVerticalRelatorioVM<TConteudo> Fracionar(int quantidadeColunas, TipoOrientacaoFracionamento orientacao = TipoOrientacaoFracionamento.Horizontal);

        // <summary>
        /// Define que a coluna sera exibida como uma imagem. O tipo da coluna precisa ser uma string contendo uma Url ou um array de bytes em formato Base64.
        /// </summary>
        ITabelaVerticalRelatorioVM<TConteudo> Imagem(Expression<Func<TConteudo, string>> propriedadeExpressao, Action<IImagemRelatorioVM> opcoes = null);
    }
}
