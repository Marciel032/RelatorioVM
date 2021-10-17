using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Elementos.Relatorios;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Interfaces;
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

        public ConstrutorTabelaTotalRelatorio(ConfiguracaoRelatorio configuracaoRelatorio, IEnumerable<TConteudo> conteudo)
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

        public ITabelaTotalRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade, Expression<Func<TConteudo, decimal>> expressaoCalculo = null)
        {
            var propriedade = expressaoPropriedade.ObterPropriedade();
            var total = ObterOuAdicionarTotal(propriedade);

            if (expressaoCalculo != null)
                _totais.Totais[propriedade.Name].Propriedade.FuncaoPropriedade = (origem) => expressaoCalculo.Compile()(origem);

            return this;
        }

        public ITabelaTotalRelatorioVM<TConteudo> Coluna<TPropriedade>(Expression<Func<TConteudo, TPropriedade>> expressaoPropriedade, Expression<Func<TConteudo, long>> expressaoCalculo = null)
        {
            var propriedade = expressaoPropriedade.ObterPropriedade();
            var total = ObterOuAdicionarTotal(propriedade);

            if (expressaoCalculo != null)
                _totais.Totais[propriedade.Name].Propriedade.FuncaoPropriedade = (origem) => expressaoCalculo.Compile()(origem);

            return this;
        }

        public ITabelaTotalRelatorioVM<TConteudo> Titulo(string titulo)
        {
            _totais.Titulo = titulo;
            return this;
        }

        private TabelaColunaTotal<TConteudo> ObterOuAdicionarTotal(PropertyInfo propriedade) {
            if (_totais.Totais.TryGetValue(propriedade.Name, out var total))
                return total;

            total = propriedade.ObterTotalTabela<TConteudo>();
            _totais.Totais.Add(total.Identificador, total);
            return total;
        }
    }
}
