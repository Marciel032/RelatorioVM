using HtmlTags;
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

        public static void AdicionarTotaisHtml<T>(this List<TabelaTotal<T>> totais, HtmlTag tabelaHtml, Tabela<T> tabela, ConfiguracaoFormatacaoRelatorio formatacao)
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
                        .AddClass("tr-t-t")
                        .CriarColunaTabela()
                        .ExpandirColuna(tabela.ObterQuantidadeColunasVisiveis())
                        .Text(titulo);
                }

                HtmlTag linhaTituloTotal = null;

                if (total.TemTituloColuna) {
                    linhaTituloTotal = tabelaHtml.CriarLinhaTabela()
                        .AddClass("tr-t");
                }

                var linhaTotal = tabelaHtml.CriarLinhaTabela()
                    .AddClass("tr-t");

                for (int i = 0; i < tabela.QuantidadeFracionamentoDados; i++)
                {
                    foreach (var coluna in tabela.ObterColunasVisiveis())
                    {
                        if (total.Totais.ContainsKey(coluna.Identificador) && i == tabela.QuantidadeFracionamentoDados - 1)
                        {
                            var totalColuna = total.Totais[coluna.Identificador];

                            if (linhaTituloTotal != null)
                            {
                                linhaTituloTotal.CriarColunaTabela()
                                    .AddClass("td-t-t")
                                    .DefinirAlinhamentoHorizontal(coluna.AlinhamentoHorizontalTitulo)
                                    .ExpandirColuna(coluna.QuantidadeColunasUtilizadas)
                                    .Text(totalColuna.TituloColuna);
                            }

                            var valor = totalColuna.ObterValorConvertido(formatacao);
                            if (coluna.TemPrefixo)
                                valor = $"{coluna.Prefixo} {valor}";
                            linhaTotal.CriarColunaTabela()
                                .DefinirAlinhamentoHorizontal(coluna.AlinhamentoHorizontalColuna)
                                .ExpandirColuna(coluna.QuantidadeColunasUtilizadas)
                                .Text(valor);
                        }
                        else
                        {
                            if (linhaTituloTotal != null)
                                linhaTituloTotal.CriarColunaTabela()
                                    .ExpandirColuna(coluna.QuantidadeColunasUtilizadas);

                            linhaTotal
                                .CriarColunaTabela()
                                .ExpandirColuna(coluna.QuantidadeColunasUtilizadas);
                        }
                    }
                }                
            }
        }
    }
}
