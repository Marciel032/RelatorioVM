﻿using HtmlTags;
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
        public static void CalcularTotais<T>(this TabelaAgrupador<T> agrupador, T conteudo)
        {
            if (!agrupador.Totalizar)
                return;

            agrupador.Totais.CalcularTotais(conteudo);
        }

        public static void AdicionarTotaisHtml<T>(this TabelaAgrupador<T> agrupador, HtmlTag tabelaHtml, Tabela<T> tabela, OpcoesFormatacao formatacao)
        {
            if (!agrupador.Totalizar)
                return;

            agrupador.Totais.AdicionarTotaisHtml(tabelaHtml, tabela, formatacao);
        }

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

        public static void AdicionarCabecalhoAgrupamento<T>(this TabelaAgrupador<T> agrupador, HtmlTag tabelaHtml, T item, int quantidadeDeColunas)
        {
            var titulo = agrupador.ObterTituloAgrupamento(item);
            tabelaHtml.CriarLinhaTabela()
                .Style("font-weight", "bold")
                .Style("border-bottom", "1px solid #888")
                .CriarColunaTabela()
                .Attr("colspan", quantidadeDeColunas)
                .Text(titulo);

            agrupador.AjustarTituloComplementoTotais(titulo);
        }

        public static string ObterTituloAgrupamento<T>(this TabelaAgrupador<T> agrupador, T item) {
            var textoCabecaho = string.Empty;
            foreach (var coluna in agrupador.Colunas)
                textoCabecaho += $"{coluna.Titulo}:{coluna.Propriedade.ObterValorConvertido(item, agrupador.ObterOpcoesFormatacao())}   ";
            return textoCabecaho;
        }

        public static void AjustarTituloComplementoTotais<T>(this TabelaAgrupador<T> agrupador, string titulo)
        {
            foreach (var total in agrupador.Totais)
                total.TituloComplemento = titulo;

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
