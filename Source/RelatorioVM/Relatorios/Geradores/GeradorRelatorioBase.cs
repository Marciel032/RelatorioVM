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
.tabela-conteudo-vertical .td-titulo { 
    font-weight: bold; 
}
hr
{
    margin-top: 20px;
    margin-bottom: 20px;
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
