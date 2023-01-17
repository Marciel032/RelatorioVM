using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal class ConversorDecimal : IConversorValor
    {
        public string Converter(object valor, ConfiguracaoFormatacaoRelatorio opcoes)
        {
            if (valor == null)
                return opcoes.ValorNulavel;

            if (!(valor is decimal))
                throw new ArgumentException("Valor não é decimal");

            decimal numero = (decimal)valor;

            if (opcoes.CasasDecimais > 0)
                return numero.ToString($"N{opcoes.CasasDecimais}");
            else
                return numero.ToString("N");
        }
    }
}
