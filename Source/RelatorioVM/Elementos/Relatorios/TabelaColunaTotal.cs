using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Propriedades;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaColunaTotal<T>
    {
        public string Identificador { get; set; }
        public Propriedade<T> Propriedade { get; set; }
        public TipoAlinhamentoHorizontal AlinhamentoHorizontal { get; set; }

        public TabelaColunaTotal()
        {
            Identificador = string.Empty;
            AlinhamentoHorizontal = TipoAlinhamentoHorizontal.Direita;
        }
    }
}
