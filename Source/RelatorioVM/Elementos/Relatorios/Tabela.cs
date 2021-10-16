using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class Tabela<T>
    {
        public string Titulo { get; set; }
        public List<TabelaColuna> Colunas { get; set; }
        public IEnumerable<T> Conteudo { get; set; }

        public Tabela(IEnumerable<T> conteudo)
        {
            Titulo = string.Empty;
            Conteudo = conteudo;
            Colunas = new List<TabelaColuna>();
        }
    }
}
