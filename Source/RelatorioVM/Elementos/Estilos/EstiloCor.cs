using RelatorioVM.Dominio.Enumeradores;
using RelatorioVM.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Elementos.Estilos
{
    internal class EstiloCor
    {
        public string Cor { get; set; }
        public bool Fundo { get; set; }

        public override string ToString()
        {
            var corFundo = Fundo ? "background-" : "";
            return $"{corFundo}color:{Cor};";
        }
    }
}
