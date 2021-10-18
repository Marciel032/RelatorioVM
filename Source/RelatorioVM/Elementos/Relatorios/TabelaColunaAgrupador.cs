using RelatorioVM.Dominio.Conversores;
using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Elementos.Propriedades;
using RelatorioVM.Extensoes;
using RelatorioVM.Infraestruturas;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RelatorioVM.Elementos.Relatorios
{
    internal class TabelaColunaAgrupador
    {
        public string Identificador { get; set; }
        public TipoAlinhamentoHorizontal AlinhamentoHorizontal { get; set; }

        public TabelaColunaAgrupador(string identificador)
        {
            Identificador = identificador;
            AlinhamentoHorizontal = TipoAlinhamentoHorizontal.Esquerda;
        }
    }
}
