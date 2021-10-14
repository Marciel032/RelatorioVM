using HtmlTags;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TituloElemento
    {
        public string Texto { get; set; }

        public void AdicionarHtml(HtmlTag parent) {
            if (string.IsNullOrWhiteSpace(Texto))
                return;

            new HtmlTag("h3", parent)
                .Style("text-align", "center")
                .Style("padding", "10px")
                .AddClass("titulo")
                .Text(Texto);
        }
    }
}
