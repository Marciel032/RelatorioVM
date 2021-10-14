using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class FiltrosElemento
    {
        public List<Filtro> Filtros { get; set; }

        public FiltrosElemento()
        {
            Filtros = new List<Filtro>();
        }
    }
}
