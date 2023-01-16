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
.titulo {
    display: block; 
    text-align: center; 
    margin: 0px;
    padding-top: 20px;                        
    position: running(titulo);
}                    
table.tabela-conteudo, table.tabela-conteudo-vertical
{
    width: 100%;
}   
.tabela-conteudo .tr-cabecalho {
    border: 1px solid #777;
}
.tabela-conteudo .td-valor-complemento {
    text-align: right;
    padding-right: 1px;
}
.tabela-conteudo .td-complemento {
    text-align: left;
    padding-left: 1px;
}
.tabela-conteudo-vertical .tr-cabecalho {
    border-bottom: 1px solid #777;
}
.tabela-conteudo-vertical td,th {
    padding-left: 3px;
    padding-right: 3px;
    text-align: left;
}
.tabela-conteudo td,th {
    padding-left: 3px;
    padding-right: 3px;
    text-align: left;
}
.tabela-conteudo .tr-zebra, .tabela-conteudo-vertical .tr-zebra td {
    background-color: #f2f2f2;
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

            //construtorEstilo.Append($".titulo {{ font-family: {_configuracao.Formatacao.Fonte.Nome.ObterDescricao()};}}");
            //construtorEstilo.Append($".tabela-filtro {{ font-family: {_configuracao.Formatacao.Fonte.Nome.ObterDescricao()};}}");
            //construtorEstilo.Append($".tabela-conteudo {{ font-family: {_configuracao.Formatacao.Fonte.Nome.ObterDescricao()};}}");
            //construtorEstilo.Append($".tabela-conteudo-vertical {{ font-family: {_configuracao.Formatacao.Fonte.Nome.ObterDescricao()};}}");

            construtorEstilo.Append(FormatarCSS(".titulo", _configuracao.Formatacao.FonteTitulo));
            construtorEstilo.Append(FormatarCSS(".tabela-filtro", _configuracao.Formatacao.FonteConteudo));
            construtorEstilo.Append(FormatarCSS(".tabela-conteudo", _configuracao.Formatacao.FonteConteudo));
            construtorEstilo.Append(FormatarCSS(".tabela-conteudo-vertical", _configuracao.Formatacao.FonteConteudo));

            return construtorEstilo.ToString().Replace(Environment.NewLine, " ");
        }

        private string FormatarCSS(string classe, FonteEscrita fonte)
        {
            StringBuilder cssFormatado = new StringBuilder();

            if (fonte != null)
                cssFormatado.Append(FormatarFonte(classe, fonte.Nome.ObterDescricao(), fonte.Tamanho, fonte.Italico, fonte.Negrito));

            //if (paddings != null)
            //    cssFormatado += FormatarPaddings();

            return cssFormatado.ToString();
        }

        private string FormatarFonte(string classe, string nome, int tamanho, bool italico, bool negrito)
        {
            StringBuilder fonteFormatada = new StringBuilder();
            fonteFormatada.Append($"{classe} ");

            
            fonteFormatada.Append("{");

            if (nome != string.Empty)
                fonteFormatada.Append($"font-family: {nome};");


            if (tamanho > 0)
                fonteFormatada.Append($"font-size: {tamanho}px;");


            if (italico)
                fonteFormatada.Append($"font-style: italic;");


            if (negrito)
                fonteFormatada.Append($"font-weight: bold;");

            fonteFormatada.Append("}");

            return fonteFormatada.ToString();
        }
    }
}
