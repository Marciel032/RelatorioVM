using RelatorioVM.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RelatorioVM.Dominio.Configuracoes.Formatacoes
{
    public class FonteEscrita
    {
        public TipoFonteEscrita Nome { get; set; } = TipoFonteEscrita.Arial;

        public bool Italico { get; set; } = false;

        public bool Negrito { get; set; } = false;

        public int Tamanho { get; set; } = 12;

        public bool Sublinhado { get; set; } = false;
    }
}
