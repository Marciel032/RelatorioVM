using RelatorioVM.Elementos.Propriedades;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class Filtro
    {
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }   
        public string ValorComplemento { get; set; }
        public string Separador { get; set; }
        public Propriedade Propriedade { get; set; }
        public Propriedade PropriedadeComplemento { get; set; }

        public Filtro()
        {
            Identificador = string.Empty;
            Nome = string.Empty;
            Valor = string.Empty;
            ValorComplemento = string.Empty;
            Separador = "-";
        }
    }
}
