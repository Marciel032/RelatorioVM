using RelatorioVM.Comparadores;
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
        public static IEnumerable<IGrouping<IDictionary<string, object>, T>> AgruparConteudo<T>(this TabelaAgrupador agrupador, IEnumerable<T> conteudo, Dictionary<string, TabelaColuna<T>> colunas)
        {
            List<TabelaColuna<T>> colunasAgrupamento = new List<TabelaColuna<T>>();
            foreach (var colunaAgrupador in agrupador.Agrupadores.Values)
            {
                if (!colunas.TryGetValue(colunaAgrupador.Identificador, out var coluna))
                    continue;

                colunasAgrupamento.Add(coluna);
            }

            Func<T, IDictionary<string, object>> agrupamentoFuncao = (item) =>
            {
                dynamic objetoAgrupamento = new ExpandoObject();
                colunasAgrupamento.ForEach(x => ((IDictionary<string, object>)objetoAgrupamento).Add(x.Identificador, x.Propriedade.ObterValor(item)));
                return objetoAgrupamento;
            };

            if (colunasAgrupamento.Count > 0) {
                IOrderedEnumerable<T> conteudoOrdenado = conteudo.OrderBy(x => colunasAgrupamento[0].Propriedade.ObterValor(x));
                for (int i = 1; i < colunasAgrupamento.Count; i++)
                {
                    var colunaAgrupamento = colunasAgrupamento[i];
                    conteudoOrdenado = conteudoOrdenado.ThenBy(x => colunaAgrupamento.Propriedade.ObterValor(x));
                }

                return conteudoOrdenado.GroupBy(agrupamentoFuncao, new DicionarioComparador());
            }

            return conteudo.GroupBy(agrupamentoFuncao, new DicionarioComparador());
        }
    }
}
