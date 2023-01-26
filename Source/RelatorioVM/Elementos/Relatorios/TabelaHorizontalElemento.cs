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
        private string _classeTabela { get { return $"tch{Indice}"; } }

        public string Indice { get { return _tabela.Indice; } set { _tabela.Indice = value; } }

        public TabelaHorizontalElemento(ConfiguracaoRelatorio configuracaoRelatorio, Tabela<T> tabela)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = tabela;
        }

        public string ObterHtml(object conteudo) {
            var conteudoTabela = (IEnumerable<T>)conteudo;
            if (conteudoTabela == null)
                return string.Empty;

            var tabela = CriarTabela();
            AdicionarCabecalho(tabela);
            var corpoTabela = tabela.CriarCorpoTabela();
            AdicionarConteudo(corpoTabela, conteudoTabela);
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
                .AdicionarClasse($"{_classeTabela}-tr-h")
                .DefinirBorda(new EstiloElementoBorda() {
                    Direcao = TipoBorda.Tudo,
                    TipoBorda = TipoEstiloBorda.Solida,
                    UnidadeMedida = TipoUnidadeMedida.Pixel,
                    Tamanho = 1,
                    Cor = "#777"
                })
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela + " >")
                .AdicionarClasseElemento("thead >")
                .AdicionarClasseElemento("tr >")
                .AdicionarClasseElemento("th")
                .DefinirBorda(new EstiloElementoBorda()
                {
                    Direcao = TipoBorda.Tudo,
                    TipoBorda = TipoEstiloBorda.Solida,
                    UnidadeMedida = TipoUnidadeMedida.Pixel,
                    Tamanho = 1,
                    Cor = "#777"
                })
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
                .AdicionarClasse(_classeTabela + " >")
                .AdicionarClasseElemento("tbody >")
                .AdicionarClasseElemento("tr >")
                .AdicionarClasseElemento("td")
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

            if (_configuracaoRelatorio.Conteudo.Zebrado)
            {
                construtorEstilo.AdicionarEstilo(new EstiloElemento()
                    .AdicionarClasse(_classeTabela + " > ")
                    .AdicionarClasseElemento("tbody > ")
                    .AdicionarClasseElemento("tr:nth-child(even):not(.tr-t):not(.tr-t-t):not(.tr-g-t)")
                    .DefinirEstiloManual("background-color: #f2f2f2;")
                );

                construtorEstilo.AdicionarEstilo(new EstiloElemento()
                    .AdicionarClasse(_classeTabela + " > ")
                    .AdicionarClasseElemento("tbody > ")
                    .AdicionarClasseElemento("tr:nth-child(odd):not(.tr-t):not(.tr-t-t):not(.tr-g-t)")
                    .DefinirEstiloManual("background-color: #ffffff;")
                );
            }

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela + " >")
                .AdicionarClasseElemento("tbody >")
                .AdicionarClasse("tr-t-t")
                .DefinirEstiloManual("font-weight: bold;")
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela + " >")
                .AdicionarClasseElemento("tbody >")
                .AdicionarClasseElemento("tr >")
                .AdicionarClasse("td-t-t")
                .DefinirEstiloManual("font-weight: bold;")
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
                .AdicionarClasse(_classeTabela + " >")
                .AdicionarClasseElemento("tbody >")
                .AdicionarClasse("tr-t")
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
                .AdicionarClasse(_classeTabela + " >")
                .AdicionarClasseElemento("tbody >")
                .AdicionarClasse("tr-g-t")
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

            construtorEstilo.AdicionarEstilos(_tabela.ObterColunasVisiveis().ObterEstilos(_tabela, _classeTabela));            
            
            return construtorEstilo.ToString() + _tabela.ObterEstilo();
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
                .AddClass($"{_classeTabela}-tr-h");

            for (int i = 0; i < _tabela.QuantidadeFracionamentoDados; i++)
            {
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
        }

        private void AdicionarConteudo(HtmlTag corpoTabela, IEnumerable<T> conteudos) {
            _tabela.Totais.ZerarTotais();
            
            if (_tabela.Agrupadores.Count == 0)
                AdicionarConteudoItens(corpoTabela, conteudos);
            else
                AdicionarConteudoAgrupado(corpoTabela, conteudos, _tabela.Agrupadores, 0);
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
            if (_tabela.QuantidadeFracionamentoDados > 1)
            {
                IEnumerable<IEnumerable<T>> linhasFracionadas = null;
                if(_tabela.OrientacaoFracionamento == TipoOrientacaoFracionamento.Vertical)
                    linhasFracionadas = itens.CriarGruposDeFracionamento(_tabela.QuantidadeFracionamentoDados);
                else
                    linhasFracionadas = itens.CriarGruposDe(_tabela.QuantidadeFracionamentoDados);

                foreach (var linha in linhasFracionadas)
                {
                    var quantidadeConteudos = 0;
                    var linhas = CriarLinhasItem(corpoTabela);
                    foreach (var conteudo in linha)
                    {
                        AdicionarConteudoItemLinha(linhas, conteudo);
                        onDepoisAdicionarConteudo?.Invoke(conteudo);
                        quantidadeConteudos++;
                    }
                    //Preenche as colunas que não tem dados
                    if(quantidadeConteudos < _tabela.QuantidadeFracionamentoDados)
                        for (int i = 0; i < _tabela.QuantidadeFracionamentoDados - quantidadeConteudos; i++)
                            AdicionarConteudoItemLinha(linhas, default(T));
                }
            }
            else {                
                foreach (var conteudo in itens)
                {
                    var linhas = CriarLinhasItem(corpoTabela);
                    AdicionarConteudoItemLinha(linhas, conteudo);
                    onDepoisAdicionarConteudo?.Invoke(conteudo);
                }
            }
        }

        private (HtmlTag Conteudo, HtmlTag Complemento) CriarLinhasItem(HtmlTag corpoTabela) {
            var linhaConteudo = corpoTabela.CriarLinhaTabela();
            HtmlTag linhaComplemento = null;
            if (_tabela.TemElementosLinha)
                linhaComplemento = corpoTabela.CriarLinhaTabela();

            return (linhaConteudo, linhaComplemento);
        }

        private void AdicionarConteudoItemLinha((HtmlTag Conteudo, HtmlTag Complemento) linhas, T conteudo) {
            AdicionarConteudoItem(linhas.Conteudo, conteudo);
            if (linhas.Complemento != null)
                AdicionarConteudoItemComplemento(linhas.Complemento, conteudo);
            if (conteudo == null)
                return;

            _tabela.Totais.CalcularTotais(conteudo);            
        }

        private void AdicionarConteudoItem(HtmlTag linha, T conteudo)
        {
            if (conteudo != null)
                foreach (var coluna in _tabela.ObterColunasVisiveis())
                {
                    var colunaHtml = linha.CriarColunaTabela();
                    if (coluna.TemComplemento && coluna.AlinhamentoHorizontalColuna == TipoAlinhamentoHorizontal.Centro)
                    {
                        colunaHtml
                            .Text(coluna.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                        linha.CriarColunaTabela()
                            .Text(coluna.Separador);
                        linha.CriarColunaTabela()
                            .Text(coluna.ObterComplementoConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                    }
                    else if (coluna.TemComplemento)
                    {
                        colunaHtml
                            .Text(coluna.ObterValorConvertidoComComplemento(conteudo, _configuracaoRelatorio.Formatacao));
                    }
                    else if (coluna.TemElementosColuna)
                        coluna.AdicionarHtmlColuna(colunaHtml, conteudo);
                    else
                    {
                        colunaHtml
                            .Text(coluna.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                    }
                }
            else
                for (int i = 0; i < _tabela.ObterQuantidadeColunasVisiveisSemFracionamento(); i++)
                    linha.CriarColunaTabela();
        }

        private void AdicionarConteudoItemComplemento(HtmlTag linha, T conteudo)
        {
            if (_tabela.TemElementosLinha)
            {
                var colunaHtml = linha
                    .CriarColunaTabela()
                    .ExpandirColuna(_tabela.ObterQuantidadeColunasVisiveisSemFracionamento())
                    .Style("padding-left", "50px")
                    .Style("padding-right", "10px")
                    .Style("padding-top", "10px")
                    .Style("padding-bottom", "20px");

                if (conteudo != null)
                {
                    foreach (var coluna in _tabela.Colunas.Values)
                    {
                        if (!coluna.TemElementosLinha)
                            continue;

                        coluna.AdicionarHtmlLinha(colunaHtml, conteudo);
                    }
                }
            }
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
