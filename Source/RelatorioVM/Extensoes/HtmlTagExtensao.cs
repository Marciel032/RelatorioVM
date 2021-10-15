using HtmlTags;
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

        public static HtmlTag CriarLinhaTabela(this HtmlTag tag)
        {
            return new HtmlTag("tr", tag);
        }

        public static HtmlTag CriarColunaTabela(this HtmlTag tag)
        {
            return new HtmlTag("td", tag);
        }
    }
}
