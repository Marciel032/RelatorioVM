using DinkToPdf;

namespace RelatorioVM.ConversoresPdf
{
    internal class ConversorPdfBasico : ConversorPdf
    {
        public ConversorPdfBasico() : base(new BasicConverter(new PdfTools()))
        {
        }
    }
}
