using HtmlTags;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class FiltrosElemento
    {
        public List<Filtro> Filtros { get; set; }

        public FiltrosElemento()
        {
            Filtros = new List<Filtro>();
        }

        public void AdicionarHtml(HtmlTag parent) {
            if (Filtros.Count == 0)
                return;

            var tabela = new HtmlTag("table", parent)
                .Attr("width", "100%")
                .Attr("border", "0");
            
            foreach (var filtros in Filtros.CriarGruposDe(3)) {
                var linha = new HtmlTag("tr", tabela);
                foreach (var filtro in filtros)
                {
                    new HtmlTag("td", linha)
                        .Style("font-family", "Arial")
                        .Style("font-size", "14px")
                        .Style("text-align", "right")
                        .Text($"{filtro.Nome}:");

                    new HtmlTag("td", linha)
                        .Style("font-family", "Arial")
                        .Style("font-size", "14px")
                        .Append(
                            new HtmlTag("strong")
                                .Text(filtro.Valor)
                        );
                }
            }
        }
    }
}
