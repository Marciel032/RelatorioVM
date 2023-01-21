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
                foreach(var coluna in Colunas)
                    coluna.Value.Indice = _indice;
                } 
        }
        public string Titulo { get; set; }
        /// <summary>
        /// Utilizado ao construir a tabela vertical, para separar os valores em colunas
        /// </summary>
        public int QuantidadeColunasVertical { get; set; }
        public Dictionary<string, TabelaColuna<T>> Colunas { get; set; }        
        public List<TabelaTotal<T>> Totais { get; set; }

        public List<TabelaAgrupador<T>> Agrupadores { get; set; }

        public Tabela()
        {
            Titulo = string.Empty;
            QuantidadeColunasVertical = 1;
            Colunas = new Dictionary<string, TabelaColuna<T>>();
            Totais = new List<TabelaTotal<T>>();
            Agrupadores = new List<TabelaAgrupador<T>>();
        }

        public int ObterQuantidadeColunasVisiveis() {
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
