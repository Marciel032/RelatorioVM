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
    internal class TabelaHorizontalElemento<T>: IElementoRelatorioVM
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        private Tabela<T> _tabela;

        public TabelaHorizontalElemento(ConfiguracaoRelatorio configuracaoRelatorio, Tabela<T> tabela)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = tabela;
        }

        public string ObterHtml() {            
            var tabela = CriarTabela();
            AdicionarCabecalho(tabela);
            var corpoTabela = tabela.CriarCorpoTabela();
            AdicionarConteudo(corpoTabela);
            AdicionarTotais(tabela, corpoTabela);
            return tabela.ToHtmlString();
        }

        private HtmlTag CriarTabela() { 
            return new HtmlTag("table")
                .AddClass("tabela-conteudo");
        }

        private void AdicionarCabecalho(HtmlTag tabela) {
            var cabecalho = tabela.CriarCabecalhoTabela();

            AdicionarTitulo(cabecalho);

            var linhaCabecalho = cabecalho
                .CriarLinhaTabela()
                .AddClass("tr-cabecalho");

            foreach (var coluna in _tabela.ObterColunasVisiveis())
            {
                var colunaHtml = linhaCabecalho.CriarColunaCabecalhoTabela();
                if (coluna.TemComplemento)
                    colunaHtml.ExpandirColuna(coluna.QuantidadeColunasUtilizadas);
                    
                colunaHtml
                    .DefinirAlinhamentoHorizontal(coluna.AlinhamentoHorizontalTitulo)
                    .Text(coluna.TituloColuna);
            }
        }

        private void AdicionarConteudo(HtmlTag corpoTabela) {
            _tabela.Totais.ZerarTotais();
            
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

            var grupos = agrupador.AgruparConteudo(conteudo);
            foreach (var itensGrupo in grupos) {
                agrupador.Totais.ZerarTotais();

                agrupador.AdicionarCabecalhoAgrupamento(corpoTabela, itensGrupo.First(), _tabela.ObterQuantidadeColunasVisiveis());
                if (agrupadores.Count > 0)
                    AdicionarConteudoAgrupado(corpoTabela, itensGrupo, agrupadores.ToList());
                else
                    AdicionarConteudoItens(corpoTabela, itensGrupo, (item) => { agrupador.CalcularTotais(item); });

                agrupador.AdicionarTotaisHtml(corpoTabela, _tabela, _configuracaoRelatorio.Formatacao);
            }
        }        

        private void AdicionarConteudoItens(HtmlTag corpoTabela, IEnumerable<T> itens, Action<T> onDepoisAdicionarConteudo = null) {
            bool zebra = false;
            foreach (var conteudo in itens)
            {
                AdicionarConteudoItem(corpoTabela, conteudo, zebra);
                zebra = !zebra;
                onDepoisAdicionarConteudo?.Invoke(conteudo);
            }
        }

        private void AdicionarConteudoItem(HtmlTag corpoTabela, T conteudo, bool zebra)
        {
            var linha = corpoTabela.CriarLinhaTabela();
            if (zebra && _configuracaoRelatorio.Conteudo.Zebrado)
                linha.AddClass("tr-zebra");
            foreach (var coluna in _tabela.ObterColunasVisiveis())
            {
                var colunaHtml = linha.CriarColunaTabela();
                if (coluna.TemComplemento) {
                    colunaHtml.AddClass("td-valor-complemento");

                    linha.CriarColunaTabela()
                        .AddClass("td-complemento")
                        .Text(coluna.ObterSeparadorComComplementoConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                }
                else
                    colunaHtml.DefinirAlinhamentoHorizontal(coluna.AlinhamentoHorizontalColuna);

                colunaHtml.Text(coluna.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));                
            }

            _tabela.Totais.CalcularTotais(conteudo);           
        }

        private void AdicionarTotais(HtmlTag tabela, HtmlTag corpoTabela)
        {
            _tabela.Totais.AdicionarTotaisHtml(corpoTabela, _tabela, _configuracaoRelatorio.Formatacao);           
        }

        private void AdicionarTitulo(HtmlTag cabecalho) {
            if (string.IsNullOrWhiteSpace(_tabela.Titulo))
                return;

            cabecalho
                .CriarLinhaTabela()
                .CriarColunaCabecalhoTabela()
                .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Esquerda)
                .ExpandirColuna(_tabela.ObterQuantidadeColunasVisiveis())
                .Text(_tabela.Titulo);
        }        
    }    
}
