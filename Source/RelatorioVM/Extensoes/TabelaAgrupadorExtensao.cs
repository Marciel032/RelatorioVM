using HtmlTags;
using RelatorioVM.Comparadores;
using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Elementos.Relatorios;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class TabelaAgrupadorExtensao
    {
        public static IEnumerable<IGrouping<IDictionary<string, object>, T>> AgruparConteudo<T>(this TabelaAgrupador<T> agrupador, IEnumerable<T> conteudo)
        {
            Func<T, IDictionary<string, object>> agrupamentoFuncao = (item) =>
            {
                dynamic objetoAgrupamento = new ExpandoObject();
                agrupador.Colunas.ForEach(x => ((IDictionary<string, object>)objetoAgrupamento).Add(x.Identificador, x.Propriedade.ObterValor(item)));
                return objetoAgrupamento;
            };

            if (agrupador.Colunas.Count > 0) {
                IOrderedEnumerable<T> conteudoOrdenado = conteudo.OrderBy(x => agrupador.Colunas[0].Propriedade.ObterValor(x));
                for (int i = 1; i < agrupador.Colunas.Count; i++)
                {
                    var colunaAgrupamento = agrupador.Colunas[i];
                    conteudoOrdenado = conteudoOrdenado.ThenBy(x => colunaAgrupamento.Propriedade.ObterValor(x));
                }

                return conteudoOrdenado.GroupBy(agrupamentoFuncao, new DicionarioComparador());
            }

            return conteudo.GroupBy(agrupamentoFuncao, new DicionarioComparador());
        }

        public static HtmlTag CriarCabecalhoAgrupamento<T>(this TabelaAgrupador<T> agrupador, T item, int quantidadeDeColunas)
        {
            var linhaTabela = new HtmlTag("tr")
                .Style("font-weight", "bold")
                .Style("border-bottom", "1px solid #888");

            var colunaTabela = linhaTabela
                .CriarColunaTabela()
                .Attr("colspan", quantidadeDeColunas);

            var textoCabecaho = string.Empty;
            foreach (var coluna in agrupador.Colunas)
                textoCabecaho += $"{coluna.Titulo}:{coluna.Propriedade.ObterValorConvertido(item, agrupador.ObterOpcoesFormatacao())}   ";

            colunaTabela.Text(textoCabecaho);

            return linhaTabela;
        }

        public static List<TabelaColuna<T>> ObterColunasAgrupamento<T>(this TabelaAgrupador<T> agrupador, Dictionary<string, TabelaColuna<T>> colunas) {
            List<TabelaColuna<T>> colunasAgrupamento = new List<TabelaColuna<T>>();
            foreach (var colunaAgrupador in agrupador.Agrupadores.Values)
            {
                if (!colunas.TryGetValue(colunaAgrupador.Identificador, out var coluna))
                    continue;

                colunasAgrupamento.Add(coluna);
            }
            return colunasAgrupamento;
        }
    }
}
