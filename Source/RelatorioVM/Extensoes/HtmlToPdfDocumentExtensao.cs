using DinkToPdf;
using RelatorioVM.Dominio.Configuracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Extensoes
{
    internal static class HtmlToPdfDocumentExtensao
    {
        public static void Configurar(this HtmlToPdfDocument pdf, ConfiguracaoRelatorio configuracao) {
            var configuracaoGlobal = pdf.GlobalSettings;

            configuracaoGlobal.ColorMode = ColorMode.Color;
            configuracaoGlobal.Orientation = configuracao.Orientacao.ParaDinkPDF();
            configuracaoGlobal.PaperSize = PaperKind.A4;
            configuracaoGlobal.Margins.Top = 10;            
        }
    }
}
