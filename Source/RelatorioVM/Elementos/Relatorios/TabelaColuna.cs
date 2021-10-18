using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Propriedades;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaColuna<T>
    {
        public string Identificador { get; set; }
        public string Titulo { get; set; }
        public Propriedade<T> Propriedade { get; set; }
        public TipoAlinhamentoHorizontal AlinhamentoHorizontal { get; set; }
        public bool Visivel { get; set; }

        public TabelaColuna()
        {
            Identificador = string.Empty;
            Titulo = string.Empty;
            AlinhamentoHorizontal = TipoAlinhamentoHorizontal.Centro;
            Visivel = true;
        }
    }
}
