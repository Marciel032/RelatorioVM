using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Atributos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColunaRelatorioAttribute: Attribute
    {
        public string Titulo { get; set; } = string.Empty;
        public bool Visivel { get; set; } = true;
    }
}
