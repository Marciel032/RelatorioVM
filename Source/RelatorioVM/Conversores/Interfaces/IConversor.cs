using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Conversores.Interfaces
{
    internal interface IConversor
    {
        byte[] Converter(IDocument documento);
    }
}
