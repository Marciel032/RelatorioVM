using DinkToPdf.Contracts;
using RelatorioVM.ConversoresPdf.Interfaces;

namespace RelatorioVM.ConversoresPdf
{
    internal abstract class ConversorPdf : IConversorPdf
    {
        protected IConverter _conversor;

        public ConversorPdf(IConverter conversor)
        {
            _conversor = conversor;
        }

        public virtual byte[] Converter(IDocument documento)
        {
            return _conversor.Convert(documento);
        }
    }
}
