using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Interfaces
{
    public interface IElementoRelatorioVM
    {
        string ObterHtml();
        string ObterEstilo();
        void DefinirIndiceElemento(int indice);
    }
}
