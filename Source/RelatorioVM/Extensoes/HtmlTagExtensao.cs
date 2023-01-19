using HtmlTags;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class HtmlTagExtensao
    {
        public static HtmlTag CriarTabela(this HtmlTag tag) {
            return new HtmlTag("table", tag);
        }

        public static HtmlTag CriarCabecalhoTabela(this HtmlTag tag)
        {
            return new HtmlTag("thead", tag);
        }

        public static HtmlTag CriarRodapeTabela(this HtmlTag tag)
        {
            return new HtmlTag("tfoot", tag);
        }

        public static HtmlTag CriarCorpoTabela(this HtmlTag tag)
        {
            return new HtmlTag("tbody", tag);
        }

        public static HtmlTag CriarLinhaTabela(this HtmlTag tag)
        {
            return new HtmlTag("tr", tag);
        }

        public static HtmlTag CriarColunaTabela(this HtmlTag tag)
        {
            return new HtmlTag("td", tag);
        }

        public static HtmlTag CriarColunaCabecalhoTabela(this HtmlTag tag)
        {
            return new HtmlTag("th", tag);
        }

        public static HtmlTag DefinirAlinhamentoHorizontal(this HtmlTag tag, TipoAlinhamentoHorizontal alinhamento)
        {
            if (alinhamento != TipoAlinhamentoHorizontal.Esquerda)
                tag.Style("text-align", alinhamento.ObterDescricao());

            return tag;
        }

        public static HtmlTag ExpandirColuna(this HtmlTag tag, int colunas)
        {
            if (colunas > 1)
                tag.Attr("colspan", colunas);

            return tag;
        }
    }
}
