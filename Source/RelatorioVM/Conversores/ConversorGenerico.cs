using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal class ConversorGenerico : IConversorValor
    {
        public string Converter(object valor, OpcoesFormatacao opcoes)
        {
            if (valor == null)
                return "";

            return valor.ToString();
        }
    }
}
