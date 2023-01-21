using RelatorioVM.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Demo.ComponentesCustomizados
{
    public class ComponenteCustomizado : IElementoRelatorioVM
    {
        public string Indice { get; set; }

        public string ObterEstilo()
        {
            return $".componente-customizado{Indice} {{ color: orange; text-align: center;}}";
        }

        public string ObterHtml(object conteudo)
        {
            return $"<h1 class=\"componente-customizado{Indice}\">Testes de componente customizado</h1>";
        }
    }
}
