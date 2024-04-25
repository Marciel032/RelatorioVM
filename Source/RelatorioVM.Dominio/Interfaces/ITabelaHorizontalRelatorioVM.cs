using RelatorioVM.Dominio.Enumeradores;
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
        /// Permite realizar configurações em uma coluna especifica
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Action<IColunaRelatorioVM<TConteudo>> coluna);

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

        /// <summary>
        /// Permite formatar o layout da tabela, como cor, fonte, etc
        /// </summary>
        ITabelaHorizontalRelatorioVM<TConteudo> Formatar(Action<ITabelaFormatacaoRelatorioVM<TConteudo>> opcoes);

        /// <summary>
        /// Divide os valores em colunas, e os exibe lado a lado
        /// </summary>
        /// <param name="quantidadeColunas">Define a quantidade de dados que será exibido em cada linha.</param>
        /// <param name="orientacao">Define a ordenação dos dados exibidos.</param>
        /// <returns></returns>        
        ITabelaHorizontalRelatorioVM<TConteudo> Fracionar(int quantidadeColunas, TipoOrientacaoFracionamento orientacao = TipoOrientacaoFracionamento.Horizontal);

        /// <summary>
        /// Define que a coluna sera exibida como uma tabela vertical. O tipo da coluna precisa ser uma classe.
        /// </summary>     
        /// <param name="exibirNaColuna">Quando verdadeiro, exibe a tabela vertical dentro da coluna. Quando falso, exibe a tabela vertical em uma nova linha.</param>       
        ITabelaHorizontalRelatorioVM<TConteudo> TabelaVertical<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, bool exibirNaColuna = true, Action<ITabelaVerticalRelatorioVM<TPropriedade>> opcoes = null) where TPropriedade : class;

        /// <summary>
        /// Define que a coluna sera exibida como uma tabela vertical. O tipo da coluna precisa ser uma classe.
        /// </summary>     
        /// <param name="exibirNaColuna">Quando verdadeiro, exibe a tabela vertical dentro da coluna. Quando falso, exibe a tabela vertical em uma nova linha.</param>       
        ITabelaHorizontalRelatorioVM<TConteudo> TabelaVerticalLista<TPropriedade>(Expression<Func<TConteudo, IEnumerable<TPropriedade>>> propriedadeExpressao, bool exibirNaColuna = true, Action<ITabelaVerticalRelatorioVM<TPropriedade>> opcoes = null) where TPropriedade : class;

        /// <summary>
        /// Define que a coluna sera exibida como uma tabela horizontal. O tipo da coluna precisa ser uma LISTA de uma classe.
        /// </summary>
        /// <param name="exibirNaColuna">Quando verdadeiro, exibe a tabela horizontal dentro da coluna. Quando falso, exibe a tabela horizontal em uma nova linha.</param>       
        ITabelaHorizontalRelatorioVM<TConteudo> TabelaHorizontal<TPropriedade>(Expression<Func<TConteudo, IEnumerable<TPropriedade>>> propriedadeExpressao, bool exibirNaColuna = true, Action<ITabelaHorizontalRelatorioVM<TPropriedade>> opcoes = null) where TPropriedade : class;        
    }
}
