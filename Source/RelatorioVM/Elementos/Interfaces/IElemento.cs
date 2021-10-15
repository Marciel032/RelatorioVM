using HtmlTags;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Interfaces
{
    internal interface IElemento
    {
        bool ProcessarHtml(HtmlTag pai);
    }
}
