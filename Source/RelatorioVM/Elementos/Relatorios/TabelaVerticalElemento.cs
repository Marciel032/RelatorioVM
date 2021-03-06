using HtmlTags;
using RelatorioVM.Comparadores;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Extensoes;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaVerticalElemento<T>: IElementoRelatorioVM
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        private Tabela<T> _tabela;

        public TabelaVerticalElemento(ConfiguracaoRelatorio configuracaoRelatorio, Tabela<T> tabela)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = tabela;
        }

        public string ObterHtml() {            
            var tabela = CriarTabela();
            AdicionarCabecalho(tabela);
            var corpoTabela = tabela.CriarCorpoTabela();
            AdicionarConteudo(corpoTabela);
            return tabela.ToHtmlString();
        }

        private HtmlTag CriarTabela() { 
            return new HtmlTag("table")
                .AddClass("tabela-conteudo-vertical");
        }

        private void AdicionarCabecalho(HtmlTag tabela) {
            var cabecalho = tabela.CriarCabecalhoTabela();

            AdicionarTitulo(cabecalho);
        }

        private void AdicionarConteudo(HtmlTag corpoTabela) {
            foreach (var conteudo in _tabela.Conteudo)
                AdicionarConteudoColunas(corpoTabela, conteudo);
        }

        private void AdicionarConteudoColunas(HtmlTag corpoTabela, T conteudo)
        {
            bool zebra = false;            
            foreach (var coluna in _tabela.ObterColunasVisiveis())
            {
                var linha = corpoTabela.CriarLinhaTabela();
                if (zebra && _configuracaoRelatorio.Conteudo.Zebrado)
                    linha.AddClass("tr-zebra");

                linha.CriarColunaTabela()
                    .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Direita)
                    .Text($"{coluna.Titulo}:")
                    .AddClass("td-titulo");                   

                linha.CriarColunaTabela()
                    .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Esquerda)                    
                    .Text(coluna.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                zebra = !zebra;
            }           
        }

        private void AdicionarTitulo(HtmlTag cabecalho) {
            if (string.IsNullOrWhiteSpace(_tabela.Titulo))
                return;

            cabecalho
                .CriarLinhaTabela()
                .CriarColunaCabecalhoTabela()
                .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Esquerda)
                .ExpandirColuna(2)
                .Text(_tabela.Titulo)
                .AddClass("tr-cabecalho");
        }        
    }    
}
