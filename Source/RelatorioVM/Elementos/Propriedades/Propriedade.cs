﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Propriedades
{
    internal class Propriedade
    {
        public PropertyInfo PropriedadeInformacao { get; set; }

        public Func<object> FuncaoPropriedade { get; set; }

        public object ObterValor(object origem) {
            if (FuncaoPropriedade != null)
                return FuncaoPropriedade();

            return PropriedadeInformacao.GetValue(origem);
        }
    }
}