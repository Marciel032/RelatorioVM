using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal class ConversorNumerico : IConversorValor
    {
        public string Converter(object valor, ConfiguracaoFormatacaoRelatorio opcoes)
        {
            if (valor == null)
                return opcoes.ValorNulavel;
 
            if(valor is short)
                return ((short)valor).ToString("N0");

            if (valor is int)
                return ((int)valor).ToString("N0");

            if (valor is long)
                return ((long)valor).ToString("N0");

            throw new ArgumentException("Valor não é um numero");
        }
    }
}
