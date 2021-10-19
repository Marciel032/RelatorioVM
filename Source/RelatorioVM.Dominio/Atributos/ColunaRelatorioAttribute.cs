using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Atributos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColunaRelatorioAttribute: Attribute
    {
        public string Nome { get; set; } = string.Empty;
        public bool Ignorar { get; set; } = false;
    }
}
