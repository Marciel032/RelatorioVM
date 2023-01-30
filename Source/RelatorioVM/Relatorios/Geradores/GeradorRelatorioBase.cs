using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Estruturas;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Geradores
{
    internal class GeradorRelatorioBase : IGeradorRelatorioVM
    {
        private EstruturaRelatorio _estrutura;
        private ConfiguracaoRelatorio _configuracao;

        public GeradorRelatorioBase(EstruturaRelatorio estrutura, ConfiguracaoRelatorio configuracao)
        {
            _estrutura = estrutura;
            _configuracao = configuracao;
        }

        public string GerarHtml()
        {
            var estilo = new HtmlTag("style")
                .AppendHtml(GerarEstilo());


            var relatorio = new HtmlTag("html")
                .Append(
                    new HtmlTag("head")
                        .Append(estilo)
                );

            var corpo = new HtmlTag("body", relatorio);
            _estrutura.AdicionarHtml(corpo);

            return relatorio.ToHtmlString();
        }

        private string GerarEstilo()
        {
            StringBuilder construtorEstilo = new StringBuilder();

            construtorEstilo.Append(@"* 
body{
  -webkit-print-color-adjust:exact;
}
body > table {
    border-collapse: collapse;
    margin-top: 10px;
} 
table { 
    page-break-inside:auto;
} 
tr { page-break-inside:auto; page-break-after:auto } 
.keep-together { page-break-inside:avoid; page-break-after:auto } 
thead { display:table-header-group } 
tfoot { display:table-footer-group } 
.page-break { page-break-before: always; }
.page-break-after { page-break-after: always; }
hr
{
    margin-top: 20px;
    margin-bottom: 20px;
}
table {
    border-spacing: 0px;
    border-collapse: collapse;
}
@page {
    @top-center { content: element(titulo) }
}");

            construtorEstilo.Append(_configuracao.Formatacao.FonteTitulo.ObterEstilo("titulo"));
            construtorEstilo.Append(_estrutura.ObterEstilo());

            return construtorEstilo.ToString().Replace(Environment.NewLine, " ");
        }
    }
}
