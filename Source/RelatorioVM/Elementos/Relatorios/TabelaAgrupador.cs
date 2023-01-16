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
        public List<TabelaTotal<T>> Totais { get; set; }
        public bool Totalizar { get; set; }

        public TabelaAgrupador(ConfiguracaoRelatorio configuracao)
        {
            _configuracaoRelatorio = configuracao;
            Agrupadores = new Dictionary<string, TabelaColunaAgrupador>();
            Totais = new List<TabelaTotal<T>>();
            Totalizar = true;
        }

        public ConfiguracaoFormatacaoRelatorio ObterOpcoesFormatacao() {
            return _configuracaoRelatorio.Formatacao;
        }
    }
}
