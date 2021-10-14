using RelatorioVM.Elementos.Relatorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Relatorios.Estruturas
{
    internal class EstruturaRelatorio
    {
        public FiltrosElemento Filtro { get; set; }

        public EstruturaRelatorio()
        {
            Filtro = new FiltrosElemento();
        }
    }
}
