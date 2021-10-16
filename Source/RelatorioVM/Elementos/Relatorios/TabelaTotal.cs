using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaTotal<T>
    {
        public string Titulo { get; set; }
        public Dictionary<string, TabelaColunaTotal<T>> Totais { get; set; }

        public TabelaTotal(Dictionary<string, TabelaColunaTotal<T>> totais)
        {
            Titulo = "Total";
            Totais = totais;
        }
    }
}
