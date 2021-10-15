using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Conversores.Interfaces
{
    public interface IConversorValor
    {
        string Converter(object valor, OpcoesFormatacao opcoes);
    }
}
