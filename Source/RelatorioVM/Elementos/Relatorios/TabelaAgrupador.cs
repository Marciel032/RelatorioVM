using RelatorioVM.Dominio.Configuracoes;
using RelatorioVM.Dominio.Conversores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaAgrupador<T>
    {
        public readonly ConfiguracaoRelatorio _configuracaoRelatorio;
        public Dictionary<string, TabelaColunaAgrupador> Agrupadores { get; set; }
        public List<TabelaColuna<T>> Colunas { get; set; }

        public TabelaAgrupador(ConfiguracaoRelatorio configuracao)
        {
            _configuracaoRelatorio = configuracao;
            Agrupadores = new Dictionary<string, TabelaColunaAgrupador>();
        }

        public OpcoesFormatacao ObterOpcoesFormatacao() {
            return _configuracaoRelatorio.Formatacao;
        }
    }
}
