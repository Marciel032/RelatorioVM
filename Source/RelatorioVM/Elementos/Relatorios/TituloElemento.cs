using HtmlTags;
using RelatorioVM.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TituloElemento: IElementoRelatorioVM
    {
        public string Texto { get; set; }

        public string ObterHtml() {
            if (string.IsNullOrWhiteSpace(Texto))
                return string.Empty;

            var titulo = new HtmlTag("h3")                
                .AddClass("titulo")
                .Text(Texto);

            return titulo.ToHtmlString();
        }
    }
}
