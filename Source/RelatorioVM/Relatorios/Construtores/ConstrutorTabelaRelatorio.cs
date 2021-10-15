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
        private IEnumerable<TConteudo> _conteudo;
        private List<TabelaColuna> _colunas;

        public ConstrutorTabelaRelatorio(ConfiguracaoRelatorio configuracaoRelatorio, IEnumerable<TConteudo> conteudo)
        {
            _configuracaoRelatorio = configuracaoRelatorio;
            _conteudo = conteudo;
            _colunas = typeof(TConteudo)
                .ObterPropriedades()
                .Where(x => x.PodeSerColunaTabela())
                .Select(x => x.ObterColunaTabela())
                .ToList();
        }

        public TabelaElemento<TConteudo> Construir()
        {
            return new TabelaElemento<TConteudo>(_configuracaoRelatorio, _colunas, _conteudo);
        }
    }
}
