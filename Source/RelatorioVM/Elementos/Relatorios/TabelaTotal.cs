using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaTotal<T>
    {
        public string Titulo { get; set; }
        public string TituloComplemento { get; set; }
        public Dictionary<string, TabelaColunaTotal<T>> Totais { get; set; }

        public TabelaTotal(Dictionary<string, TabelaColunaTotal<T>> totais)
        {
            Titulo = "Total";
            Totais = totais;
        }

        public TabelaTotal<T> Clonar() {
            var clone = this.MemberwiseClone() as TabelaTotal<T>;
            clone.Totais = Totais.Values.Select(x => x.Clonar()).ToDictionary(x => x.Identificador);
            return clone;
        }

        public string ObterTituloCompleto() {
            if (string.IsNullOrWhiteSpace(TituloComplemento))
                return Titulo;

            return $"{Titulo} {TituloComplemento}";
        }
    }
}
