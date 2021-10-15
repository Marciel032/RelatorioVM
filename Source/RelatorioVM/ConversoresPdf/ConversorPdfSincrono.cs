using DinkToPdf;

namespace RelatorioVM.ConversoresPdf
{
    internal class ConversorPdfSincrono : ConversorPdf
    {
        public ConversorPdfSincrono() : base(new SynchronizedConverter(new PdfTools()))
        {

        }
    }
}
