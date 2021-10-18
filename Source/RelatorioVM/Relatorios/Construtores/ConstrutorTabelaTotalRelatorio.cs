using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Interfaces;
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
    internal class ConstrutorTabelaTotalRelatorio<TConteudo>: ITabelaTotalRelatorioVM<TConteudo>
    {
        private readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        private TabelaTotal<TConteudo> _totais;

        public ConstrutorTabelaTotalRelatorio(ConfiguracaoRelatorio configuracaoRelatorio)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _totais = new TabelaTotal<TConteudo>(
                typeof(TConteudo)
                    .ObterPropriedades()
                    .Where(x => x.PodeSerTotalTabela())
                    .Select(x => x.ObterTotalTabela<TConteudo>())
                    .ToDictionary(x => x.Identificador)
            );
        }

        public TabelaTotal<TConteudo> Construir() {
            return _totais;
        }

        public ITabelaTotalRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade, Expression<Func<TConteudo, decimal>> expressaoCalculo)
        {
            var propriedade = expressaoPropriedade.ObterPropriedadeBase();
            var total = ObterOuAdicionarTotal(propriedade);

            if (expressaoCalculo != null)
                total.Propriedade.FuncaoPropriedade = (origem) => expressaoCalculo.Compile()(origem);

            return this;
        }

        public ITabelaTotalRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade, Expression<Func<TConteudo, long>> expressaoCalculo)
        {
            var propriedade = expressaoPropriedade.ObterPropriedadeBase();
            var total = ObterOuAdicionarTotal(propriedade);

            if (expressaoCalculo != null)
                total.Propriedade.FuncaoPropriedade = (origem) => expressaoCalculo.Compile()(origem);

            return this;
        }

        public ITabelaTotalRelatorioVM<TConteudo> Titulo(string titulo)
        {
            _totais.Titulo = titulo;
            return this;
        }

        public ITabelaTotalRelatorioVM<TConteudo> Ignorar<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade)
        {
            var propriedade = expressaoPropriedade.ObterPropriedadeBase();
            _totais.Totais.Remove(propriedade.Name);

            return this;
        }

        public ITabelaTotalRelatorioVM<TConteudo> IgnorarTodos()
        {
            _totais.Totais.Clear();
            return this;
        }

        private TabelaColunaTotal<TConteudo> ObterOuAdicionarTotal(PropertyInfo propriedade) {
            if (ObterTotal(propriedade, out var total))
                return total;

            total = propriedade.ObterTotalTabela<TConteudo>();
            _totais.Totais.Add(total.Identificador, total);
            return total;
        }

        private bool ObterTotal(PropertyInfo propriedade, out TabelaColunaTotal<TConteudo> total)
        {
            return _totais.Totais.TryGetValue(propriedade.Name, out total);
        }
    }
}
