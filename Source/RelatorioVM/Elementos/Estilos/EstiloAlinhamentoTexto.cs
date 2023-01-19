using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Estilos
{
    internal class EstiloAlinhamentoTexto
    {
        public TipoAlinhamentoHorizontal Direcao { get; set; }

        public override string ToString()
        {
            return $"text-align:{Direcao.ObterDescricao()};";
        }
    }
}
