using DinkToPdf;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Estruturas;
using RelatorioVM.Relatorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Geradores
{
    internal class GeradorRelatorioBase: IGeradorRelatorioVM
    {
        private EstruturaRelatorio _estrutura;
        private ConfiguracaoRelatorio _configuracao;

        public GeradorRelatorioBase(EstruturaRelatorio estrutura, ConfiguracaoRelatorio configuracao)
        {
            _estrutura = estrutura;
            _configuracao = configuracao;
        }

        public byte[] Gerar()
        {
            var relatorio = @"<html>
        <head>
        <style>
            * { font-family: arial;}
            .tr-totais td { border-top: 1px solid #888; font-weight: bold; }
            table { page-break-inside:auto } 
            tr { page-break-inside:avoid; page-break-after:auto } 
            .keep-together { page-break-inside:avoid; page-break-after:auto } 
            thead { display:table-header-group } 
            tfoot { display:table-footer-group } 
            .page-break  { page-break-before: always; }
        .titulo {
            display: block; text-align: center; 
            position: running(titulo);
        } 
        @page {
            @top-center { content: element(titulo) }
        }
        </style>
        </head>
        <body>";

            relatorio += "</body></html>";
            
            var documento = new ObjectSettings()
            {
                HtmlContent = relatorio
            };
            documento.Configurar(_configuracao);

            var pdf = new HtmlToPdfDocument();
            pdf.Configurar(_configuracao);
            pdf.Objects.Add(documento);

            return new SynchronizedConverter(new PdfTools()).Convert(pdf);
        }
    }
}
