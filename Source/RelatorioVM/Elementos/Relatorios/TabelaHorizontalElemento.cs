using HtmlTags;
using RelatorioVM.Comparadores;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Estilos;
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
        private int _indiceElemento;
        private string _classeTabela;

        public TabelaHorizontalElemento(ConfiguracaoRelatorio configuracaoRelatorio, Tabela<T> tabela)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = tabela;
            _classeTabela = "tch";
        }

        public void DefinirIndiceElemento(int indice)
        {
            _indiceElemento = indice;
            _classeTabela = $"tch{indice}";
        }

        public string ObterHtml() {            
            var tabela = CriarTabela();
            AdicionarCabecalho(tabela);
            var corpoTabela = tabela.CriarCorpoTabela();
            AdicionarConteudo(corpoTabela);
            AdicionarTotais(tabela, corpoTabela);
            return tabela.ToHtmlString();
        }

        public string ObterEstilo()
        {
            var construtorEstilo = new EstiloConstrutor();            
            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela)
                .DefinirFonte(_configuracaoRelatorio.Formatacao.FonteConteudo)
                .DefinirMedida(new EstiloElementoMedida()
                {
                    Direcao = TipoDirecaoMedida.Largura,
                    Tamanho = 100,
                    UnidadeMedida = TipoUnidadeMedida.Percentual
                })
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela)
                .AdicionarClasse("tr-cabecalho")
                .DefinirFonte(_configuracaoRelatorio.Formatacao.FonteConteudo)
                .DefinirBorda(new EstiloElementoBorda() {
                    Direcao = TipoBorda.Tudo,
                    TipoBorda = TipoEstiloBorda.Solida,
                    UnidadeMedida = TipoUnidadeMedida.Pixel,
                    Tamanho = 1,
                    Cor = "#777"
                })
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela)
                .AdicionarClasseElemento("th")
                .DefinirBorda(new EstiloElementoBorda()
                {
                    Direcao = TipoBorda.Tudo,
                    TipoBorda = TipoEstiloBorda.Solida,
                    UnidadeMedida = TipoUnidadeMedida.Pixel,
                    Tamanho = 1,
                    Cor = "#777"
                })
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela)
                .AdicionarClasseElemento("td,th")
                .DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = TipoAlinhamentoHorizontal.Esquerda })
                .DefinirPreenchimento(new EstiloElementoPreenchimento()
                {
                    Direcao = TipoPreenchimento.Direita,
                    Tamanho = 3,
                    UnidadeMedida = TipoUnidadeMedida.Pixel
                })
                .DefinirPreenchimento(new EstiloElementoPreenchimento()
                {
                    Direcao = TipoPreenchimento.Esquerda,
                    Tamanho = 3,
                    UnidadeMedida = TipoUnidadeMedida.Pixel
                })
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela)
                .AdicionarClasse("tr-zebra")
                .DefinirEstiloManual("background-color: #f2f2f2;")
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela)
                .AdicionarClasse("tr-totais-titulo")
                .AdicionarClasseElemento("td")
                .DefinirEstiloManual("font-weight: bold;")
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela)
                .AdicionarClasse("tr-totais")
                .AdicionarClasseElemento("td")
                .DefinirEstiloManual("font-weight: bold;")
                .DefinirBorda(new EstiloElementoBorda() { 
                    Direcao = TipoBorda.Topo,
                    Tamanho = 1,
                    UnidadeMedida = TipoUnidadeMedida.Pixel,
                    TipoBorda = TipoEstiloBorda.Solida,
                    Cor = "#888"
                })
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela)
                .AdicionarClasse("tr-grupo-titulo")
                .AdicionarClasseElemento("td")
                .DefinirEstiloManual("font-weight: bold;")
                .DefinirBorda(new EstiloElementoBorda()
                {
                    Direcao = TipoBorda.Fundo,
                    Tamanho = 1,
                    UnidadeMedida = TipoUnidadeMedida.Pixel,
                    TipoBorda = TipoEstiloBorda.Solida,
                    Cor = "#888"
                })
            );

            construtorEstilo.AdicionarEstilos(_tabela.ObterColunasVisiveis().ObterEstilos(_classeTabela));
            
            return construtorEstilo.ToString();
        }

        private HtmlTag CriarTabela() { 
            return new HtmlTag("table")
                .AddClass(_classeTabela);
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
                AdicionarConteudoAgrupado(corpoTabela, _tabela.Conteudo, _tabela.Agrupadores, 0);
        }

        private void AdicionarConteudoAgrupado(HtmlTag corpoTabela, IEnumerable<T> conteudo, List<TabelaAgrupador<T>> agrupadores, int indice) {
            if (agrupadores.Count <= indice)
                return;

            var agrupador = agrupadores[indice];

            var grupos = agrupador.AgruparConteudo(conteudo);
            foreach (var itensGrupo in grupos) {
                agrupador.Totais.ZerarTotais();

                agrupador.AdicionarCabecalhoAgrupamento(corpoTabela, itensGrupo.First(), _tabela.ObterQuantidadeColunasVisiveis());
                if (agrupadores.Count > indice + 1)
                    AdicionarConteudoAgrupado(corpoTabela, itensGrupo, agrupadores, indice + 1);
                else
                    AdicionarConteudoItens(corpoTabela, itensGrupo, (item) => { 
                        for (int i = 0; i <= indice; i++)
                            agrupadores[i].CalcularTotais(item); 
                    });

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
                if (coluna.TemComplemento && coluna.AlinhamentoHorizontalColuna == TipoAlinhamentoHorizontal.Centro)
                {
                    colunaHtml
                        .AddClass($"{coluna.Identificador}-valor")
                        .Text(coluna.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                    linha.CriarColunaTabela()
                        .AddClass($"{coluna.Identificador}-separador")
                        .Text(coluna.Separador);
                    linha.CriarColunaTabela()
                        .AddClass($"{coluna.Identificador}-complemento")
                        .Text(coluna.ObterComplementoConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                }
                else if (coluna.TemComplemento) {
                    colunaHtml
                        .AddClass(coluna.Identificador)
                        .Text(coluna.ObterValorConvertidoComComplemento(conteudo, _configuracaoRelatorio.Formatacao));
                }
                else {
                    colunaHtml
                        .AddClass(coluna.Identificador)
                        .Text(coluna.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                }               
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
