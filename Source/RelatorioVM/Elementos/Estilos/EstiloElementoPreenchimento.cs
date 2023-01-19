using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Estilos
{
    internal class EstiloElementoPreenchimento
    {
        public TipoPreenchimento Direcao { get; set; }
        public int Tamanho { get; set; }
        public TipoUnidadeMedida UnidadeMedida { get; set; }

        public override string ToString()
        {
            return $"{Direcao.ObterDescricao()}:{Tamanho}{UnidadeMedida.ObterDescricao()};";
        }
    }
}
