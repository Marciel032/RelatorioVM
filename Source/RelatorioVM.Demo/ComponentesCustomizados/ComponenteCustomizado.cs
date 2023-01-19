using RelatorioVM.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Demo.ComponentesCustomizados
{
    public class ComponenteCustomizado : IElementoRelatorioVM
    {
        private int indiceElemento;

        public void DefinirIndiceElemento(int indice)
        {
            indiceElemento = indice;
        }

        public string ObterEstilo()
        {
            return $".componente-customizado{indiceElemento} {{ color: orange; text-align: center;}}";
        }

        public string ObterHtml()
        {
            return $"<h1 class=\"componente-customizado{indiceElemento}\">Testes de componente customizado</h1>";
        }
    }
}
