﻿using RelatorioVM.Elementos.Propriedades;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class Filtro<T>
    {
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string Separador { get; set; }
        
        public Propriedade<T> Propriedade { get; set; }
        public Propriedade<T> PropriedadeComplemento { get; set; }

        public Filtro()
        {
            Identificador = string.Empty;
            Nome = string.Empty;
            Separador = "-";
        }
    }
}
