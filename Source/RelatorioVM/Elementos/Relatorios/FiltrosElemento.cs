using HtmlTags;
using RelatorioVM.Elementos.Interfaces;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class FiltrosElemento: IElemento
    {
        public List<Filtro> Filtros { get; set; }

        public FiltrosElemento()
        {
            Filtros = new List<Filtro>();
        }

        public bool ProcessarHtml(HtmlTag pai) {
            if (Filtros.Count == 0)
                return false;

            var tabela = CriarTabela(pai);
            
            foreach (var filtros in Filtros.CriarGruposDe(4)) {
                var linha = tabela.CriarLinhaTabela();
                foreach (var filtro in filtros)
                {
                    linha.CriarColunaTabela()
                        .Style("font-family", "Arial")
                        .Style("font-size", "14px")
                        .Style("text-align", "right")
                        .Text($"{filtro.Nome}:");

                    linha.CriarColunaTabela()
                        .Style("font-family", "Arial")
                        .Style("font-size", "14px")
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
                .Attr("width", "100%")
                .Attr("border", "0");
        }

        private HtmlTag ObterValorTag(Filtro filtro) {
            var valor = filtro.Valor;
            if (!string.IsNullOrWhiteSpace(filtro.ValorComplemento))
                valor = $"{valor} - {filtro.ValorComplemento}";

            return new HtmlTag("strong")
                                .Text(valor);
        }
    }
}
