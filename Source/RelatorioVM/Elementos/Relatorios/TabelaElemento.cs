﻿using HtmlTags;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Interfaces;
using RelatorioVM.Extensoes;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaElemento<T>: IElemento
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        private Tabela<T> _tabela;

        public TabelaElemento(ConfiguracaoRelatorio configuracaoRelatorio, Tabela<T> tabela)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = tabela;
        }

        public bool ProcessarHtml(HtmlTag pai) {            
            var tabela = CriarTabela(pai);
            AdicionarCabecalho(tabela);
            AdicionarConteudo(tabela);
            AdicionarTotais(tabela);
            return true;
        }

        private HtmlTag CriarTabela(HtmlTag pai) { 
            return pai
                .CriarTabela()
                .Style("width", "100%")
                .Style("font-family", "courier new");
        }

        private void AdicionarCabecalho(HtmlTag tabela) {
            var cabecalho = tabela.CriarCabecalhoTabela()
                    .Style("display", "table-header-group");

            AdicionarTitulo(cabecalho);

            var linhaCabecalho = cabecalho
                .CriarLinhaTabela()
                .Style("border", "1px solid #777");

            foreach (var coluna in _tabela.Colunas)
            {
                linhaCabecalho.CriarColunaCabecalhoTabela()
                     .Style("text-align", coluna.AlinhamentoHorizontal.ObterDescricao())
                     .Style("padding-left", "3px")
                     .Style("padding-right", "3px")
                     .Text(coluna.Titulo);
            }
        }

        private void AdicionarConteudo(HtmlTag tabela) {
            var corpoTabela = tabela.CriarCorpoTabela();
            foreach (var conteudo in _tabela.Conteudo) {
                var linha = corpoTabela.CriarLinhaTabela();
                foreach (var coluna in _tabela.Colunas) {
                    linha.CriarColunaTabela()
                        .Style("text-align", coluna.AlinhamentoHorizontal.ObterDescricao())
                        .Style("padding-left", "3px")
                        .Style("padding-right", "3px")
                        .Text(coluna.Propriedade.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                }
            }
        }

        private void AdicionarTotais(HtmlTag tabela)
        {
            if (_tabela.Totais.Count == 0)
                return;

            //var rodapeTabela = tabela.CriarRodapeTabela();
            foreach (var total in _tabela.Totais)
            {
                var linha = tabela.CriarLinhaTabela().AddClass("tr-totais");
                foreach (var coluna in _tabela.Colunas)
                {
                    if (total.Totais.ContainsKey(coluna.Identificador))
                    {
                        linha.CriarColunaTabela()
                            .Style("text-align", coluna.AlinhamentoHorizontal.ObterDescricao())
                            .Style("padding-left", "3px")
                            .Style("padding-right", "3px")                            
                            .Text("Teste");
                    }
                    else
                        linha.CriarColunaTabela();

                }
            }
        }

        private void AdicionarTitulo(HtmlTag cabecalho) {
            if (string.IsNullOrWhiteSpace(_tabela.Titulo))
                return;

            cabecalho
                .CriarLinhaTabela()
                .CriarColunaCabecalhoTabela()
                .Text(_tabela.Titulo)
                .Attr("colspan", _tabela.Colunas.Count)
                .Style("text-align", TipoAlinhamentoHorizontal.Esquerda.ObterDescricao());
        }
    }
}
