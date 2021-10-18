using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Elementos.Interfaces;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class FiltrosElemento<T>: IElemento
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        public List<Filtro<T>> Filtros { get; set; }

        public FiltrosElemento(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            Filtros = new List<Filtro<T>>();
        }

        public bool ProcessarHtml(HtmlTag pai) {
            if (Filtros.Count == 0)
                return false;

            var tabela = CriarTabela(pai)
                .Style("font-family", "Courier new")
                .Style("font-size", "14px");

            tabela
                .CriarLinhaTabela()
                .CriarColunaTabela()
                .Text("Filtros")
                .Attr("colspan", _configuracaoRelatorio.Cabecalho.QuantidadeDeFiltrosPorLinha * 2);
            
            foreach (var filtros in Filtros.CriarGruposDe(_configuracaoRelatorio.Cabecalho.QuantidadeDeFiltrosPorLinha)) {
                var linha = tabela.CriarLinhaTabela()
                    .Style("border", "1px solid #888") ;
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
                            ObterValorTag(filtro)
                        );
                }
            }

            return true;
        }

        private HtmlTag CriarTabela(HtmlTag pai) { 
            return pai
                .CriarTabela()
                .Attr("width", "100%");
        }

        private HtmlTag ObterValorTag(Filtro<T> filtro) {
            if (!string.IsNullOrWhiteSpace(filtro.ValorComplemento))
                return ObterValorComComplemento(filtro);

            return new HtmlTag("strong")
                .Text(filtro.Valor);
        }

        private HtmlTag ObterValorComComplemento(Filtro<T> filtro) {
            return new HtmlTag("strong")
                .Text(filtro.Valor)                
                .After(
                    new HtmlTag("strong")
                        .Text(filtro.ValorComplemento))
                .After(
                    new HtmlTag("span")
                        .Text($" {filtro.Separador} ")
                );
        }
    }
}
