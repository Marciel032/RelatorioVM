using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal class ConversorCor : IConversorValor
    {
        public string Converter(object valor, ConfiguracaoFormatacaoRelatorio opcoes)
        {
            if (valor is Color)
                return string.Empty;

            throw new ArgumentException("Valor não é uma cor");
        }
    }
}
