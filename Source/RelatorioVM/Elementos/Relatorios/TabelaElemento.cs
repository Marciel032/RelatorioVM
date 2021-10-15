using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Elementos.Interfaces;
using RelatorioVM.Extensoes;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaElemento<T>: IElemento
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        public List<TabelaColuna> Colunas { get; set; }
        public IEnumerable<T> Conteudos { get; set; }

        public TabelaElemento(ConfiguracaoRelatorio configuracaoRelatorio, List<TabelaColuna> colunas, IEnumerable<T> conteudos)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            Colunas = colunas;
            Conteudos = conteudos;
        }

        public bool ProcessarHtml(HtmlTag pai) {            
            var tabela = CriarTabela(pai);
            AdicionarCabecalho(tabela);
            AdicionarConteudo(tabela);
            return true;
        }

        private HtmlTag CriarTabela(HtmlTag pai) { 
            return pai
                .CriarTabela()
                .Style("width", "100%")
                .Style("font-family", "courier new")
                //.Style("margin-top", "20px")
                .Style("margin-bottom", "10px")
                .Style("border-top", "1px solid #888");
        }

        private void AdicionarCabecalho(HtmlTag tabela) {
            var cabecalho = new HtmlTag("thead", tabela)
                    .Style("display", "table-header-group");

            var linhaCabecalho = new HtmlTag("tr", cabecalho)
                   .Style("border-bottom", "1px solid #000");

            foreach (var coluna in Colunas)
            {
                new HtmlTag("th", linhaCabecalho)
                     .Style("text-align", "left")
                     .Text(coluna.Titulo);
            }
        }

        private void AdicionarConteudo(HtmlTag tabela) {
            foreach (var conteudo in Conteudos) {
                var linha = new HtmlTag("tr", tabela);
                foreach (var coluna in Colunas) {
                    new HtmlTag("td", linha)
                        .Style("text-align", "left")
                        .Text(coluna.Propriedade.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                }
            }
        }
    }
}
