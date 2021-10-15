using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal class ConversorDataHora : IConversorValor
    {
        public string Converter(object valor, OpcoesFormatacao opcoes)
        {
            if (valor == null)
                return opcoes.ValorNulavel;

            if (!(valor is DateTime))
                throw new ArgumentException("Valor não é data e hora");

            DateTime dataHora = (DateTime)valor;

            if (dataHora.TimeOfDay.TotalSeconds == 0)
                return dataHora.ToString(opcoes.FormatoData);
            else
                return dataHora.ToString(opcoes.FormatoDataHora);
        }        
    }
}
