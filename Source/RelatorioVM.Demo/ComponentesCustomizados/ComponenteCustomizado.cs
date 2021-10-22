using RelatorioVM.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Demo.ComponentesCustomizados
{
    public class ComponenteCustomizado : IElementoRelatorioVM
    {
        public string ObterHtml()
        {
            return "<h1 style=\"color: orange; text-align: center;\">Testes de componente customizado</h1>";
        }
    }
}
