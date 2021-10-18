using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Estruturas;
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

        public string GerarHtml() {
            var estilo = new HtmlTag("style")
                .AppendHtml(
                   @"* { font-family: courier new;}
                    .tr-totais td { border-top: 1px solid #888; font-weight: bold; }
                    table { 
                        page-break-inside:auto;
                        border-collapse: collapse;
                        margin-top: 10px;
                    } 
                    tr { page-break-inside:avoid; page-break-after:auto } 
                    .keep-together { page-break-inside:avoid; page-break-after:auto } 
                    thead { display:table-header-group } 
                    tfoot { display:table-footer-group } 
                    .page-break  { page-break-before: always; }
                    .titulo {
                        display: block; 
                        text-align: center; 
                        margin: 0px;
                        padding-top: 20px;                        
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

            return relatorio.ToHtmlString();
        }
    }
}
