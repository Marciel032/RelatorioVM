using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Elementos.Interfaces;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class FiltrosElemento: IElemento
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        public List<Filtro> Filtros { get; set; }

        public FiltrosElemento(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            Filtros = new List<Filtro>();
        }

        public bool ProcessarHtml(HtmlTag pai) {
            if (Filtros.Count == 0)
                return false;

            var tabela = CriarTabela(pai);
            
            foreach (var filtros in Filtros.CriarGruposDe(_configuracaoRelatorio.Cabecalho.QuantidadeDeFiltrosPorLinha)) {
                var linha = tabela.CriarLinhaTabela();
                foreach (var filtro in filtros)
                {
                    linha.CriarColunaTabela()
                        .Style("font-family", "Courier new")
                        .Style("font-size", "14px")
                        .Style("text-align", "right")
                        .Append(
                            new HtmlTag("span")
                                .AppendText(filtro.Nome)
                                .AppendText(":")
                        );                    
                    linha.CriarColunaTabela()
                        .Style("font-family", "Courier new")
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
            if (!string.IsNullOrWhiteSpace(filtro.ValorComplemento))
                return ObterValorComComplemento(filtro);

            return new HtmlTag("strong")
                .Text(filtro.Valor);
        }

        private HtmlTag ObterValorComComplemento(Filtro filtro) {
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
