using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Dominio.Interfaces;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Elementos.Relatorios;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Relatorios.Construtores
{
    internal class ConstrutorTabelaHorizontalRelatorio<TConteudo> : ConstrutorTabelaRelatorio<TConteudo>, ITabelaHorizontalRelatorioVM<TConteudo>
    {
        public ConstrutorTabelaHorizontalRelatorio(ConfiguracaoRelatorio configuracaoRelatorio) : base(configuracaoRelatorio)
        {

        }

        public TabelaHorizontalElemento<TConteudo> Construir()
        {
            foreach (var agrupador in _tabela.Agrupadores)
            {
                agrupador.Colunas = agrupador.ObterColunasAgrupamento(_tabela.Colunas);
                agrupador.Colunas.ForEach(x => x.Visivel = false);
                foreach (var total in _tabela.Totais)
                    agrupador.AdicionarTotal(total.Clonar());
            }
            return new TabelaHorizontalElemento<TConteudo>(_configuracaoRelatorio, _tabela);
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> Titulo(string titulo) {
            DefinirTitulo(titulo);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> Totalizar(Action<ITabelaTotalRelatorioVM<TConteudo>> opcoes = null)
        {
            TotalizarConteudo(opcoes);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> Agrupar(Action<ITabelaAgrupadorRelatorioVM<TConteudo>> opcoes)
        {
            AgruparConteudo(opcoes);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> Ignorar<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao) {
            IgnorarColuna(propriedadeExpressao);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> Exibir<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao)
        {
            ExibirColuna(propriedadeExpressao);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Action<IColunaRelatorioVM<TConteudo>> coluna)
        {
            ConfigurarColuna(propriedadeExpressao, coluna);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> ComplementarValor<TPropriedade, TPropriedadeComplemento>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Expression<Func<TConteudo, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true)
        {
            ComplementarValorPropriedade(propriedadeExpressao, propriedadeComplementoExpressao, ignorar);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao)
        {
            ComplementarValorPropriedade(propriedadeExpressao, funcao);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> ComplementarValor<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TConteudo, TPropriedade, string> funcao)
        {
            ComplementarValorPropriedade(propriedadeExpressao, funcao);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> IgnorarTudo()
        {
            IgnorarTodasColunas();
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> Fracionar(int quantidadeColunas, TipoOrientacaoFracionamento orientacao = TipoOrientacaoFracionamento.Horizontal) {
            if(quantidadeColunas <= 1)
                return this;

            _tabela.QuantidadeFracionamentoDados = quantidadeColunas;
            _tabela.OrientacaoFracionamento = orientacao;
            
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> TabelaVertical<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, bool exibirNaColuna = true, Action<ITabelaVerticalRelatorioVM<TPropriedade>> opcoes = null) where TPropriedade : class
        {
            if (typeof(TPropriedade).EhLista())
                throw new Exception("Utiliza o método TabelaVerticalLista para exibir uma lista como tabela vertical");

            var construtorTabela = new ConstrutorTabelaVerticalRelatorio<TPropriedade>(_configuracaoRelatorio);
            opcoes?.Invoke(construtorTabela);
            AdicionarElementoColuna(propriedadeExpressao, construtorTabela.Construir(), exibirNaColuna);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> TabelaVerticalLista<TPropriedade>(Expression<Func<TConteudo, IEnumerable<TPropriedade>>> propriedadeExpressao, bool exibirNaColuna = true, Action<ITabelaVerticalRelatorioVM<TPropriedade>> opcoes = null) where TPropriedade : class
        {

            var construtorTabela = new ConstrutorTabelaVerticalRelatorio<TPropriedade>(_configuracaoRelatorio);
            opcoes?.Invoke(construtorTabela);
            AdicionarElementoColuna(propriedadeExpressao, construtorTabela.Construir(), exibirNaColuna);
            return this;
        }

        public ITabelaHorizontalRelatorioVM<TConteudo> TabelaHorizontal<TPropriedade>(Expression<Func<TConteudo, IEnumerable<TPropriedade>>> propriedadeExpressao, bool exibirNaColuna = true, Action<ITabelaHorizontalRelatorioVM<TPropriedade>> opcoes = null) where TPropriedade : class
        {
            var construtorTabela = new ConstrutorTabelaHorizontalRelatorio<TPropriedade>(_configuracaoRelatorio);
            opcoes?.Invoke(construtorTabela);
            AdicionarElementoColuna(propriedadeExpressao, construtorTabela.Construir(), exibirNaColuna);
            return this;
        }
    }
}
