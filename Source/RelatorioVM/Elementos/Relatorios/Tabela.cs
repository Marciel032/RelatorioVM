using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class Tabela<T>
    {
        public string Titulo { get; set; }
        public Dictionary<string, TabelaColuna<T>> Colunas { get; set; }
        public IEnumerable<T> Conteudo { get; set; }
        public List<TabelaTotal<T>> Totais { get; set; }

        public List<TabelaAgrupador> Agrupadores { get; set; }

        public Tabela(IEnumerable<T> conteudo)
        {
            Titulo = string.Empty;
            Conteudo = conteudo;
            Colunas = new Dictionary<string, TabelaColuna<T>>();
            Totais = new List<TabelaTotal<T>>();
            Agrupadores = new List<TabelaAgrupador>();
        }
    }
}
