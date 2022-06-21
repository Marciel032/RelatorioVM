﻿using HtmlTags;
using RelatorioVM.Comparadores;
using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
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

        public TabelaVerticalElemento(ConfiguracaoRelatorio configuracaoRelatorio, Tabela<T> tabela)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = tabela;
        }

        public string ObterHtml() {            
            var tabela = CriarTabela();
            AdicionarCabecalho(tabela);
            var corpoTabela = tabela.CriarCorpoTabela();
            AdicionarConteudo(corpoTabela);
            return tabela.ToHtmlString();
        }

        private HtmlTag CriarTabela() { 
            return new HtmlTag("table")
                .AddClass("tabela-conteudo-vertical");
        }

        private void AdicionarCabecalho(HtmlTag tabela) {
            var cabecalho = tabela.CriarCabecalhoTabela();

            AdicionarTitulo(cabecalho);
        }

        private void AdicionarConteudo(HtmlTag corpoTabela) {
            foreach (var conteudo in _tabela.Conteudo)
                AdicionarConteudoColunas(corpoTabela, conteudo);
        }

        private void AdicionarConteudoColunas(HtmlTag corpoTabela, T conteudo)
        {
            bool zebra = false;
            var colunasVisiveis = _tabela.ObterColunasVisiveis();
            if (colunasVisiveis.Count() == 0)
                return;

            var colunasVerticais = colunasVisiveis.CriarGruposDe(_tabela.QuantidadeColunasVertical);
            HtmlTag linha = null;
            foreach (var colunaVertical in colunasVerticais)
            {
                linha = corpoTabela.CriarLinhaTabela();
                if (zebra && _configuracaoRelatorio.Conteudo.Zebrado)
                    linha.AddClass("tr-zebra");

                foreach (var conteudoVertical in colunaVertical)
                {                    
                    linha.CriarColunaTabela()
                        .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Direita)
                        .Text($"{conteudoVertical.Titulo}:")
                        .AddClass("td-titulo");

                    linha.CriarColunaTabela()
                        .DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Esquerda)
                        .Text(conteudoVertical.ObterValorConvertido(conteudo, _configuracaoRelatorio.Formatacao));
                }
                
                zebra = !zebra;
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
                .Text(_tabela.Titulo)
                .AddClass("tr-cabecalho");
        }        
    }    
}