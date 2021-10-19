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
table.tabela-conteudo {
    width: 100%;
}   
.tabela-conteudo .tr-cabecalho {
    border: 1px solid #777;
}
.tabela-conteudo td,th {
    padding-left: 3px;
    padding-right: 3px;
    text-align: left;
}
.tabela-conteudo .tr-totais-titulo td { 
    font-weight: bold; 
}
.tabela-conteudo .tr-totais td { 
    border-top: 1px solid #888; 
    font-weight: bold; 
}
.tabela-conteudo .tr-grupo-titulo td { 
    border-bottom: 1px solid #888; 
    font-weight: bold; 
}
@page {
    @top-center { content: element(titulo) }
}".Replace(Environment.NewLine, " "));

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
