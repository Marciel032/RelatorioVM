﻿using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Propriedades
{
    internal class Propriedade<T>
    {        
        public PropertyInfo PropriedadeInformacao { get; set; }

        public Func<T,object> FuncaoPropriedade { get; set; }

        public object ObterValor(T origem) {
            if (FuncaoPropriedade != null)
                return FuncaoPropriedade(origem);

            return PropriedadeInformacao.GetValue(origem);
        }

        public string ObterValorConvertido(T origem, ConfiguracaoFormatacaoRelatorio formato) {
            return ObterValor(origem).ObterValorConvertido(formato, PropriedadeInformacao.PropertyType);
        }

        public Propriedade(PropertyInfo propriedadeInformacao)
        {
            PropriedadeInformacao = propriedadeInformacao;
        }
    }
}
