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
    internal class TabelaVerticalElemento<T>: IElementoRelatorioVM
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        private Tabela<T> _tabela;
        private string _classeTabela { get { return $"tcv{Indice}"; } }

        public string Indice { get; set; }

        public TabelaVerticalElemento(ConfiguracaoRelatorio configuracaoRelatorio, Tabela<T> tabela)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = tabela;
        }

        public string ObterHtml(object conteudo) {
            if (conteudo == null)
                return string.Empty;

            var tabela = CriarTabela();
            AdicionarCabecalho(tabela);
            var corpoTabela = tabela.CriarCorpoTabela();
            if (conteudo is IEnumerable<T>)
                AdicionarConteudo(corpoTabela, (IEnumerable<T>)conteudo);
            else if (conteudo is T)
                AdicionarConteudoColunas(corpoTabela, (T)conteudo);
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
                .AdicionarClasse(_classeTabela + " >")
                .AdicionarClasseElemento("thead >")
                .AdicionarClasseElemento("tr >")
                .AdicionarClasseElemento("th")
                .DefinirBorda(new EstiloElementoBorda()
                {
                    Direcao = TipoBorda.Fundo,
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
                .DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { Direcao = TipoAlinhamentoHorizontal.Esquerda})
                .DefinirPreenchimento(new EstiloElementoPreenchimento() { 
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
                    .AdicionarClasseElemento("tr:nth-child(even)")
                    .DefinirEstiloManual("background-color: #f2f2f2;")
                );

                construtorEstilo.AdicionarEstilo(new EstiloElemento()
                    .AdicionarClasse(_classeTabela + " > ")
                    .AdicionarClasseElemento("tbody > ")
                    .AdicionarClasseElemento("tr:nth-child(odd)")
                    .DefinirEstiloManual("background-color: #ffffff;")
                );
            }

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeTabela + " >")
                .AdicionarClasseElemento("tbody >")
                .AdicionarClasseElemento("tr >")
                .AdicionarClasse("td-t")
                .DefinirEstiloManual("font-weight: bold;")
                .DefinirAlinhamentoTexto(new EstiloAlinhamentoTexto() { 
                    Direcao = TipoAlinhamentoHorizontal.Direita
                })
            );

            return construtorEstilo.ToString();
        }

        private HtmlTag CriarTabela() { 
            return new HtmlTag("table")
                .AddClass(_classeTabela);
        }

        private void AdicionarCabecalho(HtmlTag tabela) {
            var cabecalho = tabela.CriarCabecalhoTabela();

            AdicionarTitulo(cabecalho);
        }

        private void AdicionarConteudo(HtmlTag corpoTabela, IEnumerable<T> conteudos) {
            foreach (var conteudo in conteudos)
                AdicionarConteudoColunas(corpoTabela, conteudo);
        }

        private void AdicionarConteudoColunas(HtmlTag corpoTabela, T conteudo)
        {
            var colunasVisiveis = _tabela.ObterColunasVisiveis();
            if (colunasVisiveis.Count() == 0)
                return;

            var colunasVerticais = colunasVisiveis.CriarGruposDe(_tabela.QuantidadeColunasVertical);
            HtmlTag linha = null;
            foreach (var colunaVertical in colunasVerticais)
            {
                linha = corpoTabela.CriarLinhaTabela();

                foreach (var conteudoVertical in colunaVertical)
                {                    
                    linha.CriarColunaTabela()
                        .Text($"{conteudoVertical.TituloColuna}:")
                        .AddClass("td-t");

                    linha.CriarColunaTabela()
                        .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Esquerda)
                        .Text(conteudoVertical.ObterValorConvertidoComComplemento(conteudo, _configuracaoRelatorio.Formatacao));
                }
            }

            //Completa as colunas no final, para não ficar espaço vazio na ultima linha
            var quantidadeColunasFaltantes = colunasVisiveis.Count() % _tabela.QuantidadeColunasVertical;
            if (quantidadeColunasFaltantes > 0)
                for (int i = 0; i < _tabela.QuantidadeColunasVertical - quantidadeColunasFaltantes; i++)
                {
                    linha.CriarColunaTabela();
                    linha.CriarColunaTabela();
                }
        }

        private void AdicionarTitulo(HtmlTag cabecalho) {
            if (string.IsNullOrWhiteSpace(_tabela.Titulo))
                return;

            cabecalho
                .CriarLinhaTabela()
                .CriarColunaCabecalhoTabela()
                .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Esquerda)
                .ExpandirColuna(_tabela.QuantidadeColunasVertical * 2)
                .Text(_tabela.Titulo);
        }        
    }    
}
