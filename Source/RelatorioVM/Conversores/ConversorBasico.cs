using DinkToPdf;
using DinkToPdf.Contracts;
using RelatorioVM.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal class ConversorBasico : Conversor
    {
        public ConversorBasico() : base(new BasicConverter(new PdfTools()))
        {
        }
    }
}
