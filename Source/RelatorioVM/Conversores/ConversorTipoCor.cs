using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Conversores.Interfaces;
using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal class ConversorTipoCor : IConversorValor
    {
        public string Converter(object valor, ConfiguracaoFormatacaoRelatorio opcoes)
        {
            if (valor is TipoCor)
                return string.Empty;

            throw new ArgumentException("Valor não é um tipo de cor");
        }
    }
}
