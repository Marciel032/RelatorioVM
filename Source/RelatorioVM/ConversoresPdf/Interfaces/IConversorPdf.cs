using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.ConversoresPdf.Interfaces
{
    internal interface IConversorPdf
    {
        byte[] Converter(IDocument documento);
    }
}
