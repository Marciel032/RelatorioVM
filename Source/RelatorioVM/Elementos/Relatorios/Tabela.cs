using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class Tabela<T>
    {
        public string Titulo { get; set; }
        /// <summary>
        /// Utilizado ao construir a tabela vertical, para separar os valores em colunas
        /// </summary>
        public int QuantidadeColunasVertical { get; set; }
        public Dictionary<string, TabelaColuna<T>> Colunas { get; set; }
        public IEnumerable<T> Conteudo { get; set; }
        public List<TabelaTotal<T>> Totais { get; set; }

        public List<TabelaAgrupador<T>> Agrupadores { get; set; }

        public Tabela(IEnumerable<T> conteudo)
        {
            Titulo = string.Empty;
            QuantidadeColunasVertical = 1;
            Conteudo = conteudo;
            Colunas = new Dictionary<string, TabelaColuna<T>>();
            Totais = new List<TabelaTotal<T>>();
            Agrupadores = new List<TabelaAgrupador<T>>();
        }

        public int ObterQuantidadeColunasVisiveis() {
            return Colunas.Count(x => x.Value.Visivel);
        }

        public IEnumerable<TabelaColuna<T>> ObterColunasVisiveis() {
            return Colunas.Values.Where(x => x.Visivel);
        }
    }
}
