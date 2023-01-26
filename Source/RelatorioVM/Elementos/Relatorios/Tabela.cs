using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class Tabela<T>
    {
        private string _indice;
        public string Indice { 
            get { return _indice; } 
            set { 
                _indice = value;
                var indice = 0;
                foreach (var coluna in Colunas)
                    coluna.Value.Indice = $"{_indice}-{indice++}";
                } 
        }
        public string Titulo { get; set; }
        /// <summary>
        /// Utilizado ao construir a tabela vertical, para separar os valores em colunas
        /// </summary>
        public int QuantidadeColunasVertical { get; set; }
        public int QuantidadeFracionamentoDados { get; set; }
        public TipoOrientacaoFracionamento OrientacaoFracionamento { get; set; }
        public Dictionary<string, TabelaColuna<T>> Colunas { get; set; }        
        public List<TabelaTotal<T>> Totais { get; set; }

        public List<TabelaAgrupador<T>> Agrupadores { get; set; }

        public bool TemElementosLinha { get { return Colunas.Values.Any(x => x.TemElementosLinha); } }

        public Tabela()
        {
            Titulo = string.Empty;
            QuantidadeColunasVertical = 1;
            QuantidadeFracionamentoDados = 1;
            OrientacaoFracionamento = TipoOrientacaoFracionamento.Horizontal;
            Colunas = new Dictionary<string, TabelaColuna<T>>();
            Totais = new List<TabelaTotal<T>>();
            Agrupadores = new List<TabelaAgrupador<T>>();
        }

        public int ObterQuantidadeColunasVisiveis() {
            return Colunas
                .Where(x => x.Value.Visivel)
                .Select(x => x.Value.QuantidadeColunasUtilizadas)
                .Sum() * QuantidadeFracionamentoDados;
        }

        public int ObterQuantidadeColunasVisiveisSemFracionamento()
        {
            return Colunas
                .Where(x => x.Value.Visivel)
                .Select(x => x.Value.QuantidadeColunasUtilizadas)
                .Sum();
        }

        public IEnumerable<TabelaColuna<T>> ObterColunasVisiveis() {
            return Colunas.Values.Where(x => x.Visivel);
        }

        public string ObterEstilo()
        {
            var construtorEstilo = new StringBuilder();

            foreach (var coluna in Colunas.Values)
            {
                var estilo = coluna.ObterEstilo();
                if (string.IsNullOrEmpty(estilo))
                    continue;

                construtorEstilo.AppendLine(estilo);
            }
            return construtorEstilo.ToString();
        }
    }
}
