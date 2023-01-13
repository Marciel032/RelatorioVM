using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class FiltrosElemento<T>: IElementoRelatorioVM
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        private readonly T _filtroVM;
        public List<Filtro<T>> Filtros { get; set; }

        public FiltrosElemento(T filtroVM, ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _filtroVM = filtroVM;
            Filtros = new List<Filtro<T>>();
        }

        public string ObterHtml() {
            if (Filtros.Count == 0)
                return string.Empty;

            var tabela = CriarTabela()
                .AddClass("tabela-filtro");

            var quantidadeDeFiltrosNaLinha = Math.Clamp(Filtros.Count, 1, _configuracaoRelatorio.Cabecalho.QuantidadeDeFiltrosPorLinha);
            tabela
                .CriarLinhaTabela()
                .CriarColunaTabela()
                .Text("Filtros")
                .Attr("colspan", quantidadeDeFiltrosNaLinha * 2);
            
            foreach (var filtros in Filtros.CriarGruposDe(_configuracaoRelatorio.Cabecalho.QuantidadeDeFiltrosPorLinha)) {
                var linha = tabela.CriarLinhaTabela()
                    .Style("border", "1px solid #888") ;
                foreach (var filtro in filtros)
                {
                    if (string.IsNullOrWhiteSpace(filtro.Valor))
                    { 
                        filtro.Valor = filtro.Propriedade.ObterValorConvertido(_filtroVM, _configuracaoRelatorio.Formatacao);
                        if (filtro.PropriedadeComplemento != null)
                            filtro.ValorComplemento = filtro.PropriedadeComplemento.ObterValorConvertido(_filtroVM, _configuracaoRelatorio.Formatacao);
                    }

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

            return tabela.ToHtmlString();
        }

        private HtmlTag CriarTabela() { 
            return new HtmlTag("table")
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
