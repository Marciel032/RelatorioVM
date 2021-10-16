using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaColuna
    {
        public string Identificador { get; set; }
        public string Titulo { get; set; }
        public PropertyInfo Propriedade { get; set; }
        public TipoAlinhamentoHorizontal AlinhamentoHorizontal { get; set; }

        public TabelaColuna()
        {
            Identificador = string.Empty;
            Titulo = string.Empty;
            AlinhamentoHorizontal = TipoAlinhamentoHorizontal.Centro;
        }
    }
}
