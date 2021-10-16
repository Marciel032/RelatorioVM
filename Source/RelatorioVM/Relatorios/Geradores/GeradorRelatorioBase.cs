﻿using DinkToPdf;
using HtmlTags;
using RelatorioVM.ConversoresPdf.Interfaces;
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
        private IConversorPdf _conversor;

        public GeradorRelatorioBase(EstruturaRelatorio estrutura, ConfiguracaoRelatorio configuracao, IConversorPdf conversor)
        {
            _estrutura = estrutura;
            _configuracao = configuracao;
            _conversor = conversor;
        }

        public byte[] Gerar()
        {
            var estilo = new HtmlTag("style")
                .AppendHtml(
                   @"* { font-family: arial;}.tr-totais td { border-top: 1px solid #888; font-weight: bold; }
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
                    }"
                );

            var relatorio = new HtmlTag("html")
                .Append(
                    new HtmlTag("head")
                        .Append(estilo)
                );

            var corpo = new HtmlTag("body", relatorio);
            _estrutura.AdicionarHtml(corpo);
            
            var documento = new ObjectSettings()
            {
                HtmlContent = relatorio.ToHtmlString()
            };
            documento.Configurar(_configuracao);

            var pdf = new HtmlToPdfDocument();
            pdf.Configurar(_configuracao);
            pdf.Objects.Add(documento);

            return _conversor.Converter(pdf);
        }
    }
}