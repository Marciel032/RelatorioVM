using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Estilos;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class FiltrosElemento<T>: IElementoRelatorioVM
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        private readonly string _classeFiltro = "tabela_filtro";
        public List<Filtro<T>> Filtros { get; set; }
        public string Indice { get; set; }

        public FiltrosElemento(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            Filtros = new List<Filtro<T>>();
        }

        public string ObterHtml(object conteudo) {
            var filtroConteudo = (T)conteudo;
            if (filtroConteudo == null)
                return string.Empty;

            if (Filtros.Count == 0)
                return string.Empty;

            var tabela = CriarTabela()
                .AddClass(_classeFiltro);

            var quantidadeDeFiltrosNaLinha = Math.Clamp(Filtros.Count, 1, _configuracaoRelatorio.Cabecalho.QuantidadeDeFiltrosPorLinha);
            tabela
                .CriarLinhaTabela()
                .CriarColunaTabela()
                .Text("Filtros")
                .Attr("colspan", quantidadeDeFiltrosNaLinha * 2);

            var gruposFiltros = Filtros.CriarGruposDe(_configuracaoRelatorio.Cabecalho.QuantidadeDeFiltrosPorLinha).ToList();
            foreach (var filtros in gruposFiltros) {
                var linha = tabela.CriarLinhaTabela()
                    .AddClass("linha-filtro");
                foreach (var filtro in filtros)
                {
                    linha.CriarColunaTabela()
                        .Style("text-align", "right")
                        .Append(
                            new HtmlTag("span")
                                .AppendText(filtro.Nome)
                                .AppendText(":")
                        );                    
                    linha.CriarColunaTabela()
                        .Append(
                            ObterValorTag(filtro, filtroConteudo)
                        );
                }


                if (gruposFiltros.Count > 1)
                    PreencherUltimaLinhaComColunasFaltando(linha, filtros.Count());
            }

            return tabela.ToHtmlString();
        }

        public string ObterEstilo()
        {
            var construtorEstilo = new EstiloConstrutor();
            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeFiltro)
                .DefinirFonte(_configuracaoRelatorio.Formatacao.FonteConteudo)
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeFiltro)
                .AdicionarClasse("linha-filtro")
                .AdicionarClasseElemento("td")
                .DefinirBorda(new EstiloElementoBorda() { 
                    Direcao = Dominio.Enumeradores.TipoBorda.Topo,
                    Tamanho = 1,
                    UnidadeMedida = Dominio.Enumeradores.TipoUnidadeMedida.Pixel,
                    TipoBorda = Dominio.Enumeradores.TipoEstiloBorda.Solida,
                    Cor = "#888"
                })
                .DefinirBorda(new EstiloElementoBorda() { 
                    Direcao = Dominio.Enumeradores.TipoBorda.Fundo,
                    Tamanho = 1,
                    UnidadeMedida = Dominio.Enumeradores.TipoUnidadeMedida.Pixel,
                    TipoBorda = Dominio.Enumeradores.TipoEstiloBorda.Solida,
                    Cor = "#888"
                })
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeFiltro)
                .AdicionarClasse("linha-filtro")
                .AdicionarClasseElemento("td:first-child")
                .DefinirBorda(new EstiloElementoBorda()
                {
                    Direcao = Dominio.Enumeradores.TipoBorda.Esquerda,
                    Tamanho = 1,
                    UnidadeMedida = Dominio.Enumeradores.TipoUnidadeMedida.Pixel,
                    TipoBorda = Dominio.Enumeradores.TipoEstiloBorda.Solida,
                    Cor = "#888"
                })
            );

            construtorEstilo.AdicionarEstilo(new EstiloElemento()
                .AdicionarClasse(_classeFiltro)
                .AdicionarClasse("linha-filtro")
                .AdicionarClasseElemento("td:last-child")
                .DefinirBorda(new EstiloElementoBorda()
                {
                    Direcao = Dominio.Enumeradores.TipoBorda.Direita,
                    Tamanho = 1,
                    UnidadeMedida = Dominio.Enumeradores.TipoUnidadeMedida.Pixel,
                    TipoBorda = Dominio.Enumeradores.TipoEstiloBorda.Solida,
                    Cor = "#888"
                })
            );

            return construtorEstilo.ToString();
        }

        private HtmlTag CriarTabela() { 
            return new HtmlTag("table")
                .Attr("width", "100%");
        }

        private HtmlTag ObterValorTag(Filtro<T> filtro, T conteudo) {
            if(filtro.PropriedadeComplemento != null)
                return ObterValorComComplemento(filtro, conteudo);

            return new HtmlTag("strong")
                .Text(filtro.Propriedade.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
        }

        private HtmlTag ObterValorComComplemento(Filtro<T> filtro, T conteudo) {
            return new HtmlTag("strong")
                .Text(filtro.Propriedade.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao))                
                .After(
                    new HtmlTag("strong")
                        .Text(filtro.PropriedadeComplemento.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao)))
                .After(
                    new HtmlTag("span")
                        .Text($" {filtro.Separador} ")
                );
        }

        private void PreencherUltimaLinhaComColunasFaltando(HtmlTag linha, int filtrosLinha) {
            if (filtrosLinha >= _configuracaoRelatorio.Cabecalho.QuantidadeDeFiltrosPorLinha)
                return;

            var filtrosFaltando = (_configuracaoRelatorio.Cabecalho.QuantidadeDeFiltrosPorLinha - filtrosLinha) * 2;
            for (int i = 0; i < filtrosFaltando; i++)
                linha.CriarColunaTabela().Append(new HtmlTag("span"));
        }        
    }
}
