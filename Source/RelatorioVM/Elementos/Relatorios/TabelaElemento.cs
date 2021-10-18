using HtmlTags;
using RelatorioVM.Comparadores;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Interfaces;
using RelatorioVM.Extensoes;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaElemento<T>: IElemento
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        private Tabela<T> _tabela;

        public TabelaElemento(ConfiguracaoRelatorio configuracaoRelatorio, Tabela<T> tabela)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = tabela;
        }

        public bool ProcessarHtml(HtmlTag pai) {            
            var tabela = CriarTabela(pai);
            AdicionarCabecalho(tabela);
            AdicionarConteudo(tabela);
            AdicionarTotais(tabela);
            return true;
        }

        private HtmlTag CriarTabela(HtmlTag pai) { 
            return pai
                .CriarTabela()
                .Style("width", "100%")
                .Style("font-family", "courier new");
        }

        private void AdicionarCabecalho(HtmlTag tabela) {
            var cabecalho = tabela.CriarCabecalhoTabela()
                    .Style("display", "table-header-group");

            AdicionarTitulo(cabecalho);

            var linhaCabecalho = cabecalho
                .CriarLinhaTabela()
                .Style("border", "1px solid #777");

            foreach (var coluna in _tabela.ObterColunasVisiveis())
            {
                linhaCabecalho.CriarColunaCabecalhoTabela()
                     .Style("text-align", coluna.AlinhamentoHorizontal.ObterDescricao())
                     .Style("padding-left", "3px")
                     .Style("padding-right", "3px")
                     .Text(coluna.Titulo);
            }
        }

        private void AdicionarConteudo(HtmlTag tabela) {
            foreach (var total in _tabela.Totais)
                foreach (var colunaTotal in total.Totais)
                    colunaTotal.Value.Zerar();

            var corpoTabela = tabela.CriarCorpoTabela();
            if (_tabela.Agrupadores.Count == 0)
                AdicionarConteudoItens(corpoTabela, _tabela.Conteudo);
            else
                AdicionarConteudoAgrupado(corpoTabela, _tabela.Conteudo, _tabela.Agrupadores.ToList());
        }

        private void AdicionarConteudoAgrupado(HtmlTag corpoTabela, IEnumerable<T> conteudo, List<TabelaAgrupador<T>> agrupadores) {
            if (agrupadores.Count == 0)
                return;

            var agrupador = agrupadores[0];
            agrupadores.RemoveAt(0);

            var itens = agrupador.AgruparConteudo(conteudo);
            foreach (var item in itens) {         
                corpoTabela.Append(agrupador.CriarCabecalhoAgrupamento(item.First(), _tabela.ObterQuantidadeColunasVisiveis()));
                if (agrupadores.Count > 0)
                    AdicionarConteudoAgrupado(corpoTabela, item, agrupadores.ToList());
                else
                    AdicionarConteudoItens(corpoTabela, item);
            }
        }        

        private void AdicionarConteudoItens(HtmlTag corpoTabela, IEnumerable<T> itens) {
            foreach (var conteudo in itens)
            {
                var linha = corpoTabela.CriarLinhaTabela();
                foreach (var coluna in _tabela.ObterColunasVisiveis())
                {
                    linha.CriarColunaTabela()
                        .Style("text-align", coluna.AlinhamentoHorizontal.ObterDescricao())
                        .Style("padding-left", "3px")
                        .Style("padding-right", "3px")
                        .Text(coluna.Propriedade.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                }

                foreach (var total in _tabela.Totais)
                    foreach (var colunaTotal in total.Totais)
                        colunaTotal.Value.Calcular(conteudo);
            }
        }

        private void AdicionarTotais(HtmlTag tabela)
        {
            if (_tabela.ObterQuantidadeColunasVisiveis() == 0)
                return;

            if (_tabela.Totais.Count == 0)
                return;

            //var rodapeTabela = tabela.CriarRodapeTabela();            

            foreach (var total in _tabela.Totais)
            {               
                if (!string.IsNullOrWhiteSpace(total.Titulo))
                {
                    tabela.CriarLinhaTabela()
                        .Style("font-weight", "bold")
                        .CriarColunaTabela()
                        .Text(total.Titulo)                                                
                        .Attr("colspan", _tabela.ObterQuantidadeColunasVisiveis());
                }

                var linhaTotal = tabela.CriarLinhaTabela()                    
                    .Style("font-weight", "bold")
                    .Style("border-top", "1px solid #888");

                foreach (var coluna in _tabela.ObterColunasVisiveis())
                {
                    if (total.Totais.ContainsKey(coluna.Identificador))
                    {
                        var totalColuna = total.Totais[coluna.Identificador];
                        linhaTotal.CriarColunaTabela()
                            .Style("text-align", coluna.AlinhamentoHorizontal.ObterDescricao())
                            .Style("padding-left", "3px")
                            .Style("padding-right", "3px")
                            .Text(totalColuna.ObterValorConvertido(_configuracaoRelatorio.Formatacao));
                    }
                    else
                        linhaTotal
                            .CriarColunaTabela();
                }
            }
        }

        private void AdicionarTitulo(HtmlTag cabecalho) {
            if (string.IsNullOrWhiteSpace(_tabela.Titulo))
                return;

            cabecalho
                .CriarLinhaTabela()
                .CriarColunaCabecalhoTabela()
                .Text(_tabela.Titulo)
                .Attr("colspan", _tabela.ObterQuantidadeColunasVisiveis())
                .Style("text-align", TipoAlinhamentoHorizontal.Esquerda.ObterDescricao());
        }        
    }    
}
