using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaAgrupador
    {
        public Dictionary<string, TabelaColunaAgrupador> Agrupadores { get; set; }

        public TabelaAgrupador()
        {
            Agrupadores = new Dictionary<string, TabelaColunaAgrupador>();
        }
    }
}
