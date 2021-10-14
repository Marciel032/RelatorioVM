using DinkToPdf.Contracts;
using RelatorioVM.Conversores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Conversores
{
    internal abstract class Conversor : IConversor
    {
        protected IConverter _conversor;

        public Conversor(IConverter conversor)
        {
            _conversor = conversor;
        }

        public virtual byte[] Converter(IDocument documento)
        {
            return _conversor.Convert(documento);
        }
    }
}
