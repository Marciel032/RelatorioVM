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
    internal abstract class ConstrutorTabelaRelatorio<TConteudo>
    {
        protected ConfiguracaoRelatorio _configuracaoRelatorio;
        protected Tabela<TConteudo> _tabela;        

        public ConstrutorTabelaRelatorio(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = new Tabela<TConteudo>();
            _tabela.Colunas = typeof(TConteudo)
                .ObterPropriedades()
                .Where(x => x.PodeSerColunaTabela())
                .Select(x => x.ObterColunaTabela<TConteudo>())
                .ToDictionary(x => x.Identificador);
        }              

        public void ComplementarValorPropriedade<TPropriedade, TPropriedadeComplemento>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Expression<Func<TConteudo, TPropriedadeComplemento>> propriedadeComplementoExpressao, bool ignorar = true)
        {
            if (!ObterColuna(propriedadeExpressao, out var coluna))
                return;

            ExibirColuna(propriedadeExpressao);

            coluna.PropriedadeComplemento = new Propriedade<TConteudo>(propriedadeExpressao.ObterPropriedadeBase())
                {
                    FuncaoPropriedade = (origem) => propriedadeComplementoExpressao.Compile()(origem)
                };

            coluna.DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Centro);

            if (ignorar)
                IgnorarColuna(propriedadeComplementoExpressao);
        }

        public void ComplementarValorPropriedade<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TPropriedade, string> funcao)
        {
            if (funcao == null)
                return;

            if (!ObterColuna(propriedadeExpressao, out var coluna))
                return;

            ExibirColuna(propriedadeExpressao);

            coluna.PropriedadeComplemento = new Propriedade<TConteudo>(propriedadeExpressao.ObterPropriedadeBase())
            {
                FuncaoPropriedade = (origem) => funcao(propriedadeExpressao.Compile()(origem))
            };

            coluna.DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Centro);
        }

        public void ComplementarValorPropriedade<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Func<TConteudo, TPropriedade, string> funcao)
        {
            if (funcao == null)
                return;

            if (!ObterColuna(propriedadeExpressao, out var coluna))
                return;

            ExibirColuna(propriedadeExpressao);

            coluna.PropriedadeComplemento = new Propriedade<TConteudo>(propriedadeExpressao.ObterPropriedadeBase())
            {
                FuncaoPropriedade = (origem) => funcao(origem, propriedadeExpressao.Compile()(origem))
            };

            coluna.DefinirAlinhamentoHorizontal(TipoAlinhamentoHorizontal.Centro);
        }

        protected void IgnorarTodasColunas()
        {
            foreach(var coluna in _tabela.Colunas.Values)
                coluna.Visivel = false;
        }

        protected void IgnorarColuna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao)
        {
            if (ObterColuna(propriedadeExpressao, out var coluna))
                coluna.Visivel = false;
        }

        protected void ExibirColuna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao)
        {
            if (ObterColuna(propriedadeExpressao, out var coluna))
                coluna.Visivel = true;
        }

        protected void ConfigurarColuna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, Action<IColunaRelatorioVM<TConteudo>> configuracao)
        {
            if (ObterColuna(propriedadeExpressao, out var coluna))
                configuracao?.Invoke(coluna);
        }

        protected void DefinirTitulo(string titulo)
        {
            _tabela.Titulo = titulo;
        }

        protected void DefinirTituloColuna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, string titulo)
        {
            if (ObterColuna(propriedadeExpressao, out var coluna))
                coluna.TituloColuna = titulo;
        }

        protected void TotalizarConteudo(Action<ITabelaTotalRelatorioVM<TConteudo>> opcoes = null)
        {
            var totaisConstrutor = new ConstrutorTabelaTotalRelatorio<TConteudo>(_configuracaoRelatorio);
            opcoes?.Invoke(totaisConstrutor);
            var total = totaisConstrutor.Construir();
            if (total.Totais.Count > 0)
                _tabela.Totais.Add(total);
        }

        protected void AgruparConteudo(Action<ITabelaAgrupadorRelatorioVM<TConteudo>> opcoes)
        {
            var agrupadorConstrutor = new ConstrutorTabelaAgrupadorRelatorio<TConteudo>(_configuracaoRelatorio);
            opcoes?.Invoke(agrupadorConstrutor);
            var agrupador = agrupadorConstrutor.Construir();
            if (agrupador.Agrupadores.Count > 0)
                _tabela.Agrupadores.Add(agrupador);
        }

        protected void AdicionarElementoColuna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, IElementoRelatorioVM elemento, bool exibirNaColuna)
        {
            if (ObterColuna(propriedadeExpressao, out var coluna))
                coluna.AdicionarElemento(elemento, exibirNaColuna);
        }

        private bool ObterColuna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> propriedadeExpressao, out TabelaColuna<TConteudo> coluna) {
            var propriedade = propriedadeExpressao.ObterPropriedadeBase();
            return _tabela.Colunas.TryGetValue(propriedade.Name, out coluna);
        }
    }
}
