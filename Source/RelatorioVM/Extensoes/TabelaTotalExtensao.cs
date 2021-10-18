﻿using HtmlTags;
using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Elementos.Relatorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class TabelaTotalExtensao
    {
        public static void ZerarTotais<T>(this List<TabelaTotal<T>> totais) {
            foreach (var total in totais)
                foreach (var colunaTotal in total.Totais)
                    colunaTotal.Value.Zerar();
        }

        public static void CalcularTotais<T>(this List<TabelaTotal<T>> totais, T conteudo)
        {
            foreach (var total in totais)
                foreach (var colunaTotal in total.Totais)
                    colunaTotal.Value.Calcular(conteudo);
        }

        public static void AdicionarTotaisHtml<T>(this List<TabelaTotal<T>> totais, HtmlTag tabelaHtml, Tabela<T> tabela, OpcoesFormatacao formatacao)
        {
            if (tabela.ObterQuantidadeColunasVisiveis() == 0)
                return;

            if (totais.Count == 0)
                return;

            //var rodapeTabela = tabela.CriarRodapeTabela();            

            foreach (var total in totais)
            {
                var titulo = total.ObterTituloCompleto();
                if (!string.IsNullOrWhiteSpace(titulo))
                {
                    tabelaHtml.CriarLinhaTabela()
                        .Style("font-weight", "bold")
                        .CriarColunaTabela()
                        .Text(titulo)
                        .Attr("colspan", tabela.ObterQuantidadeColunasVisiveis());
                }

                var linhaTotal = tabelaHtml.CriarLinhaTabela()
                    .Style("font-weight", "bold")
                    .Style("border-top", "1px solid #888");

                foreach (var coluna in tabela.ObterColunasVisiveis())
                {
                    if (total.Totais.ContainsKey(coluna.Identificador))
                    {
                        var totalColuna = total.Totais[coluna.Identificador];
                        linhaTotal.CriarColunaTabela()
                            .Style("text-align", coluna.AlinhamentoHorizontal.ObterDescricao())
                            .Style("padding-left", "3px")
                            .Style("padding-right", "3px")
                            .Text(totalColuna.ObterValorConvertido(formatacao));
                    }
                    else
                        linhaTotal
                            .CriarColunaTabela();
                }
            }
        }
    }
}
