using HtmlTags;
using RelatorioVM.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Complementares
{
    internal class LinhaHorizontalElemento : IElementoRelatorioVM
    {
        public void DefinirIndiceElemento(int indice)
        {
            
        }

        public string ObterEstilo()
        {
            return string.Empty;
        }

        public string ObterHtml()
        {
            return new HtmlTag("hr").ToHtmlString();
        }
    }
}
