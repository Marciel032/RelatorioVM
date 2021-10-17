using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Elementos.Relatorios;
using RelatorioVM.Extensoes;
using RelatorioVM.Relatorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Relatorios.Construtores
{
    internal class ConstrutorTabelaRelatorio<TConteudo>: ITabelaRelatorioVM<TConteudo>
    {
        private ConfiguracaoRelatorio _configuracaoRelatorio;
        private Tabela<TConteudo> _tabela;        

        public ConstrutorTabelaRelatorio(ConfiguracaoRelatorio configuracaoRelatorio, IEnumerable<TConteudo> conteudo)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _tabela = new Tabela<TConteudo>(conteudo);
            _tabela.Colunas = typeof(TConteudo)
                .ObterPropriedades()
                .Where(x => x.PodeSerColunaTabela())
                .Select(x => x.ObterColunaTabela<TConteudo>())
                .ToList();
        }

        public TabelaElemento<TConteudo> Construir()
        {
            return new TabelaElemento<TConteudo>(_configuracaoRelatorio, _tabela);
        }

        public ITabelaRelatorioVM<TConteudo> Titulo(string titulo) {
            _tabela.Titulo = titulo;
            return this;
        }

        public ITabelaRelatorioVM<TConteudo> Totalizar(Action<ITabelaTotalRelatorioVM<TConteudo>> opcoes = null)
        {
            var totaisConstrutor = new ConstrutorTabelaTotalRelatorio<TConteudo>(_configuracaoRelatorio, _tabela.Conteudo);
            opcoes?.Invoke(totaisConstrutor);
            _tabela.Totais.Add(totaisConstrutor.Construir());
            return this;
        }
    }
}
