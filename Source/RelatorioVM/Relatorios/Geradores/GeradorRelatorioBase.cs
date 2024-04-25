using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Configuracoes.Formatacoes;
using RelatorioVM.Dominio.Enumeradores;
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
    margin-top: 10px;
} 
table { 
    border-collapse: collapse;
    page-break-inside:auto;
} 
tr { 
    page-break-inside:auto; 
    page-break-after:auto; 
} 
thead { 
    display:table-header-group; 
} 
tfoot { 
    display:table-footer-group; 
} 
.page-break { 
    page-break-before:always; 
}
.page-break-after { 
    page-break-after:always;
}
hr
{
    margin-top: 20px;
    margin-bottom: 20px;
}
.paginas::before {
    content: counter(page);
}
.paginas::after {
    content: counter(pages);
}
table {
    border-spacing: 0px;
    border-collapse: collapse;
}
@media screen {
   .somente-impressao {
       display: none;
   }
}");

            construtorEstilo.Append(_configuracao.Formatacao.FonteTitulo.ObterEstilo("titulo"));
            construtorEstilo.Append(_estrutura.ObterEstilo());
            construtorEstilo.Append(GerarEstiloPagina());

            return construtorEstilo.ToString().Replace(Environment.NewLine, " ");
        }

        private string GerarEstiloPagina() {
            StringBuilder construtorEstilo = new StringBuilder();

            construtorEstilo.Append("@page {")
                .Append("@top-left { content: element(").Append(TipoPosicaoCabecalhoRodape.CabecalhoEsquerdo.ObterDescricao()).Append(")}")
                .Append("@top-center { content: element(").Append(TipoPosicaoCabecalhoRodape.CabecalhoCentro.ObterDescricao()).Append(")}")
                .Append("@top-right { content: element(").Append(TipoPosicaoCabecalhoRodape.CabecalhoDireito.ObterDescricao()).Append(")}")
                .Append("@bottom-left { content: element(").Append(TipoPosicaoCabecalhoRodape.RodapeEsquerdo.ObterDescricao()).Append(")}")
                .Append("@bottom-center { content: element(").Append(TipoPosicaoCabecalhoRodape.RodapeCentro.ObterDescricao()).Append(")}")
                .Append("@bottom-right { content: element(").Append(TipoPosicaoCabecalhoRodape.RodapeDireito.ObterDescricao()).Append(")}")
                //.Append(".").Append(TipoPosicaoCabecalhoRodape.RodapeDireito.ObterDescricao()).Append("{ display:block }")
                .Append("}");

            //construtorEstilo.Append(".").Append(TipoPosicaoCabecalhoRodape.RodapeDireito.ObterDescricao()).Append("{ display:none }");

            return construtorEstilo.ToString();
        }
    }
}
