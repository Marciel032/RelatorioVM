using HtmlTags;
using RelatorioVM.Elementos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TituloElemento: IElemento
    {
        public string Texto { get; set; }

        public bool ProcessarHtml(HtmlTag pai) {
            if (string.IsNullOrWhiteSpace(Texto))
                return false;

            new HtmlTag("h3", pai)                
                .AddClass("titulo")
                .Text(Texto);

            return true;
        }
    }
}
