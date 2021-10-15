﻿using System;
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
        public PropertyInfo Propriedade { get; set; }
        public PropertyInfo PropriedadeComplemento { get; set; }
    }
}
