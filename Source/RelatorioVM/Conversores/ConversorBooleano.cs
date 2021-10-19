using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal class ConversorBooleano : IConversorValor
    {
        public string Converter(object valor, OpcoesFormatacao opcoes)
        {
            if (valor == null)
                return opcoes.ValorNulavel;

            if (!(valor is bool))
                throw new ArgumentException("Valor não é booleano");

            bool booleano = (bool)valor;

            if (booleano)
                return "Sim";
            else
                return "Não";
        }
    }
}
